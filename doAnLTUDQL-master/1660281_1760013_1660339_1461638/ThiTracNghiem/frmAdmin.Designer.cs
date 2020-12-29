namespace ThiTracNghiem
{
    partial class frmAdmin
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsLblHoTenAd = new System.Windows.Forms.ToolStripLabel();
            this.tsBtnDangXuat = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpQLCH = new System.Windows.Forms.TabPage();
            this.gbND = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.qlndBtnExport = new Guna.UI2.WinForms.Guna2Button();
            this.qlndBtnImport = new Guna.UI2.WinForms.Guna2Button();
            this.qlndCbLoaiND = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.qlndBtnSua = new Guna.UI2.WinForms.Guna2Button();
            this.qlndDtpNgaySinh = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.qlndBtnThem = new Guna.UI2.WinForms.Guna2Button();
            this.qlndCbLop = new Guna.UI2.WinForms.Guna2ComboBox();
            this.qlndCbKhoi = new Guna.UI2.WinForms.Guna2ComboBox();
            this.qlndCbChuyenMon = new Guna.UI2.WinForms.Guna2ComboBox();
            this.qlndTxtHoTen = new Guna.UI2.WinForms.Guna2TextBox();
            this.qlndTxtMaND = new Guna.UI2.WinForms.Guna2TextBox();
            this.qlndDgvCTGiangDay = new System.Windows.Forms.DataGridView();
            this.lblCTGiangDay = new System.Windows.Forms.Label();
            this.lblChuyenMon = new System.Windows.Forms.Label();
            this.lblLop = new System.Windows.Forms.Label();
            this.lblNgaySinh = new System.Windows.Forms.Label();
            this.lblMaSo = new System.Windows.Forms.Label();
            this.lblKhoi = new System.Windows.Forms.Label();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.tpQLDT = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.qlhtCbDbName = new System.Windows.Forms.ComboBox();
            this.qlhtLblDbName = new System.Windows.Forms.Label();
            this.qlhtCbAuthentication = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.qlhtTxtPassword = new System.Windows.Forms.TextBox();
            this.qlhtTxtUsername = new System.Windows.Forms.TextBox();
            this.qlhtLblUsername = new System.Windows.Forms.Label();
            this.qlhtLblPassword = new System.Windows.Forms.Label();
            this.qlhtCbServerName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.qlhtBtnConnect = new Guna.UI2.WinForms.Guna2Button();
            this.qlhtBtnTestConnection = new Guna.UI2.WinForms.Guna2Button();
            this.qlndDgvND = new Guna.UI2.WinForms.Guna2DataGridView();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpQLCH.SuspendLayout();
            this.gbND.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qlndDgvCTGiangDay)).BeginInit();
            this.tpQLDT.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qlndDgvND)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsLblHoTenAd,
            this.tsBtnDangXuat});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1344, 27);
            this.toolStrip1.TabIndex = 34;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsLblHoTenAd
            // 
            this.tsLblHoTenAd.Name = "tsLblHoTenAd";
            this.tsLblHoTenAd.Size = new System.Drawing.Size(69, 24);
            this.tsLblHoTenAd.Text = "Xin chào:";
            // 
            // tsBtnDangXuat
            // 
            this.tsBtnDangXuat.Image = global::ThiTracNghiem.Properties.Resources.logout;
            this.tsBtnDangXuat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnDangXuat.Name = "tsBtnDangXuat";
            this.tsBtnDangXuat.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tsBtnDangXuat.Size = new System.Drawing.Size(101, 24);
            this.tsBtnDangXuat.Text = "Đăng xuất";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpQLCH);
            this.tabControl1.Controls.Add(this.tpQLDT);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.ItemSize = new System.Drawing.Size(86, 18);
            this.tabControl1.Location = new System.Drawing.Point(0, 27);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1344, 596);
            this.tabControl1.TabIndex = 35;
            // 
            // tpQLCH
            // 
            this.tpQLCH.BackColor = System.Drawing.Color.Transparent;
            this.tpQLCH.Controls.Add(this.gbND);
            this.tpQLCH.Controls.Add(this.groupBox2);
            this.tpQLCH.Controls.Add(this.groupBox4);
            this.tpQLCH.Location = new System.Drawing.Point(4, 22);
            this.tpQLCH.Margin = new System.Windows.Forms.Padding(4);
            this.tpQLCH.Name = "tpQLCH";
            this.tpQLCH.Padding = new System.Windows.Forms.Padding(4);
            this.tpQLCH.Size = new System.Drawing.Size(1336, 570);
            this.tpQLCH.TabIndex = 0;
            this.tpQLCH.Text = "Quản lý người dùng";
            // 
            // gbND
            // 
            this.gbND.BackColor = System.Drawing.Color.LightGray;
            this.gbND.Controls.Add(this.qlndDgvND);
            this.gbND.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbND.Location = new System.Drawing.Point(490, 17);
            this.gbND.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbND.Name = "gbND";
            this.gbND.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbND.Size = new System.Drawing.Size(838, 553);
            this.gbND.TabIndex = 32;
            this.gbND.TabStop = false;
            this.gbND.Text = "Tổng số người dùng:";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.LightGray;
            this.groupBox2.Controls.Add(this.qlndBtnExport);
            this.groupBox2.Controls.Add(this.qlndBtnImport);
            this.groupBox2.Controls.Add(this.qlndCbLoaiND);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(19, 17);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(465, 167);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            // 
            // qlndBtnExport
            // 
            this.qlndBtnExport.BorderRadius = 20;
            this.qlndBtnExport.CheckedState.Parent = this.qlndBtnExport;
            this.qlndBtnExport.CustomImages.Parent = this.qlndBtnExport;
            this.qlndBtnExport.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.qlndBtnExport.ForeColor = System.Drawing.Color.White;
            this.qlndBtnExport.HoverState.Parent = this.qlndBtnExport;
            this.qlndBtnExport.Location = new System.Drawing.Point(238, 86);
            this.qlndBtnExport.Name = "qlndBtnExport";
            this.qlndBtnExport.ShadowDecoration.Parent = this.qlndBtnExport;
            this.qlndBtnExport.Size = new System.Drawing.Size(180, 45);
            this.qlndBtnExport.TabIndex = 35;
            this.qlndBtnExport.Text = "Xuất từ excel";
            // 
            // qlndBtnImport
            // 
            this.qlndBtnImport.BorderRadius = 20;
            this.qlndBtnImport.CheckedState.Parent = this.qlndBtnImport;
            this.qlndBtnImport.CustomImages.Parent = this.qlndBtnImport;
            this.qlndBtnImport.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.qlndBtnImport.ForeColor = System.Drawing.Color.White;
            this.qlndBtnImport.HoverState.Parent = this.qlndBtnImport;
            this.qlndBtnImport.Location = new System.Drawing.Point(39, 86);
            this.qlndBtnImport.Name = "qlndBtnImport";
            this.qlndBtnImport.ShadowDecoration.Parent = this.qlndBtnImport;
            this.qlndBtnImport.Size = new System.Drawing.Size(180, 45);
            this.qlndBtnImport.TabIndex = 34;
            this.qlndBtnImport.Text = "Nhập từ excel";
            this.qlndBtnImport.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // qlndCbLoaiND
            // 
            this.qlndCbLoaiND.BackColor = System.Drawing.Color.Transparent;
            this.qlndCbLoaiND.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.qlndCbLoaiND.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.qlndCbLoaiND.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.qlndCbLoaiND.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.qlndCbLoaiND.FocusedState.Parent = this.qlndCbLoaiND;
            this.qlndCbLoaiND.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.qlndCbLoaiND.ForeColor = System.Drawing.Color.Black;
            this.qlndCbLoaiND.HoverState.Parent = this.qlndCbLoaiND;
            this.qlndCbLoaiND.ItemHeight = 30;
            this.qlndCbLoaiND.ItemsAppearance.Parent = this.qlndCbLoaiND;
            this.qlndCbLoaiND.Location = new System.Drawing.Point(194, 22);
            this.qlndCbLoaiND.Name = "qlndCbLoaiND";
            this.qlndCbLoaiND.ShadowDecoration.Parent = this.qlndCbLoaiND;
            this.qlndCbLoaiND.Size = new System.Drawing.Size(224, 36);
            this.qlndCbLoaiND.TabIndex = 33;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(35, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 22);
            this.label3.TabIndex = 32;
            this.label3.Text = "Loại người dùng:";
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.LightGray;
            this.groupBox4.Controls.Add(this.qlndBtnSua);
            this.groupBox4.Controls.Add(this.qlndDtpNgaySinh);
            this.groupBox4.Controls.Add(this.qlndBtnThem);
            this.groupBox4.Controls.Add(this.qlndCbLop);
            this.groupBox4.Controls.Add(this.qlndCbKhoi);
            this.groupBox4.Controls.Add(this.qlndCbChuyenMon);
            this.groupBox4.Controls.Add(this.qlndTxtHoTen);
            this.groupBox4.Controls.Add(this.qlndTxtMaND);
            this.groupBox4.Controls.Add(this.qlndDgvCTGiangDay);
            this.groupBox4.Controls.Add(this.lblCTGiangDay);
            this.groupBox4.Controls.Add(this.lblChuyenMon);
            this.groupBox4.Controls.Add(this.lblLop);
            this.groupBox4.Controls.Add(this.lblNgaySinh);
            this.groupBox4.Controls.Add(this.lblMaSo);
            this.groupBox4.Controls.Add(this.lblKhoi);
            this.groupBox4.Controls.Add(this.lblHoTen);
            this.groupBox4.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(19, 190);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Size = new System.Drawing.Size(465, 378);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Thông tin chi tiết";
            // 
            // qlndBtnSua
            // 
            this.qlndBtnSua.BorderRadius = 20;
            this.qlndBtnSua.CheckedState.Parent = this.qlndBtnSua;
            this.qlndBtnSua.CustomImages.Parent = this.qlndBtnSua;
            this.qlndBtnSua.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.qlndBtnSua.ForeColor = System.Drawing.Color.White;
            this.qlndBtnSua.HoverState.Parent = this.qlndBtnSua;
            this.qlndBtnSua.Location = new System.Drawing.Point(242, 328);
            this.qlndBtnSua.Name = "qlndBtnSua";
            this.qlndBtnSua.ShadowDecoration.Parent = this.qlndBtnSua;
            this.qlndBtnSua.Size = new System.Drawing.Size(180, 45);
            this.qlndBtnSua.TabIndex = 37;
            this.qlndBtnSua.Text = "Cập Nhật";
            this.qlndBtnSua.Click += new System.EventHandler(this.qlndBtnSua_Click);
            // 
            // qlndDtpNgaySinh
            // 
            this.qlndDtpNgaySinh.CheckedState.Parent = this.qlndDtpNgaySinh;
            this.qlndDtpNgaySinh.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.qlndDtpNgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.qlndDtpNgaySinh.HoverState.Parent = this.qlndDtpNgaySinh;
            this.qlndDtpNgaySinh.Location = new System.Drawing.Point(185, 106);
            this.qlndDtpNgaySinh.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.qlndDtpNgaySinh.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.qlndDtpNgaySinh.Name = "qlndDtpNgaySinh";
            this.qlndDtpNgaySinh.ShadowDecoration.Parent = this.qlndDtpNgaySinh;
            this.qlndDtpNgaySinh.Size = new System.Drawing.Size(260, 36);
            this.qlndDtpNgaySinh.TabIndex = 48;
            this.qlndDtpNgaySinh.Value = new System.DateTime(2020, 12, 22, 14, 28, 40, 70);
            // 
            // qlndBtnThem
            // 
            this.qlndBtnThem.BorderRadius = 20;
            this.qlndBtnThem.CheckedState.Parent = this.qlndBtnThem;
            this.qlndBtnThem.CustomImages.Parent = this.qlndBtnThem;
            this.qlndBtnThem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.qlndBtnThem.ForeColor = System.Drawing.Color.White;
            this.qlndBtnThem.HoverState.Parent = this.qlndBtnThem;
            this.qlndBtnThem.Location = new System.Drawing.Point(39, 328);
            this.qlndBtnThem.Name = "qlndBtnThem";
            this.qlndBtnThem.ShadowDecoration.Parent = this.qlndBtnThem;
            this.qlndBtnThem.Size = new System.Drawing.Size(180, 45);
            this.qlndBtnThem.TabIndex = 36;
            this.qlndBtnThem.Text = "Thêm";
            this.qlndBtnThem.Click += new System.EventHandler(this.qlndBtnThem_Click);
            // 
            // qlndCbLop
            // 
            this.qlndCbLop.BackColor = System.Drawing.Color.Transparent;
            this.qlndCbLop.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.qlndCbLop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.qlndCbLop.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.qlndCbLop.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.qlndCbLop.FocusedState.Parent = this.qlndCbLop;
            this.qlndCbLop.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.qlndCbLop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.qlndCbLop.HoverState.Parent = this.qlndCbLop;
            this.qlndCbLop.ItemHeight = 30;
            this.qlndCbLop.ItemsAppearance.Parent = this.qlndCbLop;
            this.qlndCbLop.Location = new System.Drawing.Point(183, 148);
            this.qlndCbLop.Name = "qlndCbLop";
            this.qlndCbLop.ShadowDecoration.Parent = this.qlndCbLop;
            this.qlndCbLop.Size = new System.Drawing.Size(262, 36);
            this.qlndCbLop.TabIndex = 47;
            // 
            // qlndCbKhoi
            // 
            this.qlndCbKhoi.BackColor = System.Drawing.Color.Transparent;
            this.qlndCbKhoi.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.qlndCbKhoi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.qlndCbKhoi.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.qlndCbKhoi.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.qlndCbKhoi.FocusedState.Parent = this.qlndCbKhoi;
            this.qlndCbKhoi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.qlndCbKhoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.qlndCbKhoi.HoverState.Parent = this.qlndCbKhoi;
            this.qlndCbKhoi.ItemHeight = 30;
            this.qlndCbKhoi.ItemsAppearance.Parent = this.qlndCbKhoi;
            this.qlndCbKhoi.Location = new System.Drawing.Point(183, 200);
            this.qlndCbKhoi.Name = "qlndCbKhoi";
            this.qlndCbKhoi.ShadowDecoration.Parent = this.qlndCbKhoi;
            this.qlndCbKhoi.Size = new System.Drawing.Size(262, 36);
            this.qlndCbKhoi.TabIndex = 46;
            // 
            // qlndCbChuyenMon
            // 
            this.qlndCbChuyenMon.BackColor = System.Drawing.Color.Transparent;
            this.qlndCbChuyenMon.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.qlndCbChuyenMon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.qlndCbChuyenMon.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.qlndCbChuyenMon.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.qlndCbChuyenMon.FocusedState.Parent = this.qlndCbChuyenMon;
            this.qlndCbChuyenMon.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.qlndCbChuyenMon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.qlndCbChuyenMon.HoverState.Parent = this.qlndCbChuyenMon;
            this.qlndCbChuyenMon.ItemHeight = 30;
            this.qlndCbChuyenMon.ItemsAppearance.Parent = this.qlndCbChuyenMon;
            this.qlndCbChuyenMon.Location = new System.Drawing.Point(183, 148);
            this.qlndCbChuyenMon.Name = "qlndCbChuyenMon";
            this.qlndCbChuyenMon.ShadowDecoration.Parent = this.qlndCbChuyenMon;
            this.qlndCbChuyenMon.Size = new System.Drawing.Size(262, 36);
            this.qlndCbChuyenMon.TabIndex = 36;
            this.qlndCbChuyenMon.SelectedIndexChanged += new System.EventHandler(this.qlndCbChuyenMon_SelectedIndexChanged);
            // 
            // qlndTxtHoTen
            // 
            this.qlndTxtHoTen.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.qlndTxtHoTen.DefaultText = "";
            this.qlndTxtHoTen.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.qlndTxtHoTen.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.qlndTxtHoTen.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.qlndTxtHoTen.DisabledState.Parent = this.qlndTxtHoTen;
            this.qlndTxtHoTen.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.qlndTxtHoTen.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.qlndTxtHoTen.FocusedState.Parent = this.qlndTxtHoTen;
            this.qlndTxtHoTen.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.qlndTxtHoTen.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.qlndTxtHoTen.HoverState.Parent = this.qlndTxtHoTen;
            this.qlndTxtHoTen.Location = new System.Drawing.Point(185, 62);
            this.qlndTxtHoTen.Name = "qlndTxtHoTen";
            this.qlndTxtHoTen.PasswordChar = '\0';
            this.qlndTxtHoTen.PlaceholderText = "";
            this.qlndTxtHoTen.SelectedText = "";
            this.qlndTxtHoTen.ShadowDecoration.Parent = this.qlndTxtHoTen;
            this.qlndTxtHoTen.Size = new System.Drawing.Size(260, 36);
            this.qlndTxtHoTen.TabIndex = 45;
            // 
            // qlndTxtMaND
            // 
            this.qlndTxtMaND.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.qlndTxtMaND.DefaultText = "";
            this.qlndTxtMaND.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.qlndTxtMaND.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.qlndTxtMaND.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.qlndTxtMaND.DisabledState.Parent = this.qlndTxtMaND;
            this.qlndTxtMaND.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.qlndTxtMaND.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.qlndTxtMaND.FocusedState.Parent = this.qlndTxtMaND;
            this.qlndTxtMaND.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.qlndTxtMaND.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.qlndTxtMaND.HoverState.Parent = this.qlndTxtMaND;
            this.qlndTxtMaND.Location = new System.Drawing.Point(185, 20);
            this.qlndTxtMaND.Name = "qlndTxtMaND";
            this.qlndTxtMaND.PasswordChar = '\0';
            this.qlndTxtMaND.PlaceholderText = "";
            this.qlndTxtMaND.SelectedText = "";
            this.qlndTxtMaND.ShadowDecoration.Parent = this.qlndTxtMaND;
            this.qlndTxtMaND.Size = new System.Drawing.Size(260, 36);
            this.qlndTxtMaND.TabIndex = 44;
            // 
            // qlndDgvCTGiangDay
            // 
            this.qlndDgvCTGiangDay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.qlndDgvCTGiangDay.ColumnHeadersVisible = false;
            this.qlndDgvCTGiangDay.Location = new System.Drawing.Point(183, 200);
            this.qlndDgvCTGiangDay.Margin = new System.Windows.Forms.Padding(4);
            this.qlndDgvCTGiangDay.Name = "qlndDgvCTGiangDay";
            this.qlndDgvCTGiangDay.RowHeadersVisible = false;
            this.qlndDgvCTGiangDay.RowHeadersWidth = 51;
            this.qlndDgvCTGiangDay.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.qlndDgvCTGiangDay.Size = new System.Drawing.Size(262, 111);
            this.qlndDgvCTGiangDay.TabIndex = 7;
            this.qlndDgvCTGiangDay.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.qlndDgvCTGiangDay_CellContentClick);
            // 
            // lblCTGiangDay
            // 
            this.lblCTGiangDay.AutoSize = true;
            this.lblCTGiangDay.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCTGiangDay.Location = new System.Drawing.Point(39, 200);
            this.lblCTGiangDay.Name = "lblCTGiangDay";
            this.lblCTGiangDay.Size = new System.Drawing.Size(137, 22);
            this.lblCTGiangDay.TabIndex = 43;
            this.lblCTGiangDay.Text = "Đang dạy: 3 lớp";
            // 
            // lblChuyenMon
            // 
            this.lblChuyenMon.AutoSize = true;
            this.lblChuyenMon.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChuyenMon.Location = new System.Drawing.Point(40, 158);
            this.lblChuyenMon.Name = "lblChuyenMon";
            this.lblChuyenMon.Size = new System.Drawing.Size(112, 22);
            this.lblChuyenMon.TabIndex = 40;
            this.lblChuyenMon.Text = "Chuyên môn:";
            // 
            // lblLop
            // 
            this.lblLop.AutoSize = true;
            this.lblLop.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLop.Location = new System.Drawing.Point(40, 200);
            this.lblLop.Name = "lblLop";
            this.lblLop.Size = new System.Drawing.Size(48, 22);
            this.lblLop.TabIndex = 20;
            this.lblLop.Text = "Lớp:";
            // 
            // lblNgaySinh
            // 
            this.lblNgaySinh.AutoSize = true;
            this.lblNgaySinh.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNgaySinh.Location = new System.Drawing.Point(40, 120);
            this.lblNgaySinh.Name = "lblNgaySinh";
            this.lblNgaySinh.Size = new System.Drawing.Size(94, 22);
            this.lblNgaySinh.TabIndex = 19;
            this.lblNgaySinh.Text = "Ngày sinh:";
            // 
            // lblMaSo
            // 
            this.lblMaSo.AutoSize = true;
            this.lblMaSo.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaSo.Location = new System.Drawing.Point(40, 34);
            this.lblMaSo.Name = "lblMaSo";
            this.lblMaSo.Size = new System.Drawing.Size(65, 22);
            this.lblMaSo.TabIndex = 18;
            this.lblMaSo.Text = "Mã số:";
            // 
            // lblKhoi
            // 
            this.lblKhoi.AutoSize = true;
            this.lblKhoi.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKhoi.Location = new System.Drawing.Point(40, 158);
            this.lblKhoi.Name = "lblKhoi";
            this.lblKhoi.Size = new System.Drawing.Size(55, 22);
            this.lblKhoi.TabIndex = 17;
            this.lblKhoi.Text = "Khối:";
            // 
            // lblHoTen
            // 
            this.lblHoTen.AutoSize = true;
            this.lblHoTen.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHoTen.Location = new System.Drawing.Point(40, 76);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new System.Drawing.Size(68, 22);
            this.lblHoTen.TabIndex = 15;
            this.lblHoTen.Text = "Họ tên:";
            // 
            // tpQLDT
            // 
            this.tpQLDT.BackColor = System.Drawing.Color.White;
            this.tpQLDT.Controls.Add(this.groupBox1);
            this.tpQLDT.Location = new System.Drawing.Point(4, 22);
            this.tpQLDT.Margin = new System.Windows.Forms.Padding(4);
            this.tpQLDT.Name = "tpQLDT";
            this.tpQLDT.Padding = new System.Windows.Forms.Padding(4);
            this.tpQLDT.Size = new System.Drawing.Size(1336, 570);
            this.tpQLDT.TabIndex = 1;
            this.tpQLDT.Text = "Quản lý hệ thống";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LightGray;
            this.groupBox1.Controls.Add(this.qlhtBtnConnect);
            this.groupBox1.Controls.Add(this.qlhtBtnTestConnection);
            this.groupBox1.Controls.Add(this.qlhtCbDbName);
            this.groupBox1.Controls.Add(this.qlhtLblDbName);
            this.groupBox1.Controls.Add(this.qlhtCbAuthentication);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.qlhtTxtPassword);
            this.groupBox1.Controls.Add(this.qlhtTxtUsername);
            this.groupBox1.Controls.Add(this.qlhtLblUsername);
            this.groupBox1.Controls.Add(this.qlhtLblPassword);
            this.groupBox1.Controls.Add(this.qlhtCbServerName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(271, 164);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(798, 371);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Kết nối cơ sở dữ liệu";
            // 
            // qlhtCbDbName
            // 
            this.qlhtCbDbName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.qlhtCbDbName.FormattingEnabled = true;
            this.qlhtCbDbName.Location = new System.Drawing.Point(223, 165);
            this.qlhtCbDbName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.qlhtCbDbName.MaxDropDownItems = 5;
            this.qlhtCbDbName.Name = "qlhtCbDbName";
            this.qlhtCbDbName.Size = new System.Drawing.Size(415, 27);
            this.qlhtCbDbName.TabIndex = 41;
            // 
            // qlhtLblDbName
            // 
            this.qlhtLblDbName.AutoSize = true;
            this.qlhtLblDbName.Location = new System.Drawing.Point(35, 169);
            this.qlhtLblDbName.Name = "qlhtLblDbName";
            this.qlhtLblDbName.Size = new System.Drawing.Size(142, 19);
            this.qlhtLblDbName.TabIndex = 42;
            this.qlhtLblDbName.Text = "Chọn cơ sở dữ liệu:";
            // 
            // qlhtCbAuthentication
            // 
            this.qlhtCbAuthentication.FormattingEnabled = true;
            this.qlhtCbAuthentication.Items.AddRange(new object[] {
            "Window",
            "Sql"});
            this.qlhtCbAuthentication.Location = new System.Drawing.Point(223, 66);
            this.qlhtCbAuthentication.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.qlhtCbAuthentication.MaxDropDownItems = 5;
            this.qlhtCbAuthentication.Name = "qlhtCbAuthentication";
            this.qlhtCbAuthentication.Size = new System.Drawing.Size(415, 27);
            this.qlhtCbAuthentication.TabIndex = 39;
            this.qlhtCbAuthentication.Text = "Window";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(129, 19);
            this.label5.TabIndex = 38;
            this.label5.Text = "Chọn phân quyền:";
            // 
            // qlhtTxtPassword
            // 
            this.qlhtTxtPassword.Location = new System.Drawing.Point(223, 132);
            this.qlhtTxtPassword.Margin = new System.Windows.Forms.Padding(4);
            this.qlhtTxtPassword.Name = "qlhtTxtPassword";
            this.qlhtTxtPassword.PasswordChar = '$';
            this.qlhtTxtPassword.Size = new System.Drawing.Size(415, 26);
            this.qlhtTxtPassword.TabIndex = 37;
            this.qlhtTxtPassword.Text = "123456789";
            this.qlhtTxtPassword.UseSystemPasswordChar = true;
            // 
            // qlhtTxtUsername
            // 
            this.qlhtTxtUsername.Location = new System.Drawing.Point(223, 101);
            this.qlhtTxtUsername.Margin = new System.Windows.Forms.Padding(4);
            this.qlhtTxtUsername.Name = "qlhtTxtUsername";
            this.qlhtTxtUsername.Size = new System.Drawing.Size(415, 26);
            this.qlhtTxtUsername.TabIndex = 36;
            this.qlhtTxtUsername.Text = "Điền tên đăng nhập";
            // 
            // qlhtLblUsername
            // 
            this.qlhtLblUsername.AutoSize = true;
            this.qlhtLblUsername.Location = new System.Drawing.Point(35, 105);
            this.qlhtLblUsername.Name = "qlhtLblUsername";
            this.qlhtLblUsername.Size = new System.Drawing.Size(110, 19);
            this.qlhtLblUsername.TabIndex = 35;
            this.qlhtLblUsername.Text = "Tên đăng nhập:";
            // 
            // qlhtLblPassword
            // 
            this.qlhtLblPassword.AutoSize = true;
            this.qlhtLblPassword.Location = new System.Drawing.Point(36, 135);
            this.qlhtLblPassword.Name = "qlhtLblPassword";
            this.qlhtLblPassword.Size = new System.Drawing.Size(76, 19);
            this.qlhtLblPassword.TabIndex = 34;
            this.qlhtLblPassword.Text = "Mật khẩu:";
            // 
            // qlhtCbServerName
            // 
            this.qlhtCbServerName.FormattingEnabled = true;
            this.qlhtCbServerName.Location = new System.Drawing.Point(223, 32);
            this.qlhtCbServerName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.qlhtCbServerName.MaxDropDownItems = 5;
            this.qlhtCbServerName.Name = "qlhtCbServerName";
            this.qlhtCbServerName.Size = new System.Drawing.Size(415, 27);
            this.qlhtCbServerName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 19);
            this.label1.TabIndex = 32;
            this.label1.Text = "Chọn máy chủ:";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // qlhtBtnConnect
            // 
            this.qlhtBtnConnect.BorderRadius = 20;
            this.qlhtBtnConnect.CheckedState.Parent = this.qlhtBtnConnect;
            this.qlhtBtnConnect.CustomImages.Parent = this.qlhtBtnConnect;
            this.qlhtBtnConnect.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.qlhtBtnConnect.ForeColor = System.Drawing.Color.White;
            this.qlhtBtnConnect.HoverState.Parent = this.qlhtBtnConnect;
            this.qlhtBtnConnect.Location = new System.Drawing.Point(376, 272);
            this.qlhtBtnConnect.Name = "qlhtBtnConnect";
            this.qlhtBtnConnect.ShadowDecoration.Parent = this.qlhtBtnConnect;
            this.qlhtBtnConnect.Size = new System.Drawing.Size(180, 45);
            this.qlhtBtnConnect.TabIndex = 44;
            this.qlhtBtnConnect.Text = "Kết nối";
            // 
            // qlhtBtnTestConnection
            // 
            this.qlhtBtnTestConnection.BorderRadius = 20;
            this.qlhtBtnTestConnection.CheckedState.Parent = this.qlhtBtnTestConnection;
            this.qlhtBtnTestConnection.CustomImages.Parent = this.qlhtBtnTestConnection;
            this.qlhtBtnTestConnection.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.qlhtBtnTestConnection.ForeColor = System.Drawing.Color.White;
            this.qlhtBtnTestConnection.HoverState.Parent = this.qlhtBtnTestConnection;
            this.qlhtBtnTestConnection.Location = new System.Drawing.Point(172, 272);
            this.qlhtBtnTestConnection.Name = "qlhtBtnTestConnection";
            this.qlhtBtnTestConnection.ShadowDecoration.Parent = this.qlhtBtnTestConnection;
            this.qlhtBtnTestConnection.Size = new System.Drawing.Size(180, 45);
            this.qlhtBtnTestConnection.TabIndex = 43;
            this.qlhtBtnTestConnection.Text = "Kiểm tra kết nối";
            // 
            // qlndDgvND
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.qlndDgvND.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.qlndDgvND.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.qlndDgvND.BackgroundColor = System.Drawing.Color.White;
            this.qlndDgvND.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.qlndDgvND.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.qlndDgvND.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.qlndDgvND.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.qlndDgvND.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.qlndDgvND.DefaultCellStyle = dataGridViewCellStyle3;
            this.qlndDgvND.EnableHeadersVisualStyles = false;
            this.qlndDgvND.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.qlndDgvND.Location = new System.Drawing.Point(6, 30);
            this.qlndDgvND.Name = "qlndDgvND";
            this.qlndDgvND.RowHeadersVisible = false;
            this.qlndDgvND.RowHeadersWidth = 51;
            this.qlndDgvND.RowTemplate.Height = 24;
            this.qlndDgvND.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.qlndDgvND.Size = new System.Drawing.Size(826, 516);
            this.qlndDgvND.TabIndex = 11;
            this.qlndDgvND.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.Default;
            this.qlndDgvND.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.qlndDgvND.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.qlndDgvND.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.qlndDgvND.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.qlndDgvND.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.qlndDgvND.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.qlndDgvND.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.qlndDgvND.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.qlndDgvND.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.qlndDgvND.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.qlndDgvND.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.qlndDgvND.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.qlndDgvND.ThemeStyle.HeaderStyle.Height = 4;
            this.qlndDgvND.ThemeStyle.ReadOnly = false;
            this.qlndDgvND.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.qlndDgvND.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.qlndDgvND.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.qlndDgvND.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.qlndDgvND.ThemeStyle.RowsStyle.Height = 24;
            this.qlndDgvND.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.qlndDgvND.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // frmAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 674);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmAdmin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản trị viên";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpQLCH.ResumeLayout(false);
            this.gbND.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qlndDgvCTGiangDay)).EndInit();
            this.tpQLDT.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qlndDgvND)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel tsLblHoTenAd;
        private System.Windows.Forms.ToolStripButton tsBtnDangXuat;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpQLCH;
        private System.Windows.Forms.GroupBox gbND;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblChuyenMon;
        private System.Windows.Forms.Label lblLop;
        private System.Windows.Forms.Label lblNgaySinh;
        private System.Windows.Forms.Label lblMaSo;
        private System.Windows.Forms.Label lblKhoi;
        private System.Windows.Forms.Label lblHoTen;
        private System.Windows.Forms.TabPage tpQLDT;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Label lblCTGiangDay;
        private System.Windows.Forms.DataGridView qlndDgvCTGiangDay;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox qlhtCbDbName;
        private System.Windows.Forms.Label qlhtLblDbName;
        private System.Windows.Forms.ComboBox qlhtCbAuthentication;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox qlhtTxtPassword;
        private System.Windows.Forms.TextBox qlhtTxtUsername;
        private System.Windows.Forms.Label qlhtLblUsername;
        private System.Windows.Forms.Label qlhtLblPassword;
        private System.Windows.Forms.ComboBox qlhtCbServerName;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2ComboBox qlndCbLoaiND;
        private Guna.UI2.WinForms.Guna2Button qlndBtnExport;
        private Guna.UI2.WinForms.Guna2Button qlndBtnImport;
        private Guna.UI2.WinForms.Guna2ComboBox qlndCbChuyenMon;
        private Guna.UI2.WinForms.Guna2TextBox qlndTxtHoTen;
        private Guna.UI2.WinForms.Guna2TextBox qlndTxtMaND;
        private Guna.UI2.WinForms.Guna2ComboBox qlndCbLop;
        private Guna.UI2.WinForms.Guna2ComboBox qlndCbKhoi;
        private Guna.UI2.WinForms.Guna2DateTimePicker qlndDtpNgaySinh;
        private Guna.UI2.WinForms.Guna2Button qlndBtnSua;
        private Guna.UI2.WinForms.Guna2Button qlndBtnThem;
        private Guna.UI2.WinForms.Guna2Button qlhtBtnConnect;
        private Guna.UI2.WinForms.Guna2Button qlhtBtnTestConnection;
        private Guna.UI2.WinForms.Guna2DataGridView qlndDgvND;
    }
}