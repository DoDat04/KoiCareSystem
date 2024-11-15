using BusinessObject;
using Services;
using Services.WATER;
using System;
using System.Windows;

namespace WpfApp.WaterParam
{
    public partial class ShowWater : Window
    {
        private readonly IWaterParamService _waterParamService;
        private readonly WaterParameter _waterParameter;
        private readonly Pond _pond;

        public ShowWater(WaterParameter waterParameter, Pond pond)
        {
            InitializeComponent();
            _waterParameter = waterParameter ?? throw new ArgumentNullException(nameof(waterParameter));
            _pond = pond ?? throw new ArgumentNullException(nameof(pond));
            DataContext = _waterParameter;
            _waterParamService = new WaterParamService();
            
            CheckWaterParameters();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ValidateInputs())
                    return;

                _waterParamService.UpdateWaterParameter(_waterParameter);
                MessageBox.Show("Water parameters updated successfully!", "Success", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating water parameters: {ex.Message}", 
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to delete these water parameters?", 
                "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _waterParamService.DeleteWaterParameter(_waterParameter.ParameterId);
                    MessageBox.Show("Water parameters deleted successfully!", "Success", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting water parameters: {ex.Message}", 
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private bool ValidateInputs()
        {
            if (_waterParameter.PH < 0 || _waterParameter.PH > 14)
            {
                MessageBox.Show("pH level must be between 0 and 14.", 
                    "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (_waterParameter.Ammonia < 0)
            {
                MessageBox.Show("Ammonia level cannot be negative.", 
                    "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (_waterParameter.Nitrite < 0)
            {
                MessageBox.Show("Nitrite level cannot be negative.", 
                    "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (_waterParameter.Nitrate < 0)
            {
                MessageBox.Show("Nitrate level cannot be negative.", 
                    "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (_waterParameter.Temperature < 0 || _waterParameter.Temperature > 40)
            {
                MessageBox.Show("Temperature must be between 0°C and 40°C.", 
                    "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private void CheckWaterParameters()
        {
            // Clear previous warnings
            phWarning.Visibility = Visibility.Collapsed;
            ammoniaWarning.Visibility = Visibility.Collapsed;
            nitriteWarning.Visibility = Visibility.Collapsed;
            nitrateWarning.Visibility = Visibility.Collapsed;
            temperatureWarning.Visibility = Visibility.Collapsed;

            // Calculate pond volume in cubic meters
            decimal pondVolume = _pond.Length * _pond.Width * _pond.Depth;

            // Adjust thresholds based on pond size
            // Small pond: < 1,000 m³
            // Medium pond: 1,000 - 10,000 m³
            // Large pond: > 10,000 m³
            decimal ammoniaThreshold, nitriteThreshold, nitrateThreshold;
            decimal minTemp, maxTemp;

            if (pondVolume < 10m) // Small pond
            {
                ammoniaThreshold = 0.25m;
                nitriteThreshold = 0.5m;
                nitrateThreshold = 40m;
                minTemp = 20m;
                maxTemp = 30m;
            }
            else if (pondVolume < 100m) // Medium pond
            {
                ammoniaThreshold = 0.5m;
                nitriteThreshold = 1.0m;
                nitrateThreshold = 50m;
                minTemp = 18m;
                maxTemp = 32m;
            }
            else // Large pond
            {
                ammoniaThreshold = 1.0m;
                nitriteThreshold = 1.5m;
                nitrateThreshold = 60m;
                minTemp = 16m;
                maxTemp = 35m;
            }

            // Check pH (pH is generally consistent regardless of pond size)
            if (_waterParameter.PH < 6.5m || _waterParameter.PH > 8.5m)
            {
                phWarning.Text = "Warning: Ideal pH should be between 6.5 and 8.5";
                phWarning.Visibility = Visibility.Visible;
            }

            // Check Ammonia
            if (_waterParameter.Ammonia > ammoniaThreshold)
            {
                ammoniaWarning.Text = $"Warning: Ammonia levels are too high (should be < {ammoniaThreshold} mg/L for a {GetPondSizeCategory(pondVolume)} pond)";
                ammoniaWarning.Visibility = Visibility.Visible;
            }

            // Check Nitrite
            if (_waterParameter.Nitrite > nitriteThreshold)
            {
                nitriteWarning.Text = $"Warning: Nitrite levels are too high (should be < {nitriteThreshold} mg/L for a {GetPondSizeCategory(pondVolume)} pond)";
                nitriteWarning.Visibility = Visibility.Visible;
            }

            // Check Nitrate
            if (_waterParameter.Nitrate > nitrateThreshold)
            {
                nitrateWarning.Text = $"Warning: Nitrate levels are too high (should be < {nitrateThreshold} mg/L for a {GetPondSizeCategory(pondVolume)} pond)";
                nitrateWarning.Visibility = Visibility.Visible;
            }

            // Check Temperature
            if (_waterParameter.Temperature < minTemp || _waterParameter.Temperature > maxTemp)
            {
                temperatureWarning.Text = $"Warning: Temperature should be between {minTemp}°C and {maxTemp}°C for a {GetPondSizeCategory(pondVolume)} pond";
                temperatureWarning.Visibility = Visibility.Visible;
            }
        }

        private string GetPondSizeCategory(decimal volume)
        {
            if (volume < 10m) return "small";
            if (volume < 100m) return "medium";
            return "large";
        }
    }
}
