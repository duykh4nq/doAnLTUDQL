using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThiTracNghiem
{
    public partial class frmDangKy : Form
    {
        AccessData acc = new AccessData();
        frmLogin frmlogin;
        public frmDangKy(frmLogin frmlogin)
        {
            this.frmlogin = frmlogin;
            InitializeComponent();
            qlndDgvDSLop.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "Chon",
                TrueValue = true,
                FalseValue = false,
                IndeterminateValue = false,
                Width = 20
            });

            loadCBLoaiND();
            loadCBKhoi();
            loadCBLop();
            loadCBChuyenMon();
            LoadDgvDSLop();
            cbLoaiND.SelectedIndexChanged += (s, e) =>
            {
                lblChuyenMon.Visible = false;
                lblCTGiangDay.Visible = false;
                lblHoTen.Visible = false;
                lblKhoi.Visible = false;
                lblLop.Visible = false;
                lblNgaySinh.Visible = false;

                qlndCbChuyenMon.Visible = false;
                qlndCbKhoi.Visible = false;
                qlndCbLop.Visible = false;
                qlndDgvDSLop.Visible = false;
                qlndTxtHoTen.Visible = false;
                qlndDtpNgaySinh.Visible = false;


                if (cbLoaiND.SelectedValue.ToString() == "HS")
                {
                    //lblChuyenMon.Visible = true;
                    //lblCTGiangDay.Visible = true;
                    lblHoTen.Visible = true;
                    lblKhoi.Visible = true;
                    lblLop.Visible = true;
                    lblNgaySinh.Visible = true;

                    //qlndCbChuyenMon.Visible = true;
                    qlndCbKhoi.Visible = true;
                    qlndCbLop.Visible = true;
                    //qlndDgvCTGiangDay.Visible = true;
                    qlndTxtHoTen.Visible = true;
                    qlndDtpNgaySinh.Visible = true;

                }
                else if (cbLoaiND.SelectedValue.ToString() == "GV")
                {
                    lblChuyenMon.Visible = true;
                    lblCTGiangDay.Visible = true;
                    lblHoTen.Visible = true;
                    //lblKhoi.Visible = true;
                    //lblLop.Visible = true;
                    lblNgaySinh.Visible = true;

                    qlndCbChuyenMon.Visible = true;
                    //qlndCbKhoi.Visible = true;
                    //qlndCbLop.Visible = true;
                    qlndDgvDSLop.Visible = true;
                    qlndTxtHoTen.Visible = true;
                    qlndDtpNgaySinh.Visible = true;
                }
                else if (cbLoaiND.SelectedValue.ToString() == "AD")
                {

                }

            };

            cbLoaiND.SelectedIndex = 2;
            btndangky.Click += btndangky_Click;
        }

        private void Btndangky_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void LoadDgvDSLop()
        {
            using (var qlttn = new QLTTNDataContext())
            {
                qlndDgvDSLop.DataSource = qlttn.LopHocQL.Select(lh => new { lh.maKhoi, lh.maLop }).ToList();
                qlndDgvDSLop.Columns["maKhoi"].Width = 60;
                qlndDgvDSLop.Columns["maLop"].Width = 60;

            }
        }

        private void loadCBChuyenMon()
        {
            using (var qlttn = new QLTTNDataContext())
            {
                qlndCbChuyenMon.DataSource = qlttn.MonHocQL.Select(mh => new { mh.maMH, mh.tenMH }).ToList();
                qlndCbChuyenMon.ValueMember = "maMH";
                qlndCbChuyenMon.DisplayMember = "tenMH";
            }
        }
        private void loadCBLop()
        {

            using (var qlttn = new QLTTNDataContext())
            {
                qlndCbLop.DataSource = qlttn.LopHocQL.Where(lh => lh.maKhoi == qlndCbKhoi.SelectedValue.ToString()).Select(lh => new { lh.maLop }).ToList();
                qlndCbLop.ValueMember = "maLop";
                qlndCbLop.DisplayMember = "maLop";
            }
        }

        private void loadCBKhoi()
        {
            using (var qlttn = new QLTTNDataContext())
            {
                qlndCbKhoi.DataSource = qlttn.KhoiLopQL.Select(kl => new { kl.maKhoi }).ToList();
                qlndCbKhoi.ValueMember = "maKhoi";
                qlndCbKhoi.DisplayMember = "maKhoi";
            }
        }

        private void loadCBLoaiND()
        {
            using (var qlttn = new QLTTNDataContext())
            {
                cbLoaiND.DataSource = qlttn.LoaiNguoiDungQL.Select(lnd => new { lnd.maLND, lnd.TenLND }).ToList();
                cbLoaiND.ValueMember = "maLND";
                cbLoaiND.DisplayMember = "TenLND";
            }
        }

        private void btndangky_Click(object sender, EventArgs e)
        {
            AccessData acc = new AccessData();

            using (var qlttn = new QLTTNDataContext())
            {
                qlttn.NguoiDungQL.InsertOnSubmit(new NguoiDung
                {
                    maND = txtmanguoidung.Text,
                    maLND = cbLoaiND.SelectedValue.ToString(),
                    MatKhau = txtmk.Text
                });
                qlttn.HocSinhQL.InsertOnSubmit(new HocSinh
                {
                    maHS = txtmanguoidung.Text,
                    HoTen = qlndTxtHoTen.Text,
                    NgaySinh = qlndDtpNgaySinh.Value,
                    maKhoi = qlndCbKhoi.SelectedValue.ToString(),
                    maLop = qlndCbLop.SelectedValue.ToString()
                });
                qlttn.GiaoVienQL.InsertOnSubmit(new GiaoVien
                {
                    maGV = txtmanguoidung.Text,
                    HoTen = qlndTxtHoTen.Text,
                    NgaySinh = qlndDtpNgaySinh.Value,
                    maMH = qlndCbChuyenMon.SelectedValue.ToString()

                });
                qlttn.SubmitChanges();
            }

            MessageBox.Show("Đăng Ký Thành Công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information); // Nếu đang ký thành công => Sẽ có thông báo Thành Công và đồng thời các TextBox sẽ mất giá trị do [B]ClearTextBox()[/B].
            ClearTextBox();

            frmlogin.Show();
            this.Close();
        }


        private void ClearTextBox()
        {
            txtmanguoidung.Clear();
            txtmk.Clear();
        }

        private void lblLop_Click(object sender, EventArgs e)
        {

        }
    }
}