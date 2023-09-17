using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using RDMS.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RDMS.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        private string _navigationSource = string.Empty;

        public string NavigationSource
        {
            get { return _navigationSource; }
            set { SetProperty(ref _navigationSource, value); }
        }

        public ShellViewModel() 
        {
            Title = "Shell Window";
            Description = "The main window which provides a navigation frame.";

            Initialize();
        }

        private void Initialize()
        {
            // Sets the start-up page.
            NavigationSource = "./Views/Pages/DashboardPage.xaml";

            // Subscribes the messenger to get navigation messages.
            WeakReferenceMessenger.Default.Register<NavigationMessage>(this, OnNavigationMessageReceived);
        }

        private void OnNavigationMessageReceived(object recipient, NavigationMessage message)
        {
            NavigationSource = message.Value;
        }
    }
}
