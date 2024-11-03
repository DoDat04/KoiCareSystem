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
using WpfApp.User;

namespace WpfApp.User
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Page
    {
        private readonly IMemberService _memberService;
        private Member _currentMember;

        public Profile()
        {
            InitializeComponent();
            _memberService = new MemberService();
            LoadMemberData();
        }

        private void LoadMemberData()
        {
            var session = UserSession.GetInstance();
            if (session.IsLoggedIn)
            {
                _currentMember = _memberService.GetMemberById(session.MemberId);
                if (_currentMember != null)
                {
                    FirstNameBox.Text = _currentMember.FirstName;
                    LastNameBox.Text = _currentMember.LastName;
                    EmailBox.Text = _currentMember.Email;
                    PhoneBox.Text = _currentMember.PhoneNumber;
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentMember != null)
            {
                _currentMember.FirstName = FirstNameBox.Text;
                _currentMember.LastName = LastNameBox.Text;
                _currentMember.Email = EmailBox.Text;
                _currentMember.PhoneNumber = PhoneBox.Text;

                try
                {
                    _memberService.UpdateMember(_currentMember);
                    MessageBox.Show("Profile updated successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating profile: {ex.Message}");
                }
            }
        }

        private void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            var forgotPassWindow = new ForgotPass();
            forgotPassWindow.Owner = Window.GetWindow(this); // Set owner to center relative to main window
            forgotPassWindow.ShowDialog();
        }
    }
}
