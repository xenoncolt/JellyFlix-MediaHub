namespace JellyFlix_MediaHub.UI
{
    partial class Login
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
            this.DragControl = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.ClosedApp = new Guna.UI2.WinForms.Guna2ControlBox();
            this.MinimizeApp = new Guna.UI2.WinForms.Guna2ControlBox();
            this.LoginButton = new Guna.UI2.WinForms.Guna2GradientButton();
            this.SignupButton = new Guna.UI2.WinForms.Guna2GradientButton();
            this.materialTextBox1 = new MaterialSkin.Controls.MaterialTextBox();
            this.UsernameTextBox = new MaterialSkin.Controls.MaterialTextBox2();
            this.WrittingPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.AvatarBox = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.PanelElipse = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.WrittingPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AvatarBox)).BeginInit();
            this.SuspendLayout();
            // 
            // Elipse
            // 
            this.Elipse.BorderRadius = 30;
            this.Elipse.TargetControl = this;
            // 
            // DragControl
            // 
            this.DragControl.DockIndicatorColor = System.Drawing.Color.Transparent;
            this.DragControl.DockIndicatorTransparencyValue = 0.6D;
            this.DragControl.TargetControl = this;
            this.DragControl.UseTransparentDrag = true;
            // 
            // ClosedApp
            // 
            this.ClosedApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ClosedApp.Animated = true;
            this.ClosedApp.AutoRoundedCorners = true;
            this.ClosedApp.BackColor = System.Drawing.Color.Transparent;
            this.ClosedApp.FillColor = System.Drawing.Color.Brown;
            this.ClosedApp.HoverState.BorderColor = System.Drawing.Color.Transparent;
            this.ClosedApp.HoverState.FillColor = System.Drawing.Color.Maroon;
            this.ClosedApp.HoverState.IconColor = System.Drawing.Color.Maroon;
            this.ClosedApp.IconColor = System.Drawing.Color.White;
            this.ClosedApp.Location = new System.Drawing.Point(432, 12);
            this.ClosedApp.Name = "ClosedApp";
            this.ClosedApp.PressedColor = System.Drawing.Color.White;
            this.ClosedApp.ShowIcon = false;
            this.ClosedApp.Size = new System.Drawing.Size(20, 20);
            this.ClosedApp.TabIndex = 0;
            this.ClosedApp.UseTransparentBackground = true;
            // 
            // MinimizeApp
            // 
            this.MinimizeApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MinimizeApp.Animated = true;
            this.MinimizeApp.AutoRoundedCorners = true;
            this.MinimizeApp.BackColor = System.Drawing.Color.Transparent;
            this.MinimizeApp.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            this.MinimizeApp.CustomIconSize = 8F;
            this.MinimizeApp.FillColor = System.Drawing.Color.Goldenrod;
            this.MinimizeApp.HoverState.BorderColor = System.Drawing.Color.Transparent;
            this.MinimizeApp.HoverState.FillColor = System.Drawing.Color.DarkGoldenrod;
            this.MinimizeApp.HoverState.IconColor = System.Drawing.Color.Blue;
            this.MinimizeApp.IconColor = System.Drawing.Color.Black;
            this.MinimizeApp.Location = new System.Drawing.Point(399, 12);
            this.MinimizeApp.Name = "MinimizeApp";
            this.MinimizeApp.PressedColor = System.Drawing.Color.DimGray;
            this.MinimizeApp.ShowIcon = false;
            this.MinimizeApp.Size = new System.Drawing.Size(20, 20);
            this.MinimizeApp.TabIndex = 0;
            this.MinimizeApp.UseTransparentBackground = true;
            // 
            // LoginButton
            // 
            this.LoginButton.Animated = true;
            this.LoginButton.AnimatedGIF = true;
            this.LoginButton.BorderRadius = 15;
            this.LoginButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.LoginButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.LoginButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.LoginButton.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.LoginButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.LoginButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(103)))), ((int)(((byte)(237)))));
            this.LoginButton.FillColor2 = System.Drawing.Color.MidnightBlue;
            this.LoginButton.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoginButton.ForeColor = System.Drawing.Color.White;
            this.LoginButton.Location = new System.Drawing.Point(236, 372);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(168, 47);
            this.LoginButton.TabIndex = 3;
            this.LoginButton.Text = "Login";
            // 
            // SignupButton
            // 
            this.SignupButton.Animated = true;
            this.SignupButton.AnimatedGIF = true;
            this.SignupButton.BorderRadius = 15;
            this.SignupButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.SignupButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.SignupButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.SignupButton.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.SignupButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.SignupButton.FillColor = System.Drawing.Color.MidnightBlue;
            this.SignupButton.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(103)))), ((int)(((byte)(237)))));
            this.SignupButton.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SignupButton.ForeColor = System.Drawing.Color.White;
            this.SignupButton.Location = new System.Drawing.Point(31, 372);
            this.SignupButton.Name = "SignupButton";
            this.SignupButton.Size = new System.Drawing.Size(168, 47);
            this.SignupButton.TabIndex = 3;
            this.SignupButton.Text = "Sign Up";
            this.SignupButton.Click += new System.EventHandler(this.SignupButton_Click);
            // 
            // materialTextBox1
            // 
            this.materialTextBox1.AnimateReadOnly = true;
            this.materialTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.materialTextBox1.Depth = 0;
            this.materialTextBox1.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialTextBox1.ForeColor = System.Drawing.Color.Black;
            this.materialTextBox1.Hint = "Password";
            this.materialTextBox1.LeadingIcon = null;
            this.materialTextBox1.LeaveOnEnterKey = true;
            this.materialTextBox1.Location = new System.Drawing.Point(34, 272);
            this.materialTextBox1.MaxLength = 50;
            this.materialTextBox1.MouseState = MaterialSkin.MouseState.OUT;
            this.materialTextBox1.Multiline = false;
            this.materialTextBox1.Name = "materialTextBox1";
            this.materialTextBox1.Password = true;
            this.materialTextBox1.Size = new System.Drawing.Size(373, 50);
            this.materialTextBox1.TabIndex = 2;
            this.materialTextBox1.Text = "";
            this.materialTextBox1.TrailingIcon = null;
            this.materialTextBox1.UseAccent = false;
            // 
            // UsernameTextBox
            // 
            this.UsernameTextBox.AnimateReadOnly = true;
            this.UsernameTextBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.UsernameTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.UsernameTextBox.Depth = 0;
            this.UsernameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.UsernameTextBox.HideSelection = true;
            this.UsernameTextBox.Hint = "Username";
            this.UsernameTextBox.LeadingIcon = null;
            this.UsernameTextBox.LeaveOnEnterKey = true;
            this.UsernameTextBox.Location = new System.Drawing.Point(34, 197);
            this.UsernameTextBox.MaxLength = 32767;
            this.UsernameTextBox.MouseState = MaterialSkin.MouseState.OUT;
            this.UsernameTextBox.Name = "UsernameTextBox";
            this.UsernameTextBox.PasswordChar = '\0';
            this.UsernameTextBox.PrefixSuffixText = null;
            this.UsernameTextBox.ReadOnly = false;
            this.UsernameTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.UsernameTextBox.SelectedText = "";
            this.UsernameTextBox.SelectionLength = 0;
            this.UsernameTextBox.SelectionStart = 0;
            this.UsernameTextBox.ShortcutsEnabled = true;
            this.UsernameTextBox.Size = new System.Drawing.Size(373, 48);
            this.UsernameTextBox.TabIndex = 1;
            this.UsernameTextBox.TabStop = false;
            this.UsernameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.UsernameTextBox.TrailingIcon = null;
            this.UsernameTextBox.UseAccent = false;
            this.UsernameTextBox.UseSystemPasswordChar = false;
            // 
            // WrittingPanel
            // 
            this.WrittingPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(198)))), ((int)(((byte)(243)))));
            this.WrittingPanel.BorderRadius = 400;
            this.WrittingPanel.Controls.Add(this.AvatarBox);
            this.WrittingPanel.Controls.Add(this.UsernameTextBox);
            this.WrittingPanel.Controls.Add(this.materialTextBox1);
            this.WrittingPanel.Controls.Add(this.SignupButton);
            this.WrittingPanel.Controls.Add(this.LoginButton);
            this.WrittingPanel.Location = new System.Drawing.Point(12, 136);
            this.WrittingPanel.Name = "WrittingPanel";
            this.WrittingPanel.Size = new System.Drawing.Size(435, 486);
            this.WrittingPanel.TabIndex = 4;
            // 
            // AvatarBox
            // 
            this.AvatarBox.BackColor = System.Drawing.Color.Transparent;
            this.AvatarBox.FillColor = System.Drawing.Color.Transparent;
            this.AvatarBox.Image = global::JellyFlix_MediaHub.Properties.Resources.user_tie_solid;
            this.AvatarBox.ImageRotate = 0F;
            this.AvatarBox.Location = new System.Drawing.Point(158, 36);
            this.AvatarBox.Name = "AvatarBox";
            this.AvatarBox.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.AvatarBox.Size = new System.Drawing.Size(125, 125);
            this.AvatarBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.AvatarBox.TabIndex = 4;
            this.AvatarBox.TabStop = false;
            // 
            // PanelElipse
            // 
            this.PanelElipse.BorderRadius = 100;
            this.PanelElipse.TargetControl = this.WrittingPanel;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.BackgroundImage = global::JellyFlix_MediaHub.Properties.Resources.unknown;
            this.ClientSize = new System.Drawing.Size(464, 742);
            this.Controls.Add(this.ClosedApp);
            this.Controls.Add(this.MinimizeApp);
            this.Controls.Add(this.WrittingPanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Login";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.WrittingPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AvatarBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse Elipse;
        private Guna.UI2.WinForms.Guna2DragControl DragControl;
        private Guna.UI2.WinForms.Guna2ControlBox ClosedApp;
        private Guna.UI2.WinForms.Guna2ControlBox MinimizeApp;
        private Guna.UI2.WinForms.Guna2GradientButton SignupButton;
        private Guna.UI2.WinForms.Guna2GradientButton LoginButton;
        private MaterialSkin.Controls.MaterialTextBox materialTextBox1;
        private MaterialSkin.Controls.MaterialTextBox2 UsernameTextBox;
        private Guna.UI2.WinForms.Guna2Panel WrittingPanel;
        private Guna.UI2.WinForms.Guna2Elipse PanelElipse;
        private Guna.UI2.WinForms.Guna2CirclePictureBox AvatarBox;
    }
}