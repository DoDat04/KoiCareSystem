using BusinessObject;
using Services;
using Services.POND;
using Services.WATER;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp.WaterParam
{
    /// <summary>
    /// Interaction logic for WaterPage.xaml
    /// </summary>
    public partial class WaterPage : Page
    {
        private readonly IPondService _pondService;
        private readonly IWaterParamService _waterParameterService;

        public WaterPage()
        {
            InitializeComponent();
            var session = UserSession.GetInstance();
            _pondService = new PondService();
            _waterParameterService = new WaterParamService();
            LoadPonds();
        }

        private void LoadPonds()
        {
            try
            {
                var session = UserSession.GetInstance();
                PondItemsControl.ItemsSource = _pondService.GetAll(session.MemberId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading ponds: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CheckParameters_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is Pond selectedPond)
            {
                try
                {
                    var waterParam = _waterParameterService.GetLatestByPondId(selectedPond.PondId);
                    
                    if (waterParam != null)
                    {
                        ShowWater showWaterWindow = new(waterParam, selectedPond);
                        showWaterWindow.ShowDialog();
                    }
                    else
                    {
                        AddWater addWaterWindow = new AddWater(selectedPond);
                        bool? result = addWaterWindow.ShowDialog();
                        if (result == true)
                        {
                            // Optionally refresh the view
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error checking water parameters: {ex.Message}", 
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
