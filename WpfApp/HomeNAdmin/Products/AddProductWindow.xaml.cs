using BusinessObject;
using Services.CATEGORY;
using Services.PRODUCT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp.HomeNAdmin.Product
{
    /// <summary>
    /// Interaction logic for AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        private readonly ICategoryServices _categoryServices;
        private readonly IProductServices _productServices;
        public event EventHandler ProductAdded;

        public AddProductWindow()
        {
            InitializeComponent();
            _categoryServices = new CategoryServices();
            _productServices = new ProductServices();
            LoadCategoriesAsync();
        }

        private async void LoadCategoriesAsync()
        {
            try
            {
                IEnumerable<BusinessObject.Category> categories = await _categoryServices.GetAllAsync();
                if (categories != null)
                {
                    CategoryComboBox.ItemsSource = categories;
                }
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading categories: {ex.Message}");
                // Optionally show an error message to the user
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ProductNameTextBox.Text) ||
        CategoryComboBox.SelectedItem == null ||
        !int.TryParse(UnitsInStockTextBox.Text, out int unitsInStock) ||
        !decimal.TryParse(UnitPriceTextBox.Text, out decimal unitPrice))
            {
                MessageBox.Show("Please enter valid product details.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var product = new BusinessObject.Product
            {
                ProductName = ProductNameTextBox.Text,
                CategoryId = ((Category)CategoryComboBox.SelectedItem).CategoryId,
                UnitsInStock = (short?)unitsInStock,
                UnitPrice = unitPrice
            };


            try
            {
                _productServices.CreateAsync(product);

                MessageBox.Show("Product added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                ProductAdded?.Invoke(this, EventArgs.Empty);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding product: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
