using BusinessObject;
using Services.CATEGORY;
using System;
using System.Windows;

namespace WpfApp.HomeNAdmin
{
    /// <summary>
    /// Interaction logic for AddCategoryWindow.xaml
    /// </summary>
    public partial class AddCategoryWindow : Window
    {
        private readonly ICategoryServices _categoryService;
        public event EventHandler CategoryAdded;

        public AddCategoryWindow()
        {
            InitializeComponent();
            _categoryService = new CategoryServices();
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
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

                await _categoryService.CreateAsync(newCategory);
                
                CategoryAdded?.Invoke(this, EventArgs.Empty);
                MessageBox.Show("Category added successfully!", "Success", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding category: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
