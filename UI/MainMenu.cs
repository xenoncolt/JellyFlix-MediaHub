using Guna.UI2.WinForms;
using JellyFlix_MediaHub.Data.Handlers;
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
        private readonly AvatarImage avatar_img;
        private List<User> all_users;

        public MainMenu(User user)
        {
            InitializeComponent();
            currentUser = user;
            avatar_img = new AvatarImage(currentUser.Username);
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

            if (avatar_img.LoadUserAvatar() != null)
            {
                ProfileBox.Image = avatar_img.LoadUserAvatar();
            }

            LoadAllUsers();
        }

        private void LoadAllUsers()
        {
            try
            {
                Console.WriteLine("Loading all users...");
                all_users = UserHandle.GetAllUsers();
                Console.WriteLine($"Loaded {all_users.Count} users.");
                PopulateUserPanels();
            } catch (Exception e)
            {
                Console.WriteLine($"Error loading users: {e.Message}");
                MessageBox.Show($"Error loading users: {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                all_users = new List<User>();
            }
        }

        private void PopulateUserPanels()
        {
            var panels_to_remove = UsersTab.Controls.OfType<Control>()
                .Where(p => p is Guna2GradientPanel && p != UserListPanel && p != TableTitlePanel)
                .ToList();

            foreach (var panel in panels_to_remove)
            {
                UsersTab.Controls.Remove(panel);
                panel.Dispose();
            }

            UserListPanel.Visible = false;

            int y_position = TableTitlePanel.Bottom + 10; 

            foreach (var user in all_users)
            {
                CreateUserPanel(user, y_position);
                y_position += 59; // Panel height + spacing
            }
        }

        private void CreateUserPanel(User user, int yPosition)
        {
            // Clone the UserListPanel
            Guna2GradientPanel userPanel = new Guna2GradientPanel();
            userPanel.Size = UserListPanel.Size;
            userPanel.Location = new Point(UserListPanel.Location.X, yPosition);
            userPanel.FillColor = UserListPanel.FillColor;
            userPanel.FillColor2 = UserListPanel.FillColor2;
            userPanel.GradientMode = UserListPanel.GradientMode;
            userPanel.AutoRoundedCorners = true;
            userPanel.Dock = DockStyle.Top;
            userPanel.Tag = user;

            // Username Label
            Guna2HtmlLabel usernameLabel = new Guna2HtmlLabel();
            usernameLabel.Text = user.Username;
            usernameLabel.Font = UsernameColumn.Font;
            usernameLabel.ForeColor = UsernameColumn.ForeColor;
            usernameLabel.BackColor = Color.Transparent;
            usernameLabel.Location = new Point(23, 11);
            usernameLabel.AutoSize = true;

            // Email Label
            Guna2HtmlLabel emailLabel = new Guna2HtmlLabel();
            emailLabel.Text = user.Email;
            emailLabel.Font = EmailColumn.Font;
            emailLabel.ForeColor = EmailColumn.ForeColor;
            emailLabel.BackColor = Color.Transparent;
            emailLabel.Location = new Point(260, 11);
            emailLabel.AutoSize = true;

            // Role ComboBox
            Guna2ComboBox roleComboBox = new Guna2ComboBox();
            roleComboBox.Size = UserTypeColumn.Size;
            roleComboBox.Location = new Point(610, 9);
            roleComboBox.FillColor = UserTypeColumn.FillColor;
            roleComboBox.Font = UserTypeColumn.Font;
            roleComboBox.ForeColor = UserTypeColumn.ForeColor;
            roleComboBox.AutoRoundedCorners = true;
            roleComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            roleComboBox.Items.AddRange(new string[] { "User", "Premium", "Admin" });

            // Set current role
            roleComboBox.SelectedItem = char.ToUpper(user.Role[0]) + user.Role.Substring(1);
            roleComboBox.Tag = user;
            roleComboBox.SelectedIndexChanged += UserTypeColumn_SelectedIndexChanged;

            // Created Date Label
            Guna2HtmlLabel createdDateLabel = new Guna2HtmlLabel();
            createdDateLabel.Text = user.CreatedDate.ToString("dd/MM/yyyy - HH:mm");
            createdDateLabel.Font = CreatedTimeColumn.Font;
            createdDateLabel.ForeColor = CreatedTimeColumn.ForeColor;
            createdDateLabel.BackColor = Color.Transparent;
            createdDateLabel.Location = new Point(857, 11);
            createdDateLabel.AutoSize = true;

            // Add controls to panel
            userPanel.Controls.Add(usernameLabel);
            userPanel.Controls.Add(emailLabel);
            userPanel.Controls.Add(roleComboBox);
            userPanel.Controls.Add(createdDateLabel);

            // Add double-click event for deletion
            userPanel.MouseDoubleClick += UserListPanel_MouseDoubleClick;
            usernameLabel.MouseDoubleClick += UserListPanel_MouseDoubleClick;
            emailLabel.MouseDoubleClick += UserListPanel_MouseDoubleClick;
            createdDateLabel.MouseDoubleClick += UserListPanel_MouseDoubleClick;

            // Add panel to UsersTab
            UsersTab.Controls.Add(userPanel);
            userPanel.BringToFront();
        }

        private void ProfileBox_Click(object sender, EventArgs e)
        {
            UserProfile user_profile = new UserProfile(currentUser);
            App.Show(user_profile, this);
        }

        private void NavMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (NavMenu.SelectedTab == UsersTab)
            {
                LoadAllUsers();
            }
        }

        private void SettingsTab_Click(object sender, EventArgs e)
        {

        }

        private void UserTypeColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is Guna2ComboBox combo_box && combo_box.Tag is User user)
            {
                string new_role = combo_box.SelectedItem.ToString().ToLower();

                if (user.UserId == currentUser.UserId)
                {
                    MsgBox.Caption = "Permission Denied";
                    MsgBox.Text = "You cannot change your own role.";
                    MsgBox.Icon = MessageDialogIcon.Warning;
                    MsgBox.Buttons = MessageDialogButtons.OK;
                    MsgBox.Show();
                    combo_box.SelectedItem = char.ToUpper(user.Role[0]) + user.Role.Substring(1);
                    return;
                }

                if (UserHandle.UpdateUserRole(user.UserId, new_role))
                {
                    user.Role = new_role;
                    MsgBox.Caption = "Success";
                    MsgBox.Text = "User role updated successfully.";
                    MsgBox.Icon = MessageDialogIcon.Information;
                    MsgBox.Buttons = MessageDialogButtons.OK;
                    MsgBox.Show();
                } else
                {
                    MsgBox.Caption = "Error";
                    MsgBox.Text = "Failed to update user role.";
                    MsgBox.Icon = MessageDialogIcon.Error;
                    MsgBox.Buttons = MessageDialogButtons.OK;
                    MsgBox.Show();
                    combo_box.SelectedItem = char.ToUpper(user.Role[0]) + user.Role.Substring(1);
                }
            }
        }

        private void UserListPanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            User user = null;

            if (sender is Guna2GradientPanel panel) user = panel.Tag as User;
            else if (sender is Control control && control.Parent is Guna2GradientPanel parentPanel) user = parentPanel.Tag as User;

            if (user == null) return;

            if (user.UserId == currentUser.UserId)
            {
                MsgBox.Caption = "Permission Denied";
                MsgBox.Text = "You can't delete your own account";
                MsgBox.Icon = MessageDialogIcon.Warning;
                MsgBox.Buttons = MessageDialogButtons.OK;
                MsgBox.Show();
                return;
            }

            MsgBox.Caption = "Confirm User Deletion";
            MsgBox.Text = $"Are you sure you want to delete the user '{user.Username}'?\n\nThis action cannot be undone!";
            MsgBox.Icon = MessageDialogIcon.Question;
            MsgBox.Buttons = MessageDialogButtons.YesNo;
            DialogResult result = MsgBox.Show();

            if (result == DialogResult.Yes)
            {
                if (UserHandle.DeleteUser(user.UserId))
                {
                    MsgBox.Caption = "Success";
                    MsgBox.Text = $"User '{user.Username}' has been delted!";
                    MsgBox.Icon = MessageDialogIcon.Information;
                    MsgBox.Buttons = MessageDialogButtons.OK;
                    MsgBox.Show();
                    LoadAllUsers();
                }
                else
                {
                    MsgBox.Caption = "Error";
                    MsgBox.Text = "Failed to delete user.";
                    MsgBox.Icon = MessageDialogIcon.Error;
                    MsgBox.Buttons = MessageDialogButtons.OK;
                    MsgBox.Show();
                }
            }
        }
    }
}
