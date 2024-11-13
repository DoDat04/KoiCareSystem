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
using WpfApp.Store;
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
            WindowState = WindowState.Normal;
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

        private void Cart_Click(object sender, RoutedEventArgs e)
        {
            var cartPopup = new Cart();

            // Get the position of the button
            var buttonPosition = (sender as Button).PointToScreen(new Point(0, 0));

            // Set the position of the popup window to be below the button
            cartPopup.Left = buttonPosition.X; // Align horizontally with the button
            cartPopup.Top = buttonPosition.Y + ((Button)sender).ActualHeight; // Position below the button

            // Ensure the popup is within screen bounds
            if (cartPopup.Left < 0)
                cartPopup.Left = 0; // Prevent going off the left side of the screen

            if (cartPopup.Top < 0)
                cartPopup.Top = 0; // Prevent going off the top of the screen

            // Get the screen width and height
            var screenWidth = SystemParameters.PrimaryScreenWidth;
            var screenHeight = SystemParameters.PrimaryScreenHeight;

            // Adjust if the popup goes beyond the right or bottom of the screen
            if (cartPopup.Left + cartPopup.Width > screenWidth)
                cartPopup.Left = screenWidth - cartPopup.Width;

            if (cartPopup.Top + cartPopup.Height > screenHeight)
                cartPopup.Top = screenHeight - cartPopup.Height;

            // Set the owner of the popup window
            cartPopup.Owner = this;
            cartPopup.NavigateToPurchase += CartPopup_NavigateToPurchase; // Subscribe to the event

            // Show the popup window
            cartPopup.Show();
        }

        private void CartPopup_NavigateToPurchase()
        {
            MainFrame.Navigate(new Purchase()); // Navigate to the Purchase page
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

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new HistoryOrder());
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

        private void NavigateToStore(object sender, MouseButtonEventArgs e)
        {
            if (!UserSession.GetInstance().IsLoggedIn)
            {
                MessageBox.Show("Please log in to use this feature.", "Login Required", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            MainFrame.Navigate(new StorePage());
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