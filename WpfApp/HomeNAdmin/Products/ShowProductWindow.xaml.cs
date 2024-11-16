using BusinessObject;
using Repositories.CATEGORY;
using Repositories.PRODUCT;
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

namespace WpfApp.HomeNAdmin.Products
{
    public partial class ShowProductWindow : Window
    {
        private readonly IProductServices _productService;
        private readonly ICategoryServices _categoryService;
        public BusinessObject.Product CurrentProduct { get; set; }

        public ShowProductWindow(int productId)
        {
            InitializeComponent();
            _productService = new ProductServices();
            _categoryService = new CategoryServices();
            
            LoadProduct(productId);
            LoadCategories();
        }

        private async void LoadProduct(int productId)
        {
            CurrentProduct = await _productService.GetByIdAsync(productId);
            DataContext = CurrentProduct;

            // Load categories and set the selected category
            var categories = await _categoryService.GetAllAsync();
            CategoryComboBox.ItemsSource = categories;
            CategoryComboBox.SelectedValue = CurrentProduct.CategoryId;
        }

        private async void LoadCategories()
        {
            try
            {
                var categories = await _categoryService.GetAllAsync();
                CategoryComboBox.ItemsSource = categories;
                CategoryComboBox.SelectedValue = CurrentProduct.CategoryId;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CurrentProduct.ProductName = ProductNameTextBox.Text;
                var selectedCategory = (Category)CategoryComboBox.SelectedItem;
                CurrentProduct.CategoryId = selectedCategory.CategoryId;
                CurrentProduct.UnitsInStock = short.Parse(UnitsInStockTextBox.Text);
                CurrentProduct.UnitPrice = decimal.Parse(UnitPriceTextBox.Text);
                await _productService.UpdateAsync(CurrentProduct);
                MessageBox.Show("Product updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating product: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

       
        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to delete this product?", "Confirm Delete", 
                MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    await _productService.DeleteAsync(CurrentProduct.ProductId);
                    MessageBox.Show("Product deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.DialogResult = true;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting product: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
