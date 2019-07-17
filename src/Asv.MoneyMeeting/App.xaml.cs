using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Asv.MoneyMeeting
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            ViewModel = new ShellViewModel();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var wnd = new MainWindow { DataContext = ViewModel };
            wnd.Show();
        }

        public ShellViewModel ViewModel { get; set; }
    }
}
