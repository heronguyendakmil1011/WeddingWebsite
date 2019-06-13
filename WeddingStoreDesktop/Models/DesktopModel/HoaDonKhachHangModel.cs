using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WeddingStoreDesktop.Models.DesktopModel
{
    public class HoaDonKhachHangModel
    {
        public string MaHD { get; set; }
        public string MaKH { get; set; }
        public string NgayLap { get; set; }
        public string NgayTrangTri { get; set; }
        public string NgayThaoDo { get; set; }
        public string TenKH { get; set; }
        public string SoDT { get; set; }
        public string DiaChi { get; set; }
        public float? TongTien { get; set; }

        //public HoaDonKhachHangModel(string maHD, string maKH, DateTime ngayLap, DateTime ngayTrangTri, DateTime ngayThaoDo, string tenKH, string soDT, string diaChi, float tongTien)
        //{
        //    MaHD = maHD;
        //    MaKH = maKH;
        //    NgayLap = ngayLap;
        //    NgayTrangTri = ngayTrangTri;
        //    NgayThaoDo = ngayThaoDo;
        //    TenKH = tenKH;
        //    SoDT = soDT;
        //    DiaChi = diaChi;
        //    TongTien = tongTien;
        //}
    }
}
