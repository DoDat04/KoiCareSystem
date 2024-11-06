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
            MainFrame.Navigate(new HomePage());
            UpdateLoginState();
        }

        public void UpdateLoginState()
        {
            var session = UserSession.GetInstance();
            if (session.IsLoggedIn)
            {
                LoggedInPanel.Visibility = Visibility.Visible;
                LoggedOutPanel.Visibility = Visibility.Collapsed;
                ProfileLink.Inlines.Clear();
                ProfileLink.Inlines.Add(session.Email);
            }
            else
            {
                LoggedInPanel.Visibility = Visibility.Collapsed;
                LoggedOutPanel.Visibility = Visibility.Visible;
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            UserSession.GetInstance().Logout();
            UpdateLoginState();
            MainFrame.Navigate(new HomePage());
        }

        private void MainScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var scrollViewer = (ScrollViewer)sender;
            bool isNearBottom = scrollViewer.ScrollableHeight - scrollViewer.VerticalOffset <= 50;
            Footer.Visibility = isNearBottom ? Visibility.Visible : Visibility.Collapsed;
        }

        private void NavigateToKoi(object sender, MouseButtonEventArgs e)
        {
            if (!UserSession.GetInstance().IsLoggedIn)
            {
                MessageBox.Show("Please log in to use this feature.", "Login Required", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            MainFrame.Navigate(new KoiPage());
        }

        private void NavigateToPond(object sender, MouseButtonEventArgs e)
        {
            if (!UserSession.GetInstance().IsLoggedIn)
            {
                MessageBox.Show("Please log in to use this feature.", "Login Required", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            MainFrame.Navigate(new PondPage());
        }

        private void NavigateToWater(object sender, MouseButtonEventArgs e)
        {
            if (!UserSession.GetInstance().IsLoggedIn)
            {
                MessageBox.Show("Please log in to use this feature.", "Login Required", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            MainFrame.Navigate(new WaterPage());
        }

        private void NavigateToFood(object sender, MouseButtonEventArgs e)
        {
            if (!UserSession.GetInstance().IsLoggedIn)
            {
                MessageBox.Show("Please log in to use this feature.", "Login Required", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            MainFrame.Navigate(new FishFoodCalculator());
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

        private void ProfileLink_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new User.Profile());
        }
    }
}