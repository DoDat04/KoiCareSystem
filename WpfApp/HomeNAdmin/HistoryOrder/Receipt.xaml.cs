using BusinessObject;
using Services.MEMBER;
using System.Windows;

namespace WpfApp.HomeNAdmin.HistoryOrder
{
    public partial class Receipt : Window
    {
        private Order _order;
        private Member _member;
        private readonly IMemberService _memberService;

        public Receipt(Order order)
        {
            InitializeComponent();
            _order = order;
            _memberService = new MemberService();
            LoadReceiptDetails();
        }

        private void LoadReceiptDetails()
        {
            try
            {
                OrderIDText.Text = _order.OrderId.ToString();
                OrderDateText.Text = _order.OrderDate.ToString("dd/MM/yyyy");
                TotalAmountText.Text = _order.TotalAmount.ToString();

                if (_order.MemberId != null)
                {
                    _member = _memberService.GetMemberById(_order.MemberId);

                    if (_member != null)
                    {
                        MemberFullNameText.Text = _member.FullName ?? "N/A";
                        MemberPhoneText.Text = _member.PhoneNumber ?? "N/A";
                    }
                    else
                    {
                        MessageBox.Show($"No member found with ID: {_order.MemberId}", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        MemberPhoneText.Text = "Unknown";
                    }
                }
                else
                {
                    MessageBox.Show("MemberId is null.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    MemberFullNameText.Text = "N/A";
                    MemberPhoneText.Text = "N/A";
                }

                var receiptItems = new List<object>();

                foreach (var cartItem in _order.CartItems)
                {
                    if (cartItem.Product != null)
                    {
                        var receiptItem = new
                        {
                            ProductName = cartItem.Product.ProductName ?? "Unknown Product",
                            Quantity = cartItem.Quantity,
                            UnitPrice = cartItem.Product.UnitPrice ?? 0,
                            SubTotal = cartItem.Quantity * (cartItem.Product.UnitPrice ?? 0)
                        };
                        receiptItems.Add(receiptItem);
                    }
                }

                ReceiptItemsListView.ItemsSource = receiptItems;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading receipt details: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}