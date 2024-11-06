using BusinessObject;
using Services;
using System;
using System.Windows;

namespace WpfApp.MyPond
{
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
                if (!ValidateInputs())
                    return;

                var session = UserSession.GetInstance();
                var newPond = new Pond
                {
                    Name = PondNameTextBox.Text,
                    Length = decimal.Parse(LengthTextBox.Text),
                    Width = decimal.Parse(WidthTextBox.Text),
                    Depth = decimal.Parse(DepthTextBox.Text),
                    MemberId = session.MemberId,
                    IsActive = true,
                    CreateDate = DateTime.Now
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
    }
}
