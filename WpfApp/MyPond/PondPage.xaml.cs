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
using WpfApp.MyPond;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for PondPage.xaml
    /// </summary>
    public partial class PondPage : Page
    {
        private readonly IPondService _pondService;
        public string MemberIdText { get; set; }

        public PondPage()
        {
            InitializeComponent();
            var session = UserSession.GetInstance();
            MemberIdText = $"Member ID: {session.MemberId}";
            this.DataContext = this;
            _pondService = new PondService();
            LoadPonds();
        }

        private void LoadPonds()
        {
            try
            {
                var session = UserSession.GetInstance();
                PondListView.ItemsSource = _pondService.GetAll(session.MemberId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading ponds: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAddNewPond_Click(object sender, RoutedEventArgs e)
        {
            AddPond addPondWindow = new AddPond();
            bool? result = addPondWindow.ShowDialog();
            if (result == true)
            {
                LoadPonds();
            }
        }

        private void btnViewPond_Click(object sender, RoutedEventArgs e)
        {
            var selectedPond = PondListView.SelectedItem as Pond;
            if (selectedPond != null)
            {
                ShowPond showPondWindow = new ShowPond(selectedPond);
                showPondWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a pond to view details.", "No Pond Selected", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
