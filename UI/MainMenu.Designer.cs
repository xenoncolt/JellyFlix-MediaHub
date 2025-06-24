namespace JellyFlix_MediaHub.UI
{
    partial class MainMenu
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
            this.TabBarDrag = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.ResizeForm = new Guna.UI2.WinForms.Guna2ResizeForm(this.components);
            this.MovieTab = new System.Windows.Forms.TabPage();
            this.NavMenu = new Guna.UI2.WinForms.Guna2TabControl();
            this.SeriesTab = new System.Windows.Forms.TabPage();
            this.TrendingTab = new System.Windows.Forms.TabPage();
            this.UsersTab = new System.Windows.Forms.TabPage();
            this.SettingsTab = new System.Windows.Forms.TabPage();
            this.HeadingSection = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.SearchBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.ProfileBox = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.TabBar.SuspendLayout();
            this.NavMenu.SuspendLayout();
            this.HeadingSection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProfileBox)).BeginInit();
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
            this.TabBar.Size = new System.Drawing.Size(1091, 29);
            this.TabBar.TabIndex = 1;
            // 
            // MinimizeApp
            // 
            this.MinimizeApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MinimizeApp.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            this.MinimizeApp.FillColor = System.Drawing.Color.Transparent;
            this.MinimizeApp.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(255)))));
            this.MinimizeApp.HoverState.IconColor = System.Drawing.Color.White;
            this.MinimizeApp.IconColor = System.Drawing.Color.White;
            this.MinimizeApp.Location = new System.Drawing.Point(976, 0);
            this.MinimizeApp.Name = "MinimizeApp";
            this.MinimizeApp.Size = new System.Drawing.Size(39, 29);
            this.MinimizeApp.TabIndex = 1;
            // 
            // MaximizeApp
            // 
            this.MaximizeApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MaximizeApp.Animated = true;
            this.MaximizeApp.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MaximizeBox;
            this.MaximizeApp.FillColor = System.Drawing.Color.Transparent;
            this.MaximizeApp.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(255)))));
            this.MaximizeApp.HoverState.IconColor = System.Drawing.Color.White;
            this.MaximizeApp.IconColor = System.Drawing.Color.White;
            this.MaximizeApp.Location = new System.Drawing.Point(1015, 0);
            this.MaximizeApp.Name = "MaximizeApp";
            this.MaximizeApp.Size = new System.Drawing.Size(39, 29);
            this.MaximizeApp.TabIndex = 1;
            // 
            // CloseApp
            // 
            this.CloseApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseApp.FillColor = System.Drawing.Color.Transparent;
            this.CloseApp.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(28)))), ((int)(((byte)(17)))));
            this.CloseApp.HoverState.IconColor = System.Drawing.Color.White;
            this.CloseApp.IconColor = System.Drawing.Color.White;
            this.CloseApp.Location = new System.Drawing.Point(1054, 0);
            this.CloseApp.Name = "CloseApp";
            this.CloseApp.Size = new System.Drawing.Size(39, 29);
            this.CloseApp.TabIndex = 1;
            // 
            // TabBarDrag
            // 
            this.TabBarDrag.DockIndicatorColor = System.Drawing.Color.Transparent;
            this.TabBarDrag.DockIndicatorTransparencyValue = 0.6D;
            this.TabBarDrag.TargetControl = this.TabBar;
            this.TabBarDrag.UseTransparentDrag = true;
            // 
            // ResizeForm
            // 
            this.ResizeForm.TargetForm = this;
            // 
            // MovieTab
            // 
            this.MovieTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(33)))), ((int)(((byte)(63)))));
            this.MovieTab.Location = new System.Drawing.Point(174, 4);
            this.MovieTab.Name = "MovieTab";
            this.MovieTab.Padding = new System.Windows.Forms.Padding(3);
            this.MovieTab.Size = new System.Drawing.Size(913, 657);
            this.MovieTab.TabIndex = 1;
            this.MovieTab.Text = "Movies";
            // 
            // NavMenu
            // 
            this.NavMenu.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.NavMenu.Controls.Add(this.MovieTab);
            this.NavMenu.Controls.Add(this.SeriesTab);
            this.NavMenu.Controls.Add(this.TrendingTab);
            this.NavMenu.Controls.Add(this.UsersTab);
            this.NavMenu.Controls.Add(this.SettingsTab);
            this.NavMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NavMenu.HotTrack = true;
            this.NavMenu.ImeMode = System.Windows.Forms.ImeMode.AlphaFull;
            this.NavMenu.ItemSize = new System.Drawing.Size(170, 40);
            this.NavMenu.Location = new System.Drawing.Point(0, 67);
            this.NavMenu.Name = "NavMenu";
            this.NavMenu.SelectedIndex = 0;
            this.NavMenu.Size = new System.Drawing.Size(1091, 665);
            this.NavMenu.TabButtonHoverState.BorderColor = System.Drawing.Color.Empty;
            this.NavMenu.TabButtonHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(52)))), ((int)(((byte)(70)))));
            this.NavMenu.TabButtonHoverState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.NavMenu.TabButtonHoverState.ForeColor = System.Drawing.Color.White;
            this.NavMenu.TabButtonHoverState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(52)))), ((int)(((byte)(70)))));
            this.NavMenu.TabButtonIdleState.BorderColor = System.Drawing.Color.Empty;
            this.NavMenu.TabButtonIdleState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.NavMenu.TabButtonIdleState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.NavMenu.TabButtonIdleState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(160)))), ((int)(((byte)(167)))));
            this.NavMenu.TabButtonIdleState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.NavMenu.TabButtonSelectedState.BorderColor = System.Drawing.Color.Empty;
            this.NavMenu.TabButtonSelectedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(37)))), ((int)(((byte)(49)))));
            this.NavMenu.TabButtonSelectedState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.NavMenu.TabButtonSelectedState.ForeColor = System.Drawing.Color.White;
            this.NavMenu.TabButtonSelectedState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(132)))), ((int)(((byte)(255)))));
            this.NavMenu.TabButtonSize = new System.Drawing.Size(170, 40);
            this.NavMenu.TabIndex = 2;
            this.NavMenu.TabMenuBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(33)))), ((int)(((byte)(63)))));
            // 
            // SeriesTab
            // 
            this.SeriesTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(33)))), ((int)(((byte)(63)))));
            this.SeriesTab.Location = new System.Drawing.Point(174, 4);
            this.SeriesTab.Name = "SeriesTab";
            this.SeriesTab.Padding = new System.Windows.Forms.Padding(3);
            this.SeriesTab.Size = new System.Drawing.Size(913, 657);
            this.SeriesTab.TabIndex = 2;
            this.SeriesTab.Text = "Series";
            // 
            // TrendingTab
            // 
            this.TrendingTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(33)))), ((int)(((byte)(63)))));
            this.TrendingTab.Location = new System.Drawing.Point(174, 4);
            this.TrendingTab.Name = "TrendingTab";
            this.TrendingTab.Size = new System.Drawing.Size(913, 657);
            this.TrendingTab.TabIndex = 4;
            this.TrendingTab.Text = "Trending";
            // 
            // UsersTab
            // 
            this.UsersTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(33)))), ((int)(((byte)(63)))));
            this.UsersTab.Location = new System.Drawing.Point(174, 4);
            this.UsersTab.Name = "UsersTab";
            this.UsersTab.Padding = new System.Windows.Forms.Padding(3);
            this.UsersTab.Size = new System.Drawing.Size(913, 657);
            this.UsersTab.TabIndex = 3;
            this.UsersTab.Text = "Users";
            // 
            // SettingsTab
            // 
            this.SettingsTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(33)))), ((int)(((byte)(63)))));
            this.SettingsTab.Location = new System.Drawing.Point(174, 4);
            this.SettingsTab.Name = "SettingsTab";
            this.SettingsTab.Padding = new System.Windows.Forms.Padding(3);
            this.SettingsTab.Size = new System.Drawing.Size(913, 657);
            this.SettingsTab.TabIndex = 5;
            this.SettingsTab.Text = "Settings";
            // 
            // HeadingSection
            // 
            this.HeadingSection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(33)))), ((int)(((byte)(63)))));
            this.HeadingSection.Controls.Add(this.guna2HtmlLabel1);
            this.HeadingSection.Controls.Add(this.SearchBox);
            this.HeadingSection.Controls.Add(this.ProfileBox);
            this.HeadingSection.Dock = System.Windows.Forms.DockStyle.Top;
            this.HeadingSection.Location = new System.Drawing.Point(0, 29);
            this.HeadingSection.Name = "HeadingSection";
            this.HeadingSection.Size = new System.Drawing.Size(1091, 38);
            this.HeadingSection.TabIndex = 3;
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.ForeColor = System.Drawing.Color.Purple;
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(12, 3);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(81, 29);
            this.guna2HtmlLabel1.TabIndex = 0;
            this.guna2HtmlLabel1.Text = "JellyFlix";
            // 
            // SearchBox
            // 
            this.SearchBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchBox.Animated = true;
            this.SearchBox.AutoRoundedCorners = true;
            this.SearchBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(33)))), ((int)(((byte)(63)))));
            this.SearchBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.SearchBox.DefaultText = "";
            this.SearchBox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.SearchBox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.SearchBox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.SearchBox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.SearchBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.SearchBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.SearchBox.ForeColor = System.Drawing.Color.Black;
            this.SearchBox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.SearchBox.IconLeft = global::JellyFlix_MediaHub.Properties.Resources.magnifying_glass_solid;
            this.SearchBox.Location = new System.Drawing.Point(218, 3);
            this.SearchBox.Name = "SearchBox";
            this.SearchBox.PlaceholderForeColor = System.Drawing.Color.DarkGray;
            this.SearchBox.PlaceholderText = "Search Movies & TV Shows";
            this.SearchBox.SelectedText = "";
            this.SearchBox.Size = new System.Drawing.Size(719, 32);
            this.SearchBox.TabIndex = 1;
            // 
            // ProfileBox
            // 
            this.ProfileBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ProfileBox.BackColor = System.Drawing.Color.Transparent;
            this.ProfileBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ProfileBox.Image = global::JellyFlix_MediaHub.Properties.Resources.user_tie_solid;
            this.ProfileBox.ImageRotate = 0F;
            this.ProfileBox.Location = new System.Drawing.Point(1055, 2);
            this.ProfileBox.Name = "ProfileBox";
            this.ProfileBox.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.ProfileBox.Size = new System.Drawing.Size(34, 35);
            this.ProfileBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ProfileBox.TabIndex = 0;
            this.ProfileBox.TabStop = false;
            this.ProfileBox.UseTransparentBackground = true;
            this.ProfileBox.Click += new System.EventHandler(this.ProfileBox_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(33)))), ((int)(((byte)(63)))));
            this.ClientSize = new System.Drawing.Size(1091, 732);
            this.Controls.Add(this.NavMenu);
            this.Controls.Add(this.HeadingSection);
            this.Controls.Add(this.TabBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(1091, 732);
            this.Name = "MainMenu";
            this.Text = "MainMenu";
            this.TabBar.ResumeLayout(false);
            this.NavMenu.ResumeLayout(false);
            this.HeadingSection.ResumeLayout(false);
            this.HeadingSection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProfileBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse Elipse;
        private Guna.UI2.WinForms.Guna2ContainerControl TabBar;
        private Guna.UI2.WinForms.Guna2ControlBox MinimizeApp;
        private Guna.UI2.WinForms.Guna2ControlBox MaximizeApp;
        private Guna.UI2.WinForms.Guna2ControlBox CloseApp;
        private Guna.UI2.WinForms.Guna2DragControl TabBarDrag;
        private Guna.UI2.WinForms.Guna2ResizeForm ResizeForm;
        private Guna.UI2.WinForms.Guna2TabControl NavMenu;
        private System.Windows.Forms.TabPage MovieTab;
        private System.Windows.Forms.TabPage SeriesTab;
        private Guna.UI2.WinForms.Guna2Panel HeadingSection;
        private Guna.UI2.WinForms.Guna2CirclePictureBox ProfileBox;
        private Guna.UI2.WinForms.Guna2TextBox SearchBox;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private System.Windows.Forms.TabPage UsersTab;
        private System.Windows.Forms.TabPage TrendingTab;
        private System.Windows.Forms.TabPage SettingsTab;
    }
}