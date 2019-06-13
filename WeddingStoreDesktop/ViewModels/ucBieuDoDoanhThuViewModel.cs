using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Models.DesktopModel;
using WeddingStoreDesktop.Models.SystemModel;
using WeddingStoreDesktop.Services.DesktopService;

namespace WeddingStoreDesktop.ViewModels
{
    public class ucBieuDoDoanhThuViewModel : BaseViewModel
    {
        private List<HoaDon> _LstHoaDon { get; set; }
        public List<HoaDon> LstHoaDon
        {
            get => _LstHoaDon;
            set
            {
                if (_LstHoaDon != value)
                {
                    _LstHoaDon = value;
                    OnPropertyChanged();
                }
            }
        }

        private List<LuongNhanVienModel> _LstLuongNhanVien { get; set; }
        public List<LuongNhanVienModel> LstLuongNhanVien
        {
            get => _LstLuongNhanVien;
            set
            {
                if (_LstLuongNhanVien != value)
                {
                    _LstLuongNhanVien = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<VatLieuNhapModel> _LstVatLieuNhap { get; set; }
        public List<VatLieuNhapModel> LstVatLieuNhap
        {
            get => _LstVatLieuNhap;
            set
            {
                if (_LstVatLieuNhap != value)
                {
                    _LstVatLieuNhap = value;
                    OnPropertyChanged();
                }
            }
        }

        private Dictionary<string, float> _dicVatLieuNhap = new Dictionary<string, float>();
        public Dictionary<string, float> dicVatLieuNhap
        {
            get => _dicVatLieuNhap;
            set
            {
                _dicVatLieuNhap = value;
                OnPropertyChanged();
            }
        }

        private Dictionary<string, float> _dicLuongNhanVien = new Dictionary<string, float>();
        public Dictionary<string, float> dicLuongNhanVien
        {
            get => _dicLuongNhanVien;
            set
            {
                _dicLuongNhanVien = value;
                OnPropertyChanged();
            }
        }

        private Dictionary<string, float> _dicHoaDon = new Dictionary<string, float>();
        public Dictionary<string, float> dicHoaDon
        {
            get => _dicHoaDon;
            set
            {
                _dicHoaDon = value;
                OnPropertyChanged();
            }
        }

        public ucBieuDoDoanhThuViewModel(string thang, int nam)
        {
            Title = "Doanh thu " + thang + " Năm " + nam.ToString();

            string[] strThang = thang.Split(' ');
            int myThang = int.Parse(strThang[1]);

            GetData(myThang, nam);
        }

        public ucBieuDoDoanhThuViewModel(string fromThang, string toThang, int nam)
        {
            Title = "Doanh thu từ " + fromThang + " đến " + toThang + " Năm " + nam.ToString();
            GetByCustom(fromThang, toThang, nam);
        }

        private float _tienLuong;
        private float _tienNhap;
        private float _tienHoaDon;

        void GetData(int thang, int nam)
        {
            _tienLuong = 0;
            _tienNhap = 0;
            _tienHoaDon = 0;
            List<HoaDon> lstHoaDon = DataProvider.Ins.DB.HoaDons.Where(hd => hd.NgayLap.Value.Month == thang
                                                                    && hd.NgayLap.Value.Year == nam).ToList();
            foreach (var hd in lstHoaDon)
            {
                _tienHoaDon += hd.TongTien.Value;
            }
            dicHoaDon.Add("Tháng " + thang, _tienHoaDon);

            LuongNhanVienService luongNV = new LuongNhanVienService();
            List<LuongNhanVienModel> lstLuongNV = luongNV.GetLuongNVByThang(thang, nam);
            foreach (var luong in lstLuongNV)
                _tienLuong += luong.TongTien;
            dicLuongNhanVien.Add("Tháng " + thang, _tienLuong);

            VatLieuNhapService vatLieuNhap = new VatLieuNhapService();
            List<VatLieuNhapModel> lstVatLieuNhap = vatLieuNhap.GetVatLieuNhapByThang(thang, nam);
            foreach (var vl in lstVatLieuNhap)
                _tienNhap += vl.ThanhTien.Value;
            dicVatLieuNhap.Add("Tháng " + thang, _tienNhap);
        }

        void GetByCustom(string myFrom, string myTo, int nam)
        {
            string[] fromThang = myFrom.Split(' ');
            int from = int.Parse(fromThang[1]);

            string[] toThang = myTo.Split(' ');
            int to = int.Parse(toThang[1]);

            if (from > to)
                MessageBox.Show("Nooooo!!! Wrong");
            else
            {
                for (int i = from; i <= to; i++)
                {
                    GetData(i, nam);
                }
            }
        }
    }
}
