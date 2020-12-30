use master
go
if db_ID('DoAn_LTUDQL') is not null
begin
	ALTER DATABASE DoAn_LTUDQL SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	drop database DoAn_LTUDQL
end
create database DoAn_LTUDQL
go
use DoAn_LTUDQL
go

/*
	khi thêm 1 kỳ thi thử (LoaiKT="ThiThu") thì cần phải thêm thời gian bắt đầu và thời gian kết thúc
*/


/* ============================= TẠO BẢNG VÀ KHÓA CHÍNH =============================*/

create table KyThi(						-- một kỳ thi sẽ có nhiều đề thi, phân biệt bằng mã
	maKT int primary key identity not null,
	TenKT nvarchar(100),
	LoaiKT varchar(10),
	maKhoi varchar(3),
	NgayBD datetime,					-- khi nào LoaiKT="ThiThu" thì sẽ có NgayBD
	NgayKT datetime,						-- 
	maGV varchar(10)
)

create table BuoiThi(					-- danh sách các đề thi trong kỳ thi đó, khi xếp lịch thi thì chỉ xuất ra môn học mà thôi
	maKT int,
	maDT int,
	NgayGioThi datetime,
	primary key (maKT, maDT)
)

-- danh sách thí sinh trong buổi thi chính là bài làm
create table BaiLam(					-- bài làm được thực hiện trên chi tiết kỳ thi
	maHS varchar(10),					-- phải có tên trong lịch thi mới làm được bài
	maKT int,
	maDT int,							-- học sinh làm bài trên đề thi trong kỳ thi
	DiemSo decimal null,			 -- nếu chưa thi thì điểm = null
	primary key (maHS, maKT, maDT)
)

create table CT_BaiLam(					-- tham chiếu tới thi
	maHS varchar(10),
	maKT int,					
	maDT int,							
	maCH int,							-- câu hỏi có trong đề thi mà học sinh làm
	maDA int,							-- đáp án học sinh chọn
	DungSai bit,
	primary key (maHS, maKT, maDT, maCH)
)

create table DeThi(
	maDT int primary key identity not null,
	maMH varchar(10),
	maKhoi varchar(3),
	maGV varchar(10),
	TenDT nvarchar(100),
	ThoiGianLamBai time,
	NgayTao DateTime
)

create table CT_DeThi(
	maDT int,
	maCH int,
	primary key (maDT, maCH)
)

create table CauHoi(
	maCH int primary key identity not null,
	NoiDung nvarchar(1000),
	maCD int,
	maMH varchar(10),
	maKhoi varchar(3),
	GoiY nvarchar(1000) default N'Không có gợi ý'
)

create table CapDo(
	maCD int primary key identity not null,
	TenCD nvarchar(100)
)

create table DapAn(
	maCH int,
	maDA int identity not null,
	NoiDung nvarchar(1000),
	DungSai bit,
	primary key (maCH, maDA)
)

create table MonHoc(
	maMH varchar(10),
	tenMH nvarchar(100),
	primary key (maMH)
)

create table KhoiLop(
	maKhoi varchar(3) primary key
)

create table LopHoc(
	maKhoi varchar(3),
	maLop varchar(3),
	primary key (maKhoi, maLop),
	SiSo int default 0
)

create table NguoiDung(
	maND varchar(10) primary key,
	MatKhau varchar(100) default '123',
	maLND varchar(10)
)

create table LoaiNguoiDung(
	maLND varchar(10) primary key,
	TenLND nvarchar(100),
)

create table HocSinh(
	maHS varchar(10) primary key,
	HoTen nvarchar(100),
	NgaySinh datetime,
	maKhoi varchar(3),
	maLop varchar(3)
)

create table GiaoVien(
	maGV varchar(10) primary key,
	HoTen nvarchar(100),
	NgaySinh datetime,
	maMH varchar(10)
)

create table CT_GiangDay(
	maGV varchar(10),
	maKhoi varchar(3),
	maLop varchar(3),
	primary key (maGV, maKhoi, maLop)
)
go


/* ============================== CẬP NHẬT KHÓA NGOẠI =============================*/
alter table HocSinh
add 
	constraint fk_hs_nd
	foreign key (maHS)
	references NguoiDung(maND)

alter table GiaoVien
add 
	constraint fk_gv_nd
	foreign key (maGV)
	references NguoiDung(maND),
	constraint fk_gv_mh
	foreign key (maMH)
	references MonHoc(maMH)

alter table CT_GiangDay
add
	constraint fk_ctgd_gv
	foreign key (maGV)
	references GiaoVien(maGV),
	constraint fk_ctgd_lh
	foreign key (maKhoi, maLop)
	references LopHoc(maKhoi, maLop)

alter table KyThi
add
	constraint fk_kt_kl
	foreign key (maKhoi)
	references KhoiLop(maKhoi)

alter table BuoiThi
add
	constraint fk_bt_kt
	foreign key (maKT)
	references KyThi(maKT),
	constraint fk_bt_dt
	foreign key (maDT)
	references DeThi(maDT)

alter table BaiLam
add 
	constraint fk_bl_hs
	foreign key (maHS)
	references HocSinh(maHS),
	constraint fk_bl_bt
	foreign key (maKT, maDT)
	references BuoiThi(maKT, maDT)

alter table CT_BaiLam
add 
	constraint fk_ctbl_bl
	foreign key (maHS, maKT, maDT)
	references BaiLam(maHS, maKT, maDT),
	constraint fk_ctbl_ctdt
	foreign key (maDT, maCH)
	references CT_DeThi(maDT, maCH),
	constraint fk_ctbl_da
	foreign key (maCH, maDA)
	references DapAn(maCH, maDA)

alter table DeThi
add
	constraint fk_dt_mh
	foreign key (maMH)
	references MonHoc(maMH),
	constraint fk_dt_kl
	foreign key (maKhoi)
	references KhoiLop(maKhoi),
	constraint fk_dt_gv
	foreign key (maGV)
	references GiaoVien(maGV)

alter table CT_DeThi
add
	constraint fk_ctdt_dt
	foreign key (maDT)
	references DeThi(maDT),
	constraint fk_ctdt_ch
	foreign key (maCH)
	references CauHoi(maCH)

alter table CauHoi
add 
	constraint fk_ch_cd
	foreign key (maCD)
	references CapDo(maCD),
	constraint fk_ch_kl
	foreign key (maKhoi)
	references KhoiLop(maKhoi),
	constraint fk_ch_mh
	foreign key (maMH)
	references MonHoc(maMH)

alter table DapAn
add 
	constraint fk_da_ch
	foreign key (maCH)
	references CauHoi(maCH)

alter table LopHoc
add
	constraint fk_lh_kl
	foreign key (maKhoi)
	references KhoiLop(maKhoi)

alter table HocSinh
add
	constraint fk_hs_lh
	foreign key (maKhoi, maLop)
	references LopHoc(maKhoi, maLop)

alter table NguoiDung
add 
	constraint fk_nd_lnd
	foreign key (maLND)
	references LoaiNguoiDung(maLND)
go

/* ================================ TẠO TRIGGER =============================*/
--drop trigger trg_ThemCTBaiLam
--go
create trigger trg_ThemCTBaiLam on CT_BaiLam
	for insert
as begin
	-- lấy ra được kết quả câu trả  lời của ct_bailam
	declare @DungSai bit
	set @DungSai = (	select da.DungSai
						from inserted i, DapAn da
						where i.maCH=da.maCH and i.maDA=da.maDA
					)
	print @DungSai
	update CT_BaiLam
	set CT_BaiLam.DungSai=DapAn.DungSai
	from inserted i join CT_BaiLam on CT_BaiLam.maKT=i.maKT and CT_BaiLam.maDT=i.maDT and CT_BaiLam.maHS=i.maHS
					join DapAn on CT_BaiLam.maCH = DapAn.maCH and CT_BaiLam.maDA=DapAn.maDA

	update BaiLam
	set DiemSo = dbo.TinhDiem(i.maKT, i.maDT, i.maHS, 10)
	from inserted i join BaiLam on BaiLam.maKT=i.maKT and BaiLam.maDT=i.maDT and BaiLam.maHS=i.maHS
end
go

create trigger trg_XoaCTBaiLam on CT_BaiLam
	for delete 
as begin
	update BaiLam
	set DiemSo = dbo.TinhDiem(i.maKT, i.maDT, i.maHS, 10)
	from deleted i, BaiLam bl
	where bl.maKT=i.maKT and bl.maDT=i.maDT and bl.maHS=i.maHS
end
go

create function demSiSoHS (@maKhoi varchar(3), @maLop varchar(3))
returns int
as begin
	return	(select count(*)
			from HocSinh hs
			where hs.maKhoi = @maKhoi and hs.maLop = maLop)
end
go

create trigger trg_CapNhatSiSoLopHoc on HocSinh
	for insert, update, delete
as begin
	-- update lớp học mới của học sinh đó
	update LopHoc
	set SiSo = dbo.demSiSoHS(i.maKhoi, i.maLop)
	from inserted i, LopHoc lh
	where i.maKhoi = lh.maKhoi and i.maLop = lh.maLop

	-- update lớp học cũ của học sinh đó
	update LopHoc
	set SiSo = dbo.demSiSoHS(d.maKhoi, d.maLop)
	from deleted d, LopHoc lh
	where d.maKhoi = lh.maKhoi and d.maLop = lh.maLop
end
go

/* ================================ TẠO Funtion =============================*/
-- tính số câu đúng trong 1 bài làm
--drop function TinhSoCauDung
--go
create function TinhSoCauDung(@maKT int, @maDT int, @maHS varchar(10))
returns int
as begin
	return	(select count(ctbl.DungSai)
			from CT_BaiLam ctbl
			where	ctbl.maKT=@maKT and
					ctbl.maDT=@maDT and
					ctbl.maHS=@maHS and
					ctbl.DungSai=1)
end
go

--drop function TinhTongSoCauTrongDeThi
--go
create function TinhTongSoCauTrongDeThi(@maDT int)
returns int
as begin
	return (select COUNT(maCH)
			from CT_DeThi
			where maDT=@maDT)
end
go
print dbo.TinhTongSoCauTrongDeThi(3)
print dbo.TinhSoCauDung(1, 3, 1660281)
print dbo.TinhDiem(1, 3, 1660281, 10)

--drop function TinhDiem
--go
create function TinhDiem(@maKT int, @maDT int, @maHS varchar(10), @thangDiem int) -- thang điểm 10, 100, hay là 4 như điểm tốt nghiệp đại học
returns decimal(18,2)
as begin
	declare @soCauDung int, @tongSoCau int
	set @soCauDung = dbo.TinhSoCauDung(@maKT, @maDT, @maHS)
	set @tongSoCau = dbo.TinhTongSoCauTrongDeThi(@maDT)

	if @tongSoCau=0
		return 0

	-- quy ra điểm
	return (cast(@soCauDung as decimal)/ cast(@tongSoCau as decimal)) * cast(@thangDiem as decimal)
end
go

/* ================================ TẠO STORE =============================*/
--drop proc sp_loadKetQuaThiCuaHocSinh 
--go
create proc sp_loadKetQuaThiCuaHocSinh @makt int, @mahs varchar(10)
as begin
	select	kt.TenKT,
			hs.maHS, 
			hs.HoTen, 
			hs.NgaySinh, 
			hs.maKhoi, 
			hs.maLop,
			mh.tenMH,
			dbo.TinhTongSoCauTrongDeThi(dt.maDT) as TongSoCau,
			dbo.TinhSoCauDung(bl.maKT, bl.maDT, bl.maHS) as TongSoCauDung,
			bl.DiemSo,
			bt.NgayGioThi,
			dt.ThoiGianLamBai,
			bl.maKT,
			bl.maDT
	from HocSinh hs, KyThi kt, DeThi dt, MonHoc mh, BaiLam bl, BuoiThi bt
	where hs.maHS=bl.maHS and bl.maKT=kt.maKT and bl.maDT=dt.maDT and dt.maMH=mh.maMH and
			bt.maKT=bl.maKT and bt.maDT=bl.maDT and
			hs.maHS=@mahs and kt.maKT=@makt
end
go

--drop proc sp_loadDanhSachThiSinhKemThongTinKyThi
--go
create proc sp_loadDanhSachThiSinhKemThongTinKyThi @makt int
as begin
	select DISTINCT bl.maHS, bl.maKT, bl.maDT, kt.TenKT, hs.HoTen, hs.maLop, kt.maKhoi, mh.tenMH, bt.NgayGioThi, dt.ThoiGianLamBai
	from KyThi kt, BuoiThi bt, DeThi dt, BaiLam bl, MonHoc mh, HocSinh hs
	where bl.maKT=@makt and kt.maKT=bt.maKT and bt.maDT=dt.maDT and bl.maKT=kt.maKT and bl.maDT=dt.maDT and bl.maHS=hs.maHS and dt.maMH=mh.maMH
	order by bl.maHS
end
go

--drop proc sp_loadKetQuaKyThi
--go
create proc sp_loadKetQuaKyThi @makt int
as begin
	select DISTINCT  bl.maHS, bl.maDT, bl.maKT, bl.DiemSo, kt.TenKT , hs.HoTen, hs.maLop, kt.maKhoi, mh.tenMH
	from KyThi kt, DeThi dt, BaiLam bl, MonHoc mh, HocSinh hs, GiaoVien gv
	where bl.maKT=@makt and bl.maKT=kt.maKT and bl.maDT=dt.maDT and bl.maHS=hs.maHS and dt.maMH=mh.maMH
	order by bl.maHS
end
go


/* ================================ TẠO DỮ LIỆU MẪU =============================*/
-- thêm khối lớp K10,K11,K12, mỗi khối có 9 lớp A1 -> C3
declare @i int, @c int
set @i = 10 
set @c = 65
while @i <= 12
begin
	declare @maK varchar(3)
	set @maK = 'K' + CAST(@i as char)

	insert into KhoiLop(maKhoi) values (@maK)
	while @c <= 67
	begin
		insert into LopHoc(maKhoi, maLop) values (@maK, char(@c) + '1')
		insert into LopHoc(maKhoi, maLop) values (@maK, char(@c) + '2')
		insert into LopHoc(maKhoi, maLop) values (@maK, char(@c) + '3')
		set @c += 1
	end

	set @i += 1
	set @c = 65
end
go

insert into MonHoc(maMH, tenMH) values ('T',  N'Toán')
insert into MonHoc(maMH, tenMH) values ('VL', N'Vật lý')
insert into MonHoc(maMH, tenMH) values ('HH', N'Hóa học')
insert into MonHoc(maMH, tenMH) values ('AV', N'Anh văn')
insert into MonHoc(maMH, tenMH) values ('LS', N'Lịch Sữ')

insert into LoaiNguoiDung(maLND, TenLND) values ('HS', N'Học sinh')
insert into LoaiNguoiDung(maLND, TenLND) values ('GV', N'Giáo viên')
insert into LoaiNguoiDung(maLND, TenLND) values ('AD', N'Quản trị viên')
go

insert into NguoiDung(MaND, MatKhau, maLND) values (1660339, '123', 'HS')
insert into NguoiDung(MaND, MatKhau, maLND) values (1660281, '123', 'HS')
insert into NguoiDung(MaND, MatKhau, maLND) values (1461638, '123', 'HS')
insert into NguoiDung(maND, MatKhau, maLND) values (1760013, '123', 'GV')
insert into NguoiDung(maND, MatKhau, maLND) values (1721001902, '123', 'GV')
insert into NguoiDung(maND, MatKhau, maLND) values ('ad', '123', 'AD')
go

insert into HocSinh(maHS, HoTen, NgaySinh, maKhoi, maLop) values(1660339, N'Nguyễn Thị Lý'  , '12/29/1998', 'K10', 'A1')
insert into HocSinh(maHS, HoTen, NgaySinh, maKhoi, maLop) values(1660281, N'Trần Khôi'      , '03/14/1998', 'K10', 'A1')
insert into HocSinh(maHS, HoTen, NgaySinh, maKhoi, maLop) values(1461638, N'Phan Xiêu Thiên', '01/09/1996', 'K12', 'A2')

insert into GiaoVien(maGV,HoTen, NgaySinh, MaMH) values(1760013, N'Lê Thanh Bình', '11/20/1990', 'T')
insert into GiaoVien(maGV,HoTen, NgaySinh, MaMH) values(1721001902, N'Trần Lam Ngọc', '09/21/1999', 'T')
go

insert into CT_GiangDay(maGV, maKhoi, maLop) values(1760013, 'K10', 'A1')
insert into CT_GiangDay(maGV, maKhoi, maLop) values(1760013, 'K12', 'A2')
insert into CT_GiangDay(maGV, maKhoi, maLop) values(1721001902, 'K10', 'A1')
go

insert into CapDo values
(N'Trứng cút'),
(N'Gà con'),
(N'Diều hâu'),
(N'Đại bàng')
go

insert into CauHoi(maMH, maKhoi, maCD, NoiDung) values
('LS', 'K12', 1, N' Cuộc Cách mạng Tư sản trên thế giới diễn ra đầu tiên ở nước nào?'),
('LS', 'K12', 1, N'Chiến thắng Điện Biên Phủ vào ngày, tháng, năm nào?'),
('LS', 'K12', 1, N' Bác Hồ ra đi tìm đường cứu nước vào ngày tháng năm nào?'),
('LS', 'K12', 2, N'Cuộc kháng chiến chống quân Tống xâm lược lần thứ nhất vào năm nào? '),
('LS', 'K12', 2, N'Tổ chức cộng sản nào ra đời đầu tiên ở Việt Nam?'),
('LS', 'K12', 2, N'Quốc hiệu đầu tiên của nước Việt Nam?'),
('LS', 'K12', 3, N'An Dương Vương đặt tên nước ta là? '),
('LS', 'K12', 3, N'Đây là con sông gắn liền với cuộc khởi nghĩa của Hai Bà Trưng?'),
('LS', 'K12', 3, N'Trần Quang Khải là con trai thứ mấy của vua Trần Thánh Tông? '),
('LS', 'K12', 4, N'Tỉnh nào là quê hương của đội quân tóc dài?'),
('LS', 'K12', 4, N' Chiến dịch Điện Biên Phủ diễn ra trong bao nhiêu ngày đêm?'),

('AV', 'K10', 1, N'The little boy pleaded _____ not to leave him alone in the dark.'),
('AV', 'K10', 1, N'It was very kind  _____  you to lend me a money I needed.')

go

insert into DapAn(maCH, NoiDung, DungSai) values
--------------- đáp án môn THiên Văn Học K12
(1,  N'Anh', 1),
(1,  N'Mỹ', 0),
(1,  N'Pháp', 0),
(1,  N'Việt Nam', 0),
(2,  N'05-05-1954', 0),
(2,  N'06-05-1954', 0),
(2,  N'07-05-1954', 1),
(2,  N'08-05-1954', 0),
(3,  N'05-09-1911', 0),
(3,  N'05-09-1910', 0),
(3,  N'05-06-1910', 0),
(3,  N'05-06-1911', 1),
(4,  N'Năm 983', 0),
(4,  N'Năm 982', 0),
(4,  N'Năm 981', 1),
(5,  N'Hội Việt Nam cách mạng thanh niên', 0),
(5,  N'Đông Dương cộng sản Đảng', 1),
(5,  N'An Nam cộng sản Đảng', 0),
(5,  N'Đông Dương cộng sản liên đoàn', 0),
(6,  N'Văn Lang', 1),
(6,  N'Âu lạc', 0),
(6,  N'Đại Cồ Việt', 0),
(7,  N'Đại Việt', 0),
(7,  N'Âu Lạc', 1),
(7,  N'Đại Nam', 0),
(7,  N'Vạn Xuân', 0),
(8,  N'Sông Hàng', 0),
(8,  N'Sông Hò', 0),
(8,  N'Sông Hậu', 0),
(8,  N'Sông Hát', 1),
(9,  N'Thứ hai', 0),
(9,  N'Thứ ba', 1),
(9,  N'Thứ tư', 0),
(10, N'Cà Mau', 0),
(10, N'Bến Tre', 1),
(10, N'Long An', 0),
(11, N'55 ngày đêm', 0),
(11, N'56 ngày đêm.', 1),
(11, N'57 ngày đêm', 0),
(11, N'Tôi không biết', 0),

------------ đáp án Môn Toán K10
(12, N'on his mother', 0),            
(12, N'his mother', 0),
(12, N'at his mother',0),                     
(12, N'with his mother', 1),
(13, N'for', 0),
(13, N'to', 0),                   
(13, N'with', 0),       
(13, N'of', 1)

go

--create table DeThi(
--	maDT int primary key identity not null,
--	maMH varchar(10),
--	maKhoi varchar(3),
--	maGV varchar(10),
--	TenDT nvarchar(100),
--	ThoiGianLamBai time,
--	NgayTao DateTime
--)

--create table CT_DeThi(
--	maDT int,
--	maCH int,
--	primary key (maDT, maCH)
--)

--insert into DeThi()


use QLTTN
select * from CauHoi
select * from DapAn
select * from CapDo
select * from NguoiDung
select * from HocSinh
select * from GiaoVien
select IDENT_CURRENT('dbo.CauHoi')

/*
-- xóa những câu hỏi mà chưa có đáp án
delete from CauHoi
where not exists (select * from DapAn
				where DapAn.maCH = CauHoi.maCH)
delete from DapAn where maCH > 50
delete from CauHoi where maCH > 50
delete from DapAn
delete from CauHoi
delete from CapDo
1,  
2,  
3,  
4,  
5,  
6,  
7,  
8,  
9,  
10, 
11, 
12, 
13, 
14, 
15, 
16, 
17, 
18, 
19, 
20, 
21, 
22, 
23, 
24, 
25, 
26, 
27, 
28, 
29, 
30, 
31, 
32, 
33, 
34, 
35, 
36, 
37, 
38, 
39, 
40, 
*/

/*
bỏ mã khối trong bảng môn học: xong
thêm khóa ngoại từ bảng giáo viên sang bảng môn học: xong
bõ mã câu hỏi trong DeThi, thêm bảng chi tiết đề thi, tronng bảng chi tiết đề thi sẽ có mã đề thi và mã câu hỏi, cả hai cùng là khóa chính
thêm khóa ngoại mã khối trong bảng câu hỏi tham chiếu đến bảng Khối
*/