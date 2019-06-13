using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Models.SystemModel;

namespace WeddingStoreDesktop.Services.SystemService
{
    public class SanPhamService
    {
        public List<SanPham> GetByIDLoaiDV(string maLDV)
        {
            return DataProvider.Ins.DB.SanPhams.Where(sp => sp.DichVu1.LoaiDV == maLDV).ToList();
        }

        public List<SanPham> GetByIDDichVu(string maDV)
        {
            return DataProvider.Ins.DB.SanPhams.Where(sp => sp.DichVu == maDV).ToList();
        }

        public void DeleteSanPham(SanPham mySanPham)
        {
            // Xóa danh sách chi tiết của sản phẩm
            List<ChiTietSanPham> lstChiTiet = DataProvider.Ins.DB.ChiTietSanPhams.Where(ct => ct.MaSP == mySanPham.MaSP).ToList();
            foreach (var ct in lstChiTiet)
            {
                DataProvider.Ins.DB.ChiTietSanPhams.Remove(ct);
                DataProvider.Ins.DB.SaveChanges();
            }

            // Xóa sản phẩm có trong chi tiết hóa đơn
            List<ChiTietHoaDon> lstChiTietHoaDon = DataProvider.Ins.DB.ChiTietHoaDons.Where(ct => ct.MaSP == mySanPham.MaSP).ToList();
            foreach(var ct in lstChiTietHoaDon)
            {
                DataProvider.Ins.DB.ChiTietHoaDons.Remove(ct);
                DataProvider.Ins.DB.SaveChanges();
            }

            // Xóa sản phẩm
            DataProvider.Ins.DB.SanPhams.Remove(mySanPham);
            DataProvider.Ins.DB.SaveChanges();
        }
    }
}
