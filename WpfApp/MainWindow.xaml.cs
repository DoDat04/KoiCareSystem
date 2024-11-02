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
using System.Windows.Shapes;
using WpfApp.FoodCalc;
using WpfApp.WaterParam;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            MainFrame.Navigate(new HomePage()); // Add this line to load HomePage at startup
        }

        private void MainScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var scrollViewer = (ScrollViewer)sender;

            // Calculate if we're near the bottom (within 50 pixels)
            bool isNearBottom = scrollViewer.ScrollableHeight - scrollViewer.VerticalOffset <= 50;

            // Show/hide footer based on scroll position
            Footer.Visibility = isNearBottom ? Visibility.Visible : Visibility.Collapsed;
        }

        private void NavigateToKoi(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new KoiPage());
        }

        private void NavigateToPond(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new PondPage());
        }

        private void NavigateToWater(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new WaterPage());
        }

        private void NavigateToFood(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new FoodPage());
        }

        private void NavigateToHome(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new HomePage());
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginSignPopup(true);
            loginWindow.ShowDialog();
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginSignPopup(false);
            loginWindow.ShowDialog();
        }
    }
}