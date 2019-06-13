using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input;
using WeddingStoreDesktop.Interfaces.Dialog;
using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Models.DesktopModel;
using WeddingStoreDesktop.Models.SystemModel;

namespace WeddingStoreDesktop.ViewModels
{
    public class ModifySoLuongViewModel : BaseViewModel, IDialogRequestClose
    {
        #region Properties
        private ThongTinMauModel _thongTinMau { get; set; }
        public ThongTinMauModel thongTinMau

        {
            get => _thongTinMau;
            set
            {
                if (_thongTinMau != value)
                {
                    _thongTinMau = value;
                    OnPropertyChanged();

                }
            }
        }

        private ThongTinPhatSinhModel _thongTinPhatSinh { get; set; }
        public ThongTinPhatSinhModel thongTinPhatSinh
        {
            get => _thongTinPhatSinh;
            set
            {
                if (_thongTinPhatSinh != value)
                {
                    _thongTinPhatSinh = value;
                    OnPropertyChanged();
                }
            }
        }

        public SanPham sanPham = new SanPham();
        public KhoVatLieu vatLieu = new KhoVatLieu();
        public ThongTinVatLieuChiTietMauModel vlChiTietMau = new ThongTinVatLieuChiTietMauModel();

        private int? _soLuong { get; set; }
        public int? soLuong
        {
            get => _soLuong;
            set
            {
                if (_soLuong != value)
                {
                    _soLuong = value;
                    OnPropertyChanged();
                    updateThanhTien();
                }
            }
        }

        private int _MaxSoLuong { get; set; }
        public int MaxSoLuong
        {
            get => _MaxSoLuong;
            set
            {
                _MaxSoLuong = value;
                OnPropertyChanged();
            }
        }
        private float _ThanhTien { get; set; }
        public float ThanhTien
        {
            get => _ThanhTien;
            set
            {
                _ThanhTien = value;
                OnPropertyChanged();
            }
        }

        private int _type;
        private bool _IsRequired { get; set; }
        public bool IsRequired
        {
            get => _IsRequired;
            set
            {
                _IsRequired = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructors
        // update số lượng sản phẩm trong chi tiết hóa đơn
        public ModifySoLuongViewModel(ThongTinMauModel mau, int maxSoLuong)
        {
            if (CheckAllNhap(mau.MaSP))
                IsRequired = false;
            else
                IsRequired = true;
            _type = 1;
            _thongTinMau = mau;
            _soLuong = _thongTinMau.SoLuong.Value;
            updateThanhTien();

            _MaxSoLuong = maxSoLuong + _thongTinMau.SoLuong.Value;

            SaveCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true)));
            CancelCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false)));
        }

        // update số lượng vật liệu phát sinh trong chi tiết hóa đơn
        public ModifySoLuongViewModel(ThongTinPhatSinhModel phatSinh, int maxSoLuong)
        {
            _type = 2;
            _thongTinPhatSinh = phatSinh;
            if (_thongTinPhatSinh.IsNhap)
                IsRequired = false;
            else
                IsRequired = true;
            _soLuong = _thongTinPhatSinh.SoLuong.Value;
            updateThanhTien();

            _MaxSoLuong = maxSoLuong + _thongTinPhatSinh.SoLuong.Value;

            SaveCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true)));
            CancelCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false)));
        }

        // Thêm sản phẩm vào hóa đơn với số lượng
        public ModifySoLuongViewModel(SanPham sanPham, int maxSoLuong)
        {
            if (CheckAllNhap(sanPham.MaSP))
                IsRequired = false;
            else
                IsRequired = true;
            _type = 3;
            _soLuong = 1;
            this.sanPham = sanPham;
            updateThanhTien();

            _MaxSoLuong = maxSoLuong;

            SaveCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true)));
            CancelCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false)));
        }

        // Thêm phát sinh vào hóa đơn với số lượng
        public ModifySoLuongViewModel(KhoVatLieu vatLieu, int maxSoLuong)
        {
            if (vatLieu.IsNhap.Value)
                IsRequired = false;
            else
                IsRequired = true;
            _type = 4;
            this.vatLieu = vatLieu;
            _soLuong = 1;
            updateThanhTien();

            _MaxSoLuong = maxSoLuong;

            SaveCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true)));
            CancelCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false)));
        }

        // Thêm phát sinh vào hóa đơn với số lượng
        public ModifySoLuongViewModel(KhoVatLieu vatLieu)
        {
            IsRequired = false;
            _type = 4;
            this.vatLieu = vatLieu;
            _soLuong = 1;
            updateThanhTien();

            SaveCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true)));
            CancelCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false)));
        }

        // Update số lượng vật liệu trong chi tiết mẫu
        public ModifySoLuongViewModel(ThongTinVatLieuChiTietMauModel vatLieuCTMau)
        {
            IsRequired = false;
            _type = 5;
            vlChiTietMau = vatLieuCTMau;
            _soLuong = vlChiTietMau.SoLuong.Value;
            updateThanhTien();

            SaveCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true)));
            CancelCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false)));
        }
        #endregion

        #region Commands
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        #endregion

        #region Methods
        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        void updateThanhTien()
        {
            if (_soLuong.HasValue)
            {
                switch (_type)
                {
                    case 1: // update số lượng sản phẩm trong chi tiết hóa đơn
                        SanPham mySP = DataProvider.Ins.DB.SanPhams.FirstOrDefault(sp => sp.MaSP == _thongTinMau.MaSP);
                        ThanhTien = mySP.GiaTien.Value * _soLuong.Value;
                        break;
                    case 2: // update số lượng vật liệu phát sinh trong chi tiết hóa đơn
                        KhoVatLieu myVL = DataProvider.Ins.DB.KhoVatLieux.FirstOrDefault(vl => vl.MaVL == _thongTinPhatSinh.MaVL);
                        ThanhTien = myVL.GiaTien.Value * _soLuong.Value;
                        break;
                    case 3: // Thêm sản phẩm vào hóa đơn với số lượng
                        ThanhTien = sanPham.GiaTien.Value * _soLuong.Value;
                        break;
                    case 4: // Thêm phát sinh vào hóa đơn với số lượng
                        ThanhTien = vatLieu.GiaTien.Value * _soLuong.Value;
                        break;
                    case 5: // Update số lượng vật liệu trong chi tiết mẫu
                        KhoVatLieu vlChitiet = DataProvider.Ins.DB.KhoVatLieux.FirstOrDefault(vl => vl.MaVL == vlChiTietMau.MaVL);
                        ThanhTien = vlChitiet.GiaTien.Value * _soLuong.Value;
                        break;
                }
            }
        }

        bool CheckAllNhap(string maSP)
        {
            List<ChiTietSanPham> lstChiTiet = DataProvider.Ins.DB.ChiTietSanPhams.Where(ct => ct.MaSP == maSP).ToList();
            foreach (var ct in lstChiTiet)
            {
                if (!ct.KhoVatLieu.IsNhap.Value)
                    return false;
            }
            return true;
        }
        #endregion
    }
}
