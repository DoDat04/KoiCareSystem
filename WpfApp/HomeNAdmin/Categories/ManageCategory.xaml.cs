using BusinessObject;
using Services.CATEGORY;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp.HomeNAdmin.Categories
{
    /// <summary>
    /// Interaction logic for ManageCategory.xaml
    /// </summary>
    public partial class ManageCategory : Page
    {
        private readonly ICategoryServices _categoryService;
        private ObservableCollection<Category> Categories { get; set; }

        public ManageCategory()
        {
            InitializeComponent();
            _categoryService = new CategoryServices();
            Categories = new ObservableCollection<Category>();
            dgCategory.ItemsSource = Categories;
            LoadCategoriesAsync();
        }

        private async void LoadCategoriesAsync()
        {
            try
            {
                Categories.Clear();
                var categories = await _categoryService.GetAllAsync();
                if (categories != null)
                {
                    foreach (var category in categories)
                    {
                        Categories.Add(category);
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddNewCategory(object sender, RoutedEventArgs e)
        {
            var addNewCategory = new AddCategoryWindow();
            addNewCategory.CategoryAdded += (s, args) =>
            {
                LoadCategoriesAsync();
            };
            addNewCategory.ShowDialog();
        }
    }
}
