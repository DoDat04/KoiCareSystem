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
    /// Interaction logic for ShowPond.xaml
    /// </summary>
    public partial class ShowPond : Window
    {
        private readonly IPondService _pondService;
        private readonly IFishService _fishService;
        private readonly Pond _pond;
        public string MemberIdText { get; set; }


        public ShowPond(Pond pond)
        {
            InitializeComponent();
            _pond = pond ?? throw new ArgumentNullException(nameof(pond));
            DataContext = _pond;  
            _pondService = new PondService();
            _fishService = new FishService();
            var session = UserSession.GetInstance();
            MemberIdText = $"Member ID: {session.MemberId}";
            ListAllFish();
            LoadPonds();
        }
        private void LoadPonds()
        {
            try
            {
                var session = UserSession.GetInstance();
                var ponds = _pondService.GetAll(session.MemberId);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ListAllFish()
        {
            try
            {
                var session = UserSession.GetInstance();
                var fishInfo = _fishService.GetAll(session.MemberId);
                FishListView.ItemsSource = fishInfo;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Ensure any UI changes are applied to _pond
                if (_pond == null)
                {
                    MessageBox.Show("No pond selected for update.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                _pondService.UpdatePond(_pond);  // Call UpdatePond with the updated _pond object
                MessageBox.Show("Pond updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating pond: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_pond == null)
                {
                    MessageBox.Show("No pond selected for deletion.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this pond?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    _pondService.DeletePond(_pond.PondId);  // Call DeletePond with _pond's ID
                    MessageBox.Show("Pond deleted successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting pond: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
