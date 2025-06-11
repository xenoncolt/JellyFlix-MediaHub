using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            signUpForm.Show();
            signUpForm.FormClosed += (s, args) => Application.Exit();
            this.Hide();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(UsernameTextBox.Text))
            {
                UsernameTextBox.ErrorMessage = "Username is required";
                UsernameTextBox.SetErrorState(true);
                
            } else
            {
                UsernameTextBox.SetErrorState(false);
            }

            if (string.IsNullOrEmpty(PasswordTextBox.Text))
            {
                PasswordTextBox.ErrorMessage = "Password is required";
                PasswordTextBox.SetErrorState(true);
                
            } else
            {
                PasswordTextBox.SetErrorState(false);
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
    }
}
