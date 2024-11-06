using BusinessObject;
using Services;
using System;
using System.Windows;

namespace WpfApp.MyPond
{
    public partial class ShowPond : Window
    {
        private readonly IPondService _pondService;
        private readonly IFishService _fishService;
        private readonly Pond _pond;

        public ShowPond(Pond pond)
        {
            InitializeComponent();
            _pond = pond ?? throw new ArgumentNullException(nameof(pond));
            DataContext = _pond;
            _pondService = new PondService();
            _fishService = new FishService();
            LoadFishList();
        }

        private void LoadFishList()
        {
            try
            {
                var fishList = _fishService.GetFishByPondId(_pond.PondId);
                FishListView.ItemsSource = fishList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading fish list: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

                _pondService.UpdatePond(_pond);
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
            if (string.IsNullOrWhiteSpace(_pond.Name))
            {
                MessageBox.Show("Please enter a pond name.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (_pond.Length <= 0)
            {
                MessageBox.Show("Length must be greater than 0.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (_pond.Width <= 0)
            {
                MessageBox.Show("Width must be greater than 0.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (_pond.Depth <= 0)
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
                    _pondService.DeletePond(_pond.PondId);
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
    }
}
