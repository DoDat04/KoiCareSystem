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
using BusinessObject;
using Services;

namespace WpfApp.HomeNAdmin
{
    /// <summary>
    /// Interaction logic for ManageUser.xaml
    /// </summary>
    public partial class ManageUser : Page
    {
        private readonly IMemberService _memberService;

        public ManageUser()
        {
            InitializeComponent();
            _memberService = new MemberService();
            LoadMember();
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
    }
}
