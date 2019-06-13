using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WeddingStoreDesktop.Views;

namespace WeddingStoreDesktop.Functions
{
    public class CheckTinhNang
    {
        public static UserControl Check(int id)
        {
            UserControl uc = null;
            switch (id)
            {
                case 1:
                    uc = new ucThem();
                    break;
                case 2:
                    uc = new ucHoaDon();
                    break;
                case 3:
                    uc = new ucNhanVien();
                    break;
                case 4:
                    uc = new ucVatLieu();
                    break;
                case 6:
                    uc = new ucBangPhanCong();
                    break;
                case 7:
                    uc = new ucSanPham();
                    break;
                case 9:
                    uc = new ucThongKe();
                    break;
                case 10:
                    uc = new ucHoaDonNhap();
                    break;
            }
            return uc;
        }
    }
}
