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
            this.guna2Shapes5 = new Guna.UI2.WinForms.Guna2Shapes();
            this.NavMenu = new Guna.UI2.WinForms.Guna2TabControl();
            this.SeriesTab = new System.Windows.Forms.TabPage();
            this.guna2Shapes4 = new Guna.UI2.WinForms.Guna2Shapes();
            this.TrendingTab = new System.Windows.Forms.TabPage();
            this.guna2Shapes2 = new Guna.UI2.WinForms.Guna2Shapes();
            this.UsersTab = new System.Windows.Forms.TabPage();
            this.guna2Shapes1 = new Guna.UI2.WinForms.Guna2Shapes();
            this.SettingsTab = new System.Windows.Forms.TabPage();
            this.guna2Shapes3 = new Guna.UI2.WinForms.Guna2Shapes();
            this.ProwlarrApilLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.TMDBApiLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2TextBox1 = new Guna.UI2.WinForms.Guna2TextBox();
            this.TMDBApiTextBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.HeadingSection = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.SearchBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.ProfileBox = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.materialCheckbox1 = new MaterialSkin.Controls.MaterialCheckbox();
            this.materialCheckbox2 = new MaterialSkin.Controls.MaterialCheckbox();
            this.materialCheckbox3 = new MaterialSkin.Controls.MaterialCheckbox();
            this.materialCheckbox4 = new MaterialSkin.Controls.MaterialCheckbox();
            this.TabBar.SuspendLayout();
            this.MovieTab.SuspendLayout();
            this.NavMenu.SuspendLayout();
            this.SeriesTab.SuspendLayout();
            this.TrendingTab.SuspendLayout();
            this.UsersTab.SuspendLayout();
            this.SettingsTab.SuspendLayout();
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
            this.MovieTab.Controls.Add(this.guna2Shapes5);
            this.MovieTab.Location = new System.Drawing.Point(174, 4);
            this.MovieTab.Name = "MovieTab";
            this.MovieTab.Padding = new System.Windows.Forms.Padding(3);
            this.MovieTab.Size = new System.Drawing.Size(913, 657);
            this.MovieTab.TabIndex = 1;
            this.MovieTab.Text = "Movies";
            // 
            // guna2Shapes5
            // 
            this.guna2Shapes5.Dock = System.Windows.Forms.DockStyle.Left;
            this.guna2Shapes5.LineOrientation = System.Windows.Forms.Orientation.Vertical;
            this.guna2Shapes5.Location = new System.Drawing.Point(3, 3);
            this.guna2Shapes5.Name = "guna2Shapes5";
            this.guna2Shapes5.PolygonSkip = 1;
            this.guna2Shapes5.Rotate = 0F;
            this.guna2Shapes5.Shape = Guna.UI2.WinForms.Enums.ShapeType.Line;
            this.guna2Shapes5.Size = new System.Drawing.Size(12, 651);
            this.guna2Shapes5.TabIndex = 3;
            this.guna2Shapes5.Text = "guna2Shapes5";
            this.guna2Shapes5.Zoom = 80;
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
            this.NavMenu.SelectedIndexChanged += new System.EventHandler(this.NavMenu_SelectedIndexChanged);
            // 
            // SeriesTab
            // 
            this.SeriesTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(33)))), ((int)(((byte)(63)))));
            this.SeriesTab.Controls.Add(this.guna2Shapes4);
            this.SeriesTab.Location = new System.Drawing.Point(174, 4);
            this.SeriesTab.Name = "SeriesTab";
            this.SeriesTab.Padding = new System.Windows.Forms.Padding(3);
            this.SeriesTab.Size = new System.Drawing.Size(913, 657);
            this.SeriesTab.TabIndex = 2;
            this.SeriesTab.Text = "Series";
            // 
            // guna2Shapes4
            // 
            this.guna2Shapes4.Dock = System.Windows.Forms.DockStyle.Left;
            this.guna2Shapes4.LineOrientation = System.Windows.Forms.Orientation.Vertical;
            this.guna2Shapes4.Location = new System.Drawing.Point(3, 3);
            this.guna2Shapes4.Name = "guna2Shapes4";
            this.guna2Shapes4.PolygonSkip = 1;
            this.guna2Shapes4.Rotate = 0F;
            this.guna2Shapes4.Shape = Guna.UI2.WinForms.Enums.ShapeType.Line;
            this.guna2Shapes4.Size = new System.Drawing.Size(12, 651);
            this.guna2Shapes4.TabIndex = 3;
            this.guna2Shapes4.Text = "guna2Shapes4";
            this.guna2Shapes4.Zoom = 80;
            // 
            // TrendingTab
            // 
            this.TrendingTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(33)))), ((int)(((byte)(63)))));
            this.TrendingTab.Controls.Add(this.guna2Shapes2);
            this.TrendingTab.Location = new System.Drawing.Point(174, 4);
            this.TrendingTab.Name = "TrendingTab";
            this.TrendingTab.Size = new System.Drawing.Size(913, 657);
            this.TrendingTab.TabIndex = 4;
            this.TrendingTab.Text = "Trending";
            // 
            // guna2Shapes2
            // 
            this.guna2Shapes2.Dock = System.Windows.Forms.DockStyle.Left;
            this.guna2Shapes2.LineOrientation = System.Windows.Forms.Orientation.Vertical;
            this.guna2Shapes2.Location = new System.Drawing.Point(0, 0);
            this.guna2Shapes2.Name = "guna2Shapes2";
            this.guna2Shapes2.PolygonSkip = 1;
            this.guna2Shapes2.Rotate = 0F;
            this.guna2Shapes2.Shape = Guna.UI2.WinForms.Enums.ShapeType.Line;
            this.guna2Shapes2.Size = new System.Drawing.Size(15, 657);
            this.guna2Shapes2.TabIndex = 3;
            this.guna2Shapes2.Text = "guna2Shapes2";
            this.guna2Shapes2.Zoom = 80;
            // 
            // UsersTab
            // 
            this.UsersTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(33)))), ((int)(((byte)(63)))));
            this.UsersTab.Controls.Add(this.guna2Shapes1);
            this.UsersTab.Location = new System.Drawing.Point(174, 4);
            this.UsersTab.Name = "UsersTab";
            this.UsersTab.Padding = new System.Windows.Forms.Padding(3);
            this.UsersTab.Size = new System.Drawing.Size(913, 657);
            this.UsersTab.TabIndex = 3;
            this.UsersTab.Text = "Users";
            // 
            // guna2Shapes1
            // 
            this.guna2Shapes1.Dock = System.Windows.Forms.DockStyle.Left;
            this.guna2Shapes1.LineOrientation = System.Windows.Forms.Orientation.Vertical;
            this.guna2Shapes1.Location = new System.Drawing.Point(3, 3);
            this.guna2Shapes1.Name = "guna2Shapes1";
            this.guna2Shapes1.PolygonSkip = 1;
            this.guna2Shapes1.Rotate = 0F;
            this.guna2Shapes1.Shape = Guna.UI2.WinForms.Enums.ShapeType.Line;
            this.guna2Shapes1.Size = new System.Drawing.Size(12, 651);
            this.guna2Shapes1.TabIndex = 2;
            this.guna2Shapes1.Text = "guna2Shapes1";
            this.guna2Shapes1.Zoom = 80;
            // 
            // SettingsTab
            // 
            this.SettingsTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(33)))), ((int)(((byte)(63)))));
            this.SettingsTab.Controls.Add(this.guna2Shapes3);
            this.SettingsTab.Controls.Add(this.ProwlarrApilLabel);
            this.SettingsTab.Controls.Add(this.TMDBApiLabel);
            this.SettingsTab.Controls.Add(this.guna2TextBox1);
            this.SettingsTab.Controls.Add(this.TMDBApiTextBox);
            this.SettingsTab.Location = new System.Drawing.Point(174, 4);
            this.SettingsTab.Name = "SettingsTab";
            this.SettingsTab.Padding = new System.Windows.Forms.Padding(3);
            this.SettingsTab.Size = new System.Drawing.Size(913, 657);
            this.SettingsTab.TabIndex = 5;
            this.SettingsTab.Text = "Settings";
            this.SettingsTab.Click += new System.EventHandler(this.SettingsTab_Click);
            // 
            // guna2Shapes3
            // 
            this.guna2Shapes3.Dock = System.Windows.Forms.DockStyle.Left;
            this.guna2Shapes3.LineOrientation = System.Windows.Forms.Orientation.Vertical;
            this.guna2Shapes3.Location = new System.Drawing.Point(3, 3);
            this.guna2Shapes3.Name = "guna2Shapes3";
            this.guna2Shapes3.PolygonSkip = 1;
            this.guna2Shapes3.Rotate = 0F;
            this.guna2Shapes3.Shape = Guna.UI2.WinForms.Enums.ShapeType.Line;
            this.guna2Shapes3.Size = new System.Drawing.Size(12, 651);
            this.guna2Shapes3.TabIndex = 19;
            this.guna2Shapes3.Text = "guna2Shapes3";
            this.guna2Shapes3.Zoom = 80;
            // 
            // ProwlarrApilLabel
            // 
            this.ProwlarrApilLabel.BackColor = System.Drawing.Color.Transparent;
            this.ProwlarrApilLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProwlarrApilLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(204)))), ((int)(((byte)(220)))));
            this.ProwlarrApilLabel.IsSelectionEnabled = false;
            this.ProwlarrApilLabel.Location = new System.Drawing.Point(44, 156);
            this.ProwlarrApilLabel.Name = "ProwlarrApilLabel";
            this.ProwlarrApilLabel.Size = new System.Drawing.Size(117, 22);
            this.ProwlarrApilLabel.TabIndex = 18;
            this.ProwlarrApilLabel.Text = "Prowlarr Api Key";
            // 
            // TMDBApiLabel
            // 
            this.TMDBApiLabel.BackColor = System.Drawing.Color.Transparent;
            this.TMDBApiLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TMDBApiLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(204)))), ((int)(((byte)(220)))));
            this.TMDBApiLabel.IsSelectionEnabled = false;
            this.TMDBApiLabel.Location = new System.Drawing.Point(44, 53);
            this.TMDBApiLabel.Name = "TMDBApiLabel";
            this.TMDBApiLabel.Size = new System.Drawing.Size(105, 22);
            this.TMDBApiLabel.TabIndex = 18;
            this.TMDBApiLabel.Text = "TMDB Api Key";
            // 
            // guna2TextBox1
            // 
            this.guna2TextBox1.AcceptsReturn = true;
            this.guna2TextBox1.BorderRadius = 12;
            this.guna2TextBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.guna2TextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.guna2TextBox1.DefaultText = "";
            this.guna2TextBox1.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.guna2TextBox1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.guna2TextBox1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox1.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox1.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2TextBox1.ForeColor = System.Drawing.Color.Black;
            this.guna2TextBox1.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox1.Location = new System.Drawing.Point(44, 184);
            this.guna2TextBox1.Name = "guna2TextBox1";
            this.guna2TextBox1.PlaceholderText = "AWDawdawdada412awAWdad";
            this.guna2TextBox1.SelectedText = "";
            this.guna2TextBox1.Size = new System.Drawing.Size(447, 36);
            this.guna2TextBox1.TabIndex = 17;
            // 
            // TMDBApiTextBox
            // 
            this.TMDBApiTextBox.AcceptsReturn = true;
            this.TMDBApiTextBox.BorderRadius = 12;
            this.TMDBApiTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.TMDBApiTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TMDBApiTextBox.DefaultText = "";
            this.TMDBApiTextBox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.TMDBApiTextBox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.TMDBApiTextBox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.TMDBApiTextBox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.TMDBApiTextBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.TMDBApiTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TMDBApiTextBox.ForeColor = System.Drawing.Color.Black;
            this.TMDBApiTextBox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.TMDBApiTextBox.Location = new System.Drawing.Point(44, 81);
            this.TMDBApiTextBox.Name = "TMDBApiTextBox";
            this.TMDBApiTextBox.PlaceholderText = "AWDawdawdada412awAWdad";
            this.TMDBApiTextBox.SelectedText = "";
            this.TMDBApiTextBox.Size = new System.Drawing.Size(447, 36);
            this.TMDBApiTextBox.TabIndex = 17;
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
            // materialCheckbox1
            // 
            this.materialCheckbox1.Depth = 0;
            this.materialCheckbox1.Location = new System.Drawing.Point(0, 0);
            this.materialCheckbox1.Margin = new System.Windows.Forms.Padding(0);
            this.materialCheckbox1.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialCheckbox1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCheckbox1.Name = "materialCheckbox1";
            this.materialCheckbox1.ReadOnly = false;
            this.materialCheckbox1.Ripple = true;
            this.materialCheckbox1.Size = new System.Drawing.Size(104, 37);
            this.materialCheckbox1.TabIndex = 0;
            this.materialCheckbox1.Text = "materialCheckbox1";
            this.materialCheckbox1.UseVisualStyleBackColor = true;
            // 
            // materialCheckbox2
            // 
            this.materialCheckbox2.Depth = 0;
            this.materialCheckbox2.Location = new System.Drawing.Point(0, 0);
            this.materialCheckbox2.Margin = new System.Windows.Forms.Padding(0);
            this.materialCheckbox2.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialCheckbox2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCheckbox2.Name = "materialCheckbox2";
            this.materialCheckbox2.ReadOnly = false;
            this.materialCheckbox2.Ripple = true;
            this.materialCheckbox2.Size = new System.Drawing.Size(104, 37);
            this.materialCheckbox2.TabIndex = 0;
            this.materialCheckbox2.Text = "materialCheckbox2";
            this.materialCheckbox2.UseVisualStyleBackColor = true;
            // 
            // materialCheckbox3
            // 
            this.materialCheckbox3.Depth = 0;
            this.materialCheckbox3.Location = new System.Drawing.Point(0, 0);
            this.materialCheckbox3.Margin = new System.Windows.Forms.Padding(0);
            this.materialCheckbox3.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialCheckbox3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCheckbox3.Name = "materialCheckbox3";
            this.materialCheckbox3.ReadOnly = false;
            this.materialCheckbox3.Ripple = true;
            this.materialCheckbox3.Size = new System.Drawing.Size(104, 37);
            this.materialCheckbox3.TabIndex = 0;
            this.materialCheckbox3.Text = "materialCheckbox3";
            this.materialCheckbox3.UseVisualStyleBackColor = true;
            // 
            // materialCheckbox4
            // 
            this.materialCheckbox4.Depth = 0;
            this.materialCheckbox4.Location = new System.Drawing.Point(0, 0);
            this.materialCheckbox4.Margin = new System.Windows.Forms.Padding(0);
            this.materialCheckbox4.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialCheckbox4.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCheckbox4.Name = "materialCheckbox4";
            this.materialCheckbox4.ReadOnly = false;
            this.materialCheckbox4.Ripple = true;
            this.materialCheckbox4.Size = new System.Drawing.Size(104, 37);
            this.materialCheckbox4.TabIndex = 0;
            this.materialCheckbox4.Text = "materialCheckbox4";
            this.materialCheckbox4.UseVisualStyleBackColor = true;
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
            this.MovieTab.ResumeLayout(false);
            this.NavMenu.ResumeLayout(false);
            this.SeriesTab.ResumeLayout(false);
            this.TrendingTab.ResumeLayout(false);
            this.UsersTab.ResumeLayout(false);
            this.SettingsTab.ResumeLayout(false);
            this.SettingsTab.PerformLayout();
            this.HeadingSection.ResumeLayout(false);
            this.HeadingSection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProfileBox)).EndInit();
            this.ResumeLayout(false);
            this.Load += new System.EventHandler(this.MainMenu_Load);
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
        private Guna.UI2.WinForms.Guna2TextBox TMDBApiTextBox;
        private Guna.UI2.WinForms.Guna2HtmlLabel TMDBApiLabel;
        private Guna.UI2.WinForms.Guna2HtmlLabel ProwlarrApilLabel;
        private Guna.UI2.WinForms.Guna2TextBox guna2TextBox1;
        private MaterialSkin.Controls.MaterialCheckbox materialCheckbox1;
        private MaterialSkin.Controls.MaterialCheckbox materialCheckbox2;
        private MaterialSkin.Controls.MaterialCheckbox materialCheckbox3;
        private MaterialSkin.Controls.MaterialCheckbox materialCheckbox4;
        private Guna.UI2.WinForms.Guna2Shapes guna2Shapes1;
        private Guna.UI2.WinForms.Guna2Shapes guna2Shapes5;
        private Guna.UI2.WinForms.Guna2Shapes guna2Shapes4;
        private Guna.UI2.WinForms.Guna2Shapes guna2Shapes3;
        private Guna.UI2.WinForms.Guna2Shapes guna2Shapes2;
    }
}