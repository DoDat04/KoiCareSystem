using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using BusinessObject;

namespace WpfApp.HomeNAdmin.HistoryOrder
{
    /// <summary>
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class History : Page
    {
        public History()
        {
            InitializeComponent();
            LoadOrders();
        }

        private void LoadOrders()
        {
            // Assuming you have a method to get the orders, adjust as necessary
            List<Order> orders = MyStoreContext.Orders; // Fetch orders from context
            OrderListView.ItemsSource = orders; // Set the items source for the DataGrid
        }

        private void ViewDetail_Click(object sender, RoutedEventArgs e)
        {
            var orderPopup = new Receipt();
            orderPopup.Show();
        }

    
    }
}