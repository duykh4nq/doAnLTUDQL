namespace ThiTracNghiem
{
    partial class frmThemKT
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
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gbTongSoDeThi = new System.Windows.Forms.GroupBox();
            this.dgvDT = new System.Windows.Forms.DataGridView();
            this.lblTgBatDau = new System.Windows.Forms.Label();
            this.gbTongSoThiSinh = new System.Windows.Forms.GroupBox();
            this.dgvHS = new System.Windows.Forms.DataGridView();
            this.nudSoHocSinh = new System.Windows.Forms.NumericUpDown();
            this.btnThemKT = new Guna.UI2.WinForms.Guna2Button();
            this.btnRdHs = new Guna.UI2.WinForms.Guna2Button();
            this.btnChonHetHS = new Guna.UI2.WinForms.Guna2Button();
            this.txtTenKT = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtMaKhoi = new Guna.UI2.WinForms.Guna2TextBox();
            this.dtpNgayThi = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.groupBox6.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbTongSoDeThi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDT)).BeginInit();
            this.gbTongSoThiSinh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoHocSinh)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.Color.LightGray;
            this.groupBox6.Controls.Add(this.txtMaKhoi);
            this.groupBox6.Controls.Add(this.txtTenKT);
            this.groupBox6.Controls.Add(this.label1);
            this.groupBox6.Controls.Add(this.label20);
            this.groupBox6.Location = new System.Drawing.Point(0, -1);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox6.Size = new System.Drawing.Size(788, 89);
            this.groupBox6.TabIndex = 48;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Thông tin chi tiết kỳ thi";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(153, 53);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 17);
            this.label1.TabIndex = 31;
            this.label1.Text = "Khối thi:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(153, 27);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(74, 17);
            this.label20.TabIndex = 29;
            this.label20.Text = "Tên kỳ thi:";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LightGray;
            this.groupBox1.Controls.Add(this.btnThemKT);
            this.groupBox1.Location = new System.Drawing.Point(785, -1);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(201, 619);
            this.groupBox1.TabIndex = 51;
            this.groupBox1.TabStop = false;
            // 
            // gbTongSoDeThi
            // 
            this.gbTongSoDeThi.BackColor = System.Drawing.Color.LightGray;
            this.gbTongSoDeThi.Controls.Add(this.dtpNgayThi);
            this.gbTongSoDeThi.Controls.Add(this.dgvDT);
            this.gbTongSoDeThi.Controls.Add(this.lblTgBatDau);
            this.gbTongSoDeThi.Location = new System.Drawing.Point(0, 81);
            this.gbTongSoDeThi.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbTongSoDeThi.Name = "gbTongSoDeThi";
            this.gbTongSoDeThi.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbTongSoDeThi.Size = new System.Drawing.Size(788, 263);
            this.gbTongSoDeThi.TabIndex = 52;
            this.gbTongSoDeThi.TabStop = false;
            this.gbTongSoDeThi.Text = "Chọn một đề thi";
            // 
            // dgvDT
            // 
            this.dgvDT.AllowUserToAddRows = false;
            this.dgvDT.AllowUserToDeleteRows = false;
            this.dgvDT.AllowUserToOrderColumns = true;
            this.dgvDT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDT.Location = new System.Drawing.Point(21, 64);
            this.dgvDT.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvDT.MultiSelect = false;
            this.dgvDT.Name = "dgvDT";
            this.dgvDT.RowHeadersVisible = false;
            this.dgvDT.RowHeadersWidth = 51;
            this.dgvDT.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDT.Size = new System.Drawing.Size(643, 166);
            this.dgvDT.TabIndex = 35;
            // 
            // lblTgBatDau
            // 
            this.lblTgBatDau.AutoSize = true;
            this.lblTgBatDau.Location = new System.Drawing.Point(152, 33);
            this.lblTgBatDau.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTgBatDau.Name = "lblTgBatDau";
            this.lblTgBatDau.Size = new System.Drawing.Size(123, 17);
            this.lblTgBatDau.TabIndex = 30;
            this.lblTgBatDau.Text = "Thời gian bắt đầu:";
            // 
            // gbTongSoThiSinh
            // 
            this.gbTongSoThiSinh.BackColor = System.Drawing.Color.LightGray;
            this.gbTongSoThiSinh.Controls.Add(this.btnChonHetHS);
            this.gbTongSoThiSinh.Controls.Add(this.btnRdHs);
            this.gbTongSoThiSinh.Controls.Add(this.dgvHS);
            this.gbTongSoThiSinh.Controls.Add(this.nudSoHocSinh);
            this.gbTongSoThiSinh.Location = new System.Drawing.Point(0, 338);
            this.gbTongSoThiSinh.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbTongSoThiSinh.Name = "gbTongSoThiSinh";
            this.gbTongSoThiSinh.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbTongSoThiSinh.Size = new System.Drawing.Size(788, 279);
            this.gbTongSoThiSinh.TabIndex = 53;
            this.gbTongSoThiSinh.TabStop = false;
            this.gbTongSoThiSinh.Text = "Tổng số thí sinh được chọn: 0";
            // 
            // dgvHS
            // 
            this.dgvHS.AllowUserToAddRows = false;
            this.dgvHS.AllowUserToDeleteRows = false;
            this.dgvHS.AllowUserToOrderColumns = true;
            this.dgvHS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHS.Location = new System.Drawing.Point(21, 26);
            this.dgvHS.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvHS.Name = "dgvHS";
            this.dgvHS.RowHeadersVisible = false;
            this.dgvHS.RowHeadersWidth = 51;
            this.dgvHS.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHS.Size = new System.Drawing.Size(528, 236);
            this.dgvHS.TabIndex = 40;
            // 
            // nudSoHocSinh
            // 
            this.nudSoHocSinh.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudSoHocSinh.Location = new System.Drawing.Point(572, 23);
            this.nudSoHocSinh.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nudSoHocSinh.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudSoHocSinh.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSoHocSinh.Name = "nudSoHocSinh";
            this.nudSoHocSinh.Size = new System.Drawing.Size(92, 22);
            this.nudSoHocSinh.TabIndex = 39;
            this.nudSoHocSinh.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // btnThemKT
            // 
            this.btnThemKT.BorderRadius = 20;
            this.btnThemKT.CheckedState.Parent = this.btnThemKT;
            this.btnThemKT.CustomImages.Parent = this.btnThemKT;
            this.btnThemKT.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnThemKT.ForeColor = System.Drawing.Color.White;
            this.btnThemKT.HoverState.Parent = this.btnThemKT;
            this.btnThemKT.Location = new System.Drawing.Point(14, 284);
            this.btnThemKT.Name = "btnThemKT";
            this.btnThemKT.ShadowDecoration.Parent = this.btnThemKT;
            this.btnThemKT.Size = new System.Drawing.Size(180, 45);
            this.btnThemKT.TabIndex = 33;
            this.btnThemKT.Text = "Thêm Kỳ thi";
            // 
            // btnRdHs
            // 
            this.btnRdHs.BorderRadius = 20;
            this.btnRdHs.CheckedState.Parent = this.btnRdHs;
            this.btnRdHs.CustomImages.Parent = this.btnRdHs;
            this.btnRdHs.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRdHs.ForeColor = System.Drawing.Color.White;
            this.btnRdHs.HoverState.Parent = this.btnRdHs;
            this.btnRdHs.Location = new System.Drawing.Point(572, 77);
            this.btnRdHs.Name = "btnRdHs";
            this.btnRdHs.ShadowDecoration.Parent = this.btnRdHs;
            this.btnRdHs.Size = new System.Drawing.Size(180, 45);
            this.btnRdHs.TabIndex = 41;
            this.btnRdHs.Text = "Ngẫu Nhiên";
            // 
            // btnChonHetHS
            // 
            this.btnChonHetHS.BorderRadius = 20;
            this.btnChonHetHS.CheckedState.Parent = this.btnChonHetHS;
            this.btnChonHetHS.CustomImages.Parent = this.btnChonHetHS;
            this.btnChonHetHS.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnChonHetHS.ForeColor = System.Drawing.Color.White;
            this.btnChonHetHS.HoverState.Parent = this.btnChonHetHS;
            this.btnChonHetHS.Location = new System.Drawing.Point(572, 152);
            this.btnChonHetHS.Name = "btnChonHetHS";
            this.btnChonHetHS.ShadowDecoration.Parent = this.btnChonHetHS;
            this.btnChonHetHS.Size = new System.Drawing.Size(180, 45);
            this.btnChonHetHS.TabIndex = 42;
            this.btnChonHetHS.Text = "Chọn hết";
            // 
            // txtTenKT
            // 
            this.txtTenKT.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenKT.DefaultText = "";
            this.txtTenKT.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTenKT.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTenKT.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTenKT.DisabledState.Parent = this.txtTenKT;
            this.txtTenKT.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTenKT.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTenKT.FocusedState.Parent = this.txtTenKT;
            this.txtTenKT.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTenKT.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTenKT.HoverState.Parent = this.txtTenKT;
            this.txtTenKT.Location = new System.Drawing.Point(270, 13);
            this.txtTenKT.Name = "txtTenKT";
            this.txtTenKT.PasswordChar = '\0';
            this.txtTenKT.PlaceholderText = "";
            this.txtTenKT.SelectedText = "";
            this.txtTenKT.ShadowDecoration.Parent = this.txtTenKT;
            this.txtTenKT.Size = new System.Drawing.Size(200, 36);
            this.txtTenKT.TabIndex = 33;
            // 
            // txtMaKhoi
            // 
            this.txtMaKhoi.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaKhoi.DefaultText = "";
            this.txtMaKhoi.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtMaKhoi.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtMaKhoi.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMaKhoi.DisabledState.Parent = this.txtMaKhoi;
            this.txtMaKhoi.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMaKhoi.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMaKhoi.FocusedState.Parent = this.txtMaKhoi;
            this.txtMaKhoi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMaKhoi.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMaKhoi.HoverState.Parent = this.txtMaKhoi;
            this.txtMaKhoi.Location = new System.Drawing.Point(270, 53);
            this.txtMaKhoi.Name = "txtMaKhoi";
            this.txtMaKhoi.PasswordChar = '\0';
            this.txtMaKhoi.PlaceholderText = "";
            this.txtMaKhoi.SelectedText = "";
            this.txtMaKhoi.ShadowDecoration.Parent = this.txtMaKhoi;
            this.txtMaKhoi.Size = new System.Drawing.Size(200, 36);
            this.txtMaKhoi.TabIndex = 34;
            // 
            // dtpNgayThi
            // 
            this.dtpNgayThi.CheckedState.Parent = this.dtpNgayThi;
            this.dtpNgayThi.FillColor = System.Drawing.Color.White;
            this.dtpNgayThi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpNgayThi.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpNgayThi.HoverState.Parent = this.dtpNgayThi;
            this.dtpNgayThi.Location = new System.Drawing.Point(321, 14);
            this.dtpNgayThi.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpNgayThi.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpNgayThi.Name = "dtpNgayThi";
            this.dtpNgayThi.ShadowDecoration.Parent = this.dtpNgayThi;
            this.dtpNgayThi.Size = new System.Drawing.Size(343, 36);
            this.dtpNgayThi.TabIndex = 38;
            this.dtpNgayThi.Value = new System.DateTime(2020, 12, 22, 16, 20, 51, 596);
            // 
            // frmThemKT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(984, 618);
            this.Controls.Add(this.gbTongSoThiSinh);
            this.Controls.Add(this.gbTongSoDeThi);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox6);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmThemKT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tạo mới một kỳ thi";
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.gbTongSoDeThi.ResumeLayout(false);
            this.gbTongSoDeThi.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDT)).EndInit();
            this.gbTongSoThiSinh.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoHocSinh)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gbTongSoDeThi;
        private System.Windows.Forms.DataGridView dgvDT;
        private System.Windows.Forms.Label lblTgBatDau;
        private System.Windows.Forms.GroupBox gbTongSoThiSinh;
        private System.Windows.Forms.DataGridView dgvHS;
        private System.Windows.Forms.NumericUpDown nudSoHocSinh;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Button btnThemKT;
        private Guna.UI2.WinForms.Guna2TextBox txtMaKhoi;
        private Guna.UI2.WinForms.Guna2TextBox txtTenKT;
        private Guna.UI2.WinForms.Guna2Button btnChonHetHS;
        private Guna.UI2.WinForms.Guna2Button btnRdHs;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpNgayThi;
    }
}