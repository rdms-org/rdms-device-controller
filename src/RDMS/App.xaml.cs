using Microsoft.Extensions.DependencyInjection;
using RDMS.Helpers;
using RDMS.ViewModels;
using RDMS.ViewModels.Pages;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Resources;
using System.Threading;
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
            ConfigureLogging();
            ConfigureServices();

            InitializeComponent();

            LoadLocalizationResources();
        }

        /// <summary>
        /// Configures the logging system.
        /// </summary>
        private void ConfigureLogging()
        {
            string fileName = Path.Combine(VariableBuilder.GetBaseDirectory(), @"logs\.log");
            string outputTemplateString = "{Timestamp:HH:mm:ss.ms} [{Level:u4}] {Message}{NewLine}{Exception}";

            var log = new LoggerConfiguration()
                .WriteTo.Async(a => a.File(fileName, restrictedToMinimumLevel: LogEventLevel.Verbose, shared: true, outputTemplate: outputTemplateString, rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true, fileSizeLimitBytes: 10485760))
                .CreateLogger();

            Log.Logger = log;

            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                Log.Fatal(e.ExceptionObject as Exception, "An unhandled exception has been occurred. If the same problem persists, please report it to the program provider.");
                Log.CloseAndFlush();
            };
        }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private void ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton(typeof(ShellViewModel));
            services.AddSingleton(typeof(AlertViewModel));
            services.AddSingleton(typeof(ManagerViewModel));
            services.AddSingleton(typeof(NotificationViewModel));
            services.AddSingleton(typeof(StatusViewModel));
            services.AddSingleton(typeof(ScheduleViewModel));
            services.AddSingleton(typeof(ReportViewModel));

            Services = services.BuildServiceProvider();
        }

        /// <summary>
        /// Loads localization resources.
        /// </summary>
        private void LoadLocalizationResources()
        {
            LoadLocalizationResources(string.Empty);
        }

        /// <summary>
        /// Loads localization resources.
        /// </summary>
        private void LoadLocalizationResources(string culture)
        {
            // If localization resources already exist, remove them.
            var currentDict = App.Current.Resources.MergedDictionaries
                .FirstOrDefault(dict => dict.Source != null && Path.GetFileName(dict.Source.OriginalString).StartsWith("Localization", StringComparison.OrdinalIgnoreCase));

            if (currentDict != null)
            {
                App.Current.Resources.Remove(currentDict);
            }

            // Add new localization resources.
            var dict = new ResourceDictionary();

            if (string.IsNullOrEmpty(culture))
            {
                culture = Thread.CurrentThread.CurrentUICulture.Name;
            }

            switch (culture)
            {
                case "en-US":
                    dict.Source = new Uri(@"pack://application:,,,/Assets/Localizations/Localization.en-US.xaml", UriKind.Absolute);
                    break;
                case "ko-KR":
                    dict.Source = new Uri(@"pack://application:,,,/Assets/Localizations/Localization.ko-KR.xaml", UriKind.Absolute);
                    break;
                default:
                    dict.Source = new Uri(@"pack://application:,,,/Assets/Localizations/Localization.en-US.xaml", UriKind.Absolute);
                    break;
            }

            App.Current.Resources.MergedDictionaries.Add(dict);
        }
    }
}
