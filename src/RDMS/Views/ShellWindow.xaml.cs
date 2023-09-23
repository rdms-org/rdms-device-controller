using RDMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RDMS.Views
{
    /// <summary>
    /// ShellWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ShellWindow : Window
    {
        public ShellWindow()
        {
            InitializeComponent();
            DataContext = App.Current.Services?.GetService(typeof(ShellViewModel));
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            // Note: Clean up notifyicon(would otherwise stay open until application finishes).
            TrayIcon.Dispose();

            base.OnClosing(e);
        }
    }
}
