using CommunityToolkit.Mvvm.ComponentModel;
using RDMS.Behaviors.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace RDMS.ViewModels
{
    public abstract class ViewModelBase : ObservableObject, INavigationAware
    {
        private string _title = string.Empty;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _description = string.Empty;

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        public virtual void OnNavigating(object sender, NavigatingCancelEventArgs e)
        {
            throw new NotImplementedException();
        }

        public virtual void OnNavigated(object sender, NavigationEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
