using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Linq;
using System.IO;
using ClosedXML.Excel;
using System.Text.RegularExpressions;
using System.Threading;

namespace ThiTracNghiem
{
    public partial class frmGiaoVien : Form
    {
        private frmLogin frmlogin = null;
        private GiaoVien GV = null;
        private List<string> ListKL = null;
        private BindingSource BSCauHoi = new BindingSource();
        private BindingSource BSQlhsCauHoi = new BindingSource();
        private BindingSource BSHocSinh = new BindingSource();
        private BindingSource BSQLHSHocSinh = new BindingSource();
        private BindingSource BSHocSinhThu = new BindingSource();
        private BindingSource BSLichThi = new BindingSource();
        private BindingSource BSLichThiThu = new BindingSource();
        private BindingSource BSKyThi = new BindingSource();
        private BindingSource BSKyThiThu = new BindingSource();
        private BindingSource BSQLHSKyThi = new BindingSource();
        private BindingSource BSDapAn = new BindingSource();
        private BindingSource BSDethi = new BindingSource();

        private void LoadKhoiLop()
        {
            using (var QLTTN = new QLTTNDataContext())
            {
                ListKL = GV.CT_GiangDays.Select(CTGD => CTGD.maKhoi).Distinct().ToList();
                QLCbKhoiLop.DataSource = ListKL;
                QLCbKhoiLop.SelectedItem = QLCbKhoiLop.Items[0];
                string txt = QLCbKhoiLop.SelectedValue.ToString();
                txtHoTenGV.Text = GV.HoTen;
                QLDTLblNguoiTao.Text = GV.HoTen;
                QLDTLblNgayTao.Text = DateTime.UtcNow.ToString("dd-MM-yyyy");
                txtNgaySinhGV.Text = $"{GV.NgaySinh.Value.ToShortDateString()}";
                lblChuyenMon.Text = $"Chuyên môn: {QLTTN.MonHoc.Where(mh => mh.maMH == GV.maMH).Single().tenMH}";
            }
        }
        private void loadCbCauHoi()
        {
            using (var QLTTN = new QLTTNDataContext())
            { 

                var CauHoi = (QLTTN.CauHoi.Where(ch => ch.maKhoi == QLCbKhoiLop.SelectedValue.ToString() && ch.maMH == GV.maMH).Select(ch => new { ch.maCH, ch.NoiDung, ch.CapDo.TenCD, ch.maCD }).ToList());
                BSCauHoi.DataSource = CauHoi;

                if (BSCauHoi.Count > 0)
                {
                    QLCHCbDsCH.DataSource = BSCauHoi;
                    QLCHCbDsCH.DisplayMember = "NoiDung";
                    QLCHCbDsCH.ValueMember = "maCH";
                    QLCHCapDo.DataSource = QLTTN.CapDo.ToList();
                    QLCHCapDo.DisplayMember = "TenCD";
                    QLCHCapDo.ValueMember = "maCD";
                    var maCd = QLTTN.CauHoi.Where(ch => ch.maCH.ToString() == QLCHCbDsCH.SelectedValue.ToString()).Single().maCD;
                    QLCHCapDo.SelectedValue = maCd;

                    if (QLCHTxtCauHoi.DataBindings.Count == 0)
                    {
                        QLCHTxtCauHoi.DataBindings.Add("Text", BSCauHoi, "NoiDung", true, DataSourceUpdateMode.Never, "Null value");
                    }
                    if (QLCHCapDo.DataBindings.Count == 0)
                    {
                        QLCHCapDo.DataBindings.Add("SelectedValue", BSCauHoi, "maCD", true, DataSourceUpdateMode.Never, "Null value");
                        QLCHCapDo.DataBindings[0].Format += (s, e) =>
                        {
                            if (e.DesiredType == typeof(string))
                            {
                                int MaCapDo = int.Parse(e.Value.ToString());
                                e.Value = (QLCHCapDo.DataSource as List<CapDo>).Where(cd => cd.maCD == MaCapDo).FirstOrDefault().TenCD;
                            }
                        };
                        QLCHCapDo.DataBindings[0].Parse += (s, e) =>
                        {
                            if (e.DesiredType == typeof(int))
                            {
                                string noiDungCapDo = e.Value.ToString();
                                e.Value = (QLCHCapDo.DataSource as List<CapDo>).Where(cd => cd.TenCD == noiDungCapDo).FirstOrDefault().maCD;
                            }
                        };
                    }
                }
                else
                {
                    QLCHCapDo.DataBindings.Clear();
                    QLCHTxtCauHoi.DataBindings.Clear();
                    MessageBox.Show("Không có câu hỏi", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    QLCHCbDsCH.DataSource = null;
                }
            }
        }
        private void LoadQlCHDapAn()
        {
            using (var QLTTN = new QLTTNDataContext())
            {
                if (QLCHCbDsCH.Items.Count > 0)
                {
                    BSDapAn.DataSource = QLTTN.CauHoi.Where(ch => ch.maCH == int.Parse(QLCHCbDsCH.SelectedValue.ToString())).SingleOrDefault().DapAn.ToList();
                    QLCHDgvDsDA.DataSource = BSDapAn;

                    QLCHDgvDsDA.Columns["maCH"].Visible = false;
                    QLCHDgvDsDA.Columns["CauHoi"].Visible = false;
                    QLCHDgvDsDA.Columns["maDA"].Visible = false;
                    QLCHDgvDsDA.Columns["NoiDung"].DisplayIndex = 1;
                    QLCHDgvDsDA.Columns["NoiDung"].Width = 310;
                    QLCHDgvDsDA.Columns["NoiDung"].HeaderText = "Nội dung đáp án";
                    QLCHDgvDsDA.Columns["DungSai"].DisplayIndex = 2;
                    QLCHDgvDsDA.Columns["DungSai"].Width = 115;
                    QLCHDgvDsDA.Columns["DungSai"].HeaderText = "Tính chất đáp án";

                    if (QLCHTxtDapAn.DataBindings.Count == 0)
                    {
                        QLCHTxtDapAn.DataBindings.Add("Text", BSDapAn, "NoiDung", true, DataSourceUpdateMode.Never, "Null value");
                    }
                    if (QLCHCkbDungSai.DataBindings.Count == 0)
                    {
                        QLCHCkbDungSai.DataBindings.Add("Checked", BSDapAn, "DungSai", true, DataSourceUpdateMode.Never, false);
                    }
                }
                else
                {
                    QLCHTxtDapAn.DataBindings.Clear();
                    QLCHCkbDungSai.DataBindings.Clear();
                    MessageBox.Show("Không có đáp án", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    QLCHDgvDsDA.DataSource = null;
                }
            }
        }
        private void SetQLCH()
        {
            
            QLCHCbDsCH.SelectedIndexChanged += (s, e) =>
             {
                 LoadQlCHDapAn();

                 if (QLCHCbDsCH.SelectedValue == null)
                 {
                     return;
                 }
                 using (var QLTTN = new QLTTNDataContext())
                 {
                     if (QLTTN.DeThi.Count() == 0)
                     {
                         return;
                     }
                     QLCHCbDsCH.ValueMember = "maCH";
                     QLCHDTSDCH.DataSource = QLTTN.DeThi.Where(dt => dt.CT_DeThi.Where(ctdt => ctdt.maCH == int.Parse(QLCHCbDsCH.SelectedValue.ToString())).Count() > 0).Select(dt => new { dt.maDT, dt.TenDT }).ToList();
                 }
             };
            QLCHDTSDCH.Format += (s, e) =>
            {
                if (e.DesiredType == typeof(string))
                {
                    string str = e.Value.ToString();
                    str = str.Replace("{ maDT = ", "");
                    str = str.Replace(", TenDT = ", "-");
                    str = str.Remove(str.Length - 1, 1);
                    e.Value = str;
                }
            };
            QLCHTxtCauHoi.Validating += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(QLCHTxtCauHoi.Text))
                {
                    e.Cancel = true;
                    QLCHTxtCauHoi.Focus();
                    errorProvider.SetError(QLCHTxtCauHoi, "Không được để trống câu hỏi");
                }
                else
                {
                    e.Cancel = false;
                    errorProvider.SetError(QLCHTxtCauHoi, "");
                }
            };
            QLCHTxtDapAn.Validating += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(QLCHTxtDapAn.Text))
                {
                    e.Cancel = true;
                    QLCHTxtCauHoi.Focus();
                    errorProvider.SetError(QLCHTxtDapAn, "Không được để trống câu hỏi");
                }
                else
                {
                    e.Cancel = false;
                    errorProvider.SetError(QLCHTxtDapAn, "");
                }
            };
        }

        private void LoadQLDTDeThi()
        {
            var QLTTN = new QLTTNDataContext();
            
            var SourceDt = QLTTN.DeThi.Where(dt => dt.maMH == GV.maMH && dt.maKhoi == QLCbKhoiLop.SelectedValue.ToString()).Select(dt => new { dt.maDT, dt.TenDT, dt.GiaoVien.HoTen, dt.ThoiGianLamBai, dt.NgayTao }).ToList();
            BSDethi.DataSource = SourceDt;

            if (BSDethi.Count > 0)
            {
                QLDTDgvDT.DataSource = BSDethi;
                QLDTDgvDT.Columns["maDT"].Width = 40;
                QLDTDgvDT.Columns["ThoiGianLamBai"].Width = 90;
                QLDTDgvDT.Columns["maDT"].HeaderText = "Mã";
                QLDTDgvDT.Columns["TenDT"].HeaderText = "Tên đề thi";
                QLDTDgvDT.Columns["HoTen"].HeaderText = "Giáo viên ra đề";
                QLDTDgvDT.Columns["ThoiGianLamBai"].HeaderText = "Thời gian làm bài";
                QLDTDgvDT.Columns["NgayTao"].HeaderText = "Ngày tạo";

                if (QLDTTxtTenDT.DataBindings.Count == 0)
                {
                    QLDTTxtTenDT.DataBindings.Add("Text", BSDethi, "TenDT", true, DataSourceUpdateMode.Never, "null");
                }
                if (QLDTLblNgayTao.DataBindings.Count == 0)
                {
                    QLDTLblNgayTao.DataBindings.Add("Text", BSDethi, "NgayTao", true, DataSourceUpdateMode.Never, 0);
                }
                if (QLDTLblNguoiTao.DataBindings.Count == 0)
                {
                    QLDTLblNguoiTao.DataBindings.Add("Text", BSDethi, "HoTen", true, DataSourceUpdateMode.Never, 0);
                }
                if (QLDTThoiGianLamBai.DataBindings.Count == 0)
                {
                    var bd = new Binding("Text", BSDethi, "ThoiGianLamBai", true, DataSourceUpdateMode.Never, 0);
                    bd.Format += (s, e) =>
                    {
                        if (e.DesiredType == typeof(string))
                        {
                            TimeSpan soPhut = TimeSpan.Parse(e.Value.ToString());
                            e.Value = soPhut.TotalMinutes.ToString();
                        }
                    };
                    QLDTThoiGianLamBai.DataBindings.Add(bd);
                }
            }
            else
            {
                QLDTTxtTenDT.DataBindings.Clear();
                QLDTLblNguoiTao.DataBindings.Clear();
                QLDTLblNgayTao.DataBindings.Clear();
                QLDTThoiGianLamBai.DataBindings.Clear();
                QLDTDgvDT.DataSource = null;
                MessageBox.Show("Không có đề thi");
            }
            
        }
        private void LoadQLDTCauHoi()
        {
            if (BSCauHoi.Count > 0)
            {
                DgvCauHoiDT.DataSource = BSCauHoi;
                DgvCauHoiDT.Columns["maCH"].Width = 40;
                DgvCauHoiDT.Columns["NoiDung"].Width = 200;
                DgvCauHoiDT.Columns["TenCD"].Width = 80;
                DgvCauHoiDT.Columns["maCH"].HeaderText = "Mã";
                DgvCauHoiDT.Columns["NoiDung"].HeaderText = "Nội dung";
                DgvCauHoiDT.Columns["TenCD"].HeaderText = "Cấp độ";
                DgvCauHoiDT.Columns["maCD"].Visible = false;
                DgvCauHoiDT.AllowUserToOrderColumns = true;
            }
            else
            {
                MessageBox.Show("Không có câu hỏi nào", "Thông báo Quản lý đề thi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DgvCauHoiDT.DataSource = null;
            }
        }
        private void SetQLDT()
        {
            DgvCauHoiDT.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                Name = "Chon",
                HeaderText = "Chọn câu hỏi",
                Width = 100,
                TrueValue = true,    // khi được check thì value sẽ là true, được ktra trong event CellValueChanged
                FalseValue = false,
                IndeterminateValue = false
            });
            QLDTDgvDT.Columns.Add(new DataGridViewButtonColumn()
            {
                Name = "Xoa",
                HeaderText = "Xóa đề thi",
                Width = 100
            });

            QLDTThoiGianLamBai.Text = "10";
            QLDTThoiGianLamBai.KeyDown += (s, e) =>
             {
                 if (e.KeyValue >= 48 && e.KeyValue <= 57 ||
                    e.KeyValue >= 96 && e.KeyValue <= 105 ||
                    e.KeyCode == Keys.Back ||
                    e.KeyCode == Keys.Delete ||
                    e.KeyCode == Keys.Left ||
                    e.KeyCode == Keys.Right)
                 {
                     // nếu là số hoặc xóa hoặc dịch trái phải thì cho gõ
                 }
                 else if (e.KeyCode == Keys.Up)
                 {
                     int SoPhut = int.Parse(QLDTThoiGianLamBai.Text);
                     SoPhut += 5;
                     QLDTThoiGianLamBai.Text = SoPhut.ToString();
                 }
                 else if (e.KeyCode == Keys.Down)
                 {
                     int SoPhut = int.Parse(QLDTThoiGianLamBai.Text);
                     SoPhut -= 5;
                     QLDTThoiGianLamBai.Text = SoPhut.ToString();
                 }
                 else
                 {                 
                     e.SuppressKeyPress = true;
                 }
             };
            
            QLDTTxtTenDT.Validating += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(QLDTTxtTenDT.Text))
                {
                    e.Cancel = true;
                    QLDTTxtTenDT.Focus();
                    errorProvider.SetError(QLDTTxtTenDT, "Không được để trống tên đề thi");
                }
                else
                {
                    e.Cancel = false;
                    errorProvider.SetError(QLDTTxtTenDT, "");
                }
            };
            QLDTBtnRdCauHoiDT.Click += (s, e) =>
            {
                  CheckDGV(DgvCauHoiDT, (int)QLDTCauHoiNgauNhien.Value);
            };
            QLDTThoiGianLamBai.KeyUp += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(QLDTThoiGianLamBai.Text))
                {
                    QLDTThoiGianLamBai.Text = "5";
                }
                int SoPhut = int.Parse(QLDTThoiGianLamBai.Text);
                if (SoPhut < 0)
                {
                    QLDTThoiGianLamBai.Text = "5";
                }
                else if (SoPhut > 180)
                {
                    QLDTThoiGianLamBai.Text = "180";
                }
            };
            QLDTDgvDT.CellPainting += (s, e) =>
            {
                if (e.RowIndex < 0)
                {
                    return;
                }
                if (e.ColumnIndex == 0)
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                    var w = e.CellBounds.Height - 4;
                    var h = e.CellBounds.Height - 4;
                    var x = e.CellBounds.X + (e.CellBounds.Width - w) / 2;
                    var y = e.CellBounds.Y + 2;
                    e.Graphics.DrawImage(Properties.Resources.delete__1_, x, y, w, h);
                    e.Handled = true;
                }
            };
        }


        private void SetQLKT()
        {
            QLKTDgvKT.Columns.Add(new DataGridViewButtonColumn()
            {
                Name = "Xoa",
                Width = 100,
                HeaderText = "Xóa kỳ thi"
            });

           
          
            QLKTBtnXuatThongTin.Click += (s, e) =>
              {
                  if (QLKTDgvKT.RowCount > 0 && QLKTDgvKT.SelectedRows.Count > 0)
                  {
                      int makt = int.Parse(QLKTDgvKT.SelectedRows[0].Cells["maKT"].Value.ToString());
                      var frmrp = new frmReport(makt);
                      frmrp.ShowDialog();
                  }
                  else
                  {
                      MessageBox.Show("Hãy chọn một kỳ thi", "Thông báo", MessageBoxButtons.OK,
                          MessageBoxIcon.Information);
                  }
             };


            QLKTBtnXuatThongTin.Click += (s, e) =>
              {
                  if (QLKTDgvKT.RowCount > 0 && QLKTDgvKT.SelectedRows.Count > 0)
                  {
                      int maKT = int.Parse(QLKTDgvKT.SelectedRows[0].Cells["maKT"].Value.ToString());
                      var frmRp = new frmReportKyThiResult(maKT);
                      frmRp.ShowDialog();
                  }
                  else
                  {
                      MessageBox.Show("Hãy chọn một kỳ thi", "Thông báo", MessageBoxButtons.OK,
                          MessageBoxIcon.Information);
                  }
            };

            QLKTDgvKT.CellPainting += (s, e) =>
            {
                if (e.RowIndex < 0)
                {
                    return;
                }
                if (e.ColumnIndex == 0)
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                    var w = e.CellBounds.Height - 4;
                    var h = e.CellBounds.Height - 4;
                    var x = e.CellBounds.X + (e.CellBounds.Width - w) / 2;
                    var y = e.CellBounds.Y + 2;
                    e.Graphics.DrawImage(Properties.Resources.delete__1_, x, y, w, h);
                    e.Handled = true;
                }
            };
        }
        private void LoadQLKTHocSinh()
        {
            var QLTTN = new QLTTNDataContext();
            
            if (QLTTN.HocSinh.Count() == 0)
            {
                MessageBox.Show("Không có học sinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (QLKTDgvKT.SelectedRows.Count > 0 && QLKTDgvBT.SelectedRows.Count > 0)
            {
                int maKt = int.Parse(QLKTDgvKT.SelectedRows[0].Cells["maKT"].Value.ToString());
                int maDt = int.Parse(QLKTDgvBT.SelectedRows[0].Cells["maDT"].Value.ToString());
                
                var dshs = QLTTN.BaiLam.Where(bl => bl.maKT == maKt && bl.maDT == maDt).Select(bl => bl.HocSinh);
                QLKTDgvHS.DataSource = dshs.Select(hs => new { hs.maHS, hs.HoTen, hs.maKhoi, hs.maLop, hs.NgaySinh }).ToList();

                if (dshs.Count() > 0)
                {
                    QLKTDgvHS.Columns["maHS"].Width = 120;
                    QLKTDgvHS.Columns["HoTen"].Width = 220;
                    QLKTDgvHS.Columns["maLop"].Width = 80;
                    QLKTDgvHS.Columns["NgaySinh"].Width = 150;
                    QLKTDgvHS.Columns["maKhoi"].Visible = false;

                    QLKTDgvHS.Columns["maHS"].HeaderText = "Mã học sinh";
                    QLKTDgvHS.Columns["HoTen"].HeaderText = "Họ tên";
                    QLKTDgvHS.Columns["maLop"].HeaderText = "Lớp học";
                    QLKTDgvHS.Columns["NgaySinh"].HeaderText = "Ngày sinh";

                    qlktGbTongSoThiSinh.Text = $"Tổng số thí sinh tham gia: {QLKTDgvHS.RowCount}";
                }
                else
                {
                    QLKTDgvHS.DataSource = null;
                }
                
            }
            else
            {
                QLKTDgvHS.DataSource = null;
            }
            
        }
        private void LoadQLKTDeThi()
        {
            var QLTTN = new QLTTNDataContext();
            
            if (QLTTN.DeThi.Count() == 0)
            {
                MessageBox.Show("Không có đề thi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                if (QLKTDgvKT.SelectedRows.Count > 0)
                {
                    int maKt = int.Parse(QLKTDgvKT.SelectedRows[0].Cells["maKT"].Value.ToString());

                    // danh sách các buổi thi trong kỳ thi đó
                    BSLichThi.DataSource = QLTTN.BuoiThi.Where(bt => bt.maKT == maKt).Select(bt => new { bt.DeThi.maDT, bt.DeThi.TenDT, bt.DeThi.maMH, bt.DeThi.MonHoc.tenMH, bt.NgayGioThi, bt.DeThi.ThoiGianLamBai });
                    if (BSLichThi.Count > 0)
                    {
                        QLKTDgvBT.DataSource = BSLichThi;
                        QLKTDgvBT.Columns["maDT"].Width = 80;
                        QLKTDgvBT.Columns["TenDT"].Width = 120;
                        QLKTDgvBT.Columns["NgayGioThi"].Width = 160;
                        QLKTDgvBT.Columns["maDT"].HeaderText = "Mã đề";
                        QLKTDgvBT.Columns["maMH"].Visible = false;
                        QLKTDgvBT.Columns["TenDT"].HeaderText = "Tên đề thi";
                        QLKTDgvBT.Columns["tenMH"].HeaderText = "Môn thi";
                        QLKTDgvBT.Columns["ThoiGianLamBai"].HeaderText = "Thời gian làm bài";
                        QLKTDgvBT.Columns["NgayGioThi"].HeaderText = "Ngày giờ thi";

                        qlktGbTongSoDeThi.Text = $"Tổng số buổi thi (1buổi/1môn): {QLKTDgvBT.RowCount}";
                    }
                    else
                    {
                        MessageBox.Show("Không có đề thi", "Thông báo tab Quản lý kỳ thi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        QLKTDgvBT.DataSource = null;
                    }
                }
                else
                {
                    QLKTDgvBT.DataSource = null;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }
            
        }
        public void LoadQLKTKyThi()
        {
            using (var QLTTN = new QLTTNDataContext())
            {
                if (QLTTN.KyThi.Count() == 0)
                {
                    MessageBox.Show("Không có kỳ thi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //if (qlktDgvHS.Rows.Count > 0) { qlktDgvHS.Rows.Clear(); }
                    //if (qlktDgvDT.Rows.Count > 0) { qlktDgvDT.Rows.Clear(); }

                    return;
                }
                try
                {
                    string MaKhoi = QLCbKhoiLop.SelectedValue.ToString();
                    BSKyThi.DataSource = QLTTN.KyThi.Where(kt => kt.maKhoi == MaKhoi && kt.LoaiKT == "ThiThiet").Select(kt => new { kt.maKT, kt.TenKT, kt.LoaiKT }).ToList();
                }
                catch (Exception e)
                {
                    return;
                }
                if (BSKyThi.Count > 0)
                {
                    QLKTDgvKT.DataSource = BSKyThi;
                    QLKTDgvKT.Columns["TenKT"].Width = 100;
                    QLKTDgvKT.Columns["maKT"].Width = 30;
                    QLKTDgvKT.Columns["maKT"].HeaderText = "Mã";
                    QLKTDgvKT.Columns["TenKT"].HeaderText = "Tên";
                    QLKTDgvKT.Columns["LoaiKT"].HeaderText = "Phân loại";
                }
                else
                {
                    QLKTDgvKT.DataSource = null;
                }
            }
        }


        private void SetQLKTOnThi()
        {
            QLKTOTDgvKTOT.Columns.Add(new DataGridViewButtonColumn()
            {
                Name = "Xoa",
                Width = 80,
                HeaderText = "Xóa kỳ thi"
            });

            QLKTOTDgvKTOT.CellPainting += (s, e) =>
            {
                if (e.RowIndex < 0)
                {
                    return;
                }
                if (e.ColumnIndex == 0)
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                    var w = e.CellBounds.Height - 4;
                    var h = e.CellBounds.Height - 4;
                    var x = e.CellBounds.X + (e.CellBounds.Width - w) / 2;
                    var y = e.CellBounds.Y + 2;
                    e.Graphics.DrawImage(Properties.Resources.delete__1_, x, y, w, h);
                    e.Handled = true;
                }
            };
        }
        private void LoadQLKTOnThiHocSinh()
        {
            using (var QLTTN = new QLTTNDataContext())
            {
                if (QLTTN.HocSinh.Count() == 0)
                {
                    MessageBox.Show("Không có học sinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (QLKTOTDgvKTOT.SelectedRows.Count > 0 && QLKTOTDgvDTDC.SelectedRows.Count > 0)
                {
                    int maKt = int.Parse(QLKTOTDgvKTOT.SelectedRows[0].Cells["maKT"].Value.ToString());
                    int maDt = int.Parse(QLKTOTDgvDTDC.SelectedRows[0].Cells["maDT"].Value.ToString());
                    try
                    {
                        var dshs = QLTTN.BaiLam.Where(bl => bl.maKT == maKt && bl.maDT == maDt).Select(bl => bl.HocSinh);
                        QLKTONDgvHSDC.DataSource = dshs.Select(hs => new { hs.maHS, hs.HoTen, hs.maKhoi, hs.maLop, hs.NgaySinh }).ToList();

                        if (dshs.Count() > 0)
                        {
                            QLKTONDgvHSDC.Columns["maHS"].Width = 120;
                            QLKTONDgvHSDC.Columns["HoTen"].Width = 220;
                            QLKTONDgvHSDC.Columns["maLop"].Width = 80;
                            QLKTONDgvHSDC.Columns["NgaySinh"].Width = 150;
                            QLKTONDgvHSDC.Columns["maKhoi"].Visible = false;

                            QLKTONDgvHSDC.Columns["maHS"].HeaderText = "Mã học sinh";
                            QLKTONDgvHSDC.Columns["HoTen"].HeaderText = "Họ tên";
                            QLKTONDgvHSDC.Columns["maLop"].HeaderText = "Lớp học";
                            QLKTONDgvHSDC.Columns["NgaySinh"].HeaderText = "Ngày sinh";

                            qlktGbTongSoThiSinh.Text = $"Tổng số thí sinh tham gia: {QLKTONDgvHSDC.RowCount}";
                        }
                        else
                        {
                            QLKTONDgvHSDC.DataSource = null;
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                        return;
                    }
                }
                else
                {
                    QLKTONDgvHSDC.DataSource = null;
                }
            }
        }
        private void LoadQLKTOnThiDeThi()
        {
            var QLTTN = new QLTTNDataContext();
            
            if (QLTTN.DeThi.Count() == 0)
            {
                MessageBox.Show("Không có đề thi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                if (QLKTOTDgvKTOT.SelectedRows.Count > 0)
                {
                    int maKt = int.Parse(QLKTOTDgvKTOT.SelectedRows[0].Cells["maKT"].Value.ToString());

                    BSLichThiThu.DataSource = QLTTN.BuoiThi.Where(bt => bt.maKT == maKt)
                                                        .Select(bt => new { bt.DeThi.maDT, bt.DeThi.TenDT, bt.DeThi.maMH, bt.DeThi.MonHoc.tenMH, bt.NgayGioThi, bt.DeThi.ThoiGianLamBai });
                    if (BSLichThiThu.Count > 0)
                    {
                        QLKTOTDgvDTDC.DataSource = BSLichThiThu;
                        QLKTOTDgvDTDC.Columns["maDT"].Width = 80;
                        QLKTOTDgvDTDC.Columns["TenDT"].Width = 120;
                        QLKTOTDgvDTDC.Columns["NgayGioThi"].Width = 160;
                        QLKTOTDgvDTDC.Columns["maDT"].HeaderText = "Mã đề";
                        QLKTOTDgvDTDC.Columns["maMH"].Visible = false;
                        QLKTOTDgvDTDC.Columns["TenDT"].HeaderText = "Tên đề thi";
                        QLKTOTDgvDTDC.Columns["tenMH"].HeaderText = "Môn thi";
                        QLKTOTDgvDTDC.Columns["ThoiGianLamBai"].HeaderText = "Thời gian làm bài";
                        QLKTOTDgvDTDC.Columns["NgayGioThi"].HeaderText = "Ngày giờ thi";

                        qlktGbTongSoDeThi.Text = $"Tổng số đề thi được chọn: {QLKTOTDgvDTDC.RowCount}";
                    }
                    else
                    {
                        MessageBox.Show("Không có đề thi", "Thông báo Quản lý kỳ thi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        QLKTOTDgvDTDC.DataSource = null;
                    }
                }
                else
                {
                    QLKTOTDgvDTDC.DataSource = null;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }
            
        }
        public void LoadQLKTOnThiKyThi()
        {
            var QLTTN = new QLTTNDataContext();
            
            if (QLTTN.KyThi.Count() == 0)
            {
                MessageBox.Show("Không có kỳ thi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                string MaKhoi = QLCbKhoiLop.SelectedValue.ToString();
                BSKyThiThu.DataSource = QLTTN.KyThi.Where(kt => kt.maKhoi == MaKhoi && kt.LoaiKT == "ThiThu")
                    .Select(kt => new { kt.maKT, kt.TenKT, kt.LoaiKT }).ToList();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Thông báo loadQlkttotDgvKyThi");
                return;
            }
            if (BSKyThiThu.Count > 0)
            {
                QLKTOTDgvKTOT.DataSource = BSKyThiThu;
                QLKTOTDgvKTOT.Columns["maKT"].Width = 30;
                QLKTOTDgvKTOT.Columns["TenKT"].Width = 100;
                QLKTOTDgvKTOT.Columns["maKT"].HeaderText = "Mã";
                QLKTOTDgvKTOT.Columns["TenKT"].HeaderText = "Tên";
                QLKTOTDgvKTOT.Columns["LoaiKT"].HeaderText = "Phân loại";
            }
            else
            {
                MessageBox.Show("Chưa có kỳ thi cho khối này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                QLKTOTDgvKTOT.DataSource = null;
            }
            
        }

        private void LoadQLHSHocSinh()
        {
            string MaKhoi = QLCbKhoiLop.SelectedValue.ToString();
            var QLTTN = new QLTTNDataContext();
            
            BSQLHSHocSinh.DataSource = QLTTN.HocSinh.Where(hs => hs.maKhoi == MaKhoi).Select(hs => new
            {
                hs.maHS,
                hs.HoTen,
                hs.maKhoi,
                hs.maLop,
                hs.NgaySinh,
                SoKTDaThamGia = QLTTN.KyThi.Where(kt => kt.BuoiThi.Where(bt => bt.BaiLam.Where(bl => bl.maHS == hs.maHS).Count() > 0).Count() > 0).Count(),
                SoDTDaLam = hs.BaiLam.Count,
                DTB = hs.BaiLam.Average(bl => bl.DiemSo)
            }).ToList();

            if (BSQLHSHocSinh.Count > 0)
            {
                QLHSDgvDS.DataSource = BSQLHSHocSinh;

                QLHSDgvDS.Columns["maHS"].Width = 80;
                QLHSDgvDS.Columns["HoTen"].Width = 220;
                QLHSDgvDS.Columns["maLop"].Width = 80;
                QLHSDgvDS.Columns["NgaySinh"].Width = 100;
                QLHSDgvDS.Columns["SoKTDaThamGia"].Width = 100;
                QLHSDgvDS.Columns["SoDTDaLam"].Width = 100;
                QLHSDgvDS.Columns["DTB"].Width = 100;
                QLHSDgvDS.Columns["maKhoi"].Visible = false;

                QLHSDgvDS.Columns["maHS"].HeaderText = "Mã học sinh";
                QLHSDgvDS.Columns["HoTen"].HeaderText = "Họ tên";
                QLHSDgvDS.Columns["maLop"].HeaderText = "Lớp học";
                QLHSDgvDS.Columns["NgaySinh"].HeaderText = "Ngày sinh";
                QLHSDgvDS.Columns["SoKTDaThamGia"].HeaderText = "Tổng số kỳ thi đã tham gia";
                QLHSDgvDS.Columns["SoDTDaLam"].HeaderText = "Tổng số đề thi đã làm";
                QLHSDgvDS.Columns["DTB"].HeaderText = "Điểm trung bình";
            }
            else
            {
                QLHSDgvDS.DataSource = null;
            }
            
        }
        private void LoadQLHSKyThi()
        {
            string MaKhoi = QLCbKhoiLop.SelectedValue.ToString();
            var QLTTN = new QLTTNDataContext();
            
            BSQLHSKyThi.DataSource = QLTTN.KyThi.Where(kt => kt.maKhoi == MaKhoi).Select(kt => new
            {
                kt.maKT,
                kt.TenKT,
                kt.LoaiKT,
                TongSoMonThi = kt.BuoiThi.Count,
                TongSoThiSinh = kt.BuoiThi.Sum(bt => bt.BaiLam.Count),
                DTB = kt.BuoiThi.Average(bt => bt.BaiLam.Average(bl => bl.DiemSo))
            });

            if (BSQLHSKyThi.Count > 0)
            {
                QLHSDgvDS.DataSource = BSQLHSKyThi;

                QLHSDgvDS.Columns["maKT"].Width = 100;
                QLHSDgvDS.Columns["TenKT"].Width = 200;
                QLHSDgvDS.Columns["LoaiKT"].Width = 90;
                QLHSDgvDS.Columns["TongSoMonThi"].Width = 110;
                QLHSDgvDS.Columns["TongSoThiSinh"].Width = 120;
                QLHSDgvDS.Columns["DTB"].Width = 160;

                QLHSDgvDS.Columns["maKT"].HeaderText = "Mã kỳ thi";
                QLHSDgvDS.Columns["TenKT"].HeaderText = "Tên kỳ thi";
                QLHSDgvDS.Columns["LoaiKT"].HeaderText = "Loại kỳ thi";
                QLHSDgvDS.Columns["TongSoMonThi"].HeaderText = "Tổng số môn";
                QLHSDgvDS.Columns["TongSoThiSinh"].HeaderText = "Tổng số thí sinh";
                QLHSDgvDS.Columns["DTB"].HeaderText = "Phổ điểm trung bình";
            }
            else
            {
                QLHSDgvDS.DataSource = null;
            }
            
        }
        private void LoadQLHSCauHoi()
        {
            string MaKhoi = QLCbKhoiLop.SelectedValue.ToString();
            var QLTTN = new QLTTNDataContext();
            
            double tongSoDeThi = QLTTN.DeThi.Where(dt => dt.maKhoi == MaKhoi && dt.maMH == GV.maMH).Count();
            BSQlhsCauHoi.DataSource = QLTTN.CauHoi.Where(ch => ch.maKhoi == MaKhoi && ch.maMH == GV.maMH).Select(ch => new
            {
                ch.maCH,
                ch.NoiDung,
                ch.CapDo.TenCD,
                TiLeRaDe = Math.Round((decimal)((double)ch.CT_DeThi.Count * 100 / (tongSoDeThi == 0 ? 1 : tongSoDeThi)), 2, MidpointRounding.ToEven).ToString() + " %",
                TiLeDungSai = Math.Round((decimal)((double)QLTTN.CT_BaiLam.Where(ctbl => ctbl.maCH == ch.maCH && ctbl.DungSai == true).Count() * 100 /
                                ((double)(QLTTN.CT_BaiLam.Where(ctbl => ctbl.maCH == ch.maCH).Count() == 0 ? 1 :
                                QLTTN.CT_BaiLam.Where(ctbl => ctbl.maCH == ch.maCH).Count()))), 2, MidpointRounding.ToEven).ToString() + " %"
            });
            if (BSQlhsCauHoi.Count > 0)
            {
                QLHSDgvDS.DataSource = BSQlhsCauHoi;

                QLHSDgvDS.Columns["maCH"].Width = 100;
                QLHSDgvDS.Columns["NoiDung"].Width = 360;
                QLHSDgvDS.Columns["TenCD"].Width = 100;
                QLHSDgvDS.Columns["TiLeRaDe"].Width = 100;
                QLHSDgvDS.Columns["TiLeDungSai"].Width = 100;

                QLHSDgvDS.Columns["maCH"].HeaderText = "Mã câu hỏi";
                QLHSDgvDS.Columns["NoiDung"].HeaderText = "Nội dung";
                QLHSDgvDS.Columns["TenCD"].HeaderText = "Cấp độ";
                QLHSDgvDS.Columns["TiLeRaDe"].HeaderText = "Tỉ lệ ra đề";
                QLHSDgvDS.Columns["TiLeDungSai"].HeaderText = "Tỉ lệ đúng sai";
            }
            else
            {
                QLHSDgvDS.DataSource = null;
            }
            
        }

        private bool DaTonTai(DeThi dethiHientai)
        {
            var QLTTN = new QLTTNDataContext();
            

            if (QLTTN.DeThi.Where(dt => dt.TenDT == dethiHientai.TenDT).Count() > 0)
            {
                MessageBox.Show("Tên đề thi đã có rồi, xin mời đổi lại tên khác", "Trùng dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }

            List<int> lmach = new List<int>();
            foreach (DataGridViewRow row in DgvCauHoiDT.Rows)
            {
                var cell = row.Cells["Chon"] as DataGridViewCheckBoxCell;
                if (cell.Value == cell.TrueValue)
                {
                    lmach.Add(int.Parse(row.Cells["maCH"].Value.ToString()));
                }
            }
            var dsdethi = QLTTN.CT_DeThi.GroupBy(ctdt => ctdt.maDT).ToList();
            foreach (var dethi in dsdethi)
            {
                int soCauTimThay = 0;
                foreach (var ch in lmach)
                {
                    if (dethi.Where(ctdt => ctdt.maCH == ch).Count() > 0)
                    {
                        soCauTimThay++;
                    }
                }
                if (soCauTimThay == dethi.Count())
                {
                    string line = "";
                    for (int i = 0; i < dethi.Count(); i++)
                    {
                        line += $"{ Environment.NewLine } {dethi.ElementAt(i).maCH}. {dethi.ElementAt(i).CauHoi.NoiDung}";
                    }
                    MessageBox.Show($"Tên đề thi bị trùng: <{dethi.ElementAt(0).DeThi.TenDT}> {line}", "Trùng dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                }
            }
            
            return false;
        }

        private Dictionary<TabPage, Color> TabColors = new Dictionary<TabPage, Color>();
        private void SetTabHeader(TabPage page, Color color)
        {
            TabColors[page] = color;
            tabControl1.Invalidate();
        }
        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            //e.DrawBackground();
            if (e.State == DrawItemState.Selected)
            {
                using (Brush br = new SolidBrush(Color.LemonChiffon))
                {
                    e.Graphics.FillRectangle(br, e.Bounds);
                    SizeF sz = e.Graphics.MeasureString(tabControl1.TabPages[e.Index].Text, e.Font);
                    e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + (e.Bounds.Width - sz.Width) / 2, e.Bounds.Top + (e.Bounds.Height - sz.Height) / 2 + 1);

                    Rectangle rect = e.Bounds;
                    rect.Offset(0, 1);
                    rect.Inflate(0, -1);
                    e.Graphics.DrawRectangle(Pens.DarkGray, rect);
                    e.DrawFocusRectangle();
                }
            }
            else
            {
                using (Brush br = new SolidBrush(TabColors[tabControl1.TabPages[e.Index]]))
                {
                    e.Graphics.FillRectangle(br, e.Bounds);
                    SizeF sz = e.Graphics.MeasureString(tabControl1.TabPages[e.Index].Text, e.Font);
                    e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + (e.Bounds.Width - sz.Width) / 2, e.Bounds.Top + (e.Bounds.Height - sz.Height) / 2 + 1);

                    Rectangle rect = e.Bounds;
                    //rect.Offset(0, 1);
                    //rect.Inflate(0, -1);
                    e.Graphics.DrawRectangle(Pens.DarkGray, rect);
                    e.DrawFocusRectangle();
                }
            }
        }

        private int SoDongDuocChon(DataGridView dgv)
        {
            int result = 0;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                var cell = row.Cells["Chon"] as DataGridViewCheckBoxCell;
                if (cell.Value == cell.TrueValue)
                {
                    result++;
                }
            }
            return result;
        }
        private void CheckDGV(DataGridView dgv, int soDongMuonCheck)
        {
            if (dgv.RowCount <= 0)
            {
                return;
            }
            if (soDongMuonCheck > dgv.RowCount)
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    var cell = row.Cells["Chon"] as DataGridViewCheckBoxCell;
                    cell.Value = cell.TrueValue;
                }
                return;
            }

            Random rd = new Random();
            int maxCau = soDongMuonCheck;

            if (maxCau > dgv.RowCount)
            {
                maxCau = dgv.RowCount;
            }
            foreach (DataGridViewRow row in dgv.Rows)
            {
                var cell = row.Cells["Chon"] as DataGridViewCheckBoxCell;
                cell.Value = cell.FalseValue;
            }
            List<int> li = new List<int>();
            while (li.Count < maxCau)
            {
                int dongNgauNhien = rd.Next(0, dgv.RowCount);

                if (!li.Contains(dongNgauNhien))
                {
                    li.Add(dongNgauNhien);
                    var cell = dgv.Rows[dongNgauNhien].Cells["Chon"] as DataGridViewCheckBoxCell;
                    cell.Value = cell.TrueValue;
                }
            }
        }
        public frmGiaoVien(frmLogin frmlogin, GiaoVien gv)
        {
            Thread t = new Thread(new ThreadStart(() =>
            {
                Application.Run(new frmSplashScreen());
            }));
            t.Start();
            this.GV = gv;
            this.frmlogin = frmlogin;
            InitializeComponent();
            tabControl1.SelectedIndex = 2;
            LoadKhoiLop();
            loadCbCauHoi();
            LoadQlCHDapAn();
            SetQLCH();
            SetQLDT();
            LoadQLDTDeThi();
            LoadQLDTCauHoi();
            SetQLKT();
            LoadQLKTHocSinh();
            LoadQLKTDeThi();
            LoadQLKTKyThi();
            SetQLKTOnThi();
            LoadQLKTOnThiKyThi();
            LoadQLKTOnThiDeThi();
            LoadQLKTOnThiHocSinh();

            this.tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);
            foreach (TabPage tp in tabControl1.TabPages)
            {
                SetTabHeader(tp, Color.FromKnownColor(KnownColor.Control));
                tp.BackColor = Color.LemonChiffon;
            }
            this.FormClosing += (s, e) =>
            {
                this.frmlogin.Show();
            };
            this.tabControl1.SelectedIndexChanged += (s, e) =>
            {
                 if (tabControl1.SelectedIndex == 2)
                 {
                     LoadQLKTHocSinh();
                     LoadQLKTDeThi();
                     LoadQLKTKyThi();
                 }
                 else if (tabControl1.SelectedIndex == 3)
                 {
                     LoadQLKTOnThiKyThi();
                     LoadQLKTOnThiDeThi();
                     LoadQLKTOnThiHocSinh();
                 }
             };
            this.QLCbKhoiLop.SelectedIndexChanged += (s, e) =>
             {
                 loadCbCauHoi();
                 LoadQlCHDapAn();
                 LoadQLDTCauHoi();
                 LoadQLDTDeThi();
                 LoadQLKTDeThi();
                 LoadQLKTHocSinh();
                 LoadQLKTKyThi();
                 LoadQLKTOnThiHocSinh();
                 LoadQLKTOnThiDeThi();
                 LoadQLKTOnThiKyThi();
             };
            this.btnDangXuat.Click += (s, e) =>
            {
                frmlogin.Show();
                this.Close();
            };

           
            QLHSRbCH.CheckedChanged += (s, e) =>
            {
                if (QLHSRbCH.Checked == true)
                {
                    QLHSDgvDS.DataSource = null;
                    LoadQLHSCauHoi();
                }
            };

            QLKTOTBtnSuaKT.Click += (s, e) =>
            {
                if (QLKTOTDgvKTOT.RowCount > 0 && QLKTOTDgvKTOT.SelectedRows[0] != null)
                {
                    int makt = int.Parse(QLKTOTDgvKTOT.SelectedRows[0].Cells["maKT"].Value.ToString());
                    string maKhoi = QLCbKhoiLop.SelectedValue.ToString();
                    frmSuaKT frmsuakt = new frmSuaKT(this, gv, makt, maKhoi, BSKyThiThu);
                    frmsuakt.Show();
                }
                else
                {
                    MessageBox.Show("Mời bạn chọn kỳ thi cần cập nhật",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };

            QLHSRbHS.CheckedChanged += (s, e) =>
            {
                if (QLHSRbHS.Checked == true)
                {
                    QLHSDgvDS.DataSource = null;
                    LoadQLHSHocSinh();
                }
            };
            QLHSRbKT.CheckedChanged += (s, e) =>
            {
                if (QLHSRbKT.Checked == true)
                {
                    QLHSDgvDS.DataSource = null;
                    LoadQLHSKyThi();
                }
            };
            QLKTOTDgvKTOT.CellContentClick += (s, e) =>
            {
                if (e.ColumnIndex == 0)
                {
                    var makt = int.Parse(QLKTOTDgvKTOT.Rows[e.RowIndex].Cells["maKT"].Value.ToString());
                    var QLTTN = new QLTTNDataContext();
                    KyThi ktDuocChon = QLTTN.KyThi.Where(kt => kt.maKT == makt).FirstOrDefault();

                    if (ktDuocChon.BuoiThi.Where(buoithi => buoithi.NgayGioThi < DateTime.Now).Count() > 0)
                    {
                        MessageBox.Show($"Không được xóa kỳ thì có ít nhất 1 buổi thi đã diễn ra", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        QLTTN.BaiLam.DeleteAllOnSubmit(QLTTN.BaiLam.Where(bl => bl.maKT == makt));
                        QLTTN.BuoiThi.DeleteAllOnSubmit(QLTTN.BuoiThi.Where(bt => bt.maKT == makt));
                        QLTTN.KyThi.DeleteOnSubmit(ktDuocChon);
                        QLTTN.SubmitChanges();
                        QLKTOTDgvKTOT.Rows.RemoveAt(e.RowIndex);
                        LoadQLKTKyThi();
                        LoadQLKTOnThiDeThi();
                        LoadQLKTOnThiHocSinh();
                    }
                    
                }
            };
            
            QLKTOTBtnThemKT.Click += (s, e) =>
            {
                frmThemKT frmthemkt = new frmThemKT(this, gv, QLCbKhoiLop.SelectedValue.ToString(), "ThiThu");
                frmthemkt.Show();
            };
            QLKTOTDgvKTOT.SelectionChanged += (s, e) =>
            {
                LoadQLKTOnThiDeThi();
                LoadQLKTOnThiHocSinh();
            };
            QLKTOTDgvDTDC.SelectionChanged += (s, e) =>
            {
                LoadQLKTOnThiHocSinh();
            };

            QLKTBtnSuaKT.Click += (s, e) =>
              {
                  if (QLKTDgvKT.RowCount > 0 && QLKTDgvKT.SelectedRows[0] != null)
                  {
                      int makt = int.Parse(QLKTDgvKT.SelectedRows[0].Cells["maKT"].Value.ToString());
                      string maKhoi = QLCbKhoiLop.SelectedValue.ToString();
                      frmSuaKT frmsuakt = new frmSuaKT(this, gv, makt, maKhoi, BSKyThi);
                      frmsuakt.Show();
                  }
                  else
                  {
                      MessageBox.Show("Mời bạn chọn kỳ thi cần cập nhật",
                          "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  }
              };
            QLKTDgvKT.CellContentClick += (s, e) =>
            {
                if (e.ColumnIndex == 0)
                {
                    var makt = int.Parse(QLKTDgvKT.Rows[e.RowIndex].Cells["maKT"].Value.ToString());
                    var QLTTN = new QLTTNDataContext();
                    
                    /// chỉ được xóa kỳ thi nào chưa có buổi thi nào diễn ra hoặc diễn ra rồi mà chưa có thí sinh tham gia kỳ thi
                    KyThi ktDuocChon = QLTTN.KyThi.Where(kt => kt.maKT == makt).FirstOrDefault();

                    // nếu kỳ thi được chọn có ít nhất 1 buổi thi đã diễn ra rồi thì sẽ không được xóa
                    if (ktDuocChon.BuoiThi.Where(buoithi => buoithi.NgayGioThi < DateTime.Now).Count() > 0)
                    {
                        // không được xóa
                        MessageBox.Show($"Không được xóa kỳ thì có ít nhất 1 buổi thi đã diễn ra", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        // xóa tất cả bài làm có trong kỳ thi đó
                        QLTTN.BaiLam.DeleteAllOnSubmit(QLTTN.BaiLam.Where(bl => bl.maKT == makt));
                        QLTTN.BuoiThi.DeleteAllOnSubmit(QLTTN.BuoiThi.Where(bt => bt.maKT == makt));
                        QLTTN.KyThi.DeleteOnSubmit(ktDuocChon);
                        QLTTN.SubmitChanges();
                        QLKTDgvKT.Rows.RemoveAt(e.RowIndex);
                        LoadQLKTKyThi();
                        LoadQLKTDeThi();
                        LoadQLKTHocSinh();
                    }
                    
                }
            };
            
            QLKTDgvBT.SelectionChanged += (s, e) =>
             {
                 LoadQLKTHocSinh();
             };
            QLKTBtnThemKT.Click += (s, e) =>
              {
                  frmThemKT frmthemkt = new frmThemKT(this, gv, QLCbKhoiLop.SelectedValue.ToString(), "ThiThiet");
                  frmthemkt.Show();
              };

            QLKTDgvKT.SelectionChanged += (s, e) =>
            {
                LoadQLKTDeThi();
                LoadQLKTHocSinh();
            };

            QLDTBtnSuaDT.Click += (s, e) =>
            {
                int soch = int.Parse(qldtLblSoCHDuocChon.Text.Replace(" câu", ""));
                if (soch < 5)
                {
                    MessageBox.Show("Mỗi đề thi cần có ít nhất 5 câu hỏi", "Yêu cầu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                using (var QLTTN = new QLTTNDataContext())
                {

                    int madt;
                    if (QLDTDgvDT.SelectedRows.Count > 0)
                    {
                        madt = int.Parse(QLDTDgvDT.SelectedRows[0].Cells["maDT"].Value.ToString());
                        var dethiHienTai = QLTTN.DeThi.Where(dt => dt.maDT == madt).Single();

                        dethiHienTai.maGV = gv.maGV;
                        dethiHienTai.maMH = gv.maMH;
                        dethiHienTai.maKhoi = QLCbKhoiLop.SelectedValue.ToString();
                        dethiHienTai.TenDT = QLDTTxtTenDT.Text;
                        dethiHienTai.ThoiGianLamBai = TimeSpan.FromMinutes(double.Parse(QLDTThoiGianLamBai.Text));
                        dethiHienTai.NgayTao = DateTime.Now;
                        QLTTN.SubmitChanges();

                        QLTTN.CT_DeThi.DeleteAllOnSubmit(QLTTN.CT_DeThi.Where(ctdt => ctdt.maDT == madt));
                        QLTTN.SubmitChanges();

                        foreach (DataGridViewRow row in DgvCauHoiDT.Rows)
                        {
                            var cell = row.Cells["Chon"] as DataGridViewCheckBoxCell;
                            if (cell.Value == cell.TrueValue)
                            {
                                QLTTN.CT_DeThi.InsertOnSubmit(new CT_DeThi()
                                {
                                    maDT = madt,
                                    maCH = int.Parse(row.Cells["maCH"].Value.ToString())
                                });
                            }
                        }
                        QLTTN.SubmitChanges();

                        LoadQLDTDeThi();
                    }
                    else
                    {
                        MessageBox.Show("Mời bạn lựa chọn đề thi cần cập nhật", "Yêu cầu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                }
            };

            DgvCauHoiDT.CellValueChanged += (s, e) =>
              {
                  if (e.ColumnIndex == 0)
                  {
                      int soch = 0;
                      foreach (DataGridViewRow row in DgvCauHoiDT.Rows)
                      {
                          var chon = row.Cells["Chon"] as DataGridViewCheckBoxCell;
                          if (chon.Value == chon.TrueValue)
                          {
                              soch++;
                              chon.Selected = true;
                          }
                          else
                          {
                              chon.Selected = false;
                          }
                      }
                      qldtLblSoCHDuocChon.Text = soch + " câu";
                  }
              };
            
            QLDTDgvDT.CellClick += (s, e) =>
              {
                  if (e.ColumnIndex == 0)
                  {
                      int madt = int.Parse(QLDTDgvDT.Rows[e.RowIndex].Cells["maDT"].Value.ToString());
                      var QLTTN = new QLTTNDataContext();
                      
                      QLTTN.CT_DeThi.DeleteAllOnSubmit(QLTTN.CT_DeThi.Where(ctdt => ctdt.maDT == madt));
                      QLTTN.SubmitChanges();
                      QLTTN.DeThi.DeleteOnSubmit(QLTTN.DeThi.Where(dt => dt.maDT == madt).Single());
                      QLTTN.SubmitChanges();
                      if (QLTTN.DeThi.Count() == 0)
                      {
                          QLDTDgvDT.Rows.Clear();
                          QLDTThoiGianLamBai.Text = "10";
                      }
                      
                      LoadQLDTDeThi();
                      DgvCauHoiDT.Rows.RemoveAt(e.RowIndex);
                  }
                  else
                  {
                      var QLTTN = new QLTTNDataContext();
                      
                      if (QLTTN.DeThi.Count() == 0)
                      {
                          QLDTDgvDT.Rows.Clear();
                          return;
                      }
                      if (QLDTDgvDT.SelectedRows.Count > 0)
                      {
                          int madt = int.Parse(QLDTDgvDT.SelectedRows[0].Cells["maDT"].Value.ToString());
                          var dschtrongDeThi = QLTTN.CT_DeThi.Where(ctdt => ctdt.maDT == madt).GroupBy(ctdt => ctdt.maDT).Single().ToList();
                          foreach (DataGridViewRow row in DgvCauHoiDT.Rows)
                          {
                              var cell = row.Cells["Chon"] as DataGridViewCheckBoxCell;
                              string str = row.Cells["maCH"].Value.ToString();
                              var mach = int.Parse(str);
                              var timthay = dschtrongDeThi.Where(ch => ch.maCH == mach && ch.maDT == madt).Count();
                              if (timthay == 1)
                              {
                                  cell.Value = cell.TrueValue;
                              }
                              else
                              {
                                  cell.Value = cell.FalseValue;
                              }
                          }
                      }
                      else
                      {
                          MessageBox.Show("Không thể load câu hỏi từ đề thi");
                      }
                      
                  }
              };
            QLDTBtnThemDT.Click += (s, e) =>
            {
                int soch = int.Parse(qldtLblSoCHDuocChon.Text.Replace(" câu", ""));
                if (string.IsNullOrWhiteSpace(QLDTTxtTenDT.Text))
                {
                    MessageBox.Show("Bạn cần phải nhập tên đề thi", "Hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (soch >= 5)
                {
                    var QLTTN = new QLTTNDataContext();
                    
                    var dethiHientai = new DeThi()
                    {
                        maGV = gv.maGV,
                        maMH = gv.maMH,
                        maKhoi = QLCbKhoiLop.SelectedValue.ToString(),
                        TenDT = QLDTTxtTenDT.Text,
                        ThoiGianLamBai = TimeSpan.FromMinutes(double.Parse(QLDTThoiGianLamBai.Text)),
                        NgayTao = DateTime.Now
                    };

                    if (DaTonTai(dethiHientai))
                    {
                        return;
                    }

                    QLTTN.DeThi.InsertOnSubmit(dethiHientai);
                    QLTTN.SubmitChanges();

                    foreach (DataGridViewRow row in DgvCauHoiDT.Rows)
                    {
                        int maDTHienTai = (int)QLTTN.ExecuteQuery<decimal>("select IDENT_CURRENT('dbo.DeThi')").FirstOrDefault();

                        var cell = row.Cells["Chon"] as DataGridViewCheckBoxCell;
                        if (cell.Value == cell.TrueValue)
                        {
                            QLTTN.CT_DeThi.InsertOnSubmit(new CT_DeThi()
                            {
                                maDT = maDTHienTai,
                                maCH = int.Parse(row.Cells["maCH"].Value.ToString())
                            });
                        }
                    }
                    QLTTN.SubmitChanges();
                    

                    LoadQLDTDeThi();
                }
                else
                {
                    MessageBox.Show("Mỗi đề thi cần có ít nhất 5 câu hỏi", "Yêu cầu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };

            QLCHBtnXoaCH.Click += (s, e) =>
            {
                using (var QLTTN = new QLTTNDataContext())
                {
                    if (QLCHDTSDCH.Items.Count > 0)
                    {
                        string str = $"Không thể xóa câu hỏi này vì nó đang được sử dụng trong các đề thi: ";
                        foreach (var item in QLCHDTSDCH.Items)
                        {
                            str += $"{ Environment.NewLine }{item.ToString()}";
                        }
                        str += $"{ Environment.NewLine }Để xóa cần phải loại câu hỏi này khỏi các đề thi trên.";
                        MessageBox.Show(str, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    if (QLCHCbDsCH.SelectedValue == null)
                    {
                        return;
                    }

                    var cauHoiHienTai = QLTTN.CauHoi
                                        .Where(ch => ch.maCH == int.Parse(QLCHCbDsCH.SelectedValue.ToString()))
                                        .FirstOrDefault();
                    QLTTN.DapAn.DeleteAllOnSubmit(cauHoiHienTai.DapAn);
                    QLTTN.CauHoi.DeleteOnSubmit(cauHoiHienTai);
                    try
                    {
                        QLTTN.SubmitChanges();
                    }
                    catch (Exception ee)
                    {
                        return;
                    }
                }
                loadCbCauHoi();
            };

            QLCHBtnSuaCH.Click += (s, e) =>
            {
                if (QLCHCbDsCH.SelectedValue == null)
                {
                    return;
                }

                using (var QLTTN = new QLTTNDataContext())
                {
                    var cauHoiHienTai = QLTTN.CauHoi.Where(ch => ch.maCH == int.Parse(QLCHCbDsCH.SelectedValue.ToString())).FirstOrDefault();

                    cauHoiHienTai.NoiDung = QLCHTxtCauHoi.Text;
                    cauHoiHienTai.CapDo = QLTTN.CapDo.Where(cd => cd.maCD == int.Parse(QLCHCapDo.SelectedValue.ToString())).FirstOrDefault();
                    cauHoiHienTai.maKhoi = QLCbKhoiLop.SelectedValue.ToString();
                    QLTTN.SubmitChanges();
                }

                loadCbCauHoi();
            };

            QLCHBtnThemCH.Click += (s, e) =>
            {
                var QLTTN = new QLTTNDataContext();

                if (string.IsNullOrWhiteSpace(QLCHTxtCauHoi.Text))
                {
                    MessageBox.Show("Bạn cần phải nhập nội dung cho câu hỏi", "Hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (QLTTN.CauHoi.Where(ch => ch.NoiDung.ToLower() == QLCHTxtCauHoi.Text.ToLower()).Count() != 0)
                {
                    MessageBox.Show("Câu hỏi này đã có trong danh sách. Xin mời tạo câu hỏi mới", "Trùng record", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }



                QLTTN.CauHoi.InsertOnSubmit(new CauHoi()
                {
                    NoiDung = QLCHTxtCauHoi.Text,
                    CapDo = QLTTN.CapDo.Where(cd => cd.maCD == int.Parse(QLCHCapDo.SelectedValue.ToString())).SingleOrDefault(),
                    maKhoi = QLCbKhoiLop.SelectedValue.ToString(),
                    maMH = gv.maMH
                });
                QLTTN.SubmitChanges();
                loadCbCauHoi();
                QLCHCbDsCH.SelectedItem = QLCHCbDsCH.Items[QLCHCbDsCH.Items.Count - 1];
                QLCHTxtDapAn.Focus();
            };


            QLCHBtnThemDA.Click += (s, e) =>
            {
                if (QLCHCbDsCH.SelectedValue == null)
                {
                    return;
                }
                if (string.IsNullOrWhiteSpace(QLCHTxtDapAn.Text))
                {
                    MessageBox.Show("Bạn cần phải nhập nội dung cho đáp án", "Hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                using (var QLTTN = new QLTTNDataContext())
                {
                    var cauHoiHienTai = QLTTN.CauHoi.Where(ch => ch.maCH == int.Parse(QLCHCbDsCH.SelectedValue.ToString())).FirstOrDefault();
                    if (cauHoiHienTai.DapAn.Where(da => da.NoiDung.ToLower() == QLCHTxtDapAn.Text.ToLower()).Count() != 0)
                    {
                        MessageBox.Show("Đáp án này đã có trong danh sách. Xin mời tạo đáp án mới", "Lỗi trùng dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        return;
                    }
                    if (cauHoiHienTai.DapAn.Count >= 6)
                    {
                        MessageBox.Show("Mỗi câu hỏi có tối đa 6 đáp án. Xin mời xóa bớt đáp án để thêm mới", "Hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                   
                    QLTTN.SubmitChanges();
                    QLTTN.DapAn.InsertOnSubmit(new DapAn()
                    {
                        NoiDung = QLCHTxtDapAn.Text,
                        DungSai = QLCHCkbDungSai.Checked,
                        CauHoi = QLTTN.CauHoi.Where(ch => ch.maCH == int.Parse(QLCHCbDsCH.SelectedValue.ToString())).SingleOrDefault()
                    });
                }
                LoadQlCHDapAn();
                QLCHDgvDsDA.Rows[QLCHDgvDsDA.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Selected = true;
                QLCHTxtDapAn.Focus();
            };
            QLCHBtnXoaDA.Click += (s, e) =>
            {
                if (QLCHCbDsCH.SelectedValue == null)
                {
                    return;
                }
                var QLTTN = new QLTTNDataContext();
                
                var cauHoiHienTai = QLTTN.CauHoi.Where(ch => ch.maCH == int.Parse(QLCHCbDsCH.SelectedValue.ToString())).FirstOrDefault();
                if (cauHoiHienTai.DapAn.Count <= 2)
                {
                    MessageBox.Show("Mỗi câu hỏi cần phải có tối thiểu 2 đáp án", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (QLCHDgvDsDA.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Hãy chọn đáp án cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                QLTTN.DapAn.DeleteOnSubmit(cauHoiHienTai.DapAn.Where(da => da.maDA == int.Parse(QLCHDgvDsDA.SelectedRows[0].Cells["maDA"].Value.ToString())).FirstOrDefault());
                QLTTN.SubmitChanges();
                
                LoadQlCHDapAn();
            };
            QLCHBtnSuaDA.Click += (s, e) =>
            {
                if (QLCHCbDsCH.SelectedValue == null)
                {
                    return;
                }
                using (var QLTTN = new QLTTNDataContext())
                {
                    var cauHoiHienTai = QLTTN.CauHoi.Where(ch => ch.maCH == int.Parse(QLCHCbDsCH.SelectedValue.ToString())).FirstOrDefault();

                    var dapAnHienTai = cauHoiHienTai.DapAn.Where(da => da.maDA == int.Parse(QLCHDgvDsDA.SelectedRows[0].Cells["maDA"].Value.ToString())).FirstOrDefault();
                    dapAnHienTai.NoiDung = QLCHTxtDapAn.Text;
                    dapAnHienTai.DungSai = QLCHCkbDungSai.Checked;

                    QLTTN.SubmitChanges();
                }
                LoadQlCHDapAn();
                QLCHTxtDapAn.Focus();
            };
            QLCHBtnImportEx.Click += (s, e) =>
            {
                using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Excel Workbook|*.xlsx|Excel Workbook 97-2003|*.xls", ValidateNames = true })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        Cursor = Cursors.WaitCursor;
                        var stream = File.Open(ofd.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        
                        List<CauHoi> cauHoiBiTrung = new List<CauHoi>();
                        List<CauHoi> cauHoiKhongDungChuyenMon = new List<CauHoi>();

                        IExcelDataReader reader;
                        DataSet ds;
                        if (ofd.FilterIndex == 2)
                        {
                            reader = ExcelReaderFactory.CreateBinaryReader(stream);
                        }
                        else
                        {
                            reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                        }

                        ds = reader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true
                            }
                        });
                        reader.Close();


                        
                        int soCauHoiDuocThem = 0, soDapAnDuocThem = 0;
                        var QLTTN = new QLTTNDataContext();
                        
                        DataTable dtCauHoi = ds.Tables["CauHoi"];
                        DataTable dtDapAn = ds.Tables["DapAn"];
                        DataRow firstRow = dtCauHoi.Rows[0];

                        try
                        {
                            foreach (DataRow row in dtCauHoi.Rows)
                            {
                                CauHoi cauHoiTmp = new CauHoi()
                                {
                                    NoiDung = row["NoiDung"].ToString(),
                                    maCD = int.Parse(row["maCD"].ToString()),
                                    maKhoi = row["maKhoi"].ToString(),
                                    maMH = row["maMH"].ToString()
                                };
                                if (cauHoiTmp.maMH == gv.maMH)
                                {
                                    if (QLTTN.CauHoi.Where(ch => ch.NoiDung.ToLower() == cauHoiTmp.NoiDung.ToLower()).Count() == 0)
                                    {
                                        QLTTN.CauHoi.InsertOnSubmit(cauHoiTmp);
                                        QLTTN.SubmitChanges();
                                        soCauHoiDuocThem++;
                                        foreach (DataRow rowDapAn in dtDapAn.Rows)
                                        {
                                            if (rowDapAn["maCH"].ToString() == row["maCH"].ToString())
                                            {
                                                DapAn datmp = new DapAn()
                                                {
                                                    NoiDung = rowDapAn["NoiDung"].ToString(),
                                                    maCH = QLTTN.CauHoi.Max(ch => ch.maCH),
                                                    DungSai = rowDapAn["DungSai"].ToString().ToLower() == "true" ? true : false
                                                };
                                                QLTTN.DapAn.InsertOnSubmit(datmp);
                                                soDapAnDuocThem++;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        cauHoiBiTrung.Add(cauHoiTmp);
                                    }
                                }
                                else
                                {
                                    cauHoiKhongDungChuyenMon.Add(cauHoiTmp);
                                }
                            }
                            QLTTN.SubmitChanges();
                        }
                        catch (Exception err)
                        {
                            return;
                        }

                        
                        loadCbCauHoi();
                        LoadQlCHDapAn();
                        LoadQLDTCauHoi();
                        LoadQLDTDeThi();
                        Cursor = Cursors.Default;

                        if (cauHoiBiTrung.Count > 0)
                        {
                            string strCauHois = "";
                            for (int i = 0; i < cauHoiBiTrung.Count; i++)
                            {
                                string str = cauHoiBiTrung[i].NoiDung;
                                int maxLeng = 50;
                                if (str.Length > maxLeng)
                                {
                                    str = str.Replace(str.Substring(maxLeng, str.Length - maxLeng), " ...");
                                }
                                strCauHois += $"{Environment.NewLine} {i + 1}. {str}";
                            }
                            MessageBox.Show($">>>> DANH SÁCH NHỮNG CÂU HỎI BỊ TRÙNG <<<< {strCauHois}", "Không thể import", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        }
                        if (cauHoiKhongDungChuyenMon.Count > 0)
                        {
                            string strCauHois = "";
                            for (int i = 0; i < cauHoiKhongDungChuyenMon.Count; i++)
                            {
                                string str = cauHoiKhongDungChuyenMon[i].NoiDung;
                                int maxLeng = 50;
                                if (str.Length > maxLeng)
                                {
                                    str = str.Replace(str.Substring(maxLeng, str.Length - maxLeng), " ...");
                                }
                                strCauHois += $"{Environment.NewLine} {i + 1}. {str}";
                            }
                            MessageBox.Show($">>>> DANH SÁCH NHỮNG CÂU HỎI KHÔNG ĐÚNG CHUYÊN MÔN <<<< {strCauHois}", "Không thể import", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }

                        MessageBox.Show($">>>> KẾT QUẢ IMPORT DANH SÁCH CÂU HỎI <<<< {Environment.NewLine} Tổng số câu hỏi được thêm: {soCauHoiDuocThem} {Environment.NewLine} Tổng số đáp án được thêm: {soDapAnDuocThem}", "Thông báo");
                        
                    }
                }

            };
            QLCHBtnExportEX.Click += (s, e) =>
            {
                using (var sfd = new SaveFileDialog()
                {
                    CreatePrompt = false,
                    OverwritePrompt = true,
                    AddExtension = true,
                    Filter = "Excel Workbook|*.xlsx|Excel Workbook 97-2003|*.xls",
                    ValidateNames = true,
                })
                {
                    if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {

                        DataTable dtCauHoi = new DataTable();
                        dtCauHoi.TableName = "CauHoi";
                        dtCauHoi.Columns.Add("maCH", typeof(int));
                        dtCauHoi.Columns.Add("NoiDung", typeof(string));
                        dtCauHoi.Columns.Add("maCD", typeof(int));
                        dtCauHoi.Columns.Add("maMH", typeof(string));
                        dtCauHoi.Columns.Add("maKhoi", typeof(string));

                        DataTable dtDapAn = new DataTable();
                        dtDapAn.TableName = "DapAn";
                        dtDapAn.Columns.Add("maCH", typeof(int));
                        dtDapAn.Columns.Add("maDA", typeof(int));
                        dtDapAn.Columns.Add("NoiDung", typeof(string));
                        dtDapAn.Columns.Add("DungSai", typeof(bool));

                        using (var QLTTN = new QLTTNDataContext())
                        {
                            List<CauHoi> chs = QLTTN.CauHoi.Where(ch => ch.maMH == gv.maMH).ToList();
                            for (int i = 0; i < chs.Count; i++)
                            {
                                foreach (var da in chs[i].DapAn)
                                {
                                    da.maCH = i + 1;
                                    dtDapAn.Rows.Add(da.maCH, da.maDA, da.NoiDung, da.DungSai);
                                }

                                // thay đổi mã câu hỏi chỗ này để xuất ra theo thứ tự cho đẹp, khi import thì canh chỉnh lại, vì maCH
                                // có tính chất identity tự động tăng, khi insert trong db
                                chs[i].maCH = i + 1;

                                dtCauHoi.Rows.Add(chs[i].maCH, chs[i].NoiDung, chs[i].maCD, chs[i].maMH, chs[i].maKhoi);
                            }
                        };

                        XLWorkbook wb = new XLWorkbook();
                        wb.Worksheets.Add(dtCauHoi, dtCauHoi.TableName);
                        wb.Worksheets.Add(dtDapAn, dtDapAn.TableName);


                        wb.SaveAs(sfd.InitialDirectory + sfd.FileName);
                    }
                }
            };

            t.Abort();
        }

        private void qlchDgvDsda_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            
        }

        private void qlchDgvDsda_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void qlchCbDsch_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void qlchLbDeThiSuDungCauHoi_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void qlchBtnImport_Click(object sender, EventArgs e)
        {

        }

        private void qldtDgvCauHoi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

      

        private void qlktBtnThemKT_Click(object sender, EventArgs e)
        {

        }

        private void qlktBtnSuaKT_Click(object sender, EventArgs e)
        {

        }

        private void qlktBtnPrintResult_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {

        }

        private void qlhsRbHS_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dgvDsda_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            

        }

        //private void QLCHBtnThemCH_Click(object sender, EventArgs e)
        //{
        //    //MessageBox.Show("đang test ", "Trùng record", MessageBoxButtons.OK);

            
        //}
    }
}
