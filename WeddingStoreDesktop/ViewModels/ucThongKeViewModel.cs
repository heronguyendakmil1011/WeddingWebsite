using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Functions;
using System.Windows.Input;
using WeddingStoreDesktop.Interfaces.Dialog;
using System.Windows;
using WeddingStoreDesktop.Models.DesktopModel;
using WeddingStoreDesktop.Models.SystemModel;

namespace WeddingStoreDesktop.ViewModels
{
    public class ucThongKeViewModel : BaseViewModel
    {
        private bool _op { get; set; } // true: theo tháng;   false: theo custom
        public bool op
        {
            get => _op;
            set
            {
                if (_op != value)
                {
                    _op = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<string> LstThang
        {
            get => new List<string>
            {
                "Tháng 1",
                "Tháng 2",
                "Tháng 3",
                "Tháng 4",
                "Tháng 5",
                "Tháng 6",
                "Tháng 7",
                "Tháng 8",
                "Tháng 9",
                "Tháng 10",
                "Tháng 11",
                "Tháng 12",
            };
        }

        private string _Thang { get; set; }
        public string Thang
        {
            get => _Thang;
            set
            {
                if (_Thang != value)
                {
                    _Thang = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _FromThang { get; set; }
        public string FromThang
        {
            get => _FromThang;
            set
            {
                if (_FromThang != value)
                {
                    _FromThang = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _ToThang { get; set; }
        public string ToThang
        {
            get => _ToThang;
            set
            {
                if (_ToThang != value)
                {
                    _ToThang = value;
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

        public List<HoaDon> _LstHoaDon { get; set; }
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

        private float _TongTienHD { get; set; }
        public float TongTienHD
        {
            get => _TongTienHD;
            set
            {
                _TongTienHD = value;
                OnPropertyChanged();
            }
        }

        public float _TongTienNhap { get; set; }
        public float TongTienNhap
        {
            get => _TongTienNhap;
            set
            {
                if (_TongTienNhap != value)
                {
                    _TongTienNhap = value;
                    OnPropertyChanged();
                }
            }
        }

        private float _TongLuongNV { get; set; }
        public float TongLuongNV
        {
            get => _TongLuongNV;
            set
            {
                if (_TongLuongNV != value)
                {
                    _TongLuongNV = value;
                    OnPropertyChanged();
                }
            }
        }

        private float _DoanhThu { get; set; }
        public float DoanhThu
        {
            get => _DoanhThu;
            set
            {
                if (_DoanhThu != value)
                {
                    _DoanhThu = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _nam { get; set; }
        public string nam
        {
            get => _nam;
            set { _nam = value; OnPropertyChanged(); }
        }

        public ucThongKeViewModel()
        {
            _op = true; // theo tháng
            _nam = DateTime.Now.Year.ToString();
            // ShowCommand = new ActionCommand(p => Show());
        }

        //Ko Update GridData được
        //void GetByThang()
        //{
        //    if (Thang == null)
        //    {
        //        MessageBox.Show("Chọn tháng pleaseeeeeeee");
        //    }
        //    else
        //    {
        //        string[] thang = Thang.Split(' ');
        //        int myThang = int.Parse(thang[1]);

        //        _TongLuongNV = 0;
        //        _TongTienNhap = 0;
        //        _TongTienHD = 0;
        //        GetData(myThang);

        //        DoanhThu = TongTienHD - TongLuongNV - TongTienNhap;
        //    }
        //}

        //void GetByCustom()
        //{
        //    if (FromThang == null || ToThang == null)
        //    {
        //        MessageBox.Show("Chọn tháng pleaseeeeeeee");
        //    }
        //    else
        //    {
        //        string[] fromThang = FromThang.Split(' ');
        //        int from = int.Parse(fromThang[1]);

        //        string[] toThang = ToThang.Split(' ');
        //        int to = int.Parse(toThang[1]);

        //        MessageBox.Show("ahihihi");
        //    }
        //}

        //void GetData(int thang)
        //{
        //    // Get Luong Nhan Vien
        //    LuongNhanVienRepository luongNhanVien = new LuongNhanVienRepository();
        //    _LstLuongNhanVien = luongNhanVien.GetListByThang(thang);
        //    foreach (var nv in LstLuongNhanVien)
        //    {
        //        TongLuongNV += nv.TongTien;
        //    }

        //    // Get Vat Lieu Nhap
        //    VatLieuNhapRepository vatLieuNhap = new VatLieuNhapRepository();
        //    _LstVatLieuNhap = vatLieuNhap.GetListVatLieuNhapByThang(thang);

        //    MockDonGiaNhapHangRepository donGia = new MockDonGiaNhapHangRepository();
        //    List<DonGiaNhapHangModel> lstDonGia = donGia.GetListByThang(thang);
        //    foreach (var dg in lstDonGia)
        //    {
        //        TongTienNhap += dg.TongTien;
        //    }

        //    MockHoaDonRepository hoaDon = new MockHoaDonRepository();
        //    _LstHoaDon = hoaDon.GetListByThang(thang);
        //    foreach (var hd in LstHoaDon)
        //        TongTienHD += hd.TongTien;
        //}

        //public ICommand ShowCommand { get; }

        //private void Show()
        //{
        //    if (op)
        //    {
        //        GetByThang();
        //    }
        //    else
        //        GetByCustom();
        //}
    }
}
