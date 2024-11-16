using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using BusinessObject;
using Services.MEMBER;
using System.Linq;

namespace WpfApp.HomeNAdmin.HistoryOrder
{
    /// <summary>
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class History : Page

    {   
        private readonly IMemberService _memberService;

        public History()
        {
            InitializeComponent();
            _memberService = new MemberService();
            LoadOrders();
        }

        private void LoadOrders()
        {
            try
            {
                var orders = MyStoreContext.Orders.ToList();
                
                // Create a view model list to combine Order and Member information
                var orderViewModels = orders.Select(order =>
                {
                    var member = _memberService.GetMemberById(order.MemberId);
                    return new OrderViewModel
                    {
                        OrderId = order.OrderId,
                        MemberId = order.MemberId,
                        FullName = $"{member?.FirstName} {member?.LastName}",
                        PhoneNumber = member?.PhoneNumber,
                        TotalAmount = order.TotalAmount,
                        OrderDate = order.OrderDate,
                        Order = order
                    };
                }).ToList();

                OrderListView.ItemsSource = orderViewModels;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error loading orders: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ViewDetail_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is OrderViewModel orderVM)
            {
                var orderPopup = new Receipt(orderVM.Order);
                orderPopup.Show();
            }
        }
    }

    // View Model to combine Order and Member information
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public int MemberId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public Order Order { get; set; }
    }
}