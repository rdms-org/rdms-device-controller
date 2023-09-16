using Microsoft.Extensions.DependencyInjection;
using RDMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Xaml;

namespace RDMS
{
    /// <summary>
    /// Provides IoC system and interaction logic for the application.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Gets the current <see cref="App"/> instance in use.
        /// </summary>
        public new static App Current => (App)Application.Current;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public IServiceProvider? Services { get; private set; }

        /// <summary>
        /// Initializes the Application.
        /// </summary>
        public App()
        {
            ConfigureServices();
            InitializeComponent();
        }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private void ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddTransient(typeof(ShellViewModel));

            Services = services.BuildServiceProvider();
        }
    }
}
