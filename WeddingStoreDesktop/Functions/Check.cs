using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Models.DesktopModel;
using WeddingStoreDesktop.Models.SystemModel;
using WeddingStoreDesktop.Services.SystemService;

namespace WeddingStoreDesktop.Functions
{
    public class Check
    {
        // Tìm mã số lượng của mẫu, khi thao tác trên danh sách ảo
        public int CheckMauAo(List<KhoVatLieuAoModel> lstVatLieu, string maSP)
        {
            int maxSoLuong = int.MaxValue;
            bool allNhap = true;
            List<ChiTietSanPham> lstChiTiet = DataProvider.Ins.DB.ChiTietSanPhams.Where(ct => ct.MaSP == maSP).ToList();

            foreach (var ct in lstChiTiet)
            {
                foreach (var vl in lstVatLieu)
                {
                    if (!ct.KhoVatLieu.IsNhap.Value) // không tính vật liệu nhập
                    {
                        if (ct.MaVL == vl.MaVL)
                        {
                            if (vl.SoLuong / ct.SoLuong == 0)
                                return 0;
                            else if ((vl.SoLuong / ct.SoLuong) < maxSoLuong)
                            {
                                allNhap = false;
                                maxSoLuong = vl.SoLuong.Value / ct.SoLuong.Value;
                                break;
                            }
                        }
                    }
                }
            }

            return allNhap ? -1 : maxSoLuong;
        }

        public int CheckVatLieuAo(List<KhoVatLieuAoModel> lstVatLieu, string maVL)
        {
            return lstVatLieu.FirstOrDefault(vl => vl.MaVL == maVL).SoLuong.Value;
        }
    }
}
