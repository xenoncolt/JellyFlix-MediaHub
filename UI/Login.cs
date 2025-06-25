using JellyFlix_MediaHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JellyFlix_MediaHub.Data.Handlers;
using JellyFlix_MediaHub.Utils;

namespace JellyFlix_MediaHub.UI
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void SignupButton_Click(object sender, EventArgs e)
        {
            SignUp signUpForm = new SignUp();
            App.Show(signUpForm, this);
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            bool is_valid = true;
            if (string.IsNullOrEmpty(UsernameTextBox.Text))
            {
                UsernameTextBox.ErrorMessage = "Username is required";
                UsernameTextBox.SetErrorState(true);
                is_valid = false;
            } else
            {
                UsernameTextBox.SetErrorState(false);
            }

            if (string.IsNullOrEmpty(PasswordTextBox.Text))
            {
                PasswordTextBox.ErrorMessage = "Password is required";
                PasswordTextBox.SetErrorState(true);
                is_valid = false;
            } else
            {
                PasswordTextBox.SetErrorState(false);
            }

            if (!is_valid) return;

            User user = UserHandle.AuthenticateUser(UsernameTextBox.Text, PasswordTextBox.Text);

            if (user != null)
            {
                // MessageBox.Show("Everything ok", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MainMenu main_form = new MainMenu(user);
                App.Show(main_form, this);
            } else
            {
                UsernameTextBox.ErrorMessage = "Username is incorrect";
                PasswordTextBox.ErrorMessage = "Password is incorrect";
                PasswordTextBox.SetErrorState(true);
                UsernameTextBox.SetErrorState(true);
            }
        }

        private void UsernameTextBox_Click(object sender, EventArgs e)
        {
            UsernameTextBox.SetErrorState(false);
        }

        private void PasswordTextBox_Click(object sender, EventArgs e)
        {
            PasswordTextBox.SetErrorState(false);
        }

        private void PasswordTextBox_TrailingIconClick(object sender, EventArgs e)
        {
            PasswordTextBox.UseSystemPasswordChar = !PasswordTextBox.UseSystemPasswordChar;

            if (PasswordTextBox.UseSystemPasswordChar)
            {
                PasswordTextBox.TrailingIcon = Properties.Resources.eye_slash_solid;
                PasswordTextBox.PasswordChar = '●';
            }
            else
            {
                PasswordTextBox.TrailingIcon = Properties.Resources.eye_solid;
                PasswordTextBox.PasswordChar = '\0';
            }
        }
    }
}
