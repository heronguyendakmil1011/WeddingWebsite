using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Models.DesktopModel;

namespace WeddingStoreDesktop.Services.DesktopService
{
    public class ThongTinChiTietVatLieuService
    {
        public List<ThongTinChiTietVatLieuModel> GetThongTinVatLieu(string maHD, string maSP)
        {
            List<ThongTinChiTietVatLieuModel> myLst = new List<ThongTinChiTietVatLieuModel>();

            var query = from cthd in DataProvider.Ins.DB.ChiTietHoaDons.Where(mycthd => mycthd.MaHD == maHD && mycthd.MaSP == maSP)
                        join sp in DataProvider.Ins.DB.SanPhams on cthd.MaSP equals sp.MaSP
                        join ctsp in DataProvider.Ins.DB.ChiTietSanPhams on sp.MaSP equals ctsp.MaSP
                        join vl in DataProvider.Ins.DB.KhoVatLieux on ctsp.MaVL equals vl.MaVL
                        select new { cthd.SoLuong, vl.TenVL, vl.AnhMoTa, slVL = ctsp.SoLuong, vl.GiaTien, vl.DonVi };

            foreach (var my in query)
            {
                ThongTinChiTietVatLieuModel thongTin = new ThongTinChiTietVatLieuModel
                {
                    AnhMoTa = my.AnhMoTa,
                    TenVL = my.TenVL,
                    SoLuong = my.SoLuong * my.slVL,
                    ThanhTien = my.GiaTien * (my.SoLuong * my.slVL),
                    DonVi = my.DonVi
                };
                myLst.Add(thongTin);
            }
            return myLst;
        }
    }
}
