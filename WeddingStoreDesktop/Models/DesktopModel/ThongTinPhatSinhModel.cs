using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingStoreDesktop.Models.DesktopModel
{
    public class ThongTinPhatSinhModel
    {
        public string MaHD { get; set; }
        public Byte[] AnhMoTa { get; set; }
        public string MaVL { get; set; }
        public string TenVL { get; set; }
        public bool IsNhap { get; set; }
        public int? SoLuong { get; set; }
        public float? ThanhTien { get; set; }

        //public ThongTinPhatSinhModel(string maHD, string maVL, string tenVL, int soLuong, float thanhTien)
        //{
        //    MaHD = maHD;
        //    MaVL = maVL;
        //    TenVL = tenVL;
        //    SoLuong = soLuong;
        //    ThanhTien = thanhTien;
        //}
    }
}
