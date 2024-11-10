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
            // Get the quantity entered by the user
            if (int.TryParse(QuantityTextBox.Text, out int quantity) && quantity > 0)
            {
                if (quantity <= _product.UnitsInStock)
                {
                    // Proceed with purchase logic
                    _product.UnitsInStock = (short)(_product.UnitsInStock - quantity); // Decrease stock with casting

                    // Optionally, navigate to the Purchase page
                    NavigationService.Navigate(new Purchase());
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
    }
}