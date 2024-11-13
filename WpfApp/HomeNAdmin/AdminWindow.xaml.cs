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
            Home window = new Home();
            window.Show();
            this.Close();
        }
        private void btnManageUsers_Click(object sender, RoutedEventArgs e)
        {
            AdminFrame.Navigate(new ManageUser());
        }

        private void btnManageCategories_Click(object sender, RoutedEventArgs e)
        {
            AdminFrame.Navigate(new HomeNAdmin.Categories.ManageCategory());
        }

        private void btnManageProducts_Click(object sender, RoutedEventArgs e)
        {
            AdminFrame.Navigate(new HomeNAdmin.Products.ManageProduct());
        }
    }
}