using System.Windows.Navigation;

namespace RDMS.Behaviors.Abstractions
{
    /// <summary>
    /// Provides a mechanism for notifying the view model that navigation events have occurred.
    /// </summary>
    public interface INavigationAware
    {
        /// <summary>
        /// An event called when navigation is started.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The NavigatingCancelEvent arguments</param>
        void OnNavigating(object sender, NavigatingCancelEventArgs e);

        /// <summary>
        /// An event called when navigation is complete.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The NavigationEvent arguments</param>
        void OnNavigated(object sender, NavigationEventArgs e);
    }
}
