using Do_an_OOP.Controller;
using Do_an_OOP.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Do_an_OOP
{
    public partial class FTKKH : Form
    {
        CtrlKhachHang ctrlkh = new CtrlKhachHang();
        List<KHACHHANG> dskh = new List<KHACHHANG>();
        KHACHHANG kh = null;
        public FTKKH()
        {
            InitializeComponent();
        }

        private void btnChinhSuaTT_Click(object sender, EventArgs e)
        {
            txtTenTK.ReadOnly = false;
            txtEmail.ReadOnly = false;
            txtSDT.ReadOnly = false;
            txtDiaChi.ReadOnly = false;

            btnXongTKKH.Visible = true;
        }

        private void btnDoiMK_Click(object sender, EventArgs e)
        {
            txtMatKHau.ReadOnly = false;

            // Hiển thị nút Xong
            btnXongTKKH.Visible = true;
        }

        //private void btnXongTKKH_Click(object sender, EventArgs e)
        //{
        //    // Kiểm tra trạng thái của ô mật khẩu để phân biệt giữa "Chỉnh sửa thông tin" và "Đổi mật khẩu"
        //    if (!txtMatKHau.ReadOnly) // Đang đổi mật khẩu
        //    {
        //        // Lấy khách hàng đang đăng nhập
        //        var khachHang = CUtils.db.KHACHHANGs
        //            .FirstOrDefault(kh => kh.TenTaiKhoan == txtTenTK.Text);

        //        if (khachHang != null)
        //        {
        //            // Cập nhật mật khẩu
        //            khachHang.MatKhau = txtMatKHau.Text;

        //            // Lưu thay đổi
        //            await CUtils.db.SaveChangesAsync();

        //            // Hiển thị thông báo và quay lại form Login
        //            MessageBox.Show("Thay đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            FLoginKH frmLogin = new FLoginKH();
        //            frmLogin.Show();
        //            this.Close();
        //        }
        //        else
        //        {
        //            MessageBox.Show("Không tìm thấy tài khoản để cập nhật!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //    else // Đang chỉnh sửa thông tin
        //    {
        //        // Lấy khách hàng đang đăng nhập
        //        var khachHang = CUtils.db.KHACHHANGs
        //            .FirstOrDefault(kh => kh.TenTaiKhoan == txtTenTK.Text);

        //        if (khachHang != null)
        //        {
        //            // Cập nhật thông tin khách hàng
        //            khachHang.TenTaiKhoan = txtTenTK.Text;
        //            khachHang.Email = txtEmail.Text;
        //            khachHang.SDT = txtSDT.Text;
        //            khachHang.DiaChi = txtDiaChi.Text;

        //            // Lưu thay đổi
        //            await CUtils.db.SaveChangesAsync();

        //            // Hiển thị thông báo
        //            MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //        else
        //        {
        //            MessageBox.Show("Không tìm thấy tài khoản để cập nhật!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }

            // Ẩn nút Xong và đặt các ô về trạng thái chỉ đọc
        //    btnXongTKKH.Visible = false;
        //    txtTenTK.ReadOnly = true;
        //    txtEmail.ReadOnly = true;
        //    txtSDT.ReadOnly = true;
        //    txtDiaChi.ReadOnly = true;
        //    txtMatKHau.ReadOnly = true;
        //}

        private void FTKKH_Load(object sender, EventArgs e)
        {
            kh = MyGereral.KhachHang;
            txtTenKH.Text = kh.TenKhachHang;
            txtTenTK.Text = kh.TenTaiKhoan;
            txtGioiTinh.Text = kh.GioiTinh;
            txtNgaySinh.Text = kh.NgaySinh + "";
            txtNgayLapTk.Text = kh.NgayTaoTK + "";
            txtSDT.Text = kh.SDT;
            txtDiaChi.Text = kh.DiaChi;
            txtEmail.Text = kh.Email;
            txtMatKHau.Text = kh.MatKhau;
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            FHomeKH me = new FHomeKH();
            this.Hide();
            me.ShowDialog();
            this.Show();
        }

        private void btnDatSan_Click(object sender, EventArgs e)
        {
            FDatSanBR me = new FDatSanBR();
            this.Hide();
            me.ShowDialog();
            this.Show();
        }
    }
}
