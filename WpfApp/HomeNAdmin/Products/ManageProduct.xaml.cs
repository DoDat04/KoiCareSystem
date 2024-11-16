using BusinessObject;
using Services;
using Services.PRODUCT;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WpfApp.HomeNAdmin.Product;
using WpfApp.MyPond;

namespace WpfApp.HomeNAdmin.Products
{
    public partial class ManageProduct : Page
    {
        private readonly IProductServices _productService;
        private ObservableCollection<BusinessObject.Product> Products { get; set; }

        public ManageProduct()
        {
            InitializeComponent();
            _productService = new ProductServices();
            Products = new ObservableCollection<BusinessObject.Product>();
            dgProduct.ItemsSource = Products;
            LoadProductsAsync();
        }

        private async void LoadProductsAsync()
        {
            try
            {
                Products.Clear();
                var products = await _productService.GetAllAsync();
                if (products != null)
                {
                    foreach (var product in products)
                    {
                        Products.Add(product);
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddNewProduct(object sender, System.Windows.RoutedEventArgs e)
        {
            var addNewProduct = new AddProductWindow();
            addNewProduct.ProductAdded += (s, args) =>
            {
                LoadProductsAsync();
            };
            addNewProduct.ShowDialog();
        }

        // Add this new method to the ManageProduct class
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is BusinessObject.Product selectedProduct)
            {
                var showProductWindow = new ShowProductWindow(selectedProduct.ProductId);
                bool? result = showProductWindow.ShowDialog();
                
                if (result == true)
                {
                    LoadProductsAsync(); // Refresh the list after update
                }
            }
        }
    }
}
