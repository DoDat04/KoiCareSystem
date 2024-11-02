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
    /// Interaction logic for LoginSignPopup.xaml
    /// </summary>
    public partial class LoginSignPopup : Window
    {
        private bool isLoginMode;
        private readonly IMemberService _memberService;

        public LoginSignPopup(bool isLogin = true)
        {
            InitializeComponent();
            _memberService = new MemberService();
            isLoginMode = isLogin;
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (isLoginMode)
            {
                TitleText.Text = "Login";
                LoginPanel.Visibility = Visibility.Visible;
                SignUpPanel.Visibility = Visibility.Collapsed;
                SwitchText.Text = "Don't have an account? ";
                SwitchLinkText.Text = "Sign up";
            }
            else
            {
                TitleText.Text = "Sign Up";
                LoginPanel.Visibility = Visibility.Collapsed;
                SignUpPanel.Visibility = Visibility.Visible;
                SwitchText.Text = "Already have an account? ";
                SwitchLinkText.Text = "Login";
            }
        }

        private void SwitchLink_Click(object sender, RoutedEventArgs e)
        {
            isLoginMode = !isLoginMode;
            UpdateUI();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = LoginEmail.Text;
            string password = LoginPassword.Password;

            // Input validation
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Email and password must not be empty.");
                return;
            }

            try
            {
                var account = _memberService.GetMemberByEmail(email);
                if (account == null)
                {
                    MessageBox.Show("No account found with that email.");
                }
                else if (!VerifyPassword(password, account.Password)) // VerifyPassword should hash the password and compare
                {
                    MessageBox.Show("Wrong password!");
                }
                else
                {
                    MessageBox.Show("Login successfully!");
                    MessageBox.Show($"Member ID: {account.MemberId}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private bool VerifyPassword(string inputPassword, string storedPassword)
        {
            // Implement password verification logic here (e.g., hash comparison)
            return inputPassword == storedPassword; // Replace with actual hash comparison
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            // Add your sign up logic here
            string email = SignUpEmail.Text;
            string password = SignUpPassword.Password;
            string confirmPassword = SignUpConfirmPassword.Password;

            // Input validation
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("All fields must be filled.");
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            // TODO: Implement signup logic (e.g., save to database)
            MessageBox.Show("Sign up clicked");
        }
    }
}
