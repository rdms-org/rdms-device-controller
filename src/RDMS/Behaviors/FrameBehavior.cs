using Microsoft.Xaml.Behaviors;
using System;
using System.Windows.Controls;
using System.Windows;
using RDMS.Behaviors.Abstractions;
using System.Windows.Navigation;

namespace RDMS.Behaviors
{
    /// <summary>
    /// Provides navigation behavior to a <see cref="Frame"/>.
    /// </summary>
    public class FrameBehavior : Behavior<Frame>
    {
        /// <summary>
        /// A flag to prevent PropertyChanged events from <see cref="NavigationSourceProperty"/>.
        /// </summary>
        private bool _isWork;

        /// <summary>
        /// Gets or sets the navigation source.
        /// </summary>
        public static readonly DependencyProperty NavigationSourceProperty =
            DependencyProperty.Register(nameof(NavigationSource), typeof(string), typeof(FrameBehavior), new PropertyMetadata(null, OnNavigationSourceChanged));

        /// <summary>
        /// Provides a wrapper for NavigationSourceProperty.
        /// </summary>
        public string NavigationSource
        {
            get { return (string)GetValue(NavigationSourceProperty); }
            set { SetValue(NavigationSourceProperty, value); }
        }

        /// <summary>
        /// An event called when the <see cref="NavigationSource"/> is changed.
        /// </summary>
        /// <param name="d">The dependency object</param>
        /// <param name="e">The DependencyPropertyChangedEvent arguments</param>
        private static void OnNavigationSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behavior = (FrameBehavior)d;

            if (behavior._isWork)
            {
                return;
            }

            behavior.Navigate();
        }

        /// <summary>
        /// Registers event handlers when the behavior is attached.
        /// </summary>
        protected override void OnAttached()
        {
            AssociatedObject.Navigating += OnNavigating;
            AssociatedObject.Navigated += OnNavigated;
        }

        /// <summary>
        /// Unregisters event handlers when the behavior is detached.
        /// </summary>
        protected override void OnDetaching()
        {
            AssociatedObject.Navigating -= OnNavigating;
            AssociatedObject.Navigated -= OnNavigated;
        }

        /// <summary>
        /// An event called when navigation is started.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The NavigatingCancelEvent arguments</param>
        private void OnNavigating(object sender, NavigatingCancelEventArgs e)
        {
            // Notify the view model that navigation has started.
            if (AssociatedObject.Content is Page pageContent && pageContent.DataContext is INavigationAware navigationAware)
            {
                navigationAware?.OnNavigating(sender, e);
            }
        }

        /// <summary>
        /// An event called when navigation is complete.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The NavigationEvent arguments</param>
        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            // Make sure the navigation source is correct.
            _isWork = true;
            NavigationSource = e.Uri.ToString();
            _isWork = false;

            // Notify the view model that navigation has completed.
            if (AssociatedObject.Content is Page pageContent && pageContent.DataContext is INavigationAware navigationAware)
            {
                navigationAware.OnNavigated(sender, e);
            }
        }

        /// <summary>
        /// Processes the NavigationSource to an actual action.
        /// </summary>
        private void Navigate()
        {
            if (string.IsNullOrEmpty(NavigationSource) || string.IsNullOrWhiteSpace(NavigationSource))
            {
                return;
            }

            switch (NavigationSource)
            {
                case "GoBack":
                    if (AssociatedObject.CanGoBack)
                    {
                        AssociatedObject.GoBack();
                    }
                    break;
                case "GoForward":
                    if (AssociatedObject.CanGoForward)
                    {
                        AssociatedObject.GoForward();
                    }
                    break;
                case "Refresh":
                    AssociatedObject.Refresh();
                    break;
                default:
                    AssociatedObject.Navigate(new Uri(NavigationSource, UriKind.RelativeOrAbsolute));
                    break;
            }
        }
    }
}
