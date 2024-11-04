using BusinessObject;
using Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp
{
    public partial class AdminWindow : Window
    {
        private readonly IMemberService _memberService;

        public AdminWindow()
        {
            InitializeComponent();
            _memberService = new MemberService();
        }

        public void LoadMember()
        {
            try
            {
                var member = _memberService.GetMembers();
                dgMemberList.ItemsSource = member;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadMember();
        }

        private void OnToggleStatusClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var button = (Button)sender;
                var member = (Member)button.DataContext;
                string action = member.IsActive ? "ban" : "unban";

                MessageBoxResult result = MessageBox.Show(
                    $"Are you sure you want to {action} this member?",
                    "Confirm Action",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    if (member.IsActive)
                    {
                        _memberService.RemoveMember(member.MemberId);
                        MessageBox.Show("Member has been banned successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        _memberService.RestoreMember(member.MemberId);
                        MessageBox.Show("Member has been unbanned successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    LoadMember();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnLogoutClick(object sender, RoutedEventArgs e)
        {
            Home window = new Home();
            window.Show();
            this.Close();
        }

        private void dgMemberList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dgMemberList.UnselectAll();
        }
    }
}