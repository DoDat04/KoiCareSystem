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

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
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
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadMember();
        }

        private void btnDeleteMember_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgMemberList.SelectedItem != null)
                {
                    var selectedMember = (Member)dgMemberList.SelectedItem;
                    if (selectedMember.IsActive == false)
                    {
                        MessageBox.Show("This customer has already been deleted", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBoxResult result = MessageBox.Show("Are you sure to delete this member?", "Confirm delete",
                            MessageBoxButton.YesNo, MessageBoxImage.Warning);

                        if (result == MessageBoxResult.Yes)
                        {
                            _memberService.RemoveMember(selectedMember.MemberId);
                            MessageBox.Show("Delete Member Successfully");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a member to delete", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                LoadMember();
            }
        }

        private void btnRestoreMember_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgMemberList.SelectedItem != null)
                {
                    var selectedMember = (Member)dgMemberList.SelectedItem;
                    if (selectedMember.IsActive == true)
                    {
                        MessageBox.Show("This member is still activate", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        _memberService.RestoreMember(selectedMember.MemberId);
                        MessageBox.Show("Restore Member Successfully", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a member to restore", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                LoadMember();
            }
        }
    }
}
