using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingStoreDesktop.Models.DesktopModel
{
    public class ThongTinMauModel : BaseModel
    {
        public string MaHD { get; set; }
        public Byte[] HinhMoTa { get; set; }
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        private int? _SoLuong { get; set; }
        public int? SoLuong
        {
            get => _SoLuong;
            set
            {
                _SoLuong = value;
                OnPropertyChanged();
            }
        }
        private float? _ThanhTien { get; set; }
        public float? ThanhTien
        {
            get => _ThanhTien;
            set
            {
                _ThanhTien = value;
                OnPropertyChanged();
            }
        }

        //public ThongTinMauModel(string maHD, string maSP, string tenSP, int soLuong, float thanhTien)
        //{
        //    MaHD = maHD;
        //    MaSP = maSP;
        //    TenSP = tenSP;
        //    SoLuong = soLuong;
        //    ThanhTien = thanhTien;
        //}
    }
}
