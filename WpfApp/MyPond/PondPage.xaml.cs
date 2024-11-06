using BusinessObject;
using Services;
using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp.MyPond;

namespace WpfApp
{
    public partial class PondPage : Page
    {
        private readonly IPondService _pondService;
        public string MemberIdText { get; set; }

        public PondPage()
        {
            InitializeComponent();
            var session = UserSession.GetInstance();
            MemberIdText = $"Member ID: {session.MemberId}";
            _pondService = new PondService();
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

        private void btnAddNewPond_Click(object sender, RoutedEventArgs e)
        {
            AddPond addPondWindow = new AddPond();
            bool? result = addPondWindow.ShowDialog();
            if (result == true)
            {
                LoadPonds();
            }
        }

        private void ViewDetail_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Pond selectedPond)
            {
                ShowPond showPondWindow = new ShowPond(selectedPond);
                bool? result = showPondWindow.ShowDialog();
                if (result == true)
                {
                    LoadPonds();
                }
            }
        }
    }
}
