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
    }
}
