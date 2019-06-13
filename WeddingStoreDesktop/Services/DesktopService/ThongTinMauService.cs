using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Models.DesktopModel;

namespace WeddingStoreDesktop.Services.DesktopService
{
    public class ThongTinMauService
    {
        public List<ThongTinMauModel> GetChiTietByIdHD(string maHD)
        {
            List<ThongTinMauModel> myLst = new List<ThongTinMauModel>();

            var query = from cthd in DataProvider.Ins.DB.ChiTietHoaDons
                        join sp in DataProvider.Ins.DB.SanPhams on cthd.MaSP equals sp.MaSP
                        where cthd.MaHD == maHD
                        select new { cthd, sp };
            foreach (var my in query)
            {
                ThongTinMauModel thongTinMau = new ThongTinMauModel
                {
                    MaHD = maHD,
                    HinhMoTa=my.sp.HinhMoTa,
                    MaSP = my.cthd.MaSP,
                    TenSP = my.sp.TenSP,
                    SoLuong = my.cthd.SoLuong,
                    ThanhTien = my.cthd.ThanhTien
                };
                myLst.Add(thongTinMau);
            }
            return myLst;
        }
    }
}
