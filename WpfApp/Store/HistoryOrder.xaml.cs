using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp.Store
{
    /// <summary>
    /// Interaction logic for HistoryOrder.xaml
    /// </summary>
    public partial class HistoryOrder : Page
    {
        public HistoryOrder()
        {
            InitializeComponent();
            LoadOrders();
        }

        private void LoadOrders()
        {
            try
            {
                // Get the current user session
                var session = UserSession.GetInstance();
                int memberId = session.MemberId; // Get the member ID from the session

                // Fetch and sort orders for the specific member
                List<Order> orders = MyStoreContext.Orders
                    .Where(order => order.MemberId == memberId)
                    .OrderByDescending(order => order.OrderId) // Sort by OrderId descending
                    .ToList(); // Filter orders by MemberId

                // Set the items source for the ItemsControl
                OrderListView.ItemsSource = orders;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}