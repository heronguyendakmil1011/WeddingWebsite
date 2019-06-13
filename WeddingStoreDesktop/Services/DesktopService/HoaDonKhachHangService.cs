using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeddingStoreDesktop.Models.SystemModel;
using WeddingStoreDesktop.Models.DesktopModel;
using WeddingStoreDesktop.Models;
using System.Globalization;
using WeddingStoreDesktop.Converters;

namespace WeddingStoreDesktop.Services.DesktopService
{
    public class HoaDonKhachHangService
    {
        public List<HoaDonKhachHangModel> GetHoaDonKhachHang()
        {
            List<HoaDonKhachHangModel> myLst = new List<HoaDonKhachHangModel>();
            var query = from hd in DataProvider.Ins.DB.HoaDons
                        join kh in DataProvider.Ins.DB.KhachHangs on hd.MaKH equals kh.MaKH
                        select new { hd, kh };

            foreach (var my in query)
            {
                HoaDonKhachHangModel hoaDonKH = new HoaDonKhachHangModel
                {
                    MaHD = my.hd.MaHD,
                    MaKH = my.kh.MaKH,
                    NgayLap = ConvertDateTimeToString.ConverToMyDateFormat(my.hd.NgayLap),
                    NgayTrangTri = ConvertDateTimeToString.ConverToMyDateFormat(my.hd.NgayTrangTri),
                    NgayThaoDo = ConvertDateTimeToString.ConverToMyDateFormat(my.hd.NgayThaoDo),
                    DiaChi = my.kh.DiaChi,
                    SoDT = my.kh.SoDT,
                    TenKH = my.kh.TenKH,
                    TongTien = my.hd.TongTien
                };
                myLst.Add(hoaDonKH);
            }
            return myLst;
        }

        public List<HoaDonKhachHangModel> GetHoaDonKhachHangByThangNam(int thang, int nam)
        {
            List<HoaDonKhachHangModel> myLst = new List<HoaDonKhachHangModel>();
            List<HoaDon> lstHoaDon = DataProvider.Ins.DB.HoaDons.Where(hd => hd.NgayLap.Value.Month == thang && hd.NgayLap.Value.Year == nam).ToList();
            foreach(var hd in lstHoaDon)
            {
                HoaDonKhachHangModel hoaDonKH = new HoaDonKhachHangModel
                {
                    MaHD = hd.MaHD,
                    MaKH = hd.KhachHang.MaKH,
                    NgayLap = ConvertDateTimeToString.ConverToMyDateFormat(hd.NgayLap),
                    NgayTrangTri = ConvertDateTimeToString.ConverToMyDateFormat(hd.NgayTrangTri),
                    NgayThaoDo = ConvertDateTimeToString.ConverToMyDateFormat(hd.NgayThaoDo),
                    DiaChi = hd.KhachHang.DiaChi,
                    SoDT = hd.KhachHang.SoDT,
                    TenKH = hd.KhachHang.TenKH,
                    TongTien = hd.TongTien
                };
                myLst.Add(hoaDonKH);
            }
            return myLst;
        }
    }
}
