using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Models.DesktopModel;

namespace WeddingStoreDesktop.Services.DesktopService
{
    public class ThongTinPhatSinhService
    {
        public List<ThongTinPhatSinhModel> GetThongTinPhatSinh(string maHD)
        {
            List<ThongTinPhatSinhModel> myLst = new List<ThongTinPhatSinhModel>();
            var query = from ps in DataProvider.Ins.DB.PhatSinhs
                        join vl in DataProvider.Ins.DB.KhoVatLieux on ps.MaVL equals vl.MaVL
                        where ps.MaHD == maHD
                        select new { vl, ps };
            foreach (var my in query)
            {
                ThongTinPhatSinhModel thongTin = new ThongTinPhatSinhModel
                {
                    MaHD = maHD,
                    AnhMoTa = my.vl.AnhMoTa,
                    MaVL = my.vl.MaVL,
                    TenVL = my.vl.TenVL,
                    SoLuong = my.ps.SoLuong,
                    ThanhTien = my.ps.ThanhTien,
                    IsNhap = my.vl.IsNhap.Value
                };
                myLst.Add(thongTin);
            }
            return myLst;
        }
    }
}
