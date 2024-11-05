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

namespace WpfApp.MyPond
{
    /// <summary>
    /// Interaction logic for AddPond.xaml
    /// </summary>
    public partial class AddPond : Window
    {
        private readonly IPondService _pondService;

        public AddPond()
        {
            InitializeComponent();
            _pondService = new PondService();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = PondNameTextBox.Text;
                decimal length = decimal.Parse(LengthTextBox.Text);
                decimal width = decimal.Parse(WidthTextBox.Text);
                decimal depth = decimal.Parse(DepthTextBox.Text);
                bool isActive = IsActiveCheckBox.IsChecked ?? false;

                var session = UserSession.GetInstance();
                Pond newPond = new Pond
                {
                    Name = name,
                    Length = length,
                    Width = width,
                    Depth = depth,
                    MemberId = session.MemberId,
                    IsActive = isActive,
                    CreateDate = DateTime.Now
                };

                _pondService.AddPond(newPond);
                MessageBox.Show("Pond added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding pond: {ex.Message}");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
