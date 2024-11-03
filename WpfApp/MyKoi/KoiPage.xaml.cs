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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Collections.Specialized.BitVector32;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for KoiPage.xaml
    /// </summary>
    public partial class KoiPage : Page
    {
        private readonly IFishService _fishService;
        public string MemberIdText { get; set; }

        public KoiPage()
        {
            InitializeComponent();
            _fishService = new FishService();
            var session = UserSession.GetInstance();
            MemberIdText = $"Member ID: {session.MemberId}";
            this.DataContext = this;
        }

        public void ListAllFish()
        {
            try
            {
                var session = UserSession.GetInstance();
                var fishInfo = _fishService.GetAll(session.MemberId);
                dgFishInfo.ItemsSource = fishInfo;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ListAllFish();
        }

        private void btnAddNewFish_Click(object sender, RoutedEventArgs e)
        {
            AddNewFishPopup addNewFishPopup = new AddNewFishPopup();
            bool? result = addNewFishPopup.ShowDialog();
            if (result == true)
            {
                ((Home)Application.Current.MainWindow).MainFrame.Navigate(new KoiPage());
            }
        }

        private void ViewDetail_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                Fish selectedKoi = button.DataContext as Fish; // Assuming Koi is your model
                if (selectedKoi != null)
                {
                    // Create and show the detail popup
                    KoiDetailPopup detailPopup = new KoiDetailPopup(selectedKoi);
                    detailPopup.ShowDialog();
                }
            }
        }

        private void dgFishInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgFishInfo.SelectedItem != null)
                {
                    var selectedFish = (Fish)dgFishInfo.SelectedItem;
                    MessageBoxResult result = MessageBox.Show("Are you sure to delete this customer?", "Confirm delete",
                            MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        _fishService.DeleteFish(selectedFish.FishId);
                        MessageBox.Show("Delete Fish Successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a fish to delete", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                ListAllFish();
            }
        }
    }
}
