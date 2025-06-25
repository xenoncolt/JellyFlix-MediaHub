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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JellyFlix_MediaHub.UI
{
    public partial class UserProfile : Form
    {
        private readonly User current_user;
        public UserProfile(User user)
        {
            InitializeComponent();
            current_user = user;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {

        }

        private void EmailErrorMsg_Click(object sender, EventArgs e)
        {

        }

        private void ConfirmPasswordTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void UsernameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            App.Show(login, this);
        }

        private void UserProfile_Load(object sender, EventArgs e)
        {
            UsernameTextBox.PlaceholderText = current_user.Username;
            EmailTextBox.PlaceholderText = current_user.Email;
            PositionLabel.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(current_user.Role);
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            bool has_changes = false;
            string new_username = null;
            string new_email = null;
            string new_password = null;

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

                if (new_username != null) UsernameTextBox.PlaceholderText = new_username;
                if (new_email != null) EmailTextBox.PlaceholderText = new_email;

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
    }
}
