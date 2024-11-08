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

namespace WpfApp.WaterParam
{
    /// <summary>
    /// Interaction logic for AddWater.xaml
    /// </summary>
    public partial class AddWater : Window
    {
        private readonly IWaterParamService _waterParamService;
        private readonly Pond _pond;

        public AddWater(Pond pond)
        {
            InitializeComponent();
            _waterParamService = new WaterParamService();
            _pond = pond;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ValidateInputs())
                    return;

                var waterParam = new WaterParameter
                {
                    PondId = _pond.PondId,
                    MeasurementDate = DateTime.Now,
                    PH = decimal.Parse(PhTextBox.Text),
                    Ammonia = decimal.Parse(AmmoniaTextBox.Text),
                    Nitrite = decimal.Parse(NitriteTextBox.Text),
                    Nitrate = decimal.Parse(NitrateTextBox.Text),
                    Temperature = decimal.Parse(TemperatureTextBox.Text),
                    IsActive = true,
                    CreateDate = DateTime.Now
                };

                _waterParamService.AddNewWaterParameter(waterParam);
                MessageBox.Show("Water parameters added successfully!", "Success", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding water parameters: {ex.Message}", 
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateInputs()
        {
            if (!decimal.TryParse(PhTextBox.Text, out decimal ph) || ph < 0 || ph > 14)
            {
                MessageBox.Show("Please enter a valid pH value (0-14).", 
                    "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!decimal.TryParse(AmmoniaTextBox.Text, out decimal ammonia) || ammonia < 0)
            {
                MessageBox.Show("Please enter a valid ammonia value.", 
                    "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!decimal.TryParse(NitriteTextBox.Text, out decimal nitrite) || nitrite < 0)
            {
                MessageBox.Show("Please enter a valid nitrite value.", 
                    "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!decimal.TryParse(NitrateTextBox.Text, out decimal nitrate) || nitrate < 0)
            {
                MessageBox.Show("Please enter a valid nitrate value.", 
                    "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!decimal.TryParse(TemperatureTextBox.Text, out decimal temp) || temp < 0 || temp > 40)
            {
                MessageBox.Show("Please enter a valid temperature (0-40°C).", 
                    "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
