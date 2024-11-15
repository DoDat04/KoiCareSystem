using BusinessObject;
using Services.CATEGORY;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WpfApp.HomeNAdmin.Categories
{
    /// <summary>
    /// Interaction logic for ShowCategoryWindow.xaml
    /// </summary>
    public partial class ShowCategoryWindow : Window
    {
        private readonly ICategoryServices _categoryService;
        private Category _currentCategory;

        public ShowCategoryWindow(Category category)
        {
            InitializeComponent();
            _categoryService = new CategoryServices();
            _currentCategory = category;
            DataContext = _currentCategory; // Gán DataContext để hiển thị thông tin danh mục
        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(CategoryNameTextBox.Text))
                {
                    MessageBox.Show("Please enter a category name.", "Validation Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var newCategory = new Category
                {
                    CategoryName = CategoryNameTextBox.Text.Trim(),
                };

                await _categoryService.UpdateAsync(newCategory);
                
                MessageBox.Show("Category updated successfully!", "Success",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating category: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to delete this category?", "Confirm Delete",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    await _categoryService.DeleteAsync(_currentCategory.CategoryId); // Giả sử bạn có Id trong Category
                    MessageBox.Show("Category deleted successfully!", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting category: {ex.Message}", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
