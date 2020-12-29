namespace ThiTracNghiem
{
    partial class frmLogin
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
            this.errorProviderMain = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.llblDangKy = new Guna.UI2.WinForms.Guna2Button();
            this.txtMatKhau = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtTenDangNhap = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderMain)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // errorProviderMain
            // 
            this.errorProviderMain.ContainerControl = this;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.llblDangKy);
            this.groupBox1.Controls.Add(this.txtMatKhau);
            this.groupBox1.Controls.Add(this.txtTenDangNhap);
            this.groupBox1.Location = new System.Drawing.Point(147, 85);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(411, 388);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label4.ImageKey = "(none)";
            this.label4.Location = new System.Drawing.Point(105, 215);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 29);
            this.label4.TabIndex = 27;
            this.label4.Text = "Đăng Ký ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Location = new System.Drawing.Point(136, 46);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 29);
            this.label3.TabIndex = 26;
            this.label3.Text = "Đăng Nhâp";
            // 
            // llblDangKy
            // 
            this.llblDangKy.BorderRadius = 20;
            this.llblDangKy.CheckedState.Parent = this.llblDangKy;
            this.llblDangKy.CustomImages.Parent = this.llblDangKy;
            this.llblDangKy.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.llblDangKy.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.llblDangKy.ForeColor = System.Drawing.Color.White;
            this.llblDangKy.HoverState.Parent = this.llblDangKy;
            this.llblDangKy.Location = new System.Drawing.Point(96, 266);
            this.llblDangKy.Margin = new System.Windows.Forms.Padding(2);
            this.llblDangKy.Name = "llblDangKy";
            this.llblDangKy.ShadowDecoration.Parent = this.llblDangKy;
            this.llblDangKy.Size = new System.Drawing.Size(211, 37);
            this.llblDangKy.TabIndex = 24;
            this.llblDangKy.Text = "Tạo Tài Khoảng";
            this.llblDangKy.Click += new System.EventHandler(this.llblDangKy_Click_1);
            // 
            // txtMatKhau
            // 
            this.txtMatKhau.BorderRadius = 12;
            this.txtMatKhau.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMatKhau.DefaultText = "password";
            this.txtMatKhau.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtMatKhau.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtMatKhau.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMatKhau.DisabledState.Parent = this.txtMatKhau;
            this.txtMatKhau.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMatKhau.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMatKhau.FocusedState.Parent = this.txtMatKhau;
            this.txtMatKhau.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMatKhau.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMatKhau.HoverState.Parent = this.txtMatKhau;
            this.txtMatKhau.IconLeft = global::ThiTracNghiem.Properties.Resources.icon;
            this.txtMatKhau.IconLeftOffset = new System.Drawing.Point(10, 0);
            this.txtMatKhau.Location = new System.Drawing.Point(96, 158);
            this.txtMatKhau.Margin = new System.Windows.Forms.Padding(2);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.PasswordChar = '\0';
            this.txtMatKhau.PlaceholderText = "Mật Khẩu";
            this.txtMatKhau.SelectedText = "";
            this.txtMatKhau.SelectionStart = 8;
            this.txtMatKhau.ShadowDecoration.Parent = this.txtMatKhau;
            this.txtMatKhau.Size = new System.Drawing.Size(211, 46);
            this.txtMatKhau.TabIndex = 23;
            this.txtMatKhau.TextChanged += new System.EventHandler(this.txtMatKhau_TextChanged_1);
            // 
            // txtTenDangNhap
            // 
            this.txtTenDangNhap.BorderRadius = 12;
            this.txtTenDangNhap.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenDangNhap.DefaultText = "username";
            this.txtTenDangNhap.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTenDangNhap.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTenDangNhap.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTenDangNhap.DisabledState.Parent = this.txtTenDangNhap;
            this.txtTenDangNhap.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTenDangNhap.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTenDangNhap.FocusedState.Parent = this.txtTenDangNhap;
            this.txtTenDangNhap.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTenDangNhap.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTenDangNhap.HoverState.Parent = this.txtTenDangNhap;
            this.txtTenDangNhap.IconLeft = global::ThiTracNghiem.Properties.Resources.Capture;
            this.txtTenDangNhap.IconLeftOffset = new System.Drawing.Point(10, 0);
            this.txtTenDangNhap.Location = new System.Drawing.Point(96, 89);
            this.txtTenDangNhap.Margin = new System.Windows.Forms.Padding(2);
            this.txtTenDangNhap.Name = "txtTenDangNhap";
            this.txtTenDangNhap.PasswordChar = '\0';
            this.txtTenDangNhap.PlaceholderText = "Tài Khoảng";
            this.txtTenDangNhap.SelectedText = "";
            this.txtTenDangNhap.SelectionStart = 8;
            this.txtTenDangNhap.ShadowDecoration.Parent = this.txtTenDangNhap;
            this.txtTenDangNhap.Size = new System.Drawing.Size(211, 48);
            this.txtTenDangNhap.TabIndex = 22;
            this.txtTenDangNhap.TextChanged += new System.EventHandler(this.txtTenDangNhap_TextChanged);
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(212, 33);
            this.guna2HtmlLabel1.Margin = new System.Windows.Forms.Padding(2);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(294, 28);
            this.guna2HtmlLabel1.TabIndex = 8;
            this.guna2HtmlLabel1.Text = "Phần Mềm Thi Trắc nghiệm";
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ThiTracNghiem.Properties.Resources.br;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(688, 470);
            this.Controls.Add(this.guna2HtmlLabel1);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập";
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderMain)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ErrorProvider errorProviderMain;
        private System.Windows.Forms.GroupBox groupBox1;
        private Guna.UI2.WinForms.Guna2TextBox txtTenDangNhap;
        private Guna.UI2.WinForms.Guna2TextBox txtMatKhau;
        private Guna.UI2.WinForms.Guna2Button llblDangKy;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
    }
}