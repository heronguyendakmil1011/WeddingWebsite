using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Models.SystemModel;

namespace WeddingStoreDesktop.Functions
{
    public class GetMaxID
    {
        // Hóa đơn
        public static int GetMaxIdHD()
        {
            int max = 0;
            List<HoaDon> lstHD = new List<HoaDon>();
            lstHD = DataProvider.Ins.DB.HoaDons.ToList();

            foreach (var hd in lstHD)
            {
                string[] myStr = hd.MaHD.Split('-');
                int id = int.Parse(myStr[1]);
                if (id > max)
                    max = id;
            }

            return max + 1;
        }

        // Khách hàng
        public static int GetMaxIdKH()
        {
            int max = 0;
            List<KhachHang> lstKH = new List<KhachHang>();
            lstKH = DataProvider.Ins.DB.KhachHangs.ToList();

            foreach (var kh in lstKH)
            {
                string[] myStr = kh.MaKH.Split('-');
                int id = int.Parse(myStr[1]);
                if (id > max)
                    max = id;
            }

            return max + 1;
        }
        // Nhân viên
        public static int GetMaxIdNV()
        {
            int max = 0;
            List<NhanVien> lstNV = new List<NhanVien>();
            lstNV = DataProvider.Ins.DB.NhanViens.ToList();

            foreach (var nv in lstNV)
            {
                string[] myStr = nv.MaNV.Split('-');
                int id = int.Parse(myStr[1]);
                if (id > max)
                    max = id;
            }

            return max + 1;
        }
        // Vật liệu
        public static int GetMaxIdVL()
        {
            int max = 0;
            List<KhoVatLieu> lstVL = new List<KhoVatLieu>();
            lstVL = DataProvider.Ins.DB.KhoVatLieux.ToList();

            foreach (var vl in lstVL)
            {
                string[] myStr = vl.MaVL.Split('-');
                int id = int.Parse(myStr[1]);
                if (id > max)
                    max = id;
            }

            return max + 1;
        }
        //Sản phẩm
        public static int GetMaxIdSP()
        {
            int max = 0;
            List<SanPham> lstSP = new List<SanPham>();
            lstSP = DataProvider.Ins.DB.SanPhams.ToList();

            foreach (var sp in lstSP)
            {
                string[] myStr = sp.MaSP.Split('-');
                int id = int.Parse(myStr[1]);
                if (id > max)
                    max = id;
            }

            return max + 1;
        }
        // Đơn giá nhập hàng
        public static int GetMaxIdDG()
        {
            int max = 0;
            List<DonGiaNhapHang> lstDG = new List<DonGiaNhapHang>();
            lstDG = DataProvider.Ins.DB.DonGiaNhapHangs.ToList();

            foreach (var dg in lstDG)
            {
                string[] myStr = dg.MaDG.Split('-');
                int id = int.Parse(myStr[1]);
                if (id > max)
                    max = id;
            }

            return max + 1;
        }
        // Nhà cung cấp
        public static int GetMaxIdNCC()
        {
            int max = 0;
            List<NhaCungCap> lstNCC = new List<NhaCungCap>();
            lstNCC = DataProvider.Ins.DB.NhaCungCaps.ToList();

            foreach (var ncc in lstNCC)
            {
                string[] myStr = ncc.MaNCC.Split('-');
                int id = int.Parse(myStr[1]);
                if (id > max)
                    max = id;
            }

            return max + 1;
        }
        // Loại dịch vụ
        public static int GetMaxIdLDV()
        {
            int max = 0;
            List<LoaiDichVu> lstLDV = new List<LoaiDichVu>();
            lstLDV = DataProvider.Ins.DB.LoaiDichVus.ToList();

            foreach (var ldv in lstLDV)
            {
                string[] myStr = ldv.MaLoaiDV.Split('-');
                int id = int.Parse(myStr[1]);
                if (id > max)
                    max = id;
            }

            return max + 1;
        }
        // Dịch vụ
        public static int GetMaxIdDV()
        {
            int max = 0;
            List<DichVu> lstDV = new List<DichVu>();
            lstDV = DataProvider.Ins.DB.DichVus.ToList();

            foreach (var dv in lstDV)
            {
                string[] myStr = dv.MaDV.Split('-');
                int id = int.Parse(myStr[1]);
                if (id > max)
                    max = id;
            }

            return max + 1;
        }
        // Tài khoản
        public static int GetMaxIdTK()
        {
            int max = 0;
            List<TaiKhoan> lstTK = new List<TaiKhoan>();
            lstTK = DataProvider.Ins.DB.TaiKhoans.ToList();
            foreach (var tk in lstTK)
            {
                string[] myStr = tk.ID.Split('-');
                int id = int.Parse(myStr[1]);
                if (id > max)
                    max = id;
            }

            return max + 1;
        }
    }
}
