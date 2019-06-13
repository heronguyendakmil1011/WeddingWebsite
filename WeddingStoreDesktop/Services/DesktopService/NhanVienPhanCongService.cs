using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Models.DesktopModel;
using WeddingStoreDesktop.Models.SystemModel;

namespace WeddingStoreDesktop.Services.DesktopService
{
    public class NhanVienPhanCongService
    {
        public List<PhanCongNhanVienModel> GetPhanCongByNgayTrangTri(string maHD, string ngay)
        {
            List<PhanCongNhanVienModel> myLst = new List<PhanCongNhanVienModel>();

            //var query = from hd in DataProvider.Ins.DB.HoaDons
            //                .Where(myhd => myhd.MaHD == maHD && myhd.NgayTrangTri.Equals(ngay))
            //            join pc in DataProvider.Ins.DB.PhanCongs on hd.MaHD equals pc.MaHD
            //            join nv in DataProvider.Ins.DB.NhanViens on pc.MaNV equals nv.MaNV
            //            select new { nv, pc };

            DateTime myNgay = DateTime.ParseExact(ngay, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var query = from hd in DataProvider.Ins.DB.HoaDons
                        join pc in DataProvider.Ins.DB.PhanCongs on hd.MaHD equals pc.MaHD
                        where hd.MaHD == maHD && pc.Ngay == myNgay
                        join nv in DataProvider.Ins.DB.NhanViens on pc.MaNV equals nv.MaNV
                        select new { nv, pc };

            foreach (var my in query)
            {
                PhanCongNhanVienModel phanCong = new PhanCongNhanVienModel
                {
                    MaHD = maHD,
                    Ngay = ngay,
                    Avatar = my.nv.Avatar,
                    MaNV = my.nv.MaNV,
                    TenNV = my.nv.TenNV,
                    ThoiGianDen = my.pc.ThoiGianDen.ToString(),
                    ThoiGianDi = my.pc.ThoiGianDi.ToString()
                };

                myLst.Add(phanCong);
            }
            return myLst;
        }

        public List<PhanCongNhanVienModel> GetPhanCongByNgayThaoDo(string maHD, string ngay)
        {
            List<PhanCongNhanVienModel> myLst = new List<PhanCongNhanVienModel>();
            DateTime myNgay = DateTime.ParseExact(ngay, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var query = from hd in DataProvider.Ins.DB.HoaDons
                        join pc in DataProvider.Ins.DB.PhanCongs on hd.MaHD equals pc.MaHD
                        where hd.MaHD == maHD && pc.Ngay == myNgay
                        join nv in DataProvider.Ins.DB.NhanViens on pc.MaNV equals nv.MaNV
                        select new { nv, pc };

            foreach (var my in query)
            {
                PhanCongNhanVienModel phanCong = new PhanCongNhanVienModel
                {
                    MaHD = maHD,
                    Ngay = ngay,
                    Avatar = my.nv.Avatar,
                    MaNV = my.nv.MaNV,
                    TenNV = my.nv.TenNV,
                    ThoiGianDen = my.pc.ThoiGianDen.ToString(),
                    ThoiGianDi = my.pc.ThoiGianDi.ToString()
                };
                myLst.Add(phanCong);
            }
            return myLst;
        }

        public List<PhanCongNhanVienModel> GetPhanCongNhanVienByIdHDVaNgay(string maHD, string ngay)
        {
            DateTime myNgay = DateTime.ParseExact(ngay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            List<PhanCongNhanVienModel> myLst = new List<PhanCongNhanVienModel>();

            List<PhanCong> myPhanCong = DataProvider.Ins.DB.PhanCongs.Where(pc => pc.MaHD == maHD && pc.Ngay == myNgay).ToList();
            foreach (var pc in myPhanCong)
            {
                myLst.Add(new PhanCongNhanVienModel
                {
                    Avatar = pc.NhanVien.Avatar,
                    MaNV = pc.NhanVien.MaNV,
                    TenNV = pc.NhanVien.TenNV,
                    Ngay = ngay.ToString(),
                    MaHD = pc.MaHD,
                    ThoiGianDen = pc.ThoiGianDen.HasValue ? pc.ThoiGianDen.Value.ToString() : String.Empty,
                    ThoiGianDi = pc.ThoiGianDi.HasValue ? pc.ThoiGianDen.Value.ToString() : String.Empty
                });
            }

            return myLst;
        }

        public List<PhanCongNhanVienModel> GetPhanCongByIdNVVaThangVaNam(string maNV, int thang, int nam)
        {
            List<PhanCongNhanVienModel> myLst = new List<PhanCongNhanVienModel>();
            List<PhanCong> lstPhanCong = new List<PhanCong>();

            lstPhanCong = DataProvider.Ins.DB.PhanCongs.Where(pc => pc.MaNV == maNV
                                                                && pc.Ngay.Month == thang
                                                                && pc.Ngay.Year == nam).ToList();
            foreach (var pc in lstPhanCong)
            {
                myLst.Add(new PhanCongNhanVienModel
                {
                    Avatar = pc.NhanVien.Avatar,
                    MaHD = pc.MaHD,
                    MaNV = pc.MaNV,
                    TenNV = pc.NhanVien.TenNV,
                    Ngay = pc.Ngay.ToString(),
                    ThoiGianDen = pc.ThoiGianDen.ToString(),
                    ThoiGianDi = pc.ThoiGianDi.ToString()
                });
            }
            return myLst;
        }
    }
}
