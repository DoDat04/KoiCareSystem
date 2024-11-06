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
        private readonly IPondService _pondService;
        public string MemberIdText { get; set; }

        public KoiPage()
        {
            InitializeComponent();
            _fishService = new FishService();
            _pondService = new PondService();
            var session = UserSession.GetInstance();
            MemberIdText = $"Member ID: {session.MemberId}";
        }

        public void ListAllFish()
        {
            try
            {
                var session = UserSession.GetInstance();
                var fishInfo = _fishService.GetAll(session.MemberId);
                KoiItemsControl.ItemsSource = fishInfo;
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
            var session = UserSession.GetInstance();
            var pond = _pondService.GetAll(session.MemberId);
            if (pond == null || !pond.Any())
            {
                MessageBox.Show("Please create pond first", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                AddNewFishPopup addNewFishPopup = new AddNewFishPopup();
                bool? result = addNewFishPopup.ShowDialog();
                if (result == true)
                {
                    ListAllFish();
                }
            }
        }

        private void ViewDetail_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                Fish selectedKoi = button.DataContext as Fish;
                if (selectedKoi != null)
                {
                    KoiDetailPopup detailPopup = new KoiDetailPopup(selectedKoi);
                    bool? result = detailPopup.ShowDialog();
                    if (result == true)
                    {
                        ListAllFish();
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                if (button != null)
                {
                    Fish selectedFish = button.DataContext as Fish;
                    if (selectedFish != null)
                    {
                        MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this fish?", 
                            "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                        
                        if (result == MessageBoxResult.Yes)
                        {
                            _fishService.DeleteFish(selectedFish.FishId);
                            MessageBox.Show("Fish deleted successfully", "Success", 
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            ListAllFish();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a fish to delete", "Notification", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
