using BusinessObject;
using Services;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WpfApp.MyPond
{
    public partial class ShowPond : Window
    {
        private readonly IPondService _pondService;
        private readonly IFishService _fishService;
        private readonly Pond _pond;
        private string _imagePath;

        public ShowPond(Pond pond)
        {
            InitializeComponent();
            _pond = pond ?? throw new ArgumentNullException(nameof(pond));
            DataContext = _pond;
            _pondService = new PondService();
            _fishService = new FishService();
            
            // Load pond image if exists
            if (!string.IsNullOrEmpty(pond.ImagePath))
            {
                try 
                {
                    _imagePath = pond.ImagePath;
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(pond.ImagePath);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    PondImage.Source = bitmap;
                    
                }
                catch 
                {
                    _imagePath = null;
                    PondImage.Source = new BitmapImage(new Uri("/WpfApp;component/image/pond.png", UriKind.Relative));
                   
                }
            }
            
          
        }

        
        

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ValidateInputs())
                    return;

                // Handle image update
                if (!string.IsNullOrEmpty(_imagePath) && _imagePath != _pond.ImagePath)
                {
                    string fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(_imagePath);
                    string destinationPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PondImages", fileName);
                    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(destinationPath));
                    System.IO.File.Copy(_imagePath, destinationPath, true);
                    _pond.ImagePath = destinationPath;
                }

                _pondService.UpdatePond(_pond);
                MessageBox.Show("Pond updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating pond: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(_pond.Name))
            {
                MessageBox.Show("Please enter a pond name.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (_pond.Length <= 0)
            {
                MessageBox.Show("Length must be greater than 0.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (_pond.Width <= 0)
            {
                MessageBox.Show("Width must be greater than 0.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (_pond.Depth <= 0)
            {
                MessageBox.Show("Depth must be greater than 0.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to delete this pond?", 
                "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _pondService.DeletePond(_pond.PondId);
                    MessageBox.Show("Pond deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting pond: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnUploadImage_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp",
                Title = "Select a Pond Image"
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
                    PondImage.Source = bitmap;
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading image: {ex.Message}", "Error", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    _imagePath = null;
                    PondImage.Source = new BitmapImage(new Uri("/WpfApp;component/image/pond.png", UriKind.Relative));
                    
                }
            }
        }
    }
}
