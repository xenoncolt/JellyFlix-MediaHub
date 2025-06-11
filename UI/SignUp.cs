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
    }
}
