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

        public LoginSignPopup(bool isLogin = true)
        {
            InitializeComponent();
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
            // Add your login logic here
            string email = LoginEmail.Text;
            string password = LoginPassword.Password;
            
            // TODO: Implement login validation
            MessageBox.Show("Login clicked");
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            // Add your sign up logic here
            string email = SignUpEmail.Text;
            string password = SignUpPassword.Password;
            string confirmPassword = SignUpConfirmPassword.Password;
            
            // TODO: Implement signup validation
            MessageBox.Show("Sign up clicked");
        }
    }
}
