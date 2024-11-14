using BusinessObject;
using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp.Store
{
    public partial class ProductDetail : Page
    {
        private Product _product;

        public ProductDetail(Product product)
        {
            InitializeComponent();
            _product = product; // Store the product reference
            LoadProductDetails(); // Load product details into UI elements
        }

        private void LoadProductDetails()
        {
            // Load the product details into the UI elements
            ProductNameTextBlock.Text = _product.ProductName;
            ProductPriceTextBlock.Text = $"${_product.UnitPrice}";
            ProductStockTextBlock.Text = $"{_product.UnitsInStock}";
            ProductDescriptionTextBlock.Text = _product.Description;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var session = UserSession.GetInstance();

            if (int.TryParse(QuantityTextBox.Text, out int quantity) && quantity > 0)
            {
                if (quantity <= _product.UnitsInStock)
                {
                    var mainWindow = Application.Current.MainWindow as Home;
                    mainWindow?.AddToCart(_product, (short)quantity);
                    MessageBox.Show("Product added to cart.", "Add Product", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Not enough stock available.");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid quantity.");
            }
        }

        private int GetNextOrderId()
        {
            var lastOrder = MyStoreContext.Orders.OrderByDescending(o => o.OrderId).FirstOrDefault();
            return lastOrder != null ? lastOrder.OrderId + 1 : 1;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StorePage());
        }
    }
}