using BusinessObject;
using Services;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WpfApp.MyPond
{
    public partial class AddPond : Window
    {
        private readonly IPondService _pondService;
        private string _imagePath;

        public AddPond()
        {
            InitializeComponent();
            _pondService = new PondService();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ValidateInputs())
                    return;

                string imagePath = null;
                if (!string.IsNullOrEmpty(_imagePath))
                {
                    string fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(_imagePath);
                    string destinationPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PondImages", fileName);
                    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(destinationPath));
                    System.IO.File.Copy(_imagePath, destinationPath, true);
                    imagePath = destinationPath;
                }

                var session = UserSession.GetInstance();
                var newPond = new Pond
                {
                    Name = PondNameTextBox.Text,
                    Length = decimal.Parse(LengthTextBox.Text),
                    Width = decimal.Parse(WidthTextBox.Text),
                    Depth = decimal.Parse(DepthTextBox.Text),
                    MemberId = session.MemberId,
                    IsActive = true,
                    CreateDate = DateTime.Now,
                    ImagePath = imagePath
                };

                _pondService.AddPond(newPond);
                MessageBox.Show("Pond added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding pond: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(PondNameTextBox.Text))
            {
                MessageBox.Show("Please enter a pond name.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!decimal.TryParse(LengthTextBox.Text, out decimal length) || length <= 0)
            {
                MessageBox.Show("Please enter a valid length.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!decimal.TryParse(WidthTextBox.Text, out decimal width) || width <= 0)
            {
                MessageBox.Show("Please enter a valid width.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!decimal.TryParse(DepthTextBox.Text, out decimal depth) || depth <= 0)
            {
                MessageBox.Show("Please enter a valid depth.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
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
                    MessageBox.Show($"Error loading image: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    _imagePath = null;
                    PondImage.Source = new BitmapImage(new Uri("/WpfApp;component/image/pond.png", UriKind.Relative));
                }
            }
        }
    }
}
