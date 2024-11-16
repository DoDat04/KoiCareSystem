using BusinessObject;
using Services;
using Services.MEMBER;
using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp.HomeNAdmin;

namespace WpfApp
{
    public partial class AdminWindow : Window
    {
        private readonly IMemberService _memberService;

        public AdminWindow()
        {
            InitializeComponent();
            _memberService = new MemberService();
            AdminFrame.Navigate(new ManageUser());

        }

       
        private void OnLogoutClick(object sender, RoutedEventArgs e)
    {
            // Reset the user session
            UserSession.GetInstance().Logout();

            // Create and show the Home window
            Home homeWindow = new Home();
            homeWindow.Show();

            // Close all other windows including the current admin window
            foreach (Window window in Application.Current.Windows)
            {
                if (window != homeWindow)
                {
                    window.Close();
                }
            }
        }

        private void btnManageUsers_Click(object sender, RoutedEventArgs e)
        {
            AdminFrame.Navigate(new ManageUser());
        }

        private void btnManageCategories_Click(object sender, RoutedEventArgs e)
        {
            AdminFrame.Navigate(new HomeNAdmin.Categories.ManageCategory());
        }

        private void btnManageHistory_Click(object sender, RoutedEventArgs e)
        {
            AdminFrame.Navigate(new HomeNAdmin.HistoryOrder.History());
        }

        private void btnManageProducts_Click(object sender, RoutedEventArgs e)
        {
            AdminFrame.Navigate(new HomeNAdmin.Products.ManageProduct());
        }
    }
}