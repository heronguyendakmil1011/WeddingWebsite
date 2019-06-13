using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Models.DesktopModel;

namespace WeddingStoreDesktop.Services.DesktopService
{
    public class ChamCongService
    {
        public List<ChamCongModel> GetChamCongByIdNV(string maNV)
        {
            List<ChamCongModel> myLst = new List<ChamCongModel>();
            var query = from pc in DataProvider.Ins.DB.PhanCongs
                        join nv in DataProvider.Ins.DB.NhanViens on pc.MaNV equals nv.MaNV
                        join hd in DataProvider.Ins.DB.HoaDons on pc.MaHD equals hd.MaHD
                        join kh in DataProvider.Ins.DB.KhachHangs on hd.MaKH equals kh.MaKH
                        where nv.MaNV == maNV
                        select new { hd.MaHD, kh.TenKH, pc };
            foreach (var my in query)
            {
                ChamCongModel chamCong = new ChamCongModel
                {
                    MaHD = my.MaHD,
                    TenKH = my.TenKH,
                    Ngay = my.pc.Ngay,
                    ThoiGianDen = my.pc.ThoiGianDen.HasValue ? my.pc.ThoiGianDen.Value.ToString() : String.Empty,
                    ThoiGianDi = my.pc.ThoiGianDi.HasValue ? my.pc.ThoiGianDi.Value.ToString() : String.Empty,
                    TongThoiGian = my.pc.ThoiGianDi.HasValue && my.pc.ThoiGianDen.HasValue
                                    && my.pc.ThoiGianDen.Value.CompareTo(TimeSpan.Zero) != 0
                                    && my.pc.ThoiGianDi.Value.CompareTo(TimeSpan.Zero) != 0
                                    ? my.pc.ThoiGianDi.Value.Subtract(my.pc.ThoiGianDen.Value).ToString()
                                    : String.Empty
                };
                myLst.Add(chamCong);
            }
            return myLst;
        }

        public List<ChamCongModel> GetChamCongByIdNVVaThang(string maNV, int thang)
        {
            List<ChamCongModel> myLst = new List<ChamCongModel>();
            List<ChamCongModel> lstChamCong = GetChamCongByIdNV(maNV);
            foreach (var chamCong in lstChamCong)
            {
                if (chamCong.Ngay.Month == thang)
                    myLst.Add(chamCong);
            }

            return myLst;
        }

        public List<ChamCongModel> GetChamCongByIdNVVaThangNam(string maNV, int thang, int nam)
        {
            List<ChamCongModel> myLst = new List<ChamCongModel>();
            List<ChamCongModel> lstChamCong = GetChamCongByIdNV(maNV);
            foreach (var chamCong in lstChamCong)
            {
                if (chamCong.Ngay.Month == thang && chamCong.Ngay.Year == nam)
                    myLst.Add(chamCong);
            }

            return myLst;
        }
    }
}
