namespace JellyFlix_MediaHub.UI
{
    partial class SignUp
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
            this.CloseApp = new Guna.UI2.WinForms.Guna2ControlBox();
            this.TabBarDrag = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.SignUpPageTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.materialTextBox21 = new MaterialSkin.Controls.MaterialTextBox2();
            this.TabBar.SuspendLayout();
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
            this.TabBar.Controls.Add(this.CloseApp);
            this.TabBar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(23)))), ((int)(((byte)(53)))));
            this.TabBar.Location = new System.Drawing.Point(-3, 0);
            this.TabBar.Name = "TabBar";
            this.TabBar.Size = new System.Drawing.Size(1095, 29);
            this.TabBar.TabIndex = 0;
            this.TabBar.Text = "guna2ContainerControl1";
            // 
            // MinimizeApp
            // 
            this.MinimizeApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MinimizeApp.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            this.MinimizeApp.FillColor = System.Drawing.Color.Transparent;
            this.MinimizeApp.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(255)))));
            this.MinimizeApp.HoverState.IconColor = System.Drawing.Color.White;
            this.MinimizeApp.IconColor = System.Drawing.Color.White;
            this.MinimizeApp.Location = new System.Drawing.Point(1015, 0);
            this.MinimizeApp.Name = "MinimizeApp";
            this.MinimizeApp.Size = new System.Drawing.Size(39, 29);
            this.MinimizeApp.TabIndex = 1;
            // 
            // CloseApp
            // 
            this.CloseApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseApp.FillColor = System.Drawing.Color.Transparent;
            this.CloseApp.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(28)))), ((int)(((byte)(17)))));
            this.CloseApp.HoverState.IconColor = System.Drawing.Color.White;
            this.CloseApp.IconColor = System.Drawing.Color.White;
            this.CloseApp.Location = new System.Drawing.Point(1055, 0);
            this.CloseApp.Name = "CloseApp";
            this.CloseApp.Size = new System.Drawing.Size(38, 29);
            this.CloseApp.TabIndex = 1;
            // 
            // TabBarDrag
            // 
            this.TabBarDrag.DockIndicatorColor = System.Drawing.Color.Transparent;
            this.TabBarDrag.DockIndicatorTransparencyValue = 0.6D;
            this.TabBarDrag.TargetControl = this.TabBar;
            this.TabBarDrag.UseTransparentDrag = true;
            // 
            // SignUpPageTitle
            // 
            this.SignUpPageTitle.BackColor = System.Drawing.Color.Transparent;
            this.SignUpPageTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SignUpPageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(183)))), ((int)(((byte)(196)))));
            this.SignUpPageTitle.Location = new System.Drawing.Point(67, 56);
            this.SignUpPageTitle.Name = "SignUpPageTitle";
            this.SignUpPageTitle.Size = new System.Drawing.Size(222, 31);
            this.SignUpPageTitle.TabIndex = 1;
            this.SignUpPageTitle.Text = "Create New Account";
            // 
            // guna2HtmlLabel2
            // 
            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(204)))), ((int)(((byte)(220)))));
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(67, 133);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Size = new System.Drawing.Size(77, 22);
            this.guna2HtmlLabel2.TabIndex = 2;
            this.guna2HtmlLabel2.Text = "Username";
            // 
            // materialTextBox21
            // 
            this.materialTextBox21.AnimateReadOnly = false;
            this.materialTextBox21.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.materialTextBox21.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.materialTextBox21.Depth = 0;
            this.materialTextBox21.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialTextBox21.HelperText = "Must be unique";
            this.materialTextBox21.HideSelection = true;
            this.materialTextBox21.Hint = "Username";
            this.materialTextBox21.LeadingIcon = null;
            this.materialTextBox21.LeaveOnEnterKey = true;
            this.materialTextBox21.Location = new System.Drawing.Point(67, 162);
            this.materialTextBox21.MaxLength = 32767;
            this.materialTextBox21.MouseState = MaterialSkin.MouseState.OUT;
            this.materialTextBox21.Name = "materialTextBox21";
            this.materialTextBox21.PasswordChar = '\0';
            this.materialTextBox21.PrefixSuffixText = null;
            this.materialTextBox21.ReadOnly = false;
            this.materialTextBox21.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.materialTextBox21.SelectedText = "";
            this.materialTextBox21.SelectionLength = 0;
            this.materialTextBox21.SelectionStart = 0;
            this.materialTextBox21.ShortcutsEnabled = true;
            this.materialTextBox21.ShowAssistiveText = true;
            this.materialTextBox21.Size = new System.Drawing.Size(334, 64);
            this.materialTextBox21.TabIndex = 3;
            this.materialTextBox21.TabStop = false;
            this.materialTextBox21.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.materialTextBox21.TrailingIcon = null;
            this.materialTextBox21.UseAccent = false;
            this.materialTextBox21.UseSystemPasswordChar = false;
            // 
            // SignUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(33)))), ((int)(((byte)(63)))));
            this.ClientSize = new System.Drawing.Size(1091, 732);
            this.Controls.Add(this.materialTextBox21);
            this.Controls.Add(this.guna2HtmlLabel2);
            this.Controls.Add(this.SignUpPageTitle);
            this.Controls.Add(this.TabBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SignUp";
            this.Text = "SignUp";
            this.Load += new System.EventHandler(this.SignUp_Load);
            this.TabBar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse Elipse;
        private Guna.UI2.WinForms.Guna2ContainerControl TabBar;
        private Guna.UI2.WinForms.Guna2ControlBox CloseApp;
        private Guna.UI2.WinForms.Guna2ControlBox MinimizeApp;
        private Guna.UI2.WinForms.Guna2DragControl TabBarDrag;
        private Guna.UI2.WinForms.Guna2HtmlLabel SignUpPageTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private MaterialSkin.Controls.MaterialTextBox2 materialTextBox21;
    }
}