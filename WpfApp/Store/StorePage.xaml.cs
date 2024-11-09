using BusinessObject; // Assuming this namespace contains your product and other related classes
using Services;       // Assuming this namespace contains your services for accessing data
using System;
using System.Windows;
using System.Windows.Controls;

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

        
    }
}