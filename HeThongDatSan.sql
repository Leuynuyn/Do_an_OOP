CREATE DATABASE HeThongDatSanTheThao

USE HeThongDatSanTheThao

CREATE TABLE KHACHHANG (
    IDKhachHang VARCHAR(9) PRIMARY KEY NOT NULL,
    TenKhachHang NVARCHAR(50) NOT NULL,
    TenTaiKhoan NVARCHAR(50),
    MatKhau VARCHAR(9) NOT NULL,
    NgaySinh DATE,
    GioiTinh NVARCHAR(3) NOT NULL,
    SDT VARCHAR(11) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(100),
    VaiTro NVARCHAR(50) NOT NULL,
    NgayTaoTK DATE NOT NULL
)

CREATE TABLE NHANVIEN (
    IDNhanVien VARCHAR(9) PRIMARY KEY NOT NULL,
    TenNhanVien NVARCHAR(50) NOT NULL,
    TenTaiKhoan NVARCHAR(50),
    MatKhau VARCHAR(9) NOT NULL,
    NgaySinh DATE,
    GioiTinh NVARCHAR(3) NOT NULL,
    SDT VARCHAR(11) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(100),
    VaiTro NVARCHAR(50) NOT NULL,
    NgayVaoLam DATE NOT NULL
)

CREATE TABLE QUANTRIVIEN (
    IDAdmin VARCHAR(9) PRIMARY KEY NOT NULL,
    TenAdmin NVARCHAR(50) NOT NULL,
    TenTaiKhoan NVARCHAR(50),
    MatKhau VARCHAR(9) NOT NULL,
    NgaySinh DATE,
    GioiTinh NVARCHAR(3) NOT NULL,
    SDT VARCHAR(11) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(100),
    VaiTro NVARCHAR(50) NOT NULL
)

CREATE TABLE LOAISAN (
    IDLoaiSan VARCHAR(9) PRIMARY KEY NOT NULL,
    TenLoaiSan NVARCHAR(50) NOT NULL,
    MoTa NVARCHAR(100)
) 

CREATE TABLE SAN (
    IDSan VARCHAR(9) PRIMARY KEY NOT NULL,
    IDLoaiSan VARCHAR(9) NOT NULL,
    TenSan NVARCHAR(50),
    DonGia DECIMAL(10, 2) NOT NULL,
    MoTa NVARCHAR(50),
    TrangThai NVARCHAR(50) NOT NULL,
	TinhTrangHienTai NVARCHAR(50),
    FOREIGN KEY (IDLoaiSan) REFERENCES LOAISAN(IDLoaiSan)
)

CREATE TABLE HDDATSAN (
    IDHDDatSan VARCHAR(9) PRIMARY KEY NOT NULL,
    IDKhachHang VARCHAR(9) NOT NULL,
    NgayLapHD DATE,
    TienCoc DECIMAL(10, 2),
	TongTien DECIMAL(10, 2) NOT NULL,
	TrangThai NVARCHAR(30) NOT NULL,
    FOREIGN KEY (IDKhachHang) REFERENCES KHACHHANG(IDKhachHang)
)

CREATE TABLE CTHDDATSAN (
    IDCTHDDatSan VARCHAR(9) PRIMARY KEY NOT NULL,
    IDHDDatSan VARCHAR(9) NOT NULL,
    IDSan VARCHAR(9) NOT NULL,
    NgayDat DATE NOT NULL,
    GioBatDau DATETIME NOT NULL,
    GioKetThuc DATETIME NOT NULL,
    DonGia DECIMAL(10, 2),
    FOREIGN KEY (IDHDDatSan) REFERENCES HDDATSAN(IDHDDatSan),
    FOREIGN KEY (IDSan) REFERENCES SAN(IDSan)
)

CREATE TABLE LOAICALAM (
    IDLoaiCa VARCHAR(9) PRIMARY KEY NOT NULL, -- Mã loại ca làm
    TenCa NVARCHAR(50) NOT NULL,             -- Tên loại ca (Sáng, Chiều, Tối)
    GioBatDau TIME NOT NULL,                 -- Giờ bắt đầu của loại ca
    GioKetThuc TIME NOT NULL,                -- Giờ kết thúc của loại ca
    LuongTheoGio DECIMAL(10, 2) NOT NULL,
	TongGioCa int not null
);

CREATE TABLE CALAMVIEC (
    IDCaLamViec VARCHAR(9) PRIMARY KEY NOT NULL,
	IDLoaiCa VARCHAR(9),
    IDNhanVien VARCHAR(9) NOT NULL,
    NgayLam DATE NOT NULL,
    FOREIGN KEY (IDNhanVien) REFERENCES NHANVIEN(IDNhanVien),
	FOREIGN KEY (IDLoaiCa) REFERENCES LOAICALAM(IDLoaiCa)
)

CREATE TABLE TINHLUONG (
    IDLuong VARCHAR(9) PRIMARY KEY NOT NULL,   -- Mã bảng lương
    IDNhanVien VARCHAR(9) NOT NULL,           -- Mã nhân viên
    Thang INT NOT NULL CHECK (Thang BETWEEN 1 AND 12), -- Tháng tính lương (1-12)
    Nam INT NOT NULL,                         -- Năm tính lương
    TongSoGio int,        -- Tổng số giờ làm việc trong tháng
    TongLuong DECIMAL(10, 2),  
    FOREIGN KEY (IDNhanVien) REFERENCES NHANVIEN(IDNhanVien)
);

CREATE TABLE SUKIENKHUYENMAI (
    IDSuKien VARCHAR(9) PRIMARY KEY NOT NULL,
    TenSuKien NVARCHAR(100) NOT NULL,
	GiaTriGiam DECIMAL(10, 2) NOT NULL,
	MaCodeKM VARCHAR(13) NOT NULL,
    MoTa NVARCHAR(100),
    NgayBatDauSK DATETIME NOT NULL,
    NgayKetThucSK DATETIME NOT NULL,
    TrangThai NVARCHAR(50) NOT NULL
)

CREATE TABLE LSPFOOD (
    IDLoaiSP VARCHAR(9) PRIMARY KEY NOT NULL,
    TenLoaiSP NVARCHAR(50),
    MoTa NVARCHAR(100)
)

CREATE TABLE SPFOOD (
    IDSP VARCHAR(9) PRIMARY KEY NOT NULL,
    IDLoaiSP VARCHAR(9) NOT NULL,
    TenSP NVARCHAR(50),
    DonGia DECIMAL(10, 2),
    MoTa NVARCHAR(100),
    TrangThai NVARCHAR(50),
    FOREIGN KEY (IDLoaiSP) REFERENCES LSPFOOD(IDLoaiSP)
)

CREATE TABLE HDSP (
    IDHDSP VARCHAR(9) PRIMARY KEY NOT NULL,
    IDKhachHang VARCHAR(9) NOT NULL,
    NgayLap DATE,
    TongTien DECIMAL(10, 2),
    FOREIGN KEY (IDKhachHang) REFERENCES KHACHHANG(IDKhachHang)
)

CREATE TABLE CTHDSP (
    IDCTHDSP VARCHAR(9) PRIMARY KEY NOT NULL,
    IDHDSP VARCHAR(9) NOT NULL,
    IDSP VARCHAR(9) NOT NULL,
    SoLuong INT,
    DonGia DECIMAL(10, 2),
    ThanhTien DECIMAL(10, 2),
    FOREIGN KEY (IDHDSP) REFERENCES HDSP(IDHDSP),
    FOREIGN KEY (IDSP) REFERENCES SPFOOD(IDSP)
)

CREATE TABLE HDTONG (
    IDHDTong VARCHAR(9) PRIMARY KEY NOT NULL,
    IDKH VARCHAR(9) NOT NULL,
    NgayLap DATE,
    TongTienTra DECIMAL(10, 2),
    FOREIGN KEY (IDKH) REFERENCES KHACHHANG(IDKhachHang)
)

CREATE TABLE CTHDTONG (
    IDCTHDTong VARCHAR(9) PRIMARY KEY NOT NULL,
    IDHDTong VARCHAR(9) NOT NULL,
    LoaiHoaDon VARCHAR(10),
    IDHoaDonCon VARCHAR(9),
    TongHoaDonCon DECIMAL(10, 2),
    TongTienHDCon DECIMAL(10, 2),
    FOREIGN KEY (IDHDTong) REFERENCES HDTONG(IDHDTong)
)

INSERT INTO KHACHHANG (IDKhachHang, TenKhachHang, TenTaiKhoan, MatKhau, NgaySinh, GioiTinh, SDT, Email, DiaChi, VaiTro, NgayTaoTK)
VALUES
('KH001', N'Nguyễn Văn An', 'nguyenvana', '123456', '1990-01-01', N'Nam', '0987654321', 'nguyenvana@gmail.com', N'Hà Nội', N'Khách hàng', '2024-01-01'),
('KH002', N'Trần Thị Bình', 'tranthib', 'abcdef', '1995-05-15', N'Nữ', '0978123456', 'tranthib@gmail.com', N'Hồ Chí Minh', N'Khách hàng', '2024-01-02'),
('KH003', N'Lê Văn Cường', 'levanc', 'cdefgh', '1992-03-12', N'Nam', '0962345678', 'levanc@gmail.com', N'Đà Nẵng', N'Khách hàng', '2024-01-03'),
('KH004', N'Phạm Thị Dương', 'phamthid', 'ijklmn', '1988-07-22', N'Nữ', '0983456789', 'phamthid@gmail.com', N'Cần Thơ', N'Khách hàng', '2024-01-04'),
('KH005', N'Hoàng Văn Em', 'hoangvane', 'opqrst', '1999-11-11', N'Nam', '0974567890', 'hoangvane@gmail.com', N'Hải Phòng', N'Khách hàng', '2024-01-05'),
('KH006', N'Ngô Thị Hạnh', 'ngothif', 'uvwxyz', '1994-06-15', N'Nữ', '0956789012', 'ngothif@gmail.com', N'Hà Nam', N'Khách hàng', '2024-01-06'),
('KH007', N'Đặng Văn Giàu', 'dangvang', '123xyz', '1991-10-05', N'Nam', '0932109876', 'dangvang@gmail.com', N'Vĩnh Long', N'Khách hàng', '2024-01-07'),
('KH008', N'Phan Thị Hồng', 'phanthih', '456uvw', '1993-09-18', N'Nữ', '0921098765', 'phanthih@gmail.com', N'Quảng Ninh', N'Khách hàng', '2024-01-08'),
('KH009', N'Trịnh Văn Hùng', 'trinhvani', '789stu', '1990-12-24', N'Nam', '0910987654', 'trinhvani@gmail.com', N'Hòa Bình', N'Khách hàng', '2024-01-09'),
('KH010', N'Dương Thị Bé', 'duongthij', 'rstuvw', '1996-02-28', N'Nữ', '0909876543', 'duongthij@gmail.com', N'Quảng Bình', N'Khách hàng', '2024-01-10')

select * from KHACHHANG

INSERT INTO NHANVIEN (IDNhanVien, TenNhanVien, TenTaiKhoan, MatKhau, NgaySinh, GioiTinh, SDT, Email, DiaChi, VaiTro, NgayVaoLam)
VALUES
('NV001', N'Võ Minh Huy', 'minhhuy', 'nvpass1', '1990-05-12', N'Nam', '0987456123', 'minhhuy@gmail.com', N'Hà Nội', N'Quản lý sân', '2024-01-01'),
('NV002', N'Tạ Văn Tùng', 'vantung', 'nvpass2', '1995-06-18', N'Nam', '0978123987', 'vantung@gmail.com', N'Hồ Chí Minh', N'Nhân viên kế toán', '2024-01-02'),
('NV003', N'Lương Hoài Anh', 'hoaianh', 'nvpass3', '1992-03-22', N'Nữ', '0962987654', 'hoaianh@gmail.com', N'Đà Nẵng', N'Nhân viên đặt sân', '2024-01-03'),
('NV004', N'Phan Bảo Trân', 'baotran', 'nvpass4', '1988-12-01', N'Nữ', '0983124789', 'baotran@gmail.com', N'Cần Thơ', N'Nhân viên hỗ trợ', '2024-01-04'),
('NV005', N'Nguyễn Quốc Anh', 'quocanh', 'nvpass5', '1999-11-10', N'Nam', '0974658290', 'quocanh@gmail.com', N'Hải Phòng', N'Nhân viên bảo trì', '2024-01-05'),
('NV006', N'Lê Hồng Phúc', 'hongphuc', 'nvpass6', '1994-06-14', N'Nam', '0956128347', 'hongphuc@gmail.com', N'Hà Nam', N'Nhân viên kỹ thuật', '2024-01-06'),
('NV007', N'Vũ Ngọc Hà', 'ngocha', 'nvpass7', '1991-10-05', N'Nữ', '0932178456', 'ngocha@gmail.com', N'Vĩnh Long', N'Quản lý sân', '2024-01-07'),
('NV008', N'Trần Đức Long', 'duclong', 'nvpass8', '1993-09-18', N'Nam', '0921347658', 'duclong@gmail.com', N'Quảng Ninh', N'Nhân viên tư vấn', '2024-01-08'),
('NV009', N'Hoàng Thị Thu', 'thithu', 'nvpass9', '1990-12-24', N'Nữ', '0910756432', 'thithu@gmail.com', N'Hòa Bình', N'Nhân viên kế toán', '2024-01-09'),
('NV010', N'Phạm Minh Khôi', 'minhkhoi', 'nvpass10', '1996-02-28', N'Nam', '0908765432', 'minhkhoi@gmail.com', N'Quảng Bình', N'Nhân viên hỗ trợ', '2024-01-10')

select * from NHANVIEN

INSERT INTO QUANTRIVIEN (IDAdmin, TenAdmin, TenTaiKhoan, MatKhau, NgaySinh, GioiTinh, SDT, Email, DiaChi, VaiTro)
VALUES
('AD001', N'Nguyễn Hoàng Nam', 'hoangnam', 'qtvpass1', '1990-01-01', N'Nam', '0981112233', 'hoangnam@gmail.com', N'Hà Nội', N'Quản trị viên'),
('AD002', N'Phạm Ngọc Anh', 'ngocanh', 'qtvpass2', '1992-02-12', N'Nữ', '0972223344', 'ngocanh@gmail.com', N'Hồ Chí Minh', N'Quản trị viên'),
('AD003', N'Lê Thanh Tùng', 'thanhtung', 'qtvpass3', '1991-03-15', N'Nam', '0963334455', 'thanhtung@gmail.com', N'Đà Nẵng', N'Quản trị viên'),
('AD004', N'Trần Hải Yến', 'haiyen', 'qtvpass4', '1993-04-18', N'Nữ', '0954445566', 'haiyen@gmail.com', N'Cần Thơ', N'Quản trị viên'),
('AD005', N'Hoàng Minh Tuấn', 'minhtuan', 'qtvpass5', '1989-05-20', N'Nam', '0945556677', 'minhtuan@gmail.com', N'Hải Phòng', N'Quản trị viên')

select * from QUANTRIVIEN

INSERT INTO LOAISAN (IDLoaiSan, TenLoaiSan, MoTa)
VALUES
('LS001', N'Sân bóng rổ', N'Dùng cho các trận đấu và tập luyện bóng rổ'),
('LS002', N'Sân bóng đá', N'Dùng cho các trận đấu và tập luyện bóng đá'),
('LS003', N'Sân cầu lông', N'Dùng cho các trận đấu và tập luyện cầu lông');

select * from LOAISAN

-- Sân bóng đá (4 sân)
INSERT INTO SAN (IDSan, IDLoaiSan, TenSan, DonGia, MoTa, TrangThai)
VALUES
('S001', 'LS002', N'Sân bóng đá 1', 400000, N'Sân cỏ nhân tạo, tiêu chuẩn quốc tế', N'Hoạt động'),
('S002', 'LS002', N'Sân bóng đá 2', 450000, N'Sân cỏ nhân tạo, tiêu chuẩn trường học', N'Hoạt động'),
('S003', 'LS002', N'Sân bóng đá 3', 500000, N'Sân cỏ tự nhiên, chất lượng cao', N'Hoạt động'),
('S004', 'LS002', N'Sân bóng đá 4', 350000, N'Sân cỏ nhân tạo, tiêu chuẩn địa phương', N'Hoạt động'),

-- Sân cầu lông (6 sân)
('S005', 'LS003', N'Sân cầu lông 1', 100000, N'Sân cầu lông chất lượng cao', N'Hoạt động'),
('S006', 'LS003', N'Sân cầu lông 2', 120000, N'Sân cầu lông với ánh sáng tốt', N'Hoạt động'),
('S007', 'LS003', N'Sân cầu lông 3', 110000, N'Sân cầu lông tiêu chuẩn', N'Hoạt động'),
('S008', 'LS003', N'Sân cầu lông 4', 100000, N'Sân cầu lông mới được nâng cấp', N'Hoạt động'),
('S009', 'LS003', N'Sân cầu lông 5', 130000, N'Sân cầu lông dành cho thi đấu', N'Hoạt động'),
('S010', 'LS003', N'Sân cầu lông 6', 125000, N'Sân cầu lông có sân khán đài', N'Hoạt động'),

-- Sân bóng rổ (4 sân)
('S011', 'LS001', N'Sân bóng rổ 1', 200000, N'Sân bóng rổ tiêu chuẩn quốc tế', N'Hoạt động'),
('S012', 'LS001', N'Sân bóng rổ 2', 250000, N'Sân bóng rổ dành cho thi đấu', N'Hoạt động'),
('S013', 'LS001', N'Sân bóng rổ 3', 220000, N'Sân bóng rổ cho học sinh', N'Hoạt động'),
('S014', 'LS001', N'Sân bóng rổ 4', 230000, N'Sân bóng rổ với hệ thống chiếu sáng tốt', N'Hoạt động');

select * from SAN

INSERT INTO HDDATSAN (IDHDDatSan, IDKhachHang, NgayLapHD, TienCoc, TongTien, TrangThai)
VALUES
('HD001', 'KH001', '2024-11-28', 100000, 300000, N'Ðã thanh toán'),
('HD002', 'KH002', '2024-11-29', 50000, 200000, N'Chua thanh toán'),
('HD003', 'KH003', '2024-11-30', 150000, 450000, N'Ðã thanh toán'),
('HD004', 'KH004', '2024-12-01', 80000, 250000, N'Chua thanh toán'),
('HD005', 'KH005', '2024-12-02', 120000, 320000, N'Ðã thanh toán'),
('HD006', 'KH006', '2024-12-03', 60000, 200000, N'Chua thanh toán'),
('HD007', 'KH007', '2024-12-04', 90000, 300000, N'Ðã thanh toán'),
('HD008', 'KH008', '2024-12-05', 110000, 330000, N'Chua thanh toán'),
('HD009', 'KH009', '2024-12-06', 70000, 210000, N'Ðã thanh toán'),
('HD010', 'KH010', '2024-12-07', 130000, 350000, N'Chua thanh toán')

select * from HDDATSAN

INSERT INTO CTHDDATSAN (IDCTHDDatSan, IDHDDatSan, IDSan, NgayDat, GioBatDau, GioKetThuc, DonGia)
VALUES
('CT001', 'HD001', 'S001', '2024-11-28', '2024-11-28 18:00:00', '2024-11-28 20:00:00', 200000),
('CT002', 'HD001', 'S003', '2024-11-28', '2024-11-28 20:00:00', '2024-11-28 21:00:00', 100000),
('CT003', 'HD002', 'S002', '2024-11-29', '2024-11-29 15:00:00', '2024-11-29 17:00:00', 450000),
('CT004', 'HD003', 'S004', '2024-11-30', '2024-11-30 10:00:00', '2024-11-30 12:00:00', 250000),
('CT005', 'HD004', 'S005', '2024-12-01', '2024-12-01 13:00:00', '2024-12-01 15:00:00', 200000),
('CT006', 'HD005', 'S003', '2024-12-02', '2024-12-02 16:00:00', '2024-12-02 18:00:00', 180000),
('CT007', 'HD006', 'S001', '2024-12-03', '2024-12-03 08:00:00', '2024-12-03 10:00:00', 250000),
('CT008', 'HD007', 'S004', '2024-12-04', '2024-12-04 17:00:00', '2024-12-04 19:00:00', 230000),
('CT009', 'HD008', 'S002', '2024-12-05', '2024-12-05 14:00:00', '2024-12-05 16:00:00', 220000),
('CT010', 'HD010', 'S003', '2024-12-07', '2024-12-07 12:00:00', '2024-12-07 14:00:00', 200000);

select * from CTHDDATSAN

INSERT INTO LOAICALAM (IDLoaiCa, TenCa, GioBatDau, GioKetThuc, LuongTheoGio, TongGioCa)
VALUES 
    ('LC01', 'Sáng', '07:00', '11:00', 20.00, 4),   
    ('LC02', 'Trưa', '11:00', '15:00', 20.00, 4),   
	('LC03', 'Chiều', '15:00', '19:00', 20.00, 4),   
    ('LC04', 'Tối', '19:00', '23:00', 25.00, 4);     

INSERT INTO CALAMVIEC (IDCaLamViec, IDLoaiCa, IDNhanVien, NgayLam)
VALUES
    ('CLV001', 'LC01', 'NV001', '2024-12-01'),  
    ('CLV002', 'LC01', 'NV002', '2024-12-01'), 
    ('CLV003', 'LC02', 'NV002', '2024-12-01'), 
	('CLV004', 'LC02', 'NV003', '2024-12-01'), 
    ('CLV005', 'LC03', 'NV003', '2024-12-01'),  
    ('CLV006', 'LC03', 'NV004', '2024-12-01'), 
	('CLV007', 'LC04', 'NV005', '2024-12-01'),  
    ('CLV008', 'LC04', 'NV006', '2024-12-01'), 

    ('CLV009', 'LC01', 'NV007', '2024-12-01'), 
	('CLV010', 'LC02', 'NV008', '2024-12-02'), 
    ('CLV011', 'LC03', 'NV009', '2024-12-02'),  
    ('CLV012', 'LC04', 'NV010', '2024-12-02');  


INSERT INTO TINHLUONG (IDLuong, IDNhanVien, Thang, Nam, TongSoGio, TongLuong)
VALUES 
    ('TL001', 'NV001', 12, 2024, 4, 40.00),  
    ('TL022', 'NV002', 12, 2024, 8, 40.00); 


INSERT INTO SUKIENKHUYENMAI (IDSuKien, TenSuKien, GiaTriGiam, MaCodeKM, MoTa, NgayBatDauSK, NgayKetThucSK, TrangThai)
VALUES
    ('SK001', 'Giảm giá cuối năm', 20.00, 'NY2024SALE', 'Khuyến mãi giảm giá 20% cho hóa đơn từ 500k', '2024-12-15', '2024-12-31', 'Đang hoạt động'),
    ('SK002', 'Tặng voucher đầu năm', 50.00, 'VOUCHER2024', 'Tặng voucher 50k cho khách hàng mới', '2024-01-01', '2024-01-15', 'Đã kết thúc'),
    ('SK003', 'Ưu đãi Black Friday', 30.00, 'BF2024DEAL', 'Giảm 30% toàn bộ sản phẩm', '2024-11-22', '2024-11-29', 'Đã kết thúc'),
    ('SK004', 'Giảm giá mùa hè', 15.00, 'SUMMER15OFF', 'Ưu đãi giảm 15% cho khách hàng VIP', '2024-06-01', '2024-06-30', 'Đã kết thúc'),
    ('SK005', 'Khuyến mãi Noel', 25.00, 'XMAS2024', 'Ưu đãi giảm 25% cho đơn hàng từ 300k', '2024-12-20', '2024-12-25', 'Sắp diễn ra');


INSERT INTO LSPFOOD (IDLoaiSP, TenLoaiSP, MoTa)
VALUES
('LSP001', N'Đồ uống', N'Nước giải khát và các loại nước ép'),
('LSP002', N'Thức ăn nhẹ', N'Bánh snack, khoai tây chiên, các món ăn vặt'),
('LSP003', N'Combo', N'Combo kết hợp giữa đồ uống và thức ăn nhẹ');

select * from LSPFOOD

INSERT INTO SPFOOD (IDSP, IDLoaiSP, TenSP, DonGia, MoTa, TrangThai)
VALUES
('SP001', 'LSP001', N'Coca Cola', 15000, N'Nước giải khát có ga', N'Còn hàng'),
('SP002', 'LSP001', N'Pepsi', 15000, N'Nước giải khát có ga', N'Còn hàng'),
('SP003', 'LSP002', N'Khoai tây chiên', 20000, N'Bánh snack giòn tan', N'Còn hàng'),
('SP004', 'LSP002', N'Bánh ngọt', 25000, N'Bánh ngọt mứt trái cây', N'Còn hàng'),
('SP005', 'LSP003', N'Combo Coke & Snack', 35000, N'Combo 1 Coca Cola + 1 Khoai tây chiên', N'Còn hàng'),
('SP006', 'LSP003', N'Combo Pepsi & Bánh ngọt', 40000, N'Combo 1 Pepsi + 1 Bánh ngọt', N'Còn hàng');

select * from SPFOOD

INSERT INTO HDSP (IDHDSP, IDKhachHang, NgayLap, TongTien)
VALUES
('HDSP001', 'KH001', '2024-11-28', 60000),  -- Hóa đơn 1
('HDSP002', 'KH002', '2024-11-29', 70000),  -- Hóa đơn 2
('HDSP003', 'KH003', '2024-11-30', 85000),  -- Hóa đơn 3
('HDSP004', 'KH004', '2024-12-01', 120000), -- Hóa đơn 4
('HDSP005', 'KH005', '2024-12-02', 110000); -- Hóa đơn 5

select * from HDSP

INSERT INTO CTHDSP (IDCTHDSP, IDHDSP, IDSP, SoLuong, DonGia, ThanhTien)
VALUES
('CT001', 'HDSP001', 'SP001', 2, 15000, 30000),  -- Hóa đơn 1, Coca Cola x2
('CT002', 'HDSP001', 'SP003', 1, 20000, 20000),  -- Hóa đơn 1, Khoai tây chiên x1
('CT003', 'HDSP002', 'SP002', 3, 15000, 45000),  -- Hóa đơn 2, Pepsi x3
('CT004', 'HDSP002', 'SP004', 1, 25000, 25000),  -- Hóa đơn 2, Bánh ngọt x1
('CT005', 'HDSP003', 'SP005', 1, 35000, 35000),  -- Hóa đơn 3, Combo Coke & Snack x1
('CT006', 'HDSP003', 'SP006', 1, 40000, 40000),  -- Hóa đơn 3, Combo Pepsi & Bánh ngọt x1
('CT007', 'HDSP004', 'SP001', 5, 15000, 75000),  -- Hóa đơn 4, Coca Cola x5
('CT008', 'HDSP004', 'SP003', 2, 20000, 40000),  -- Hóa đơn 4, Khoai tây chiên x2
('CT009', 'HDSP005', 'SP002', 4, 15000, 60000),  -- Hóa đơn 5, Pepsi x4
('CT010', 'HDSP005', 'SP004', 2, 25000, 50000);  -- Hóa đơn 5, Bánh ngọt x2

select * from CTHDSP

INSERT INTO HDTONG (IDHDTong, IDKH, NgayLap, TongTienTra)
VALUES
('HDT001', 'KH001', '2024-11-28', 60000),  -- Hóa đơn tổng 1
('HDT002', 'KH002', '2024-11-29', 70000),  -- Hóa đơn tổng 2
('HDT003', 'KH003', '2024-11-30', 85000),  -- Hóa đơn tổng 3
('HDT004', 'KH004', '2024-12-01', 120000), -- Hóa đơn tổng 4
('HDT005', 'KH005', '2024-12-02', 110000); -- Hóa đơn tổng 5

select * from HDTONG

INSERT INTO CTHDTONG (IDCTHDTong, IDHDTong, LoaiHoaDon, IDHoaDonCon, TongHoaDonCon, TongTienHDCon)
VALUES
('CTT001', 'HDT001', 'HDSP', 'HDSP001', 60000, 60000),  -- Hóa đơn tổng 1, liên kết với hóa đơn HDSP
('CTT002', 'HDT002', 'HDSP', 'HDSP002', 70000, 70000),  -- Hóa đơn tổng 2, liên kết với hóa đơn HDSP
('CTT003', 'HDT003', 'HDSP', 'HDSP003', 85000, 85000),  -- Hóa đơn tổng 3, liên kết với hóa đơn HDSP
('CTT004', 'HDT004', 'HDSP', 'HDSP004', 120000, 120000), -- Hóa đơn tổng 4, liên kết với hóa đơn HDSP
('CTT005', 'HDT005', 'HDSP', 'HDSP005', 110000, 110000); -- Hóa đơn tổng 5, liên kết với hóa đơn HDSP

select * from CTHDTONG

