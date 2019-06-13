using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingStoreDesktop.Models.DesktopModel
{
    public class ThongTinChiTietVatLieuModel
    {
        public Byte[] AnhMoTa { get; set; }
        public string TenVL { get; set; }
        public int? SoLuong { get; set; }
        public string DonVi { get; set; }
        public float? ThanhTien { get; set; }

        //public ThongTinChiTietVatLieuModel(string anhMoTa, string tenVL, int soLuong, string donVi, float thanhTien)
        //{
        //    AnhMoTa = anhMoTa;
        //    TenVL = tenVL;
        //    SoLuong = soLuong;
        //    DonVi = donVi;
        //    ThanhTien = thanhTien;
        //}
    }
}
