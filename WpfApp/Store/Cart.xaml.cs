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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp.Store
{
    /// <summary>
    /// Interaction logic for Cart.xaml
    /// </summary>
    public partial class Cart : Window
    {
        public event Action NavigateToPurchase;

        public Cart()
        {
            InitializeComponent();
            LoadCartItems();
        }

        private void LoadCartItems()
        {
            var mainWindow = Application.Current.MainWindow as Home;
            if (mainWindow != null)
            {
                CartItemsListView.ItemsSource = mainWindow.CartItems;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as Home;
            var session = UserSession.GetInstance();

            if (mainWindow?.CartItems.Any() == true)
            {
                // Create new order
                var newOrder = new Order(GetNextOrderId(), session.MemberId)
                {
                    CartItems = mainWindow.CartItems.ToList()
                };

                // Add order to context
                MyStoreContext.Orders.Add(newOrder);

                // Clear the cart
                mainWindow.ClearCart();

                // Navigate to purchase page
                NavigateToPurchase?.Invoke();
                this.Close();
            }
        }

        private int GetNextOrderId()
        {
            var lastOrder = MyStoreContext.Orders.OrderByDescending(o => o.OrderId).FirstOrDefault();
            return lastOrder != null ? lastOrder.OrderId + 1 : 1;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
