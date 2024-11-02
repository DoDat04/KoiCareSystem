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
                var fishInfo = _fishService.GetAll();
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
    }
}
