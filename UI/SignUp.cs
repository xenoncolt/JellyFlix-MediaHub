using JellyFlix_MediaHub.Data.Handlers;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JellyFlix_MediaHub.UI
{
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void SignUp_Load(object sender, EventArgs e)
        {

        }

        private void Username_TextChanged(object sender, EventArgs e)
        {
            
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

        private void EmailTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(EmailTextBox.Text, @"^[^@\s]+@gmail\.com$"))
            {
                EmailErrorMsg.Visible = true;
            } else
            {
                EmailErrorMsg.Visible = false;
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
        }

        private void SignUpButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(UsernameTextBox.Text) || string.IsNullOrEmpty(EmailTextBox.Text) || string.IsNullOrEmpty(PasswordTextBox.Text))
            {
                MessageBox.Show("Please fill in all fields", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (PasswordErrorMsg.Visible == true || ConfirmPassErrorMsg.Visible == true || UsernameErrorMsg.Visible == true || EmailErrorMsg.Visible == true)
            {
                MessageBox.Show("Error");
                return;
            }

            bool success = UserHandle.RegisterUser(UsernameTextBox.Text, EmailTextBox.Text, PasswordTextBox.Text);

            if (success)
            {
                MessageBox.Show("Registration Success! Now u can test using login", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Login loginForm = new Login();
                loginForm.Show();
                this.Hide();
            } else
            {
                UsernameErrorMsg.Visible = true;
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {

        }
    }
}
