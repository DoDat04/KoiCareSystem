using BusinessObject;
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
            if (!CartItems.Any())
            {
                MessageBox.Show("Your cart is empty!", "Cart", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var cartPopup = new WpfApp.Store.Cart();
            cartPopup.DataContext = this;

            var buttonPosition = (sender as Button).PointToScreen(new Point(0, 0));

            cartPopup.Left = buttonPosition.X;
            cartPopup.Top = buttonPosition.Y + ((Button)sender).ActualHeight;

            if (cartPopup.Left < 0)
                cartPopup.Left = 0;

            if (cartPopup.Top < 0)
                cartPopup.Top = 0;

            var screenWidth = SystemParameters.PrimaryScreenWidth;
            var screenHeight = SystemParameters.PrimaryScreenHeight;

            if (cartPopup.Left + cartPopup.Width > screenWidth)
                cartPopup.Left = screenWidth - cartPopup.Width;

            if (cartPopup.Top + cartPopup.Height > screenHeight)
                cartPopup.Top = screenHeight - cartPopup.Height;

            cartPopup.Owner = this;
            cartPopup.NavigateToPurchase += CartPopup_NavigateToPurchase;

            cartPopup.Show();
        }


        private void CartPopup_NavigateToPurchase()
        {
            MainFrame.Navigate(new Purchase()); // Navigate to the Purchase page
        }

        public List<BusinessObject.Cart> CartItems { get; set; } = new List<BusinessObject.Cart>();

        public void AddToCart(Product product, short quantity)
        {
            var session = UserSession.GetInstance();
            var existingCartItem = CartItems.FirstOrDefault(c => c.Product.ProductId == product.ProductId);
            if (existingCartItem != null)
            {
                existingCartItem.Quantity += quantity;
            }
            else
            {
                CartItems.Add(new BusinessObject.Cart(1, session.MemberId, product, quantity));
            }
        }

        public void ClearCart()
        {
            CartItems.Clear();
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

            // Show the login dialog and check if a new session was created
            if (loginWindow.ShowDialog() == true) // Assuming this dialog returns true on successful login
            {
                // Update the login state based on the new session
                UpdateLoginState();
            }
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