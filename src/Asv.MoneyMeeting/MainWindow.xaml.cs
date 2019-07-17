using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Asv.MoneyMeeting
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            UpdateLayout();
        }

        private void UIElement_OnMouseDownClose(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void UIElement_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Space)
            {
                var vm = (ShellViewModel)DataContext;
                vm.IsSettingsOpened = false;
                vm.Start();
            }
        }

        private void ButtonBase_OnClickИупшт(object sender, RoutedEventArgs e)
        {
            var vm = (ShellViewModel)DataContext;
            vm.IsSettingsOpened = false;
            vm.StartFromBegin();
        }

        private void MainWindow_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            var vm = (ShellViewModel)DataContext;
            if (e.Key == Key.Space)
            {
                if (vm.IsPause)
                {
                    vm.Start();
                }
                else
                {
                    vm.Stop();
                }
            }
        }
    }
}
