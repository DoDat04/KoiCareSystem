using BusinessObject;
using Services;
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

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for KoiDetailPopup.xaml
    /// </summary>
    public partial class KoiDetailPopup : Window
    {
        private readonly IPondService _pondService;
        private readonly IFishService _fishService;
        private Fish _currentFish;
        private string _imagePath;

        public KoiDetailPopup(Fish fish)
        {
            InitializeComponent();
            _currentFish = fish;
            DataContext = fish;
            _pondService = new PondService();
            _fishService = new FishService();
            
            // Initialize gender ComboBox
            var genderComboBox = this.FindName("GenderComboBox") as ComboBox;
            if (genderComboBox != null)
            {
                if (genderComboBox.Items.Count == 0)
                {
                    genderComboBox.Items.Add("Male");
                    genderComboBox.Items.Add("Female");
                }
                genderComboBox.SelectedValue = fish.Gender;
            }

            // Load fish image if exists
            if (!string.IsNullOrEmpty(fish.ImagePath))
            {
                try
                {
                    _imagePath = fish.ImagePath;
                    FishImage.Source = new BitmapImage(new Uri(fish.ImagePath));
                }
                catch
                {
                    FishImage.Source = new BitmapImage(new Uri("/WpfApp;component/image/fish.png", UriKind.Relative));
                }
            }
        }

        public void LoadPonds()
        {
            try
            {
                var session = UserSession.GetInstance();
                var ponds = _pondService.GetAll(session.MemberId);
                PondComboBox.ItemsSource = ponds;
                PondComboBox.SelectedValue = _currentFish.PondId;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading ponds: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var fish = (Fish)DataContext;
                if (fish != null)
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this fish?", 
                        "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    
                    if (result == MessageBoxResult.Yes)
                    {
                        _fishService.DeleteFish(fish.FishId);
                        MessageBox.Show("Fish deleted successfully", "Success", 
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        DialogResult = true;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            var fish = (Fish)DataContext;
            try
            {
                if (PondComboBox.SelectedValue == null)
                {
                    MessageBox.Show("Please select a pond", "Validation Error", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                fish.PondId = (int)PondComboBox.SelectedValue;
                
                // Update image path if a new image was selected
                if (!string.IsNullOrEmpty(_imagePath) && _imagePath != fish.ImagePath)
                {
                    // Copy the image to your application's storage location
                    string fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(_imagePath);
                    string destinationPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FishImages", fileName);
                    
                    // Ensure directory exists
                    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(destinationPath));
                    
                    // Copy the file
                    System.IO.File.Copy(_imagePath, destinationPath, true);
                    
                    // Update the fish's image path
                    fish.ImagePath = destinationPath;
                }

                _fishService.UpdateFish(fish);
                MessageBox.Show("Fish details updated successfully!", "Success", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating fish details: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadPonds();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnUploadImage_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp",
                Title = "Select a Fish Image"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    _imagePath = openFileDialog.FileName;
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(_imagePath);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    FishImage.Source = bitmap;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading image: {ex.Message}", "Error", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    _imagePath = null;
                    FishImage.Source = new BitmapImage(new Uri("/WpfApp;component/image/fish.png", UriKind.Relative));
                }
            }
        }
    }
}
