namespace JellyFlix_MediaHub.UI
{
    partial class UserProfile
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Elipse = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.TabBar = new Guna.UI2.WinForms.Guna2ContainerControl();
            this.MinimizeApp = new Guna.UI2.WinForms.Guna2ControlBox();
            this.MaximizeApp = new Guna.UI2.WinForms.Guna2ControlBox();
            this.CloseApp = new Guna.UI2.WinForms.Guna2ControlBox();
            this.BackButton = new Guna.UI2.WinForms.Guna2ImageButton();
            this.AvatarBox = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.InsidePanel = new Guna.UI2.WinForms.Guna2Panel();
            this.LogoutButton = new Guna.UI2.WinForms.Guna2Button();
            this.PositionLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.ApplyButton = new Guna.UI2.WinForms.Guna2Button();
            this.ConfirmPassErrorMsg = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.PasswordErrorMsg = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.EmailErrorMsg = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.UsernameErrorMsg = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.ConfirmPasswordTextBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.ConfirmPassLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.PasswordTextBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.ChangePasswordLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.EmailTextBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.EmailLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.UsernameTextBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.UsernameLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.InsideElipse = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.TabDragControl = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.ProfilePageTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.usernameText = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.emailText = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.TabBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AvatarBox)).BeginInit();
            this.InsidePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Elipse
            // 
            this.Elipse.BorderRadius = 20;
            this.Elipse.TargetControl = this;
            // 
            // TabBar
            // 
            this.TabBar.BackColor = System.Drawing.Color.Transparent;
            this.TabBar.Controls.Add(this.MinimizeApp);
            this.TabBar.Controls.Add(this.MaximizeApp);
            this.TabBar.Controls.Add(this.CloseApp);
            this.TabBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.TabBar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(23)))), ((int)(((byte)(53)))));
            this.TabBar.Location = new System.Drawing.Point(0, 0);
            this.TabBar.Name = "TabBar";
            this.TabBar.Size = new System.Drawing.Size(1280, 29);
            this.TabBar.TabIndex = 2;
            // 
            // MinimizeApp
            // 
            this.MinimizeApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MinimizeApp.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            this.MinimizeApp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MinimizeApp.FillColor = System.Drawing.Color.Transparent;
            this.MinimizeApp.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(255)))));
            this.MinimizeApp.HoverState.IconColor = System.Drawing.Color.White;
            this.MinimizeApp.IconColor = System.Drawing.Color.White;
            this.MinimizeApp.Location = new System.Drawing.Point(1165, 0);
            this.MinimizeApp.Name = "MinimizeApp";
            this.MinimizeApp.Size = new System.Drawing.Size(39, 29);
            this.MinimizeApp.TabIndex = 1;
            // 
            // MaximizeApp
            // 
            this.MaximizeApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MaximizeApp.Animated = true;
            this.MaximizeApp.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MaximizeBox;
            this.MaximizeApp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MaximizeApp.FillColor = System.Drawing.Color.Transparent;
            this.MaximizeApp.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(255)))));
            this.MaximizeApp.HoverState.IconColor = System.Drawing.Color.White;
            this.MaximizeApp.IconColor = System.Drawing.Color.White;
            this.MaximizeApp.Location = new System.Drawing.Point(1204, 0);
            this.MaximizeApp.Name = "MaximizeApp";
            this.MaximizeApp.Size = new System.Drawing.Size(39, 29);
            this.MaximizeApp.TabIndex = 1;
            // 
            // CloseApp
            // 
            this.CloseApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseApp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CloseApp.FillColor = System.Drawing.Color.Transparent;
            this.CloseApp.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(28)))), ((int)(((byte)(17)))));
            this.CloseApp.HoverState.IconColor = System.Drawing.Color.White;
            this.CloseApp.IconColor = System.Drawing.Color.White;
            this.CloseApp.Location = new System.Drawing.Point(1243, 0);
            this.CloseApp.Name = "CloseApp";
            this.CloseApp.Size = new System.Drawing.Size(39, 29);
            this.CloseApp.TabIndex = 1;
            // 
            // BackButton
            // 
            this.BackButton.BackColor = System.Drawing.Color.Transparent;
            this.BackButton.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.BackButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BackButton.HoverState.ImageSize = new System.Drawing.Size(16, 16);
            this.BackButton.Image = global::JellyFlix_MediaHub.Properties.Resources.arrow_left_solid;
            this.BackButton.ImageOffset = new System.Drawing.Point(0, 0);
            this.BackButton.ImageRotate = 0F;
            this.BackButton.ImageSize = new System.Drawing.Size(16, 16);
            this.BackButton.Location = new System.Drawing.Point(29, 43);
            this.BackButton.Name = "BackButton";
            this.BackButton.PressedState.ImageSize = new System.Drawing.Size(16, 16);
            this.BackButton.Size = new System.Drawing.Size(36, 36);
            this.BackButton.TabIndex = 7;
            this.BackButton.UseTransparentBackground = true;
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // AvatarBox
            // 
            this.AvatarBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.AvatarBox.BackColor = System.Drawing.Color.Transparent;
            this.AvatarBox.FillColor = System.Drawing.Color.Transparent;
            this.AvatarBox.Image = global::JellyFlix_MediaHub.Properties.Resources.user_tie_solid;
            this.AvatarBox.ImageRotate = 0F;
            this.AvatarBox.Location = new System.Drawing.Point(454, 15);
            this.AvatarBox.Name = "AvatarBox";
            this.AvatarBox.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.AvatarBox.Size = new System.Drawing.Size(145, 131);
            this.AvatarBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.AvatarBox.TabIndex = 8;
            this.AvatarBox.TabStop = false;
            // 
            // InsidePanel
            // 
            this.InsidePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InsidePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(43)))), ((int)(((byte)(104)))));
            this.InsidePanel.Controls.Add(this.emailText);
            this.InsidePanel.Controls.Add(this.usernameText);
            this.InsidePanel.Controls.Add(this.LogoutButton);
            this.InsidePanel.Controls.Add(this.PositionLabel);
            this.InsidePanel.Controls.Add(this.ApplyButton);
            this.InsidePanel.Controls.Add(this.ConfirmPassErrorMsg);
            this.InsidePanel.Controls.Add(this.PasswordErrorMsg);
            this.InsidePanel.Controls.Add(this.EmailErrorMsg);
            this.InsidePanel.Controls.Add(this.UsernameErrorMsg);
            this.InsidePanel.Controls.Add(this.ConfirmPasswordTextBox);
            this.InsidePanel.Controls.Add(this.ConfirmPassLabel);
            this.InsidePanel.Controls.Add(this.PasswordTextBox);
            this.InsidePanel.Controls.Add(this.ChangePasswordLabel);
            this.InsidePanel.Controls.Add(this.EmailTextBox);
            this.InsidePanel.Controls.Add(this.EmailLabel);
            this.InsidePanel.Controls.Add(this.UsernameTextBox);
            this.InsidePanel.Controls.Add(this.UsernameLabel);
            this.InsidePanel.Controls.Add(this.AvatarBox);
            this.InsidePanel.Location = new System.Drawing.Point(113, 115);
            this.InsidePanel.MinimumSize = new System.Drawing.Size(868, 563);
            this.InsidePanel.Name = "InsidePanel";
            this.InsidePanel.ShadowDecoration.BorderRadius = 15;
            this.InsidePanel.ShadowDecoration.Depth = 20;
            this.InsidePanel.ShadowDecoration.Enabled = true;
            this.InsidePanel.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.InsidePanel.Size = new System.Drawing.Size(1057, 563);
            this.InsidePanel.TabIndex = 9;
            // 
            // LogoutButton
            // 
            this.LogoutButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LogoutButton.Animated = true;
            this.LogoutButton.AnimatedGIF = true;
            this.LogoutButton.BackColor = System.Drawing.Color.Transparent;
            this.LogoutButton.BorderRadius = 15;
            this.LogoutButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LogoutButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.LogoutButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.LogoutButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.LogoutButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.LogoutButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LogoutButton.Font = new System.Drawing.Font("Comic Sans MS", 14.25F);
            this.LogoutButton.ForeColor = System.Drawing.Color.White;
            this.LogoutButton.Location = new System.Drawing.Point(43, 484);
            this.LogoutButton.Name = "LogoutButton";
            this.LogoutButton.ShadowDecoration.BorderRadius = 20;
            this.LogoutButton.Size = new System.Drawing.Size(168, 51);
            this.LogoutButton.TabIndex = 23;
            this.LogoutButton.Text = "Logout";
            this.LogoutButton.Click += new System.EventHandler(this.LogoutButton_Click);
            // 
            // PositionLabel
            // 
            this.PositionLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.PositionLabel.BackColor = System.Drawing.Color.Transparent;
            this.PositionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PositionLabel.ForeColor = System.Drawing.SystemColors.Menu;
            this.PositionLabel.Location = new System.Drawing.Point(497, 170);
            this.PositionLabel.Name = "PositionLabel";
            this.PositionLabel.Size = new System.Drawing.Size(59, 22);
            this.PositionLabel.TabIndex = 22;
            this.PositionLabel.Text = "Position";
            // 
            // ApplyButton
            // 
            this.ApplyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ApplyButton.Animated = true;
            this.ApplyButton.AnimatedGIF = true;
            this.ApplyButton.BackColor = System.Drawing.Color.Transparent;
            this.ApplyButton.BorderRadius = 15;
            this.ApplyButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ApplyButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.ApplyButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.ApplyButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.ApplyButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.ApplyButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(116)))), ((int)(((byte)(195)))));
            this.ApplyButton.Font = new System.Drawing.Font("Comic Sans MS", 14.25F);
            this.ApplyButton.ForeColor = System.Drawing.Color.White;
            this.ApplyButton.Location = new System.Drawing.Point(857, 484);
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.Size = new System.Drawing.Size(168, 51);
            this.ApplyButton.TabIndex = 21;
            this.ApplyButton.Text = "Apply";
            this.ApplyButton.Click += new System.EventHandler(this.ApplyButton_Click);
            // 
            // ConfirmPassErrorMsg
            // 
            this.ConfirmPassErrorMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfirmPassErrorMsg.BackColor = System.Drawing.Color.Transparent;
            this.ConfirmPassErrorMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfirmPassErrorMsg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(40)))), ((int)(((byte)(39)))));
            this.ConfirmPassErrorMsg.Location = new System.Drawing.Point(614, 424);
            this.ConfirmPassErrorMsg.Name = "ConfirmPassErrorMsg";
            this.ConfirmPassErrorMsg.Size = new System.Drawing.Size(264, 17);
            this.ConfirmPassErrorMsg.TabIndex = 17;
            this.ConfirmPassErrorMsg.Text = "*Password did not match with confirm password";
            this.ConfirmPassErrorMsg.Visible = false;
            // 
            // PasswordErrorMsg
            // 
            this.PasswordErrorMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PasswordErrorMsg.BackColor = System.Drawing.Color.Transparent;
            this.PasswordErrorMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordErrorMsg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(40)))), ((int)(((byte)(39)))));
            this.PasswordErrorMsg.Location = new System.Drawing.Point(614, 308);
            this.PasswordErrorMsg.Name = "PasswordErrorMsg";
            this.PasswordErrorMsg.Size = new System.Drawing.Size(242, 17);
            this.PasswordErrorMsg.TabIndex = 18;
            this.PasswordErrorMsg.Text = "*Password must be greater than 4 character";
            this.PasswordErrorMsg.Visible = false;
            // 
            // EmailErrorMsg
            // 
            this.EmailErrorMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.EmailErrorMsg.BackColor = System.Drawing.Color.Transparent;
            this.EmailErrorMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmailErrorMsg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(40)))), ((int)(((byte)(39)))));
            this.EmailErrorMsg.Location = new System.Drawing.Point(88, 424);
            this.EmailErrorMsg.Name = "EmailErrorMsg";
            this.EmailErrorMsg.Size = new System.Drawing.Size(168, 17);
            this.EmailErrorMsg.TabIndex = 19;
            this.EmailErrorMsg.Text = "*Valid email address required.";
            this.EmailErrorMsg.Visible = false;
            // 
            // UsernameErrorMsg
            // 
            this.UsernameErrorMsg.BackColor = System.Drawing.Color.Transparent;
            this.UsernameErrorMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsernameErrorMsg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(40)))), ((int)(((byte)(39)))));
            this.UsernameErrorMsg.Location = new System.Drawing.Point(88, 308);
            this.UsernameErrorMsg.Name = "UsernameErrorMsg";
            this.UsernameErrorMsg.Size = new System.Drawing.Size(231, 17);
            this.UsernameErrorMsg.TabIndex = 20;
            this.UsernameErrorMsg.Text = "*Username already exist. Must be unique.";
            this.UsernameErrorMsg.Visible = false;
            // 
            // ConfirmPasswordTextBox
            // 
            this.ConfirmPasswordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfirmPasswordTextBox.BorderRadius = 12;
            this.ConfirmPasswordTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ConfirmPasswordTextBox.DefaultText = "";
            this.ConfirmPasswordTextBox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.ConfirmPasswordTextBox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.ConfirmPasswordTextBox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.ConfirmPasswordTextBox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.ConfirmPasswordTextBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.ConfirmPasswordTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ConfirmPasswordTextBox.ForeColor = System.Drawing.Color.Black;
            this.ConfirmPasswordTextBox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.ConfirmPasswordTextBox.Location = new System.Drawing.Point(614, 382);
            this.ConfirmPasswordTextBox.Name = "ConfirmPasswordTextBox";
            this.ConfirmPasswordTextBox.PlaceholderText = "Same as password";
            this.ConfirmPasswordTextBox.SelectedText = "";
            this.ConfirmPasswordTextBox.Size = new System.Drawing.Size(348, 36);
            this.ConfirmPasswordTextBox.TabIndex = 13;
            this.ConfirmPasswordTextBox.TextChanged += new System.EventHandler(this.ConfirmPasswordTextBox_TextChanged);
            // 
            // ConfirmPassLabel
            // 
            this.ConfirmPassLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfirmPassLabel.BackColor = System.Drawing.Color.Transparent;
            this.ConfirmPassLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfirmPassLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(204)))), ((int)(((byte)(220)))));
            this.ConfirmPassLabel.IsSelectionEnabled = false;
            this.ConfirmPassLabel.Location = new System.Drawing.Point(614, 347);
            this.ConfirmPassLabel.Name = "ConfirmPassLabel";
            this.ConfirmPassLabel.Size = new System.Drawing.Size(131, 22);
            this.ConfirmPassLabel.TabIndex = 9;
            this.ConfirmPassLabel.Text = "Confirm Password";
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PasswordTextBox.BorderRadius = 12;
            this.PasswordTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.PasswordTextBox.DefaultText = "";
            this.PasswordTextBox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.PasswordTextBox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.PasswordTextBox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.PasswordTextBox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.PasswordTextBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.PasswordTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.PasswordTextBox.ForeColor = System.Drawing.Color.Black;
            this.PasswordTextBox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.PasswordTextBox.Location = new System.Drawing.Point(614, 268);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.PlaceholderText = "Greater than 4 character";
            this.PasswordTextBox.SelectedText = "";
            this.PasswordTextBox.Size = new System.Drawing.Size(348, 36);
            this.PasswordTextBox.TabIndex = 14;
            this.PasswordTextBox.TextChanged += new System.EventHandler(this.PasswordTextBox_TextChanged);
            // 
            // ChangePasswordLabel
            // 
            this.ChangePasswordLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ChangePasswordLabel.BackColor = System.Drawing.Color.Transparent;
            this.ChangePasswordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChangePasswordLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(204)))), ((int)(((byte)(220)))));
            this.ChangePasswordLabel.IsSelectionEnabled = false;
            this.ChangePasswordLabel.Location = new System.Drawing.Point(614, 233);
            this.ChangePasswordLabel.Name = "ChangePasswordLabel";
            this.ChangePasswordLabel.Size = new System.Drawing.Size(132, 22);
            this.ChangePasswordLabel.TabIndex = 10;
            this.ChangePasswordLabel.Text = "Change Password";
            // 
            // EmailTextBox
            // 
            this.EmailTextBox.AcceptsReturn = true;
            this.EmailTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.EmailTextBox.BorderRadius = 12;
            this.EmailTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.EmailTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.EmailTextBox.DefaultText = "";
            this.EmailTextBox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.EmailTextBox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.EmailTextBox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.EmailTextBox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.EmailTextBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.EmailTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.EmailTextBox.ForeColor = System.Drawing.Color.Black;
            this.EmailTextBox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.EmailTextBox.Location = new System.Drawing.Point(88, 382);
            this.EmailTextBox.Name = "EmailTextBox";
            this.EmailTextBox.PlaceholderText = "";
            this.EmailTextBox.SelectedText = "";
            this.EmailTextBox.Size = new System.Drawing.Size(346, 36);
            this.EmailTextBox.TabIndex = 15;
            this.EmailTextBox.Visible = false;
            this.EmailTextBox.TextChanged += new System.EventHandler(this.EmailTextBox_TextChanged);
            this.EmailTextBox.LostFocus += new System.EventHandler(this.EmailTextBox_LostFocus);
            // 
            // EmailLabel
            // 
            this.EmailLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.EmailLabel.BackColor = System.Drawing.Color.Transparent;
            this.EmailLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmailLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(204)))), ((int)(((byte)(220)))));
            this.EmailLabel.IsSelectionEnabled = false;
            this.EmailLabel.Location = new System.Drawing.Point(88, 347);
            this.EmailLabel.Name = "EmailLabel";
            this.EmailLabel.Size = new System.Drawing.Size(105, 22);
            this.EmailLabel.TabIndex = 11;
            this.EmailLabel.Text = "Email Address";
            // 
            // UsernameTextBox
            // 
            this.UsernameTextBox.AcceptsReturn = true;
            this.UsernameTextBox.BorderRadius = 12;
            this.UsernameTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.UsernameTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.UsernameTextBox.DefaultText = "";
            this.UsernameTextBox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.UsernameTextBox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.UsernameTextBox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.UsernameTextBox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.UsernameTextBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.UsernameTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.UsernameTextBox.ForeColor = System.Drawing.Color.Black;
            this.UsernameTextBox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.UsernameTextBox.Location = new System.Drawing.Point(88, 268);
            this.UsernameTextBox.Name = "UsernameTextBox";
            this.UsernameTextBox.PlaceholderText = "";
            this.UsernameTextBox.SelectedText = "";
            this.UsernameTextBox.Size = new System.Drawing.Size(346, 36);
            this.UsernameTextBox.TabIndex = 16;
            this.UsernameTextBox.Visible = false;
            this.UsernameTextBox.TextChanged += new System.EventHandler(this.UsernameTextBox_TextChanged);
            this.UsernameTextBox.LostFocus += new System.EventHandler(this.UsernameTextBox_LostFocus);
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.BackColor = System.Drawing.Color.Transparent;
            this.UsernameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsernameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(204)))), ((int)(((byte)(220)))));
            this.UsernameLabel.IsSelectionEnabled = false;
            this.UsernameLabel.Location = new System.Drawing.Point(88, 233);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(77, 22);
            this.UsernameLabel.TabIndex = 12;
            this.UsernameLabel.Text = "Username";
            // 
            // InsideElipse
            // 
            this.InsideElipse.BorderRadius = 25;
            this.InsideElipse.TargetControl = this.InsidePanel;
            // 
            // TabDragControl
            // 
            this.TabDragControl.DockIndicatorColor = System.Drawing.Color.Transparent;
            this.TabDragControl.DockIndicatorTransparencyValue = 0.6D;
            this.TabDragControl.TargetControl = this.TabBar;
            this.TabDragControl.UseTransparentDrag = true;
            // 
            // ProfilePageTitle
            // 
            this.ProfilePageTitle.BackColor = System.Drawing.Color.Transparent;
            this.ProfilePageTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProfilePageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(183)))), ((int)(((byte)(196)))));
            this.ProfilePageTitle.IsSelectionEnabled = false;
            this.ProfilePageTitle.Location = new System.Drawing.Point(90, 44);
            this.ProfilePageTitle.Name = "ProfilePageTitle";
            this.ProfilePageTitle.Size = new System.Drawing.Size(80, 33);
            this.ProfilePageTitle.TabIndex = 10;
            this.ProfilePageTitle.Text = "Profile";
            // 
            // usernameText
            // 
            this.usernameText.BackColor = System.Drawing.Color.Transparent;
            this.usernameText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(204)))), ((int)(((byte)(220)))));
            this.usernameText.IsSelectionEnabled = false;
            this.usernameText.Location = new System.Drawing.Point(91, 275);
            this.usernameText.Name = "usernameText";
            this.usernameText.Size = new System.Drawing.Size(77, 22);
            this.usernameText.TabIndex = 24;
            this.usernameText.Text = "Username";
            this.usernameText.Click += new System.EventHandler(this.usernameText_Click);
            // 
            // emailText
            // 
            this.emailText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.emailText.BackColor = System.Drawing.Color.Transparent;
            this.emailText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emailText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(204)))), ((int)(((byte)(220)))));
            this.emailText.IsSelectionEnabled = false;
            this.emailText.Location = new System.Drawing.Point(91, 389);
            this.emailText.Name = "emailText";
            this.emailText.Size = new System.Drawing.Size(105, 22);
            this.emailText.TabIndex = 25;
            this.emailText.Text = "Email Address";
            this.emailText.Click += new System.EventHandler(this.emailText_Click);
            // 
            // UserProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(33)))), ((int)(((byte)(63)))));
            this.ClientSize = new System.Drawing.Size(1280, 732);
            this.Controls.Add(this.ProfilePageTitle);
            this.Controls.Add(this.InsidePanel);
            this.Controls.Add(this.BackButton);
            this.Controls.Add(this.TabBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(1280, 720);
            this.Name = "UserProfile";
            this.Text = "UserProfile";
            this.Load += new System.EventHandler(this.UserProfile_Load);
            this.TabBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AvatarBox)).EndInit();
            this.InsidePanel.ResumeLayout(false);
            this.InsidePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse Elipse;
        private Guna.UI2.WinForms.Guna2ContainerControl TabBar;
        private Guna.UI2.WinForms.Guna2ControlBox MinimizeApp;
        private Guna.UI2.WinForms.Guna2ControlBox MaximizeApp;
        private Guna.UI2.WinForms.Guna2ControlBox CloseApp;
        private Guna.UI2.WinForms.Guna2ImageButton BackButton;
        private Guna.UI2.WinForms.Guna2Panel InsidePanel;
        private Guna.UI2.WinForms.Guna2CirclePictureBox AvatarBox;
        private Guna.UI2.WinForms.Guna2HtmlLabel ConfirmPassErrorMsg;
        private Guna.UI2.WinForms.Guna2HtmlLabel PasswordErrorMsg;
        private Guna.UI2.WinForms.Guna2HtmlLabel EmailErrorMsg;
        private Guna.UI2.WinForms.Guna2HtmlLabel UsernameErrorMsg;
        private Guna.UI2.WinForms.Guna2TextBox ConfirmPasswordTextBox;
        private Guna.UI2.WinForms.Guna2HtmlLabel ConfirmPassLabel;
        private Guna.UI2.WinForms.Guna2TextBox PasswordTextBox;
        private Guna.UI2.WinForms.Guna2HtmlLabel ChangePasswordLabel;
        private Guna.UI2.WinForms.Guna2TextBox EmailTextBox;
        private Guna.UI2.WinForms.Guna2HtmlLabel EmailLabel;
        private Guna.UI2.WinForms.Guna2TextBox UsernameTextBox;
        private Guna.UI2.WinForms.Guna2HtmlLabel UsernameLabel;
        private Guna.UI2.WinForms.Guna2Button ApplyButton;
        private Guna.UI2.WinForms.Guna2Elipse InsideElipse;
        private Guna.UI2.WinForms.Guna2DragControl TabDragControl;
        private Guna.UI2.WinForms.Guna2HtmlLabel ProfilePageTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel PositionLabel;
        private Guna.UI2.WinForms.Guna2Button LogoutButton;
        private Guna.UI2.WinForms.Guna2HtmlLabel usernameText;
        private Guna.UI2.WinForms.Guna2HtmlLabel emailText;
    }
}