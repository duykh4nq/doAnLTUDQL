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
        private int CheckDS = 0;
        private frmLogin frmLogin = null;
        private GiaoVien GV = null;
        private List<string> lkl = null;
        private BindingSource BSCauHoi = new BindingSource();
        private BindingSource BSDapAn = new BindingSource();
        private BindingSource BSDethi = new BindingSource();
        private BindingSource BSLichThi = new BindingSource();
        private BindingSource BSHocSinh = new BindingSource();
        private BindingSource BSKyThi = new BindingSource();
        private BindingSource BSLichThiThu = new BindingSource();
        private BindingSource BSHocSinhThu = new BindingSource();
        private BindingSource BSKyThiThu = new BindingSource();                      
        private BindingSource BSQlhsHocSinh = new BindingSource();
        private BindingSource BSQlhsKyThi = new BindingSource();
        private BindingSource BSQlhsCauHoi = new BindingSource();

        private void LoadCBKhoiLop()
        {
            var QLTTN = new QLTTNDataContext();
            
            lkl = GV.CT_GiangDays.Select(ctgd => ctgd.maKhoi).Distinct().ToList();
            cbKhoiLop.DataSource = lkl;
            QLDTLblNguoiTao.Text = GV.HoTen;
            QLDTLblNgayTao.Text = DateTime.UtcNow.ToString("dd-MM-yyyy");
            if (cbKhoiLop.Items.Count != 0)
            {
                cbKhoiLop.SelectedItem = cbKhoiLop.Items[0];
                string txt = cbKhoiLop.SelectedValue.ToString();
            }
            txtHoTenGV.Text = GV.HoTen;
            txtNgaySinhGV.Text = $"{GV.NgaySinh.Value.ToShortDateString()}";
            lblChuyenMon.Text = $"Chuyên môn: {QLTTN.MonHocQL.Where(mh => mh.maMH == GV.maMH).Single().tenMH}";


            txtHoTenGV.DoubleClick += (s, e) =>
             {
                 txtHoTenGV.ReadOnly = false;
                 txtHoTenGV.SelectAll();
             };
            txtHoTenGV.Validated += (s, e) =>
              {
                  if (string.IsNullOrWhiteSpace(txtHoTenGV.Text))
                  {
                      errorProvider.SetError(txtHoTenGV, "Không được để trống họ tên");
                      txtHoTenGV.Focus();
                  }
                  else
                  {
                      errorProvider.SetError(txtHoTenGV, "");
                      txtHoTenGV.ReadOnly = true;
                      using (var qlttn2 = new QLTTNDataContext())
                      {
                          qlttn2.GiaoVienQL.Where(gv1 => gv1.maGV == GV.maGV).FirstOrDefault().HoTen = txtHoTenGV.Text;
                          qlttn2.SubmitChanges();
                      }
                  }
              };
            txtNgaySinhGV.DoubleClick += (s, e) =>
              {
                  txtNgaySinhGV.ReadOnly = false;
                  txtNgaySinhGV.SelectAll();
              };
            txtNgaySinhGV.Validated += (s, e) =>
              {
                  try
                  {
                      errorProvider.SetError(txtNgaySinhGV, "");
                      txtNgaySinhGV.ReadOnly = true;
                      using (var qlttn2 = new QLTTNDataContext())
                      {
                          qlttn2.GiaoVienQL.Where(gv1 => gv1.maGV == GV.maGV).FirstOrDefault().NgaySinh = DateTime.Parse(txtNgaySinhGV.Text);
                          qlttn2.SubmitChanges();
                      }
                  }
                  catch (Exception exec)
                  {
                      errorProvider.SetError(txtNgaySinhGV, "Hãy nhập đúng ngày sinh");
                      txtNgaySinhGV.Focus();
                      return;
                  }
            };
            
        }
        
        private void LoadCBCauHoi()
        {
            var QLTTN = new QLTTNDataContext();
            
            try
            {
                var cauHoi = (QLTTN.CauHoiQL.Where(ch => ch.maKhoi == cbKhoiLop.SelectedValue.ToString() && ch.maMH == GV.maMH)
                                                .Select(ch => new { ch.maCH, ch.NoiDung, ch.CapDo.TenCD, ch.maCD }).ToList());
                BSCauHoi.DataSource = cauHoi;
            }
            catch (Exception e)
            {
                return;
            }

            if (BSCauHoi.Count > 0)
            {
                QLCHCbDSCH.DataSource = BSCauHoi;
                QLCHCbDSCH.DisplayMember = "NoiDung";
                QLCHCbDSCH.ValueMember = "maCH";

                QLCHCapDo.DataSource = QLTTN.CapDoQL.ToList();
                QLCHCapDo.DisplayMember = "TenCD";
                QLCHCapDo.ValueMember = "maCD";

                var macd = QLTTN.CauHoiQL.Where(ch => ch.maCH.ToString() == QLCHCbDSCH.SelectedValue.ToString()).Single().maCD;
                QLCHCapDo.SelectedValue = macd;

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
                            int maCapDo = int.Parse(e.Value.ToString());
                            e.Value = (QLCHCapDo.DataSource as List<CapDo>).Where(cd => cd.maCD == maCapDo).FirstOrDefault().TenCD;
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
                QLCHCbDSCH.DataSource = null;
            }
            
        }
        private void LoadQLCHDgvDapAn()
        {
            using (var QLTTN = new QLTTNDataContext())
            {

               if (QLCHCbDSCH.Items.Count > 0)
               {
                   try
                   {
                       BSDapAn.DataSource = QLTTN.CauHoiQL.Where(ch => ch.maCH == int.Parse(QLCHCbDSCH.SelectedValue.ToString())).SingleOrDefault().DapAns.ToList();
                       QLCHDgvDSDA.DataSource = BSDapAn;
                   }
                   catch (Exception e)
                   {
                       return;
                   }

                   QLCHDgvDSDA.Columns["maCH"].Visible = false;
                   QLCHDgvDSDA.Columns["CauHoi"].Visible = false;
                   QLCHDgvDSDA.Columns["maDA"].Visible = false;
                   QLCHDgvDSDA.Columns["NoiDung"].DisplayIndex = 1;
                   QLCHDgvDSDA.Columns["NoiDung"].Width = 310;
                   QLCHDgvDSDA.Columns["NoiDung"].HeaderText = "Nội dung đáp án";
                   QLCHDgvDSDA.Columns["DungSai"].DisplayIndex = 2;
                   QLCHDgvDSDA.Columns["DungSai"].Width = 115;
                   QLCHDgvDSDA.Columns["DungSai"].HeaderText = "Đúng Sai";

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
                   QLCHDgvDSDA.DataSource = null;
               }

            
                
            }
        }
        private void SetQLCH()
        {
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
            QLCHCbDSCH.SelectedIndexChanged += (s, e) =>
             {
                 LoadQLCHDgvDapAn();

                 if (QLCHCbDSCH.SelectedValue == null)
                 {
                     return;
                 }
                 var QLTTN = new QLTTNDataContext();
                 
                 if (QLTTN.DeThiQL.Count() == 0)
                 {
                     return;
                 }
                 QLCHCbDSCH.ValueMember = "maCH";
                 
             };
            
        }

        private void LoadQLDTDgvDeThi()
        {
            var QLTTN = new QLTTNDataContext();
            
            try
            {
                var sourceDt = QLTTN.DeThiQL.Where(dt => dt.maMH == GV.maMH && dt.maKhoi == cbKhoiLop.SelectedValue.ToString())
                                            .Select(dt => new { dt.maDT, dt.TenDT, dt.GiaoVien.HoTen, dt.ThoiGianLamBai, dt.NgayTao })
                                            .ToList();
                BSDethi.DataSource = sourceDt;
            }
            catch (Exception e)
            {
                return;
            }
            if (BSDethi.Count > 0)
            {
                QLDTDgvDeThi.DataSource = BSDethi;

                QLDTDgvDeThi.Columns["maDT"].HeaderText = "Mã";
                QLDTDgvDeThi.Columns["maDT"].Width = 40;
                QLDTDgvDeThi.Columns["TenDT"].HeaderText = "Tên đề thi";
                QLDTDgvDeThi.Columns["HoTen"].HeaderText = "Giáo viên ra đề";
                QLDTDgvDeThi.Columns["ThoiGianLamBai"].HeaderText = "Thời gian làm bài";
                QLDTDgvDeThi.Columns["ThoiGianLamBai"].Width = 90;
                QLDTDgvDeThi.Columns["NgayTao"].HeaderText = "Ngày tạo";

                if (qldtTxtTenDT.DataBindings.Count == 0)
                {
                    qldtTxtTenDT.DataBindings.Add("Text", BSDethi, "TenDT", true, DataSourceUpdateMode.Never, "null");
                }
                if (QLDTLblNgayTao.DataBindings.Count == 0)
                {
                    QLDTLblNgayTao.DataBindings.Add("Text", BSDethi, "NgayTao", true, DataSourceUpdateMode.Never, 0);
                }
                if (QLDTLblNguoiTao.DataBindings.Count == 0)
                {
                    QLDTLblNguoiTao.DataBindings.Add("Text", BSDethi, "HoTen", true, DataSourceUpdateMode.Never, 0);
                }
                if (qldtTxtThoiGianLamBai.DataBindings.Count == 0)
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
                    qldtTxtThoiGianLamBai.DataBindings.Add(bd);
                }
            }
            else
            {
                qldtTxtTenDT.DataBindings.Clear();
                QLDTLblNguoiTao.DataBindings.Clear();
                QLDTLblNgayTao.DataBindings.Clear();
                qldtTxtThoiGianLamBai.DataBindings.Clear();
                QLDTDgvDeThi.DataSource = null;
            }
            
        }
        private void LoadQLDTDgvCauHoi()
        {
            if (BSCauHoi.Count > 0)
            {
                QLDTDgvCauHoi.DataSource = BSCauHoi;
                QLDTDgvCauHoi.Columns["maCH"].Width = 40;
                QLDTDgvCauHoi.Columns["maCH"].HeaderText = "Mã";
                QLDTDgvCauHoi.Columns["NoiDung"].Width = 140;
                QLDTDgvCauHoi.Columns["NoiDung"].HeaderText = "Nội dung";
                QLDTDgvCauHoi.Columns["TenCD"].Width = 80;
                QLDTDgvCauHoi.Columns["TenCD"].HeaderText = "Cấp độ";
                QLDTDgvCauHoi.Columns["maCD"].Visible = false;
                QLDTDgvCauHoi.AllowUserToOrderColumns = true;
            }
            else
            {
                QLDTDgvCauHoi.DataSource = null;
            }
        }
        private void SetQLDT()
        {
            QLDTDgvCauHoi.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                Name = "Chon",
                HeaderText = "Chọn câu hỏi",
                Width = 80,
                TrueValue = true,    
                FalseValue = false,
                IndeterminateValue = false
            });
            QLDTDgvDeThi.Columns.Add(new DataGridViewButtonColumn()
            {
                Name = "Xoa",
                HeaderText = "Xóa đề thi",
                Width = 70
            });

            qldtTxtThoiGianLamBai.Text = "10";

            qldtTxtThoiGianLamBai.KeyDown += (s, e) =>
             {
                 if (e.KeyValue >= 48 && e.KeyValue <= 57 ||
                    e.KeyValue >= 96 && e.KeyValue <= 105 ||
                    e.KeyCode == Keys.Back ||
                    e.KeyCode == Keys.Delete ||
                    e.KeyCode == Keys.Left ||
                    e.KeyCode == Keys.Right)
                 {
                 }
                 else if (e.KeyCode == Keys.Up)
                 {
                     int soPhut = int.Parse(qldtTxtThoiGianLamBai.Text);
                     soPhut += 5;
                     qldtTxtThoiGianLamBai.Text = soPhut.ToString();
                 }
                 else if (e.KeyCode == Keys.Down)
                 {
                     int soPhut = int.Parse(qldtTxtThoiGianLamBai.Text);
                     soPhut -= 5;
                     qldtTxtThoiGianLamBai.Text = soPhut.ToString();
                 }
                 else
                 {
                     e.SuppressKeyPress = true;
                 }
             };
            qldtTxtThoiGianLamBai.KeyUp += (s, e) =>
             {
                 if (string.IsNullOrWhiteSpace(qldtTxtThoiGianLamBai.Text))
                 {
                     qldtTxtThoiGianLamBai.Text = "5";
                 }
                 int soPhut = int.Parse(qldtTxtThoiGianLamBai.Text);
                 if (soPhut < 0)
                 {
                     qldtTxtThoiGianLamBai.Text = "5";
                 }
                 else if (soPhut > 180)
                 {
                     qldtTxtThoiGianLamBai.Text = "180";
                 }
             };
            qldtTxtTenDT.Validating += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(qldtTxtTenDT.Text))
                {
                    e.Cancel = true;
                    qldtTxtTenDT.Focus();
                    errorProvider.SetError(qldtTxtTenDT, "Không được để trống tên đề thi");
                }
                else
                {
                    e.Cancel = false;
                    errorProvider.SetError(qldtTxtTenDT, "");
                }
            };
           
            QLDTDgvDeThi.CellPainting += (s, e) =>
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


        private void setQlkt()
        {
            QLKTDgvKT.Columns.Add(new DataGridViewButtonColumn()
            {
                Name = "Xoa",
                Width = 80,
                HeaderText = "Xóa kỳ thi"
            });

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
        private void LoadQLKTDgvHocSinh()
        {
            var QLTTN = new QLTTNDataContext();
            
            if (QLTTN.HocSinhQL.Count() == 0)
            {
                return;
            }
            if (QLKTDgvKT.SelectedRows.Count > 0 && QLKTDgvDT.SelectedRows.Count > 0)
            {
                int makt = int.Parse(QLKTDgvKT.SelectedRows[0].Cells["maKT"].Value.ToString());
                int madt = int.Parse(QLKTDgvDT.SelectedRows[0].Cells["maDT"].Value.ToString());
                try
                {
                    var dshs = QLTTN.BaiLamQL.Where(bl => bl.maKT == makt && bl.maDT == madt).Select(bl => bl.HocSinh);
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
                catch (Exception e)
                {
                    return;
                }
            }
            else
            {
                QLKTDgvHS.DataSource = null;
            }
            
        }
        private void LoadQLKTDgvDeThi()
        {
            var QLTTN = new QLTTNDataContext();
            
            if (QLTTN.DeThiQL.Count() == 0)
            {
                return;
            }
            try
            {
                if (QLKTDgvKT.SelectedRows.Count > 0)
                {
                    int MaKT = int.Parse(QLKTDgvKT.SelectedRows[0].Cells["maKT"].Value.ToString());

                    BSLichThi.DataSource = QLTTN.BuoiThiQL.Where(bt => bt.maKT == MaKT)
                                                        .Select(bt => new { bt.DeThi.maDT, bt.DeThi.TenDT, bt.DeThi.maMH, bt.DeThi.MonHoc.tenMH, bt.NgayGioThi, bt.DeThi.ThoiGianLamBai });
                    if (BSLichThi.Count > 0)
                    {
                        QLKTDgvDT.DataSource = BSLichThi;
                        QLKTDgvDT.Columns["maDT"].HeaderText = "Mã đề";
                        QLKTDgvDT.Columns["maDT"].Width = 80;
                        QLKTDgvDT.Columns["maMH"].Visible = false;
                        QLKTDgvDT.Columns["TenDT"].HeaderText = "Tên đề thi";
                        QLKTDgvDT.Columns["TenDT"].Width = 120;
                        QLKTDgvDT.Columns["tenMH"].HeaderText = "Môn thi";
                        QLKTDgvDT.Columns["ThoiGianLamBai"].HeaderText = "Thời gian làm bài";
                        QLKTDgvDT.Columns["NgayGioThi"].Width = 160;
                        QLKTDgvDT.Columns["NgayGioThi"].HeaderText = "Ngày giờ thi";

                        qlktGbTongSoDeThi.Text = $"Tổng số buổi thi (1buổi/1môn): {QLKTDgvDT.RowCount}";
                    }
                    else
                    {
                        QLKTDgvDT.DataSource = null;
                    }
                }
                else
                {
                    QLKTDgvDT.DataSource = null;
                }
            }
            catch (Exception e)
            {
                return;
            }
            
        }
        public void LoadQLKTDgvKyThi()
        {
            using (var QLTTN = new QLTTNDataContext())
            {
                if (QLTTN.KyThiQL.Count() == 0)
                {
                    return;
                }
                try
                {
                    string makhoi = cbKhoiLop.SelectedValue.ToString();
                    BSKyThi.DataSource = QLTTN.KyThiQL.Where(kt => kt.maKhoi == makhoi && kt.LoaiKT == "ThiThiet")
                        .Select(kt => new { kt.maKT, kt.TenKT, kt.LoaiKT }).ToList();
                }
                catch (Exception e)
                {
                    return;
                }
                if (BSKyThi.Count > 0)
                {
                    QLKTDgvKT.DataSource = BSKyThi;
                    QLKTDgvKT.Columns["maKT"].Width = 30;
                    QLKTDgvKT.Columns["maKT"].HeaderText = "Mã";
                    QLKTDgvKT.Columns["TenKT"].Width = 100;
                    QLKTDgvKT.Columns["TenKT"].HeaderText = "Tên";
                    QLKTDgvKT.Columns["LoaiKT"].HeaderText = "Phân loại";
                }
                else
                {
                    QLKTDgvKT.DataSource = null;
                }
            }
        }


        private void SetQLKTOT()
        {
            QLKTOTDgvKT.Columns.Add(new DataGridViewButtonColumn()
            {
                Name = "Xoa",
                Width = 80,
                HeaderText = "Xóa kỳ thi"
            });

            QLKTOTDgvKT.CellPainting += (s, e) =>
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
        private void LoadQLKTOTDgvHocSinh()
        {
            using (var QLTTN = new QLTTNDataContext())
            {
                if (QLTTN.HocSinhQL.Count() == 0)
                {
                    return;
                }
                if (QLKTOTDgvKT.SelectedRows.Count > 0 && QLKTOTDgvDT.SelectedRows.Count > 0)
                {
                    int MaKT = int.Parse(QLKTOTDgvKT.SelectedRows[0].Cells["maKT"].Value.ToString());
                    int MaDT = int.Parse(QLKTOTDgvDT.SelectedRows[0].Cells["maDT"].Value.ToString());
                    try
                    {
                        var dshs = QLTTN.BaiLamQL.Where(bl => bl.maKT == MaKT && bl.maDT == MaDT).Select(bl => bl.HocSinh);
                        QLKTOTDgvHS.DataSource = dshs.Select(hs => new { hs.maHS, hs.HoTen, hs.maKhoi, hs.maLop, hs.NgaySinh }).ToList();

                        if (dshs.Count() > 0)
                        {
                            QLKTOTDgvHS.Columns["maHS"].Width = 120;
                            QLKTOTDgvHS.Columns["HoTen"].Width = 220;
                            QLKTOTDgvHS.Columns["maLop"].Width = 80;
                            QLKTOTDgvHS.Columns["NgaySinh"].Width = 150;
                            QLKTOTDgvHS.Columns["maKhoi"].Visible = false;

                            QLKTOTDgvHS.Columns["maHS"].HeaderText = "Mã học sinh";
                            QLKTOTDgvHS.Columns["HoTen"].HeaderText = "Họ tên";
                            QLKTOTDgvHS.Columns["maLop"].HeaderText = "Lớp học";
                            QLKTOTDgvHS.Columns["NgaySinh"].HeaderText = "Ngày sinh";

                            qlktGbTongSoThiSinh.Text = $"Tổng số thí sinh tham gia: {QLKTOTDgvHS.RowCount}";
                        }
                        else
                        {
                            QLKTOTDgvHS.DataSource = null;
                        }
                    }
                    catch (Exception e)
                    {
                        return;
                    }
                }
                else
                {
                    QLKTOTDgvHS.DataSource = null;
                }
            }
        }
        private void LoadQLKTOTDgvDeThi()
        {
            var QLTTN = new QLTTNDataContext();
            
            if (QLTTN.DeThiQL.Count() == 0)
            {
                return;
            }
            try
            {
                if (QLKTOTDgvKT.SelectedRows.Count > 0)
                {
                    int MaKT = int.Parse(QLKTOTDgvKT.SelectedRows[0].Cells["maKT"].Value.ToString());

                    BSLichThiThu.DataSource = QLTTN.BuoiThiQL.Where(bt => bt.maKT == MaKT).Select(bt => new { bt.DeThi.maDT, bt.DeThi.TenDT, bt.DeThi.maMH, bt.DeThi.MonHoc.tenMH, bt.NgayGioThi, bt.DeThi.ThoiGianLamBai });
                    if (BSLichThiThu.Count > 0)
                    {
                        QLKTOTDgvDT.DataSource = BSLichThiThu;
                        QLKTOTDgvDT.Columns["maDT"].HeaderText = "Mã đề";
                        QLKTOTDgvDT.Columns["maDT"].Width = 80;
                        QLKTOTDgvDT.Columns["maMH"].Visible = false;
                        QLKTOTDgvDT.Columns["TenDT"].HeaderText = "Tên đề thi";
                        QLKTOTDgvDT.Columns["TenDT"].Width = 120;
                        QLKTOTDgvDT.Columns["tenMH"].HeaderText = "Môn thi";
                        QLKTOTDgvDT.Columns["ThoiGianLamBai"].HeaderText = "Thời gian làm bài";
                        QLKTOTDgvDT.Columns["NgayGioThi"].Width = 160;
                        QLKTOTDgvDT.Columns["NgayGioThi"].HeaderText = "Ngày giờ thi";

                        qlktGbTongSoDeThi.Text = $"Tổng số đề thi được chọn: {QLKTOTDgvDT.RowCount}";
                    }
                    else
                    {
                        QLKTOTDgvDT.DataSource = null;
                    }
                }
                else
                {
                    QLKTOTDgvDT.DataSource = null;
                }
            }
            catch (Exception e)
            {
                return;
            }
            
        }
        public void LoadQLKTOTDgvKyThi()
        {
            var QLTTN = new QLTTNDataContext();
            
            if (QLTTN.KyThiQL.Count() == 0)
            {
                return;
            }
            try
            {
                string makhoi = cbKhoiLop.SelectedValue.ToString();
                BSKyThiThu.DataSource = QLTTN.KyThiQL.Where(kt => kt.maKhoi == makhoi && kt.LoaiKT == "ThiThu")
                    .Select(kt => new { kt.maKT, kt.TenKT, kt.LoaiKT }).ToList();
            }
            catch (Exception e)
            {
                return;
            }
            if (BSKyThiThu.Count > 0)
            {
                QLKTOTDgvKT.DataSource = BSKyThiThu;
                QLKTOTDgvKT.Columns["maKT"].Width = 30;
                QLKTOTDgvKT.Columns["maKT"].HeaderText = "Mã";
                QLKTOTDgvKT.Columns["TenKT"].Width = 100;
                QLKTOTDgvKT.Columns["TenKT"].HeaderText = "Tên";
                QLKTOTDgvKT.Columns["LoaiKT"].HeaderText = "Phân loại";
            }
            else
            {
                QLKTOTDgvKT.DataSource = null;
            }
            
        }

        private void LoadQlhsDgvHocSinh()
        {
            string makhoi = cbKhoiLop.SelectedValue.ToString();
            var QLTTN = new QLTTNDataContext();
            
            BSQlhsHocSinh.DataSource = QLTTN.HocSinhQL.Where(hs => hs.maKhoi == makhoi).Select(hs => new
            {
                hs.maHS,
                hs.HoTen,
                hs.maKhoi,
                hs.maLop,
                hs.NgaySinh,
                SoKTDaThamGia = QLTTN.KyThiQL.Where(kt => kt.BuoiThis
                                            .Where(bt => bt.BaiLams
                                            .Where(bl => bl.maHS == hs.maHS).Count() > 0).Count() > 0).Count(),
                SoDTDaLam = hs.BaiLams.Count,
                DTB = hs.BaiLams.Average(bl => bl.DiemSo)
            }).ToList();

            if (BSQlhsHocSinh.Count > 0)
            {
                QLHSDgvDS.DataSource = BSQlhsHocSinh;

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
        private void LoadQLHSDgvKyThi()
        {
            string makhoi = cbKhoiLop.SelectedValue.ToString();
            var QLTTN = new QLTTNDataContext();
            
            BSQlhsKyThi.DataSource = QLTTN.KyThiQL.Where(kt => kt.maKhoi == makhoi).Select(kt => new
            {
                kt.maKT,
                kt.TenKT,
                kt.LoaiKT,
                TongSoMonThi = kt.BuoiThis.Count,
                TongSoThiSinh = kt.BuoiThis.Sum(bt => bt.BaiLams.Count),
                DTB = kt.BuoiThis.Average(bt => bt.BaiLams.Average(bl => bl.DiemSo))
            });

            if (BSQlhsKyThi.Count > 0)
            {
                QLHSDgvDS.DataSource = BSQlhsKyThi;

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
        private void LoadQLHSDgvCauHoi()
        {
            string makhoi = cbKhoiLop.SelectedValue.ToString();
            var QLTTN = new QLTTNDataContext();
            
            double tongSoDeThi = QLTTN.DeThiQL.Where(dt => dt.maKhoi == makhoi && dt.maMH == GV.maMH).Count();
            BSQlhsCauHoi.DataSource = QLTTN.CauHoiQL.Where(ch => ch.maKhoi == makhoi && ch.maMH == GV.maMH).Select(ch => new
            {
                ch.maCH,
                ch.NoiDung,
                ch.CapDo.TenCD,
           
            });
            if (BSQlhsCauHoi.Count > 0)
            {
                QLHSDgvDS.DataSource = BSQlhsCauHoi;

                QLHSDgvDS.Columns["maCH"].Width = 100;
                QLHSDgvDS.Columns["NoiDung"].Width = 360;
                QLHSDgvDS.Columns["TenCD"].Width = 100;
                
                QLHSDgvDS.Columns["maCH"].HeaderText = "Mã câu hỏi";
                QLHSDgvDS.Columns["NoiDung"].HeaderText = "Nội dung";
                QLHSDgvDS.Columns["TenCD"].HeaderText = "Cấp độ";
                
            }
            else
            {
                QLHSDgvDS.DataSource = null;
            }
            
        }

        private bool DaTonTai(DeThi dethiHientai)
        {
            var QLTTN = new QLTTNDataContext();
            

            if (QLTTN.DeThiQL.Where(dt => dt.TenDT == dethiHientai.TenDT).Count() > 0)
            {
                MessageBox.Show("Tên đề Đã Tồn Tại", "Trùng dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }

            List<int> lmach = new List<int>();
            foreach (DataGridViewRow row in QLDTDgvCauHoi.Rows)
            {
                var cell = row.Cells["Chon"] as DataGridViewCheckBoxCell;
                if (cell.Value == cell.TrueValue)
                {
                    lmach.Add(int.Parse(row.Cells["maCH"].Value.ToString()));
                }
            }
            var DSDeThi = QLTTN.CT_DeThiQL.GroupBy(ctdt => ctdt.maDT).ToList();
            foreach (var dethi in DSDeThi)
            {
                int SoCauTimThay = 0;
                foreach (var CH in lmach)
                {
                    if (dethi.Where(ctdt => ctdt.maCH == CH).Count() > 0)
                    {
                        SoCauTimThay++;
                    }
                }
                if (SoCauTimThay == dethi.Count())
                {
                    string line = "";
                    for (int i = 0; i < dethi.Count(); i++)
                    {
                        line += $"{ Environment.NewLine } {dethi.ElementAt(i).maCH}. {dethi.ElementAt(i).CauHoi.NoiDung}";
                    }
                    MessageBox.Show($"Đã Tồn Tại tên Để Thi Này: <{dethi.ElementAt(0).DeThi.TenDT}> {line}", "Trùng dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            
            if (e.State == DrawItemState.Selected)
            {
                Brush br = new SolidBrush(Color.LemonChiffon);
                
                e.Graphics.FillRectangle(br, e.Bounds);
                SizeF sz = e.Graphics.MeasureString(tabControl1.TabPages[e.Index].Text, e.Font);
                e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + (e.Bounds.Width - sz.Width) / 2, e.Bounds.Top + (e.Bounds.Height - sz.Height) / 2 + 1);

                Rectangle rect = e.Bounds;
                rect.Offset(0, 1);
                rect.Inflate(0, -1);
                e.Graphics.DrawRectangle(Pens.DarkGray, rect);
                e.DrawFocusRectangle();
                
            }
            else
            {
                Brush br = new SolidBrush(TabColors[tabControl1.TabPages[e.Index]]);
                
                e.Graphics.FillRectangle(br, e.Bounds);
                SizeF sz = e.Graphics.MeasureString(tabControl1.TabPages[e.Index].Text, e.Font);
                e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + (e.Bounds.Width - sz.Width) / 2, e.Bounds.Top + (e.Bounds.Height - sz.Height) / 2 + 1);

                Rectangle rect = e.Bounds;
                
                e.Graphics.DrawRectangle(Pens.DarkGray, rect);
                e.DrawFocusRectangle();
                
            }
        }

        private int soDongDuocChon(DataGridView dgv)
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

       
        /// <param name="dgv">Datagridview muốn check cần phải có cột DataGridViewCheckBoxColumn.Name = "Chon"</param>
        /// <param name="soDongMuonCheck">Số dòng cần được check, phải nhỏ hơn số dòng trong dgv, nếu lớn hơn thì sẽ chọn hết</param>
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
        public frmGiaoVien(frmLogin frmlogin, GiaoVien GV)
        {
            Thread t = new Thread(new ThreadStart(() =>
            {
                Application.Run(new frmSplashScreen());
            }));
            t.Start();
            this.GV = GV;
            this.frmLogin = frmlogin;
            InitializeComponent();

            tabControl1.SelectedIndex = 2;
            LoadCBKhoiLop();
            LoadCBCauHoi();
            LoadQLCHDgvDapAn();
            SetQLCH();

            SetQLDT();
            LoadQLDTDgvDeThi();
            LoadQLDTDgvCauHoi();

            setQlkt();
            LoadQLKTDgvHocSinh();
            LoadQLKTDgvDeThi();
            LoadQLKTDgvKyThi();

            SetQLKTOT();
            LoadQLKTOTDgvKyThi();
            LoadQLKTOTDgvDeThi();
            LoadQLKTOTDgvHocSinh();


            this.tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);
            foreach (TabPage tp in tabControl1.TabPages)
            {
                SetTabHeader(tp, Color.FromKnownColor(KnownColor.Control));
                tp.BackColor = Color.LemonChiffon;
            }
            this.FormClosing += (s, e) =>
            {
                this.frmLogin.Show();
            };

            this.btnDangXuat.Click += (s, e) =>
            {
                frmlogin.Show();
                this.Close();
            };

            this.tabControl1.SelectedIndexChanged += (s, e) =>
             {
                 if (tabControl1.SelectedIndex == 2)
                 {
                     LoadQLKTDgvHocSinh();
                     LoadQLKTDgvDeThi();
                     LoadQLKTDgvKyThi();
                 }
                 else if (tabControl1.SelectedIndex == 3)
                 {
                     LoadQLKTOTDgvKyThi();
                     LoadQLKTOTDgvDeThi();
                     LoadQLKTOTDgvHocSinh();
                 }
             };

            
            this.cbKhoiLop.SelectedIndexChanged += (s, e) =>
             {
                 LoadCBCauHoi();
                 LoadQLCHDgvDapAn();
                 LoadQLDTDgvCauHoi();
                 LoadQLDTDgvDeThi();
                 LoadQLKTDgvDeThi();
                 LoadQLKTDgvHocSinh();
                 LoadQLKTDgvKyThi();
                 LoadQLKTOTDgvHocSinh();
                 LoadQLKTOTDgvDeThi();
                 LoadQLKTOTDgvKyThi();
             };

            QLHSRbCH.CheckedChanged += (s, e) =>
            {
                if (QLHSRbCH.Checked == true)
                {
                    QLHSDgvDS.DataSource = null;
                    LoadQLHSDgvCauHoi();
                }
            };

           
            QLKTOTDgvKT.CellContentClick += (s, e) =>
            {
                if (e.ColumnIndex == 0)
                {
                    var makt = int.Parse(QLKTOTDgvKT.Rows[e.RowIndex].Cells["maKT"].Value.ToString());
                    using (var QLTTN = new QLTTNDataContext())
                    {
                        KyThi ktDuocChon = QLTTN.KyThiQL.Where(kt => kt.maKT == makt).FirstOrDefault();

                        if (ktDuocChon.BuoiThis.Where(buoithi => buoithi.NgayGioThi < DateTime.Now).Count() > 0)
                        {
                            MessageBox.Show($"Không được Kỳ thi đang diễn ra", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                        {
                            QLTTN.BaiLamQL.DeleteAllOnSubmit(QLTTN.BaiLamQL.Where(bl => bl.maKT == makt));
                            QLTTN.BuoiThiQL.DeleteAllOnSubmit(QLTTN.BuoiThiQL.Where(bt => bt.maKT == makt));
                            QLTTN.KyThiQL.DeleteOnSubmit(ktDuocChon);
                            QLTTN.SubmitChanges();
                            QLKTOTDgvKT.Rows.RemoveAt(e.RowIndex);
                            LoadQLKTDgvKyThi();
                            LoadQLKTOTDgvDeThi();
                            LoadQLKTOTDgvHocSinh();
                        }
                    };
                }
            };
            QLKTOTDgvKT.SelectionChanged += (s, e) =>
            {
                LoadQLKTOTDgvDeThi();
                LoadQLKTOTDgvHocSinh();
            };
            QLKTOTDgvDT.SelectionChanged += (s, e) =>
            {
                LoadQLKTOTDgvHocSinh();
            };
           
            QLKTDgvKT.CellContentClick += (s, e) =>
            {
                if (e.ColumnIndex == 0)
                {
                    var makt = int.Parse(QLKTDgvKT.Rows[e.RowIndex].Cells["maKT"].Value.ToString());
                    using (var QLTTN = new QLTTNDataContext())
                    {
                        KyThi ktDuocChon = QLTTN.KyThiQL.Where(kt => kt.maKT == makt).FirstOrDefault();

                        if (ktDuocChon.BuoiThis.Where(buoithi => buoithi.NgayGioThi < DateTime.Now).Count() > 0)
                        {
                            MessageBox.Show($"Không được xóa kỳ thi Đang diễn ra", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                        {
                            QLTTN.BaiLamQL.DeleteAllOnSubmit(QLTTN.BaiLamQL.Where(bl => bl.maKT == makt));
                            QLTTN.BuoiThiQL.DeleteAllOnSubmit(QLTTN.BuoiThiQL.Where(bt => bt.maKT == makt));
                            QLTTN.KyThiQL.DeleteOnSubmit(ktDuocChon);
                            QLTTN.SubmitChanges();
                            QLKTDgvKT.Rows.RemoveAt(e.RowIndex);
                            LoadQLKTDgvKyThi();
                            LoadQLKTDgvDeThi();
                            LoadQLKTDgvHocSinh();
                        }
                    };
                }
            };
            QLKTDgvKT.SelectionChanged += (s, e) =>
             {
                 LoadQLKTDgvDeThi();
                 LoadQLKTDgvHocSinh();
             };
            QLKTDgvDT.SelectionChanged += (s, e) =>
             {
                 LoadQLKTDgvHocSinh();
             };
           QLDTDgvCauHoi.CellValueChanged += (s, e) =>
              {
                  if (e.ColumnIndex == 0)
                  {
                      int soch = 0;
                      foreach (DataGridViewRow row in QLDTDgvCauHoi.Rows)
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
           
            QLDTDgvDeThi.CellClick += (s, e) =>
              {
                  if (e.ColumnIndex == 0)
                  {
                      int madt = int.Parse(QLDTDgvDeThi.Rows[e.RowIndex].Cells["maDT"].Value.ToString());
                      using (var QLTTN = new QLTTNDataContext())
                      {
                          QLTTN.CT_DeThiQL.DeleteAllOnSubmit(QLTTN.CT_DeThiQL.Where(ctdt => ctdt.maDT == madt));
                          QLTTN.SubmitChanges();
                          QLTTN.DeThiQL.DeleteOnSubmit(QLTTN.DeThiQL.Where(dt => dt.maDT == madt).Single());
                          QLTTN.SubmitChanges();
                          if (QLTTN.DeThiQL.Count() == 0)
                          {
                              QLDTDgvDeThi.Rows.Clear();
                              qldtTxtThoiGianLamBai.Text = "10";
                          }
                      }
                      LoadQLDTDgvDeThi();
                      QLDTDgvCauHoi.Rows.RemoveAt(e.RowIndex);
                  }
                  else
                  {
                      using (var QLTTN = new QLTTNDataContext())
                      {
                          if (QLTTN.DeThiQL.Count() == 0)
                          {
                              QLDTDgvDeThi.Rows.Clear();
                              return;
                          }
                          if (QLDTDgvDeThi.SelectedRows.Count > 0)
                          {
                              int madt = int.Parse(QLDTDgvDeThi.SelectedRows[0].Cells["maDT"].Value.ToString());
                              var dschtrongDeThi = QLTTN.CT_DeThiQL.Where(ctdt => ctdt.maDT == madt).GroupBy(ctdt => ctdt.maDT).Single().ToList();
                              foreach (DataGridViewRow row in QLDTDgvCauHoi.Rows)
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
                  }
            };
            

            t.Abort();
        }



        private void QLKTOTBtnThemKT_Click_1(object sender, EventArgs e)
        {
            frmThemKT frmThemKT = new frmThemKT(this, GV, cbKhoiLop.SelectedValue.ToString(), "ThiThu");
            frmThemKT.Show();
        }

        private void QLKTBtnThemKT_Click(object sender, EventArgs e)
        {
            frmThemKT frmThemKT = new frmThemKT(this, GV, cbKhoiLop.SelectedValue.ToString(), "ThiThiet");
            frmThemKT.Show();
        }

        private void QLCHBtnThemCH_Click(object sender, EventArgs e)
        {
            QLCHBtnThemDA.Enabled = true;
            CheckDS = 0;
            var QLTTN = new QLTTNDataContext();

            if (string.IsNullOrWhiteSpace(QLCHTxtCauHoi.Text))
            {
                MessageBox.Show("Bạn cần phải nhập nội dung cho câu hỏi", "Hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (QLTTN.CauHoiQL.Where(ch => ch.NoiDung.ToLower() == QLCHTxtCauHoi.Text.ToLower()).Count() != 0)
            {
                MessageBox.Show("Câu hỏi này đã có trong danh sách. Xin mời tạo câu hỏi mới", "Trùng record", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            QLTTN.CauHoiQL.InsertOnSubmit(new CauHoi()
            {
                NoiDung = QLCHTxtCauHoi.Text,
                CapDo = QLTTN.CapDoQL.Where(cd => cd.maCD == int.Parse(QLCHCapDo.SelectedValue.ToString())).SingleOrDefault(),
                maKhoi = cbKhoiLop.SelectedValue.ToString(),
                maMH = GV.maMH
            });
            QLTTN.SubmitChanges();
            LoadCBCauHoi();
            QLCHCbDSCH.SelectedItem = QLCHCbDSCH.Items[QLCHCbDSCH.Items.Count - 1];
            QLCHTxtDapAn.Focus();

            QLCHBtnThemCH.Enabled = false;
            QLCHCbDSCH.Enabled =  false;

        }

        private void QLCHBtnXoaCH_Click(object sender, EventArgs e)
        {
            

            using (var QLTTN = new QLTTNDataContext())
            {

                if (QLCHCbDSCH.SelectedValue == null)
                {
                    MessageBox.Show("Loi Không Có Bất Cứ Câu Hỏi Nào");
                    return;
                }

                var cauHoiHienTai = QLTTN.CauHoiQL
                                    .Where(ch => ch.maCH == int.Parse(QLCHCbDSCH.SelectedValue.ToString()))
                                    .FirstOrDefault();
                QLTTN.DapAnQL.DeleteAllOnSubmit(cauHoiHienTai.DapAns);
                QLTTN.CauHoiQL.DeleteOnSubmit(cauHoiHienTai);
                try
                {
                    QLTTN.SubmitChanges();
                }
                catch (Exception ee)
                {
                    return;
                }
            }
            LoadCBCauHoi();
        }

        private void QLCHBtnSuaCH_Click(object sender, EventArgs e)
        {
            if (QLCHCbDSCH.SelectedValue == null)
            {
                return;
            }

            using (var QLTTN = new QLTTNDataContext())
            {
                var cauHoiHienTai = QLTTN.CauHoiQL.Where(ch => ch.maCH == int.Parse(QLCHCbDSCH.SelectedValue.ToString())).FirstOrDefault();

                cauHoiHienTai.NoiDung = QLCHTxtCauHoi.Text;
                cauHoiHienTai.CapDo = QLTTN.CapDoQL.Where(cd => cd.maCD == int.Parse(QLCHCapDo.SelectedValue.ToString())).FirstOrDefault();
                cauHoiHienTai.maKhoi = cbKhoiLop.SelectedValue.ToString();
                QLTTN.SubmitChanges();
            }

            LoadCBCauHoi();

        }

        private void QLCHBtnThemDA_Click(object sender, EventArgs e)
        {
            if (QLCHCbDSCH.SelectedValue == null)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(QLCHTxtDapAn.Text))
            {
                MessageBox.Show("Bạn cần phải nhập cho đáp án", "Hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var QLTTN = new QLTTNDataContext();
            
            var cauHoiHienTai = QLTTN.CauHoiQL
            .Where(ch => ch.maCH == int.Parse(QLCHCbDSCH.SelectedValue.ToString()))
            .FirstOrDefault();
            if (cauHoiHienTai.DapAns.Where(da => da.NoiDung.ToLower() == QLCHTxtDapAn.Text.ToLower()).Count() != 0)
            {
                MessageBox.Show("Đáp án này đã có trong danh sách Đáp Án. Xin mời tạo đáp án mới", "Lỗi trùng dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }



            if (cauHoiHienTai.DapAns.Count >= 4)
            {
                MessageBox.Show("Đã Đủ 4 Câu Trả Lời !");
                QLCHBtnThemDA.Enabled = false;
                QLCHBtnThemCH.Enabled = true;
                QLCHCbDSCH.Enabled = true;


                if (CheckDS < 1)
                {
                    MessageBox.Show("Phải có ít nhất 1 đáp án đúng !");
                    QLCHBtnThemDA.Enabled = false;
                    QLCHBtnThemCH.Enabled = false;
                    QLCHCbDSCH.Enabled = false;
                }
                else
                {
                    if (CheckDS > 1)
                    {
                        MessageBox.Show("1 Câu Hỏi Chỉ Được 1 Đáp Án Đúng !");
                        QLCHBtnThemDA.Enabled = false;
                        QLCHBtnThemCH.Enabled = false;
                        QLCHCbDSCH.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("Đã Thêm Đáp Án Thành Công");
                        QLCHBtnThemDA.Enabled = false;
                        QLCHBtnThemCH.Enabled = true;
                        QLCHCbDSCH.Enabled = true;
                    }
                }

                return;
            }


            



            QLTTN.DapAnQL.InsertOnSubmit(new DapAn()
            {
                NoiDung = QLCHTxtDapAn.Text,
                DungSai = QLCHCkbDungSai.Checked,
                CauHoi = QLTTN.CauHoiQL.Where(ch => ch.maCH == int.Parse(QLCHCbDSCH.SelectedValue.ToString())).SingleOrDefault()
            });
            QLTTN.SubmitChanges();
            
            LoadQLCHDgvDapAn();


            QLCHDgvDSDA.Rows[QLCHDgvDSDA.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Selected = true;
            
            
            
            
            QLCHTxtDapAn.Focus();


        }

        private void QLCHBtnXoaDA_Click(object sender, EventArgs e)
        {
            if (QLCHCbDSCH.SelectedValue == null)
            {
                return;
            }
            var QLTTN = new QLTTNDataContext();
            
            var cauHoiHienTai = QLTTN.CauHoiQL.Where(ch => ch.maCH == int.Parse(QLCHCbDSCH.SelectedValue.ToString())).FirstOrDefault();
            if (cauHoiHienTai.DapAns.Count <= 2)
            {
                MessageBox.Show("Mỗi câu hỏi cần phải có tối thiểu 2 đáp án", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (QLCHDgvDSDA.SelectedRows.Count == 0)
            {
                MessageBox.Show("Hãy chọn đáp án cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            QLTTN.DapAnQL.DeleteOnSubmit(cauHoiHienTai.DapAns.Where(da => da.maDA == int.Parse(QLCHDgvDSDA.SelectedRows[0].Cells["maDA"].Value.ToString())).FirstOrDefault());
            QLTTN.SubmitChanges();
            
            LoadQLCHDgvDapAn();
        }

        private void QLCHBtnSuaDA_Click(object sender, EventArgs e)
        {
            if (QLCHCbDSCH.SelectedValue == null)
            {
                return;
            }
            var QLTTN = new QLTTNDataContext();
            
            var cauHoiHienTai = QLTTN.CauHoiQL
                                .Where(ch => ch.maCH == int.Parse(QLCHCbDSCH.SelectedValue.ToString()))
                                .FirstOrDefault();

            var dapAnHienTai = cauHoiHienTai.DapAns.Where(da => da.maDA == int.Parse(QLCHDgvDSDA.SelectedRows[0].Cells["maDA"].Value.ToString())).FirstOrDefault();
            dapAnHienTai.NoiDung = QLCHTxtDapAn.Text;
            dapAnHienTai.DungSai = QLCHCkbDungSai.Checked;
            if (dapAnHienTai.DungSai == true)
            {
                CheckDS += 1;
            }


            if (CheckDS < 1)
            {
                MessageBox.Show("Phải có ít nhất 1 đáp án đúng !");
                QLCHBtnThemDA.Enabled = false;
             
                QLCHCbDSCH.Enabled = false;
                return;


            }
            else
            {
                if (CheckDS > 1)
                {
                    MessageBox.Show("1 Câu Hỏi Chỉ Được 1 Đáp Án Đúng !");
                    QLCHBtnThemDA.Enabled = false;
                   
                    QLCHCbDSCH.Enabled = false;
                    return;

                }
                else
                {
                    MessageBox.Show("Sửa Đáp Án Thành Công !");
                    QLCHBtnThemDA.Enabled = false;
                    QLCHBtnThemCH.Enabled = true;
                    QLCHCbDSCH.Enabled = true;
                }
            }


            QLTTN.SubmitChanges();
            
            LoadQLCHDgvDapAn();

            QLCHTxtDapAn.Focus();



        }

        private void QLDTBtnThemDT_Click(object sender, EventArgs e)
        {
            int soch = int.Parse(qldtLblSoCHDuocChon.Text.Replace(" câu", ""));
            if (string.IsNullOrWhiteSpace(qldtTxtTenDT.Text))
            {
                MessageBox.Show("Bạn cần phải nhập tên đề thi", "Hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (soch >= 5)
            {
                var QLTTN = new QLTTNDataContext();
                
                var dethiHientai = new DeThi()
                {
                    maGV = GV.maGV,
                    maMH = GV.maMH,
                    maKhoi = cbKhoiLop.SelectedValue.ToString(),
                    TenDT = qldtTxtTenDT.Text,
                    ThoiGianLamBai = TimeSpan.FromMinutes(double.Parse(qldtTxtThoiGianLamBai.Text)),
                    NgayTao = DateTime.Now
                };

                if (DaTonTai(dethiHientai))
                {
                    return;
                }

                QLTTN.DeThiQL.InsertOnSubmit(dethiHientai);
                QLTTN.SubmitChanges();

                foreach (DataGridViewRow row in QLDTDgvCauHoi.Rows)
                {
                    int maDTHienTai = (int)QLTTN.ExecuteQuery<decimal>("select IDENT_CURRENT('dbo.DeThi')").FirstOrDefault();

                    var cell = row.Cells["Chon"] as DataGridViewCheckBoxCell;
                    if (cell.Value == cell.TrueValue)
                    {
                        QLTTN.CT_DeThiQL.InsertOnSubmit(new CT_DeThi()
                        {
                            maDT = maDTHienTai,
                            maCH = int.Parse(row.Cells["maCH"].Value.ToString())
                        });
                    }
                }
                QLTTN.SubmitChanges();
                

                LoadQLDTDgvDeThi();
            }
            else
            {
                MessageBox.Show("cần có ít nhất 5 câu hỏi", "Yêu cầu", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void QLDTBtnSuaDT_Click(object sender, EventArgs e)
        {
            int soch = int.Parse(qldtLblSoCHDuocChon.Text.Replace(" câu", ""));
            if (soch < 5)
            {
                MessageBox.Show("Mỗi đề thi cần có ít nhất 5 câu hỏi", "Yêu cầu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var QLTTN = new QLTTNDataContext();
            

            int MaDt;
            if (QLDTDgvDeThi.SelectedRows.Count > 0)
            {
                MaDt = int.Parse(QLDTDgvDeThi.SelectedRows[0].Cells["maDT"].Value.ToString());
                var DeThiHienTai = QLTTN.DeThiQL.Where(dt => dt.maDT == MaDt).Single();

                DeThiHienTai.maGV = GV.maGV;
                DeThiHienTai.maMH = GV.maMH;
                DeThiHienTai.maKhoi = cbKhoiLop.SelectedValue.ToString();
                DeThiHienTai.TenDT = qldtTxtTenDT.Text;
                DeThiHienTai.ThoiGianLamBai = TimeSpan.FromMinutes(double.Parse(qldtTxtThoiGianLamBai.Text));
                DeThiHienTai.NgayTao = DateTime.Now;
                QLTTN.SubmitChanges();

                QLTTN.CT_DeThiQL.DeleteAllOnSubmit(QLTTN.CT_DeThiQL.Where(ctdt => ctdt.maDT == MaDt));
                QLTTN.SubmitChanges();

                foreach (DataGridViewRow row in QLDTDgvCauHoi.Rows)
                {
                    var cell = row.Cells["Chon"] as DataGridViewCheckBoxCell;
                    if (cell.Value == cell.TrueValue)
                    {
                        QLTTN.CT_DeThiQL.InsertOnSubmit(new CT_DeThi()
                        {
                            maDT = MaDt,
                            maCH = int.Parse(row.Cells["maCH"].Value.ToString())
                        });
                    }
                }
                QLTTN.SubmitChanges();

                LoadQLDTDgvDeThi();
            }
            else
            {
                MessageBox.Show("Lựa chọn đề thi cần cập nhật", "Yêu cầu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            
        }

        private void QLDTBtnRdCauHoi_Click(object sender, EventArgs e)
        {
            CheckDGV(QLDTDgvCauHoi, (int)qldtNudCauHoiNgauNhien.Value);

        }

       

        private void QLKTBtnSuaKT_Click(object sender, EventArgs e)
        {
            if (QLKTDgvKT.RowCount > 0 && QLKTDgvKT.SelectedRows[0] != null)
            {
                int makt = int.Parse(QLKTDgvKT.SelectedRows[0].Cells["maKT"].Value.ToString());
                string MaKhoi = cbKhoiLop.SelectedValue.ToString();
                frmSuaKT frmsuakt = new frmSuaKT(this, GV, makt, MaKhoi, BSKyThi);
                frmsuakt.Show();
            }
            else
            {
                MessageBox.Show("Mời bạn chọn kỳ thi cần cập nhật",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void QLKTBtnPrintInfo_Click(object sender, EventArgs e)
        {
            if (QLKTDgvKT.RowCount > 0 && QLKTDgvKT.SelectedRows.Count > 0)
            {
                int MaKT = int.Parse(QLKTDgvKT.SelectedRows[0].Cells["maKT"].Value.ToString());
                var frmRp = new frmReport(MaKT);
                frmRp.ShowDialog();
            }
            else
            {
                MessageBox.Show("Hãy chọn một kỳ thi", "Thông báo", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        

        private void QLKTOTBtnSuaKT_Click(object sender, EventArgs e)
        {
            if (QLKTOTDgvKT.RowCount > 0 && QLKTOTDgvKT.SelectedRows[0] != null)
            {
                int makt = int.Parse(QLKTOTDgvKT.SelectedRows[0].Cells["maKT"].Value.ToString());
                string maKhoi = cbKhoiLop.SelectedValue.ToString();
                frmSuaKT frmsuakt = new frmSuaKT(this, GV, makt, maKhoi, BSKyThiThu);
                frmsuakt.Show();
            }
            else
            {
                MessageBox.Show("Mời bạn chọn kỳ thi cần cập nhật", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void QLHSRbHS_CheckedChanged(object sender, EventArgs e)
        {
            if (QLHSRbHS.Checked == true)
            {
                QLHSDgvDS.DataSource = null;
                LoadQlhsDgvHocSinh();
            }
        }

        private void QLHSRbKT_CheckedChanged(object sender, EventArgs e)
        {
            if (QLHSRbKT.Checked == true)
            {
                QLHSDgvDS.DataSource = null;
                LoadQLHSDgvKyThi();
            }
        }

        private void QLHSRbCH_CheckedChanged(object sender, EventArgs e)
        {
            if (QLHSRbCH.Checked == true)
            {
                QLHSDgvDS.DataSource = null;
                LoadQLHSDgvCauHoi();
            }
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

        private void qlchBtnThemCH_Click(object sender, EventArgs e)
        {

        }
    }
}
