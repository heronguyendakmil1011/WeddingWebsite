using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Models.DesktopModel;
using WeddingStoreDesktop.Models.SystemModel;
using WeddingStoreDesktop.Services.DesktopService;
using WeddingStoreDesktop.Interfaces.Dialog;
using System.Windows.Input;
using System.Windows;

namespace WeddingStoreDesktop.ViewModels
{
    public class ThemPhanCongNhanVienViewModel : BaseViewModel, IDialogRequestClose
    {
        #region Properties
        private HoaDonKhachHangModel _hdKH;
        public KhachHang myKH { get; set; }
        public HoaDon myHD { get; set; }

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        private List<NhanVien> _LstNhanVien { get; set; }
        public List<NhanVien> LstNhanVien
        {
            get => _LstNhanVien;
            set
            {
                _LstNhanVien = value;
                OnPropertyChanged();
            }
        }

        private List<PhanCongNhanVienModel> _LstPhanCongNgayTrangTri { get; set; }
        public List<PhanCongNhanVienModel> LstPhanCongNgayTrangTri
        {
            get => _LstPhanCongNgayTrangTri;
            set
            {
                if (_LstPhanCongNgayTrangTri != value)
                {
                    _LstPhanCongNgayTrangTri = value;
                    OnPropertyChanged();
                }
            }
        }

        private PhanCongNhanVienModel _SelectedPhanCongTrangTri { get; set; }
        public PhanCongNhanVienModel SelectedPhanCongTrangTri
        {
            get => _SelectedPhanCongTrangTri;
            set
            {
                if (_SelectedPhanCongTrangTri != value)
                {
                    _SelectedPhanCongTrangTri = value;
                    OnPropertyChanged();
                }
            }
        }

        private List<PhanCongNhanVienModel> _LstPhanCongNgayThaoDo { get; set; }
        public List<PhanCongNhanVienModel> LstPhanCongNgayThaoDo
        {
            get => _LstPhanCongNgayThaoDo;
            set
            {
                if (_LstPhanCongNgayThaoDo != value)
                {
                    _LstPhanCongNgayThaoDo = value;
                    OnPropertyChanged();
                }
            }
        }

        private PhanCongNhanVienModel _SelectedPhanCongThaoDo { get; set; }
        public PhanCongNhanVienModel SelectedPhanCongThaoDo
        {
            get => _SelectedPhanCongThaoDo;
            set
            {
                if (_SelectedPhanCongThaoDo != value)
                {
                    _SelectedPhanCongThaoDo = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Services
        #endregion

        #region Constructors
        public ThemPhanCongNhanVienViewModel(HoaDonKhachHangModel hdKH)
        {
            _hdKH = hdKH;

            myHD = DataProvider.Ins.DB.HoaDons.FirstOrDefault(hd => hd.MaHD == _hdKH.MaHD);
            myKH = DataProvider.Ins.DB.KhachHangs.FirstOrDefault(kh => kh.MaKH == myHD.MaKH);

            GetNhanVien();
            GetPhanCong();

            SaveCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true)));
            CancelCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false)));
        }
        #endregion

        #region Commands
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        #endregion

        #region Methods
        void GetPhanCong()
        {
            NhanVienPhanCongService nhanVienPhanCong = new NhanVienPhanCongService();
            LstPhanCongNgayTrangTri = nhanVienPhanCong.GetPhanCongByNgayTrangTri(_hdKH.MaHD, _hdKH.NgayTrangTri);
            LstPhanCongNgayThaoDo = nhanVienPhanCong.GetPhanCongByNgayThaoDo(_hdKH.MaHD, _hdKH.NgayThaoDo);
        }

        void GetNhanVien()
        {
            LstNhanVien = DataProvider.Ins.DB.NhanViens.ToList();
        }

        public void ThemNhanVienThamGiaTrangTri(string maNV)
        {
            PhanCongNhanVienModel myPC = _LstPhanCongNgayTrangTri.FirstOrDefault(pc => pc.MaNV == maNV);
            if (myPC == null)
            {
                NhanVien myNV = DataProvider.Ins.DB.NhanViens.FirstOrDefault(nv => nv.MaNV == maNV);
                LstPhanCongNgayTrangTri.Add(new PhanCongNhanVienModel
                {
                    Avatar = myNV.Avatar,
                    MaNV = myNV.MaNV,
                    TenNV = myNV.TenNV,
                    MaHD = myHD.MaHD,
                    Ngay = myHD.NgayTrangTri.Value.ToString(),
                    ThoiGianDen = "",
                    ThoiGianDi = ""
                });
            }
        }

        public void ThemNhanVienThamGiaThaoDo(string maNV)
        {
            PhanCongNhanVienModel myPC = _LstPhanCongNgayThaoDo.FirstOrDefault(pc => pc.MaNV == maNV);
            if (myPC == null)
            {
                NhanVien myNV = DataProvider.Ins.DB.NhanViens.FirstOrDefault(nv => nv.MaNV == maNV);
                LstPhanCongNgayThaoDo.Add(new PhanCongNhanVienModel
                {
                    Avatar = myNV.Avatar,
                    MaNV = myNV.MaNV,
                    TenNV = myNV.TenNV,
                    MaHD = myHD.MaHD,
                    Ngay = myHD.NgayThaoDo.Value.ToString(),
                    ThoiGianDen = "",
                    ThoiGianDi = ""
                });
            }
        }

        public void XoaPhanCongTrangTri()
        {
            if (!String.IsNullOrEmpty(_SelectedPhanCongTrangTri.ThoiGianDen) || !String.IsNullOrEmpty(_SelectedPhanCongTrangTri.ThoiGianDi)
                            || TimeSpan.Parse(_SelectedPhanCongTrangTri.ThoiGianDen) != TimeSpan.Zero
                            || TimeSpan.Parse(_SelectedPhanCongTrangTri.ThoiGianDi) != TimeSpan.Zero)
            {
                var result = MessageBox.Show("Nhân viên đã được CheckIn hoặc CheckOut. Bạn có muốn xóa nhân viên khỏi ngày trang trí?"
                                            , "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (result == MessageBoxResult.Yes)
                    LstPhanCongNgayTrangTri.Remove(_SelectedPhanCongTrangTri);
            }
            else
                LstPhanCongNgayTrangTri.Remove(_SelectedPhanCongTrangTri);
        }
        public void XoaPhanCongThaoDo()
        {
            if (!String.IsNullOrEmpty(_SelectedPhanCongThaoDo.ThoiGianDen) || !String.IsNullOrEmpty(_SelectedPhanCongThaoDo.ThoiGianDi)
                                    || TimeSpan.Parse(_SelectedPhanCongThaoDo.ThoiGianDen) != TimeSpan.Zero
                                    || TimeSpan.Parse(_SelectedPhanCongThaoDo.ThoiGianDi) != TimeSpan.Zero)
            {
                var result = MessageBox.Show("Nhân viên đã được CheckIn hoặc CheckOut. Bạn có muốn xóa nhân viên khỏi ngày trang trí?"
                                            , "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (result == MessageBoxResult.Yes)
                    LstPhanCongNgayThaoDo.Remove(_SelectedPhanCongThaoDo);
            }
            else
                LstPhanCongNgayThaoDo.Remove(_SelectedPhanCongThaoDo);
        }

        #endregion
    }
}
