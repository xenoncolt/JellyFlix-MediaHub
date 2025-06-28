using JellyFlix_MediaHub.Data.Handlers;
using JellyFlix_MediaHub.Models;
using JellyFlix_MediaHub.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JellyFlix_MediaHub.UI
{
    public partial class UserProfile : Form
    {
        private readonly User current_user;
        private Form parent_form;

        public UserProfile(User current_user, Form parent_form = null)
        {
            InitializeComponent();
            this.current_user = current_user;
            this.parent_form = parent_form;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            if (this.parent_form != null)
            {
                App.Show(parent_form, this);
            }
            else
            {
                MainMenu main_menu = new MainMenu(current_user);
                App.Show(main_menu, this);
            }
        }

        private void ConfirmPasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ConfirmPasswordTextBox.Text != PasswordTextBox.Text)
            {
                ConfirmPassErrorMsg.Visible = true;
            }
            else
            {
                ConfirmPassErrorMsg.Visible = false;
            }
        }

        private void UsernameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (current_user.Username == UsernameTextBox.Text)
            {
                UsernameErrorMsg.Text = "*New Username can't be same as current username";
                UsernameErrorMsg.Visible = true;
            }
            else
            {
                UsernameErrorMsg.Visible = false;
            }
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            App.Show(login, this);
        }

        private void UserProfile_Load(object sender, EventArgs e)
        {
            UsernameTextBox.Visible = false;
            EmailTextBox.Visible = false;

            usernameText.Visible = true;
            usernameText.Text = current_user.Username;

            emailText.Visible = true;
            emailText.Text = current_user.Email;

            PositionLabel.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(current_user.Role);
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            bool has_changes = false;
            string new_username = null;
            string new_email = null;
            string new_password = null;

            if (PasswordErrorMsg.Visible == true || ConfirmPassErrorMsg.Visible == true || UsernameErrorMsg.Visible == true || EmailErrorMsg.Visible == true)
            {
                // MessageBox.Show("Error");
                return;
            }

            if (!string.IsNullOrEmpty(UsernameTextBox.Text) && UsernameTextBox.Text != current_user.Username)
            {
                new_username = UsernameTextBox.Text;
                has_changes = true;
            }

            if (!string.IsNullOrEmpty(EmailTextBox.Text) && EmailTextBox.Text != current_user.Email)
            {
                new_email = EmailTextBox.Text;
                has_changes = true;
            }

            if (!string.IsNullOrEmpty(PasswordTextBox.Text))
            {
                new_password = PasswordTextBox.Text;
                has_changes = true;
            }

            if (!has_changes) return;

            bool success = UserHandle.UpdateUser(current_user.UserId, new_username, new_email, new_password);

            if (success)
            {
                MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (new_username != null)
                {
                    usernameText.Text = new_username;
                    usernameText.Visible = true;
                    UsernameTextBox.Visible = false;
                }
                if (new_email != null)
                {
                    emailText.Text = new_email;
                    emailText.Visible = true;
                    EmailTextBox.Visible = false;
                }

                UsernameTextBox.Text = string.Empty;
                EmailTextBox.Text = string.Empty;
                PasswordTextBox.Text = string.Empty;

                current_user.Username = new_username ?? current_user.Username;
                current_user.Email = new_email ?? current_user.Email;
                current_user.Password = new_password ?? current_user.Password;
            }
            else
            {
                MessageBox.Show("Failed to update profile. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EmailTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(EmailTextBox.Text, @"^[^@\s]+@gmail\.com$"))
            {
                EmailErrorMsg.Visible = true;
            }
            else
            {
                EmailErrorMsg.Visible = false;
            }

            if (current_user.Email == EmailTextBox.Text)

            {
                EmailErrorMsg.Text = "*New Email can't be same as current email";
                EmailErrorMsg.Visible = true;
            }
        }

        private void PasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ConfirmPasswordTextBox.Text != PasswordTextBox.Text)
            {
                ConfirmPassErrorMsg.Visible = true;
            }
            else
            {
                ConfirmPassErrorMsg.Visible = false;
            }

            if (PasswordTextBox.Text.Length < 4)
            {
                PasswordErrorMsg.Visible = true;
            }
            else
            {
                PasswordErrorMsg.Visible = false;
            }

            if (PasswordTextBox.Text == current_user.Password)
            {
                PasswordErrorMsg.Text = "*Your new password same as old password";
                PasswordErrorMsg.Visible = true;
            }
            else
            {
                ConfirmPassErrorMsg.Visible = false;
            }
        }

        private void usernameText_Click(object sender, EventArgs e)
        {
            usernameText.Visible = false;
            UsernameTextBox.Visible = true;
        }

        private void emailText_Click(object sender, EventArgs e)
        {
            emailText.Visible = false;
            EmailTextBox.Visible = true;
        }

        private void UsernameTextBox_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(UsernameTextBox.Text))
            {
                usernameText.Text = current_user.Username;
                usernameText.Visible = true;
                UsernameTextBox.Visible = false;
            }
            else
            {
                UsernameTextBox.Visible = true;
            }
        }

        private void EmailTextBox_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(EmailTextBox.Text))
            {
                emailText.Text = current_user.Email;
                emailText.Visible = true;
                EmailTextBox.Visible = false;
            }
            else
            {
                EmailTextBox.Visible = true;
            }
        }
    }
}
