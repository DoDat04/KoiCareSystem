using BusinessObject;
using Services;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfApp.HomeNAdmin.Product;
using WpfApp.MyPond;

namespace WpfApp.HomeNAdmin.Products
{
    public partial class ManageProduct : Page
    {
        private readonly IProductServices _productService;

        public ManageProduct()
        {
            _productService = new ProductServices();
            InitializeComponent();
            LoadProductsAsync();
        }

        private async void LoadProductsAsync()
        {
            try
            {
                IEnumerable<BusinessObject.Product> products = await _productService.GetAllAsync();
                if (products != null)
                {
                    dgProduct.ItemsSource = products;
                }
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading products: {ex.Message}");
            }
        }

        private void AddNewProduct(object sender, System.Windows.RoutedEventArgs e)
        {
            AddProductWindow addNewProduct = new AddProductWindow();
            addNewProduct.ShowDialog();
        }
    }
}
