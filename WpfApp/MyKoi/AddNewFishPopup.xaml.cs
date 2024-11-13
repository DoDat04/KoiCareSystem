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
    /// Interaction logic for AddNewFishPopup.xaml
    /// </summary>
    public partial class AddNewFishPopup : Window
    {
        private readonly IPondService _pondService;
        private readonly IFishService _fishService;
        private string _imagePath; // Variable to hold the image path

        public AddNewFishPopup()
        {
            InitializeComponent();
            _pondService = new PondService();
            _fishService = new FishService();
            // Default image is now set in XAML
        }

        public void LoadPonds()
        {
            try
            {
                var session = UserSession.GetInstance();
                var ponds = _pondService.GetAll(session.MemberId);
                PondComboBox.ItemsSource = ponds;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
               
            }
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
                    MessageBox.Show($"Error loading image: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    _imagePath = null;
                    FishImage.Source = new BitmapImage(new Uri("/WpfApp;component/image/fish.png", UriKind.Relative));
                   
                }
            }
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var session = UserSession.GetInstance();
                string name = NameTextBox.Text;
                string breed = BreedTextBox.Text;
                string genderFish = (SexComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                decimal weight = decimal.Parse(WeightTextBox.Text);
                decimal length = decimal.Parse(LengthTextBox.Text);
                DateTime birthDate = DateOfBirthDatePicker.SelectedDate ?? DateTime.Now; 
                int pondId = (int)(PondComboBox.SelectedValue ?? 0);

                Fish newFish = new Fish
                (
                    pondId: pondId,
                    memberId: session.MemberId, 
                    name: name,
                    length: length,
                    weight: weight,
                    birthDate: birthDate,
                    gender: genderFish,
                    breed: breed,
                    isActive: true, 
                    createDate: null,
                    imagePath: _imagePath 

                );

                _fishService.AddNewFish(newFish);
                MessageBox.Show("Fish Added Successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding fish: {ex.Message}");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadPonds();
        }
    }
}
