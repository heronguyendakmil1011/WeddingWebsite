using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingStoreDesktop.ViewModels;

namespace WeddingStoreDesktop.Models.DesktopModel
{
    public class PhanCongNhanVienModel:BaseViewModel
    {
        public string MaHD { get; set; }
        public string Ngay { get; set; }
        public Byte[] Avatar { get; set; }
        public string MaNV { get; set; }
        public string TenNV { get; set; }
        private string _ThoiGianDen { get; set; }
        public string ThoiGianDen
        {
            get => _ThoiGianDen;
            set
            {
                _ThoiGianDen = value;
                OnPropertyChanged();
            }
        }

        private string _ThoiGianDi { get; set; }
        public string ThoiGianDi
        {
            get => _ThoiGianDi;
            set
            {
                _ThoiGianDi = value;
                OnPropertyChanged();
            }
        }
    }
}
