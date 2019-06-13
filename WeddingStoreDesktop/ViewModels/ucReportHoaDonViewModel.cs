using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Models.DesktopModel;
using WeddingStoreDesktop.Models.SystemModel;
using WeddingStoreDesktop.Services.DesktopService;

namespace WeddingStoreDesktop.ViewModels
{
    public class ucReportHoaDonViewModel : BaseViewModel
    {
        private List<ThongTinMauModel> _LstThongTinMau;
        public List<ThongTinMauModel> LstThongTinMau
        {
            get => _LstThongTinMau;
            set
            {
                if (_LstThongTinMau != value)
                {
                    _LstThongTinMau = value;
                    OnPropertyChanged();
                }
            }
        }
        private HoaDon _myHD { get; set; }
        public HoaDon myHD
        {
            get => _myHD;
            set
            {
                _myHD = value;
                OnPropertyChanged();
            }
        }

        private KhachHang _myKH { get; set; }
        public KhachHang myKH
        {
            get => _myKH;
            set
            {
                _myKH = value;
                OnPropertyChanged();
            }
        }
        private float _TienDua { get; set; }
        public float TienDua
        {
            get => _TienDua;
            set
            {
                if (TienDua != value)
                {
                    _TienDua = value;
                    OnPropertyChanged();
                    TienTra = TienDua - myHD.TongTien.Value;
                }
            }
        }
        private float _TienTra { get; set; }
        public float TienTra
        {
            get => _TienTra;
            set
            {
                if (_TienTra != value)
                {
                    _TienTra = value;
                    OnPropertyChanged();
                }
            }
        }

        private List<ThongTinPhatSinhModel> _LstThongTinPhatSinh;
        public List<ThongTinPhatSinhModel> LstThongTinPhatSinh
        {
            get => _LstThongTinPhatSinh;
            set
            {
                if (_LstThongTinPhatSinh != value)
                {
                    _LstThongTinPhatSinh = value;
                    OnPropertyChanged();
                }
            }
        }
        public string maHD;
        public string maKH;

        public ucReportHoaDonViewModel(string maHD, string maKH, float tienDua)
        {
            this.maHD = maHD;
            this.maKH = maKH;
            _TienDua = tienDua;

            GetThongTin();
            GetData();

            TienTra = _TienDua - myHD.TongTien.Value;
        }

        void GetData()
        {
            ThongTinMauService ttMau = new ThongTinMauService();
            _LstThongTinMau = ttMau.GetChiTietByIdHD(maHD);

            ThongTinPhatSinhService ttPhatSinh = new ThongTinPhatSinhService();
            _LstThongTinPhatSinh = ttPhatSinh.GetThongTinPhatSinh(maHD);
        }

        void GetThongTin()
        {
            myHD = DataProvider.Ins.DB.HoaDons.FirstOrDefault(hd => hd.MaHD == maHD);

            myKH = DataProvider.Ins.DB.KhachHangs.FirstOrDefault(kh => kh.MaKH == maKH);
        }
    }
}
