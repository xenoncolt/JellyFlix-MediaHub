using JellyFlix_MediaHub.Models;
using JellyFlix_MediaHub.Utils;
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
    public partial class MainMenu : Form
    {
        private readonly User currentUser;

        public MainMenu(User user)
        {
            InitializeComponent();
            currentUser = user;
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            if (currentUser != null)
            {
                if (currentUser.Role == "user")
                {
                    NavMenu.TabPages.Remove(InvitesPage);
                    NavMenu.TabPages.Remove(UsersTab);
                } else if (currentUser.Role == "premium")
                {
                    NavMenu.TabPages.Remove(UsersTab);
                }
            }
        }

        private void ProfileBox_Click(object sender, EventArgs e)
        {
            UserProfile user_profile = new UserProfile(currentUser);
            App.Show(user_profile, this);
        }

        private void NavMenu_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SettingsTab_Click(object sender, EventArgs e)
        {

        }
    }
}
