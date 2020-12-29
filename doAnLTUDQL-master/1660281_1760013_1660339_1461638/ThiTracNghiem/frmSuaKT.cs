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

    public partial class frmSuaKT : Form
    {
        BindingSource bsLichThi = new BindingSource();
        string maKhoi;
        GiaoVien gv;

        int makt;
        /// <summary>
        /// buổi thi cần xóa hoặc cập nhật thông tin
        /// </summary>
        BuoiThi bt;

        /// <summary>
        /// đề thi trong buổi thi cần cập nhật
        /// </summary>
        QLTTNDataContext.MyDeThi dt;

        /// <summary>
        /// danh sách học sinh có trong buổi thi
        /// </summary>
        List<QLTTNDataContext.MyHocSinh> dshs;

        QLTTNDataContext qlttn = new QLTTNDataContext();
        KyThi ktccn;

        /// <summary>
        /// danh sách các mã học sinh được chọn
        /// </summary>
        public List<string> dsMaHSDuocChon = new List<string>();
        public int madt = -1;
        public DialogResult dlresult;

        public frmSuaKT(frmGiaoVien frmgv, GiaoVien gv, int makt, string maKhoi, BindingSource bsKyThi)
        {
            frmgv.Enabled = false;
            this.gv = gv;
            this.maKhoi = maKhoi;
            this.makt = makt;

            ktccn = qlttn.KyThis.Where(kt => kt.maKT == makt).FirstOrDefault();
            bt = qlttn.BuoiThis.Where(bt => bt.maKT == makt && bt.DeThi.maMH == gv.maMH).FirstOrDefault();
            if (bt != null)
            {
                dshs = qlttn.BaiLams.Where(bl => bl.maKT == bt.maKT && bl.maDT == bt.maDT).
                               Select(hs => new QLTTNDataContext.MyHocSinh
                               {
                                   maHS = hs.maHS,
                                   HoTen = hs.HocSinh.HoTen,
                                   maKhoi = hs.HocSinh.maKhoi,
                                   maLop = hs.HocSinh.maLop,
                                   NgaySinh = (DateTime)hs.HocSinh.NgaySinh
                               }).
                               ToList();
                dt = qlttn.KyThis.Where(kt => kt.maKT == this.makt).FirstOrDefault()
                                            .BuoiThis.Where(bt => bt.DeThi.maMH == gv.maMH)
                                            .Select(bt => new QLTTNDataContext.MyDeThi
                                            {
                                                maDT = bt.maDT,
                                                maGV = bt.DeThi.maGV,
                                                maKhoi = bt.DeThi.maKhoi,
                                                maMH = bt.DeThi.maMH,
                                                TenDT = bt.DeThi.TenDT,
                                                ThoiGianLamBai = (TimeSpan)bt.DeThi.ThoiGianLamBai
                                            }).FirstOrDefault();
            }


            InitializeComponent();

            Load += (s, e) =>
              {
                  setQlkt();
                  loadQlktDgvDeThi();
                  loadQlktDgvHocSinh();
                  txtTenKT.DataBindings.Add("Text", bsKyThi, "TenKT", true, DataSourceUpdateMode.Never);
                  lblLoaiKT.DataBindings.Add("Text", bsKyThi, "LoaiKT", true, DataSourceUpdateMode.Never);
                  lblMaKhoi.Text = maKhoi;
              };

            this.FormClosing += (s, e) =>
            {
                frmgv.Enabled = true;
            };
            btnSuaKT.Click += (s, e) =>
            {

                if (string.IsNullOrWhiteSpace(txtTenKT.Text))
                {
                    MessageBox.Show("Không được để trống tên kỳ thi", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (dtpNgayThi.Value < DateTime.Now)
                {
                    MessageBox.Show("Ngày giờ thi phải lớn hơn ngày giờ hiện tại", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // sau cái vòng lặp này thì sẽ lấy ra được danh sách học sinh thi
                foreach (DataGridViewRow row in dgvHS.Rows)
                {
                    var cell = row.Cells["Chon"] as DataGridViewCheckBoxCell;
                    if (cell.Value == cell.TrueValue)
                    {
                        dsMaHSDuocChon.Add(row.Cells["maHS"].Value.ToString());
                    }
                }
                if (dsMaHSDuocChon.Count == 0)
                {
                    MessageBox.Show($"Mời bạn chọn các thí sinh (lưu ý chọn các thí sinh không bận thi vào thời gian {dtpNgayThi.Value}", "Thông báo",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                List<int> dsMaDT = new List<int>();
                foreach (DataGridViewRow row in dgvDT.Rows)
                {
                    var cell = row.Cells["Chon"] as DataGridViewCheckBoxCell;
                    if (cell.Value == cell.TrueValue && row.Cells["maMH"].Value.ToString() == gv.maMH)
                    {
                        dsMaDT.Add(int.Parse(row.Cells["maDT"].Value.ToString()));
                    }
                }
                if (dsMaDT.Count == 0)
                {
                    MessageBox.Show("Hãy chọn một đề thi của bộ môn mà bạn đang phụ trách cho kỳ thi", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    var ktccn = qlttn.KyThis.Where(kythi => kythi.maKT == makt).FirstOrDefault();
                    ktccn.TenKT = txtTenKT.Text;

                    if (ktccn.LoaiKT == "ThiThiet")
                    {
                        if (bt != null)
                        {
                            qlttn.BaiLams.DeleteAllOnSubmit(bt.BaiLams);
                            qlttn.BuoiThis.DeleteOnSubmit(bt);
                            qlttn.SubmitChanges();
                        }
                        bt = new BuoiThi
                        {
                            maKT = ktccn.maKT,
                            maDT = dsMaDT[0],
                            NgayGioThi = dtpNgayThi.Value
                        };

                        if (dsMaDT.Count>0)
                        {
                            // tạo buổi thi mới
                            qlttn.BuoiThis.InsertOnSubmit(bt);

                            // thêm các thí sinh vào buổi thi mới này
                            foreach (var mahs in dsMaHSDuocChon)
                            {
                                qlttn.BaiLams.InsertOnSubmit(new BaiLam
                                {
                                    maKT = ktccn.maKT,
                                    maDT = dsMaDT[0],
                                    maHS = mahs,
                                    DiemSo = -1
                                });
                            }
                        }
                    }
                    else if (ktccn.LoaiKT == "ThiThu")
                    {
                        foreach (var bt in ktccn.BuoiThis)
                        {
                            qlttn.BaiLams.DeleteAllOnSubmit(bt.BaiLams);
                            qlttn.BuoiThis.DeleteOnSubmit(bt);
                            qlttn.SubmitChanges();
                        }

                        foreach (var madt in dsMaDT)
                        {
                            BuoiThi bt = new BuoiThi()
                            {
                                maKT = ktccn.maKT,
                                maDT = madt,
                                NgayGioThi = dtpNgayThi.Value
                            };
                            qlttn.BuoiThis.InsertOnSubmit(bt);
                            qlttn.SubmitChanges();

                            foreach (var hs in dshs)
                            {
                                qlttn.BaiLams.InsertOnSubmit(new BaiLam
                                {
                                    maHS = hs.maHS,
                                    maDT = bt.maDT,
                                    maKT = ktccn.maKT,
                                    DiemSo = -1
                                });
                            }
                        }
                    }

                    qlttn.SubmitChanges();
                    frmgv.loadQlkttotDgvKyThi();
                    MessageBox.Show("Cập nhật thành công", "Thông báo");
                    this.Close();

                }
                catch (Exception exc)
                {
                    MessageBox.Show($"KHÔNG THỂ CẬP NHẬT KỲ THI ĐÃ DIỄN RA {Environment.NewLine}{exc.Message}", "Thông báo từ btnCapNhatKT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            };
        }
        private void setQlkt()
        {
            if (gv.maGV == ktccn.maGV)
            {
                txtTenKT.ReadOnly = false;
            }
            else
            {
                txtTenKT.ReadOnly = true;
            }

            using (var qlttn = new QLTTNDataContext())
            {
                var ktCanCapNhat = qlttn.KyThis.Where(kt => kt.maKT == makt).FirstOrDefault();
                txtTenKT.Text = ktCanCapNhat.TenKT;
                var buoiThiCanCapNhat = ktCanCapNhat.BuoiThis.Where(bt => bt.DeThi.maMH == gv.maMH).FirstOrDefault();
                if (buoiThiCanCapNhat != null)
                {
                    dtpNgayThi.Value = (DateTime)buoiThiCanCapNhat.NgayGioThi;
                }
                lblTgBatDau.Text = $"Thời gian bắt đầu môn {qlttn.MonHocs.Where(mh => mh.maMH == gv.maMH).FirstOrDefault().tenMH}: ";
            }

            dgvHS.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                Name = "Chon",
                HeaderText = "Chọn học sinh",
                Width = 80,
                TrueValue = true,
                FalseValue = false,
                IndeterminateValue = false
            });

            dgvDT.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                Name = "Chon",
                HeaderText = "Chọn đề thi",
                Width = 80,
                TrueValue = true,
                FalseValue = false,
                IndeterminateValue = false
            });

            btnRdHs.Click += (s, e) =>
            {
                CheckDGV(dgvHS, (int)nudSoHocSinh.Value);
            };
            btnChonHetHS.Click += (s, e) =>
            {
                CheckDGV(dgvHS, dgvHS.RowCount);
            };
            dtpNgayThi.ValueChanged += (s, e) =>
            {
                loadQlktDgvHocSinh();
            };
            dgvDT.CellContentClick += (s, e) =>
            {
                if (ktccn.LoaiKT == "ThiThiet")
                {
                    CheckDGV(dgvDT, 0);
                }
            };
            dgvDT.CellValueChanged += (s, e) =>
            {
                var dgv = s as DataGridView;
                if (e.ColumnIndex == 0)
                {
                    var cell = dgv.Rows[e.RowIndex].Cells[0] as DataGridViewCheckBoxCell;
                    if (cell.Value == cell.TrueValue)
                    {
                        dgv.Rows[e.RowIndex].Selected = true;
                    }
                    else
                    {
                        dgv.Rows[e.RowIndex].Selected = false;
                    }
                }
            };
            dgvHS.CellValueChanged += (s, e) =>
            {
                var dgv = s as DataGridView;
                if (e.ColumnIndex == 0)
                {
                    gbTongSoThiSinh.Text = $"Tổng số thí sinh: {soDongDuocChon(dgv)}";
                    var cell = dgv.Rows[e.RowIndex].Cells[0] as DataGridViewCheckBoxCell;
                    if (cell.Value == cell.TrueValue)
                    {
                        dgv.Rows[e.RowIndex].Selected = true;
                    }
                    else
                    {
                        dgv.Rows[e.RowIndex].Selected = false;
                    }
                }
            };
        }

        /// <summary>
        /// trả về số dòng được check trong dgv
        /// </summary>
        /// <param name="dgv">bắt buộc dgv phải có cột có Name = "Chon"</param>
        /// <returns></returns>
        private int soDongDuocChon(DataGridView dgv)
        {
            int soDong = 0;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                var cell = row.Cells["Chon"] as DataGridViewCheckBoxCell;
                if (cell.Value == cell.TrueValue)
                {
                    soDong++;
                }
            }
            return soDong;
        }

        /// <summary>
        /// lấy tất cả những học sinh rảnh ngoài khoảng ghời gian
        /// </summary>
        private void loadQlktDgvHocSinh()
        {
            DateTime thoiGianBatDau = dtpNgayThi.Value;
            TimeSpan thoiGianThi = TimeSpan.FromSeconds(0);

            var dtDuocChon = getDeThiDuocChon();
            if (dtDuocChon != null)
            {
                thoiGianThi = (TimeSpan)dtDuocChon.ThoiGianLamBai;
            }

            if (qlttn.HocSinhs.Count() == 0)
            {
                MessageBox.Show("Không có dữ liệu học sinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                if (bt != null)
                {
                    // lấy những học sinh rảnh trong thời gian định cho thi và thí sinh có tên sẵn trong kỳ thi đó
                    var dshsRanh = qlttn.HocSinhs.Where(hs => hs.maKhoi == maKhoi &&
                                            hs.BaiLams.Where(bl => bl.BuoiThi.NgayGioThi > thoiGianBatDau + thoiGianThi
                                                                    || bl.BuoiThi.NgayGioThi + bl.BuoiThi.DeThi.ThoiGianLamBai < thoiGianBatDau
                                                               ).Count() == hs.BaiLams.Count
                                                               && hs.BaiLams.Any(bl => bt.BaiLams.Contains(bl)) == false
                                                        )
                                     .Select(hs => new QLTTNDataContext.MyHocSinh
                                     {
                                         maHS = hs.maHS,
                                         HoTen = hs.HoTen,
                                         maKhoi = hs.maKhoi,
                                         maLop = hs.maLop,
                                         NgaySinh = (DateTime)hs.NgaySinh
                                     }).ToList();
                    if (dshs != null)
                    {
                        dshsRanh.InsertRange(0, dshs);
                    }
                    dgvHS.DataSource = dshsRanh;
                }
                else
                {
                    // lấy những học sinh rảnh trong thời gian định cho thi và thí sinh có tên sẵn trong kỳ thi đó
                    var dshsRanh = qlttn.HocSinhs.Where(hs => hs.maKhoi == maKhoi &&
                                            hs.BaiLams.Where(bl => bl.BuoiThi.NgayGioThi > thoiGianBatDau + thoiGianThi
                                                                    || bl.BuoiThi.NgayGioThi + bl.BuoiThi.DeThi.ThoiGianLamBai < thoiGianBatDau
                                                               ).Count() == hs.BaiLams.Count
                                                        )
                                     .Select(hs => new QLTTNDataContext.MyHocSinh
                                     {
                                         maHS = hs.maHS,
                                         HoTen = hs.HoTen,
                                         maKhoi = hs.maKhoi,
                                         maLop = hs.maLop,
                                         NgaySinh = (DateTime)hs.NgaySinh
                                     }).ToList();
                    if (dshs != null)
                    {
                        dshsRanh.InsertRange(0, dshs);
                    }
                    dgvHS.DataSource = dshsRanh;

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }
            if (dgvHS.RowCount > 0)
            {
                dgvHS.Columns["maHS"].Width = 80;
                dgvHS.Columns["HoTen"].Width = 160;
                dgvHS.Columns["maLop"].Width = 80;
                dgvHS.Columns["NgaySinh"].Width = 80;
                dgvHS.Columns["maKhoi"].Visible = false;

                dgvHS.Columns["maHS"].HeaderText = "Mã học sinh";
                dgvHS.Columns["HoTen"].HeaderText = "Họ tên";
                dgvHS.Columns["maLop"].HeaderText = "Lớp học";
                dgvHS.Columns["NgaySinh"].HeaderText = "Ngày sinh";

                if (dshs != null)
                {
                    var dsmahs = dshs.Select(hs => hs.maHS);
                    foreach (DataGridViewRow row in dgvHS.Rows)
                    {
                        var mahs = row.Cells["maHS"].Value.ToString();
                        if (dsmahs.Contains(mahs))
                        {
                            var cell = row.Cells["Chon"] as DataGridViewCheckBoxCell;
                            cell.Value = cell.TrueValue;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show($"Hiện tất cả các học sinh của khối <{maKhoi}> đều đang bận thi" +
                                $"{Environment.NewLine}Vui lòng chọn thời gian khác để tạo buổi thi",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }
        private void loadQlktDgvDeThi()
        {
            using (var qlttn = new QLTTNDataContext())
            {
                if (qlttn.DeThis.Count() == 0)
                {
                    MessageBox.Show("Không có dữ liệu đề thi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                try
                {
                    bsLichThi.DataSource = qlttn.DeThis.Where(dt => dt.maMH == gv.maMH && dt.maKhoi == maKhoi)
                                                       .Select(dt => new QLTTNDataContext.MyDeThi
                                                       {
                                                           maDT = dt.maDT,
                                                           TenDT = dt.TenDT,
                                                           maMH = dt.maMH,
                                                           maGV = dt.maGV,
                                                           TenMH = dt.MonHoc.tenMH,
                                                           maKhoi = dt.maKhoi,
                                                           ThoiGianLamBai = (TimeSpan)dt.ThoiGianLamBai
                                                       });

                    if (bsLichThi.Count > 0)
                    {
                        dgvDT.DataSource = bsLichThi;
                        dgvDT.Columns["maDT"].HeaderText = "Mã";
                        dgvDT.Columns["maDT"].Width = 30;
                        dgvDT.Columns["maMH"].Visible = false;
                        dgvDT.Columns["TenDT"].HeaderText = "Tên đề thi";
                        dgvDT.Columns["TenDT"].Width = 140;
                        dgvDT.Columns["tenMH"].HeaderText = "Môn thi";
                        dgvDT.Columns["ThoiGianLamBai"].HeaderText = "Thời gian làm bài";
                        dgvDT.Columns["ThoiGianLamBai"].Width = 130;
                    }
                    else
                    {
                        dtpNgayThi.DataBindings.Clear();
                        MessageBox.Show("Không có dữ liệu đề thi", "Thông báo tab Quản lý kỳ thi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                    /// những dòng sau dùng để check cái đề thi mà có trong kỳ thi cần cập nhật
                    /// mã đề thi (của bộ môn mà giáo viên đó đảm nhiệm) trong kỳ thi muốn cập nhật
                    if (ktccn.LoaiKT == "ThiThiet")
                    {
                        if (dt != null)
                        {
                            foreach (DataGridViewRow row in dgvDT.Rows)
                            {
                                var madt = int.Parse(row.Cells["maDT"].Value.ToString());
                                if (madt == dt.maDT)
                                {
                                    var cell = row.Cells["Chon"] as DataGridViewCheckBoxCell;
                                    cell.Value = cell.TrueValue;
                                    break;
                                }
                            }
                        }
                    }
                    else if (ktccn.LoaiKT == "ThiThu")
                    {
                        foreach (DataGridViewRow row in dgvDT.Rows)
                        {
                            var madt = int.Parse(row.Cells["maDT"].Value.ToString());
                            if (ktccn.BuoiThis.Where(bt => bt.maDT == madt).Count() > 0)
                            {
                                var cell = row.Cells["Chon"] as DataGridViewCheckBoxCell;
                                cell.Value = cell.TrueValue;
                            }
                        }
                    }

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return;
                }
            }
        }

        /// <summary>
        /// lấy đề thi được chọn trong dgvDeThi
        /// </summary>
        /// <returns></returns>
        private QLTTNDataContext.MyDeThi getDeThiDuocChon()
        {
            QLTTNDataContext.MyDeThi dt = null;
            foreach (DataGridViewRow row in dgvDT.Rows)
            {
                var cell = row.Cells["Chon"] as DataGridViewCheckBoxCell;
                if (cell.Value == cell.TrueValue)
                {
                    dt = new QLTTNDataContext.MyDeThi
                    {
                        maDT = int.Parse(row.Cells["maDT"].Value.ToString()),
                        TenDT = row.Cells["TenDT"].Value.ToString(),
                        maGV = row.Cells["maGV"].Value.ToString(),
                        maKhoi = row.Cells["maKhoi"].Value.ToString(),
                        maMH = row.Cells["maMH"].Value.ToString(),
                        ThoiGianLamBai = TimeSpan.Parse(row.Cells["ThoiGianLamBai"].Value.ToString())
                    };
                    break;
                }
            }
            return dt;
        }

        private void CheckDGV(DataGridView dgv, int soDongMuonCheck)
        {
            if (dgv.RowCount <= 0)
            {
                return;
            }
            // nếu lớn hơn thì check hết
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

    }
}
