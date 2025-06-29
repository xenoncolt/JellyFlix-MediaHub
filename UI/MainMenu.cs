using Guna.UI2.WinForms;
using JellyFlix_MediaHub.Data.Handlers;
using JellyFlix_MediaHub.Models;
using JellyFlix_MediaHub.Services;
using JellyFlix_MediaHub.Services.Prowlarr;
using JellyFlix_MediaHub.Services.TMDB;
using JellyFlix_MediaHub.Utils;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

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
            LoadApiConfig();

            if (currentUser.Role == "admin" && !AreApiKeysConfigured())
            {
                MsgBox.Caption = "API Configuration Required";
                MsgBox.Text = "Please configure the TMDB and Prowlarr API keys in the Server Config section to enable full functionality...";
                MsgBox.Icon = MessageDialogIcon.Information;
                MsgBox.Buttons = MessageDialogButtons.OK;

                if (MsgBox.Show() == DialogResult.OK)
                {
                    NavMenu.SelectedTab = SettingsTab;
                    SettingsMenu.SelectedTab = ServerSettingsPage;
                }
            }
        }

        private bool AreApiKeysConfigured()
        {
            var config = Utils.ConfigManager.LoadConfig();
            return !string.IsNullOrEmpty(config.TmdbApiKey) &&
                   !string.IsNullOrEmpty(config.ProwlarrApiKey) &&
                   !string.IsNullOrEmpty(config.ProwlarrBaseUrl);
        }

        private void LoadApiConfig()
        {
            var config = ConfigManager.LoadConfig();

            TMDBApiTextBox.Text = config.TmdbApiKey ?? "";
            ProwlarrApiTextBox.Text = config.ProwlarrApiKey ?? "";
            ProwlarrUrlTextBox.Text = config.ProwlarrBaseUrl ?? "";
            SMTPHostTextBox.Text = config.SmtpHost ?? "";
            SMTPPortTextBox.Text = config.SmtpPort?.ToString() ?? "";
            SMTPUsernameTextBox.Text = config.SmtpUsername ?? "";
            SMTPPassTextBox.Text = config.SmtpPassword ?? "";
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

        private void ProwlarrUrlTextBox_Leave(object sender, EventArgs e)
        {
            string url = ProwlarrUrlTextBox.Text.Trim();

            try
            {
                Uri uri = new Uri(url);
                ProwlarrUrlErrorMsg.Visible = false;
            }
            catch (UriFormatException)
            {
                ProwlarrUrlErrorMsg.Text = "Invalid URL format";
                ProwlarrUrlErrorMsg.ForeColor = Color.Red;
                ProwlarrUrlErrorMsg.Visible = true;
            }
        }

        private async void TMDBApiTextBox_Leave(object sender, EventArgs e)
        {
            string api_key = TMDBApiTextBox.Text.Trim();
            if (string.IsNullOrEmpty(api_key)) return;

            TMDBErrorMsg.Text = "Checking API Key...";
            TMDBErrorMsg.Visible = true;
            TMDBErrorMsg.ForeColor = Color.Yellow;

            if (await TMDBValidator.ValidApiKey(api_key))
            {
                TMDBErrorMsg.Text = "Connection Successful";
                TMDBErrorMsg.ForeColor = Color.Green;
            }
            else
            {
                TMDBErrorMsg.Text = "Invalid API key";
                TMDBErrorMsg.ForeColor = Color.Red;
            }
        }

        private async void ProwlarrApiTextBox_Leave(object sender, EventArgs e)
        {
            string api_key = ProwlarrApiTextBox.Text.Trim();
            string base_url = ProwlarrUrlTextBox.Text.Trim();

            if (string.IsNullOrEmpty(base_url))
            {
                ProwlarrErrorMsg.Text = "Please enter Prowlarr URL first";
                ProwlarrErrorMsg.ForeColor = Color.Red;
                ProwlarrErrorMsg.Visible = true;
                return;
            }

            ProwlarrErrorMsg.Text = "Checking API Key...";
            ProwlarrErrorMsg.Visible = true;
            ProwlarrErrorMsg.ForeColor = Color.Yellow;

            if (await ProwlarrValidation.ValidateApiKey(base_url, api_key))
            {
                ProwlarrErrorMsg.Text = "Connection successful";
                ProwlarrErrorMsg.ForeColor = Color.Green;
            }
            else
            {
                ProwlarrErrorMsg.Text = "Invalid API key or URL";
                ProwlarrErrorMsg.ForeColor = Color.Red;
            }
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            int? smtp_port = null;
            if (!string.IsNullOrEmpty(SMTPPortTextBox.Text) && int.TryParse(SMTPPortTextBox.Text, out int port))
            {
                smtp_port = port;
            }
            var config = new AppConfig
            {
                TmdbApiKey = TMDBApiTextBox.Text.Trim(),
                ProwlarrApiKey = ProwlarrApiTextBox.Text.Trim(),
                ProwlarrBaseUrl = ProwlarrUrlTextBox.Text.Trim(),
                SmtpHost = SMTPHostTextBox.Text.Trim(),
                SmtpPort = smtp_port,
                SmtpUsername = SMTPUsernameTextBox.Text.Trim(),
                SmtpPassword = SMTPPassTextBox.Text.Trim()
            };

            bool hasSmtpField = !string.IsNullOrEmpty(config.SmtpHost) ||
                        !string.IsNullOrEmpty(config.SmtpUsername) ||
                        !string.IsNullOrEmpty(config.SmtpPassword) ||
                        config.SmtpPort.HasValue;

            bool allSmtpFieldsFilled = !string.IsNullOrEmpty(config.SmtpHost) &&
                                       !string.IsNullOrEmpty(config.SmtpUsername) &&
                                       !string.IsNullOrEmpty(config.SmtpPassword) &&
                                       config.SmtpPort.HasValue;

            if (hasSmtpField && !allSmtpFieldsFilled)
            {
                SMTPHostErrorMsg.Visible = true;
                SMTPPassErrorMsg.Visible = true;
                SMTPUsernameErrorMsg.Visible = true;
                SMTPPortErrorMsg.Visible = true;
                SMTPEmailErrorMsg.Visible = true;
            }

            if (String.IsNullOrEmpty(config.TmdbApiKey)) return;
            if (String.IsNullOrEmpty(config.ProwlarrApiKey) && String.IsNullOrEmpty(config.ProwlarrBaseUrl)) return;

            if (ConfigManager.SaveConfig(config))
            {
                MsgBox.Caption = "Success";
                MsgBox.Text = "API configuration saved successfully.";
                MsgBox.Icon = MessageDialogIcon.Information;
                MsgBox.Buttons = MessageDialogButtons.OK;
                MsgBox.Show();
            } else
            {
                MsgBox.Caption = "Error";
                MsgBox.Text = "Failed to save API configuration.";
                MsgBox.Icon = MessageDialogIcon.Error;
                MsgBox.Buttons = MessageDialogButtons.OK;
                MsgBox.Show();
            }
        }

        private void InviteEmailTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(InviteEmailTextBox.Text, @"^[^@\s]+@gmail\.com$"))
            {
                InviteEmailErrorMsg.Visible = true;
            }
            else
            {
                InviteEmailErrorMsg.Visible = false;
            }
        }

        private async void CheckMarkBox_Click(object sender, EventArgs e)
        {
            var config = ConfigManager.LoadConfig();
            if (!config.SmtpConfigured)
            {
                MsgBox.Caption = "SMTP Not Configured";
                MsgBox.Text = "Please configure SMTP settings in the Server Config section first or Ask an Admin to Setup first.";
                MsgBox.Icon = MessageDialogIcon.Warning;
                MsgBox.Buttons = MessageDialogButtons.OK;
                MsgBox.Show();
                return;
            }

            string invite_code = SMTPService.GenerateInviteCode();

            CheckMarkBox.Enabled = false;
            InviteEmailTextBox.Enabled = false;
            CheckMarkBox.Cursor = Cursors.WaitCursor;

            try
            {
                SMTPService smtp_service = new SMTPService();

                if (await smtp_service.SendInvitationEmail(InviteEmailTextBox.Text.Trim(), invite_code))
                {
                    CheckMarkBox.Image = Properties.Resources.green_check_solid;
                    Timer green_timer = new Timer();
                    green_timer.Interval = 60 * 1000;
                    green_timer.Tick += (s, args) =>
                    {
                        CheckMarkBox.Image = Properties.Resources.check_solid;
                    };
                    green_timer.Start();
                    InviteEmailTextBox.Clear();
                }
                else
                {
                    MsgBox.Caption = "Error";
                    MsgBox.Text = "Failed to send invitation email. Please check your SMTP settings and try again.";
                    MsgBox.Icon = MessageDialogIcon.Error;
                    MsgBox.Buttons = MessageDialogButtons.OK;
                    MsgBox.Show();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending invitation email: {ex.Message}");
            }
            finally
            {
                CheckMarkBox.Enabled = true;
                InviteEmailTextBox.Enabled = true;
            }
        }
    }
}
