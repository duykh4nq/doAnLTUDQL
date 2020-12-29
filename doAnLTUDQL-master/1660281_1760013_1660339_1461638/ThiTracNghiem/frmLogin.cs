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
using ThiTracNghiem;
using Guna.UI2.WinForms;

namespace ThiTracNghiem
{
    public partial class frmLogin : Form
    {
        static Form frm = null;
        static NguoiDung nguoiDung = null;

        public frmLogin()
        {
            InitializeComponent();

            txtTenDangNhap.Validating += txtTenDangNhap_Validating;
            txtMatKhau.Validating += txtTenDangNhap_Validating;
            txtTenDangNhap.GotFocus += TxtTenDangNhap_GotFocus;
            txtMatKhau.GotFocus += TxtTenDangNhap_GotFocus;
            llblDangKy.Click += LlblDangKy_Click;
            //this.Paint += (s, e) =>
            // {
            //     Image img = ThiTracNghiem.Properties.Resources.hinh_nen_form_login;
            //     e.Graphics.DrawImage(img, groupBox1.Bounds);
            // };


            txtMatKhau.TextChanged += (s, e) =>
            {
                using (var qlttn = new QLTTNDataContext())
                {
                    nguoiDung = qlttn.NguoiDungs.Where(nd => nd.maND == txtTenDangNhap.Text && nd.MatKhau == txtMatKhau.Text).FirstOrDefault();
                    if (nguoiDung != null)
                    {
                        frm = null;
                        if (nguoiDung.maLND == "HS")
                        {
                            frm = new frmHocSinh(this, nguoiDung.HocSinh);
                        }
                        else if (nguoiDung.maLND == "GV")
                        {
                            frm = new frmGiaoVien(this, nguoiDung.GiaoVien);
                        }
                        else if (nguoiDung.maLND == "AD")
                        {
                            frm = new frmAdmin(this, nguoiDung);
                        }

                        if (frm != null)
                        {
                            frm.Show();
                            this.Hide();
                        }
                    }
                }
            };

        }

        private void LlblDangKy_Click(object sender, EventArgs e)
        {
            var frmdangky = new frmDangKy(this);
            frmdangky.Show();
        }

        private void TxtTenDangNhap_GotFocus(object sender, EventArgs e)
        {
             Guna2TextBox t = sender as Guna2TextBox;
              t.SelectAll();
        }

        private void txtTenDangNhap_Validating(object sender, CancelEventArgs e)
        {
            var strInput1 = txtTenDangNhap.Text;
            var strInput2 = txtMatKhau.Text;
            if (strInput1.Length == 0)
            {
                errorProviderMain.SetError(txtTenDangNhap, "not input");
            }else if (strInput2.Length == 0)
            {
                errorProviderMain.SetError(txtMatKhau, "not input");
            }
            else if (strInput2.Length == 0 && strInput1.Length == 0)
            {
                errorProviderMain.SetError(txtMatKhau, "not input");
                errorProviderMain.SetError(txtTenDangNhap, "not input");
            }
            else
            {
                errorProviderMain.SetError(txtMatKhau, "");
                errorProviderMain.SetError(txtTenDangNhap, "");
            }
        }

        private void txtMatKhau_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTenDangNhap_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMatKhau_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void llblDangKy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void llblDangKy_Click_1(object sender, EventArgs e)
        {

        }
    }
}
