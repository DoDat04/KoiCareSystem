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
using BusinessObject;
using Services;
using Services.MEMBER;

namespace WpfApp.User
{
    /// <summary>
    /// Interaction logic for ForgotPass.xaml
    /// </summary>
    public partial class ForgotPass : Window
    {
        private readonly IMemberService _memberService;
        private readonly Member _currentMember;

        public ForgotPass()
        {
            InitializeComponent();
            _memberService = new MemberService();
            var session = UserSession.GetInstance();
            _currentMember = _memberService.GetMemberById(session.MemberId);
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentMember == null) return;

            string currentPassword = CurrentPasswordBox.Password;
            string newPassword = NewPasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;

            // Validate current password
            if (currentPassword != _currentMember.Password)
            {
                MessageBox.Show("Current password is incorrect.");
                return;
            }

            // Validate new password
            if (string.IsNullOrWhiteSpace(newPassword))
            {
                MessageBox.Show("New password cannot be empty.");
                return;
            }

            // Validate password match
            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New passwords do not match.");
                return;
            }

            try
            {
                _currentMember.Password = newPassword;
                _memberService.UpdateMember(_currentMember);
                MessageBox.Show("Password changed successfully!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error changing password: {ex.Message}");
            }
        }
    }
}
