using BusinessObject; // Assuming this namespace contains your product and other related classes
using Services;       // Assuming this namespace contains your services for accessing data
using Services.PRODUCT;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp.Store
{
    /// <summary>
    /// Interaction logic for StorePage.xaml
    /// </summary>
    public partial class StorePage : Page
    {
        private readonly IProductServices _productService; // Assuming you have a product service defined

        public StorePage()
        {
            _productService = new ProductServices(); // Initialize your product service
            InitializeComponent();
            LoadProductsAsync();
        }

        private async Task LoadProductsAsync()
        {
            try
            {
                IEnumerable<Product> products = await _productService.GetAllAsync();
                if (products != null)
                {
                    dgProduct.ItemsSource = products;
                }
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading products: {ex.Message}");
                // Optionally show an error message to the user
            }
        }
        private void Product_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.DataContext is Product product)
            {
                // Navigate to ProductDetail page and pass the selected product
                NavigationService.Navigate(new ProductDetail(product));
            }
        }

    }
}