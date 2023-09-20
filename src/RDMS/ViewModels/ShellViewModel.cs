using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using RDMS.Messages;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RDMS.ViewModels
{
    public enum NavigationTab
    {
        Notification,
        Status,
        Schedule
    }

    public class ShellViewModel : ViewModelBase
    {
        private string _navigationSource = string.Empty;

        public string NavigationSource
        {
            get => _navigationSource;
            set => SetProperty(ref _navigationSource, value);
        }

        private NavigationTab _navigationTab = NavigationTab.Status;

        public NavigationTab NavigationTab
        {
            get => _navigationTab;
            set
            {
                SetProperty(ref _navigationTab, value);
                UpdatePage();
            }
        }

        private bool _isDialogOpen = false;

        public bool IsDialogOpen
        {
            get => _isDialogOpen;
            set => SetProperty(ref _isDialogOpen, value);
        }

        public ShellViewModel() 
        {
            Title = "Shell Window";
            Description = "The main window which provides a navigation frame";

            Initialize();
        }

        private void Initialize()
        {
            // Sets the start-up page.
            NavigationSource = "./Views/Pages/StatusPage.xaml";

            // Subscribes the messenger to get navigation messages.
            WeakReferenceMessenger.Default.Register<NavigationMessage>(this, (recipient, message) =>
            {
                NavigationSource = message.Value;
            });
        }

        private void UpdatePage()
        {
            string source = string.Empty;

            switch (_navigationTab)
            {
                case NavigationTab.Notification:
                    source = "./Views/Pages/NotificationPage.xaml";
                    break;
                case NavigationTab.Status:
                    source = "./Views/Pages/StatusPage.xaml";
                    break;
                case NavigationTab.Schedule:
                    source = "./Views/Pages/SchedulePage.xaml";
                    break;
                default:
                    source = "./Views/Pages/NotificationPage.xaml";
                    break;
            }

            Log.Information(source);
            NavigationSource = source;
        }
    }
}
