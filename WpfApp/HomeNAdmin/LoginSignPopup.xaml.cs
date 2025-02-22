﻿using Services;
using Services.MEMBER;
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
               
            }
            else
            {
                TitleText.Text = "Sign Up";
                LoginPanel.Visibility = Visibility.Collapsed;
                SignUpPanel.Visibility = Visibility.Visible;
                
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
                    return;
                }

                if (!VerifyPassword(password, account.Password))
                {
                    MessageBox.Show("Wrong password!");
                    return;
                }

                if (!account.IsActive)
                {
                    MessageBox.Show("We're sorry to inform you that your account has been banned due to a violation of our terms of service. " +
                        "We appreciate your understanding in this matter.\r\n\r\nThank you for being a part of our community. We wish you all the best and hope you have a great day ahead!",
                        "Your account is banned", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Handle admin login
                if (account.RoleId == 1)
                {
                    UserSession.GetInstance().Login(account.MemberId, account.Email, account.RoleId);
                    MessageBox.Show("Admin login successful!");
                    
                    // Create and show admin window
                    var adminWindow = new AdminWindow();
                    adminWindow.Show();

                    // Close all other windows
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window != adminWindow && window != this)
                        {
                            window.Close();
                        }
                    }

                    this.Close();
                }
                // Handle regular user login
                else
                {
                    UserSession.GetInstance().Login(account.MemberId, account.Email, account.RoleId);
                    MessageBox.Show("Login successful!");
                    
                    // Update main window if it exists
                    if (Application.Current.MainWindow is Home homeWindow)
                    {
                        homeWindow.UpdateLoginState();
                    }
                    
                    this.DialogResult = true;
                    this.Close();
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

            try
            {
                // Check if email already exists
                var existingMember = _memberService.GetMemberByEmail(email);
                if (existingMember != null)
                {
                    MessageBox.Show("An account with this email already exists.");
                    return;
                }

                // Create new member
                var newMember = new BusinessObject.Member
                {
                    Email = email,
                    Password = password, // In production, this should be hashed
                    RoleId = 2, // Assuming 2 is for regular users
                    FirstName = "New", // These can be updated later
                    LastName = "User",
                    PhoneNumber = "0000000000",
                    IsActive = true,
                    CreateDate = DateTime.Now,
                    CreateBy = email
                };

                _memberService.AddMember(newMember);
                MessageBox.Show("Account created successfully! Please log in.");
                
                // Switch to login mode
                isLoginMode = true;
                UpdateUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during sign up: {ex.Message}");
            }
        }
    }
}
