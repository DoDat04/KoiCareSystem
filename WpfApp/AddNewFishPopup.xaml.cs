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
        public AddNewFishPopup()
        {
            InitializeComponent();
            _pondService = new PondService();
            _fishService = new FishService();
        }

        public void LoadPonds()
        {
            try
            {
                var ponds = _pondService.GetAll();
                PondComboBox.ItemsSource = ponds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
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
                    memberId: 1, 
                    name: name,
                    length: length,
                    weight: weight,
                    birthDate: birthDate,
                    gender: genderFish,
                    breed: breed,
                    isActive: true, 
                    createDate: null 
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
