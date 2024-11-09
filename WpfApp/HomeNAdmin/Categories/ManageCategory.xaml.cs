using BusinessObject;
using Services;
using System.Collections.Generic;
using System.Windows.Controls;

namespace WpfApp.HomeNAdmin.Categories
{
    /// <summary>
    /// Interaction logic for ManageCategory.xaml
    /// </summary>
    public partial class ManageCategory : Page
    {
        private readonly ICategoryServices _categoryService;

        public ManageCategory()
        {
            _categoryService = new CategoryServices();
            InitializeComponent();
            LoadCategoriesAsync();
        }

        private async void LoadCategoriesAsync()
        {
            try
            {
                IEnumerable<BusinessObject.Category> categories = await _categoryService.GetAllAsync();
                if (categories != null)
                {
                    dgCategory.ItemsSource = categories;
                }
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading categories: {ex.Message}");
                // Optionally show an error message to the user
            }
        }

        private void AddNewCategory(object sender, System.Windows.RoutedEventArgs e)
        {
            AddCategoryWindow addNewCategory = new AddCategoryWindow();
            addNewCategory.ShowDialog();
        }
    }
}
