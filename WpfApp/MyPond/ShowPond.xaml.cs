using BusinessObject;
using Services;
using Services.FISH;
using Services.POND;
using System;
using System.Collections.ObjectModel;
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

        // Create a view model class to handle both pond and fish list
        public class ShowPondViewModel
        {
            public Pond Pond { get; set; }
            public ObservableCollection<Fish> FishList { get; set; }

            public ShowPondViewModel()
            {
                FishList = new ObservableCollection<Fish>();
            }
        }

        public ShowPondViewModel ViewModel { get; set; }

        public ShowPond(Pond pond)
        {
            InitializeComponent();
            _pond = pond ?? throw new ArgumentNullException(nameof(pond));
            _pondService = new PondService();
            _fishService = new FishService();
            
            // Initialize ViewModel
            ViewModel = new ShowPondViewModel
            {
                Pond = _pond
            };
            
            // Set DataContext to ViewModel
            this.DataContext = ViewModel;
            
            // Load data
            LoadPondData();
            LoadFishByPondId(_pond.PondId);
        }

        private void LoadPondData()
        {
            // Load pond image
            if (!string.IsNullOrEmpty(ViewModel.Pond.ImagePath))
            {
                try 
                {
                    _imagePath = ViewModel.Pond.ImagePath;
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(ViewModel.Pond.ImagePath);
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

        private void LoadFishByPondId(int pondId)
        {
            try
            {
                ViewModel.FishList.Clear();
                var fishList = _fishService.GetFishByPondId(pondId);
                foreach (var fish in fishList)
                {
                    // Set default image if fish image is null or empty
                    if (string.IsNullOrEmpty(fish.ImagePath))
                    {
                        fish.ImagePath = "/WpfApp;component/image/fish.png";
                    }
                    ViewModel.FishList.Add(fish);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading fish: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                if (!string.IsNullOrEmpty(_imagePath) && _imagePath != ViewModel.Pond.ImagePath)
                {
                    string fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(_imagePath);
                    string destinationPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PondImages", fileName);
                    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(destinationPath));
                    System.IO.File.Copy(_imagePath, destinationPath, true);
                    ViewModel.Pond.ImagePath = destinationPath;
                }

                _pondService.UpdatePond(ViewModel.Pond);
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
            if (string.IsNullOrWhiteSpace(ViewModel.Pond.Name))
            {
                MessageBox.Show("Please enter a pond name.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (ViewModel.Pond.Length <= 0)
            {
                MessageBox.Show("Length must be greater than 0.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (ViewModel.Pond.Width <= 0)
            {
                MessageBox.Show("Width must be greater than 0.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (ViewModel.Pond.Depth <= 0)
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
                    _pondService.DeletePond(ViewModel.Pond.PondId);
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
