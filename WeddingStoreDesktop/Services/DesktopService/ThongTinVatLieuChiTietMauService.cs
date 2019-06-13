using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Models.DesktopModel;
using WeddingStoreDesktop.Models.SystemModel;

namespace WeddingStoreDesktop.Services.DesktopService
{
    public class ThongTinVatLieuChiTietMauService
    {
        public List<ThongTinVatLieuChiTietMauModel> GetByIDSanPham(string maSP)
        {
            List<ThongTinVatLieuChiTietMauModel> myLst = new List<ThongTinVatLieuChiTietMauModel>();

            List<ChiTietSanPham> lstChiTietSanPham = new List<ChiTietSanPham>();
            lstChiTietSanPham = DataProvider.Ins.DB.ChiTietSanPhams.Where(ct => ct.MaSP == maSP).ToList();
            foreach (var ct in lstChiTietSanPham)
            {
                myLst.Add(new ThongTinVatLieuChiTietMauModel
                {
                    MaVL=ct.KhoVatLieu.MaVL,
                    TenVL=ct.KhoVatLieu.TenVL,
                    DonVi=ct.KhoVatLieu.DonVi,
                    SoLuong=ct.SoLuong,
                    ThanhTien=ct.GiaTien,
                });
            }

            return myLst;
        }
    }
}
