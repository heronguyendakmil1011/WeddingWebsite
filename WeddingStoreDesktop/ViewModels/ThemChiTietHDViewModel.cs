using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeddingStoreDesktop.Functions;
using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Interfaces.Dialog;
using System.Windows.Input;
using WeddingStoreDesktop.Models.DesktopModel;
using WeddingStoreDesktop.Models.SystemModel;
using WeddingStoreDesktop.Services.DesktopService;
using WeddingStoreDesktop.Services.SystemService;
using System.Windows;

namespace WeddingStoreDesktop.ViewModels
{
    public class ThemChiTietHDViewModel : BaseViewModel, IDialogRequestClose
    {
        #region Properties
        private HoaDon _myHoaDon = new HoaDon();
        private float? _TongTien { get; set; }
        public float? TongTien
        {
            get => _TongTien;
            set
            {
                _TongTien = value;
                OnPropertyChanged();
            }
        }
        private List<ThongTinMauModel> _LstChiTiet { get; set; }
        public List<ThongTinMauModel> LstChiTiet
        {
            get => _LstChiTiet;
            set
            {
                if (_LstChiTiet != value)
                {
                    _LstChiTiet = value;
                    OnPropertyChanged();
                }
            }
        }
        private ThongTinMauModel _SelectedMau { get; set; }
        public ThongTinMauModel SelectedMau
        {
            get => _SelectedMau;
            set
            {
                if (_SelectedMau != value)
                {
                    _SelectedMau = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _myKeyWord;

        public string mykeyWord
        {
            get { return _myKeyWord; }
            set
            {
                _myKeyWord = value;
                OnPropertyChanged();
                SearchSanPham();
            }
        }
        private List<KhoVatLieuAoModel> _lstVatLieuAo = new List<KhoVatLieuAoModel>();

        private List<SanPham> _myLstSanPham { get; set; }
        private List<SanPham> _LstSanPham { get; set; }
        public List<SanPham> LstSanPham
        {
            get => _LstSanPham;
            set
            {
                if (_LstSanPham != value)
                {
                    _LstSanPham = value;
                    OnPropertyChanged();
                }
            }
        }

        private SanPham _SelectedSanPham { get; set; }
        public SanPham SelectedSanPham
        {
            get => _SelectedSanPham;
            set
            {
                if (_SelectedSanPham != value)
                {
                    _SelectedSanPham = value;
                    OnPropertyChanged();
                }
            }
        }

        private List<DichVu> _LstDichVu { get; set; }
        public List<DichVu> LstDichVu
        {
            get => _LstDichVu;
            set
            {
                _LstDichVu = value;
                OnPropertyChanged();
            }
        }

        private DichVu _SelectedDichVu { get; set; }
        public DichVu SelectedDichVu
        {
            get => _SelectedDichVu;
            set
            {
                _SelectedDichVu = value;
                OnPropertyChanged();
                SearchSanPham();
            }
        }
        #endregion

        #region Services
        Check check = new Check();
        VatLieuService serviceVL = new VatLieuService();
        #endregion

        #region Constructors
        public ThemChiTietHDViewModel(string maHD)
        {
            using (WeddingStoreDBEntities DB = new WeddingStoreDBEntities())
            {
                _myHoaDon = DB.HoaDons.FirstOrDefault(hd => hd.MaHD == maHD);
            }
            GetDichVu();
            SearchSanPham();
            GetDataChiTietHD();
            GetDanhSachVatLieu();

            DoneCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true)));
            CancelCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false)));
        }
        #endregion

        #region Commands
        public ICommand DoneCommand { get; }
        public ICommand CancelCommand { get; }
        #endregion

        #region Methods
        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;
        void GetSanPham()
        {
            if (_SelectedDichVu == null)
                _myLstSanPham = DataProvider.Ins.DB.SanPhams.ToList();
            else
                _myLstSanPham = DataProvider.Ins.DB.SanPhams.Where(sp => sp.DichVu == _SelectedDichVu.MaDV).ToList();
        }
        void GetDataChiTietHD()
        {
            ThongTinMauService thongTinMau = new ThongTinMauService();
            LstChiTiet = thongTinMau.GetChiTietByIdHD(_myHoaDon.MaHD);
            _TongTien = 0;
            foreach (var ct in _LstChiTiet)
            {
                TongTien += ct.ThanhTien.Value;
            }
        }
        void SearchSanPham()
        {
            if (_myLstSanPham == null || _myLstSanPham.Count == 0)
                GetSanPham();
            if (!String.IsNullOrEmpty(_myKeyWord))
            {
                LstSanPham = _myLstSanPham.Where(sp => sp.TenSP.ToLower().Contains(_myKeyWord.ToLower())).ToList();
            }
            else
            {
                LstSanPham = _myLstSanPham;
            }
        }
        void GetDichVu()
        {
            _LstDichVu = DataProvider.Ins.DB.DichVus.ToList();
        }

        void GetDanhSachVatLieu()
        {
            _lstVatLieuAo = serviceVL.GetVatLieuCan(_myHoaDon.NgayTrangTri, _myHoaDon.NgayThaoDo, _myHoaDon.MaHD);
            //foreach (var ct in _LstChiTiet)
            //{
            //    List<ChiTietSanPham> lstChiTietSP = DataProvider.Ins.DB.ChiTietSanPhams.Where(ctsp => ctsp.MaSP == ct.MaSP).ToList();
            //    foreach (var ctsp in lstChiTietSP)
            //    {
            //        if (!ctsp.KhoVatLieu.IsNhap.Value)
            //        {
            //            KhoVatLieuAoModel myVLAo = _lstVatLieuAo.FirstOrDefault(vl => vl.MaVL == ctsp.KhoVatLieu.MaVL);
            //            myVLAo.SoLuong -= (ctsp.SoLuong * ct.SoLuong);
            //        }
            //    }
            //} 
        }
        // Chọn mẫu trong hóa đơn
        public void SanPhamInHD_Click()
        {
            if (_SelectedMau != null)
            {
                int maxSoLuong = check.CheckMauAo(_lstVatLieuAo, _SelectedMau.MaSP);

                var viewModel = new ModifySoLuongViewModel(_SelectedMau, maxSoLuong);
                Shared.myViewModelForModify = viewModel;
                Shared.requestModify = 1; // hiển thị thông tin mẫu trong hóa đơn và số lượng của mẫu

                bool? result = Shared.dialogService.ShowDialog(viewModel);
                if (result.HasValue)
                {
                    if (result.Value)
                    {
                        TongTien -= _SelectedMau.ThanhTien;
                        // Update danh sach san pham
                        if (viewModel.soLuong == 0)
                        {
                            // Cập nhập số lượng tồn trong danh sách vật liệu ảo
                            _lstVatLieuAo = serviceVL.UpdateVatLieuAo(_lstVatLieuAo, _myHoaDon.MaHD, _SelectedMau.MaSP, viewModel.soLuong.Value);
                            // Xóa mẫu khỏi hóa đơn
                            LstChiTiet.Remove(_SelectedMau);
                        }
                        else
                        {
                            if (!viewModel.IsRequired || viewModel.soLuong <= (maxSoLuong + _SelectedMau.SoLuong))
                            {
                                // Cập nhập số lượng tồn trong danh sách vật liệu ảo
                                _lstVatLieuAo = serviceVL.UpdateVatLieuAo(_lstVatLieuAo, _myHoaDon.MaHD, _SelectedMau.MaSP, viewModel.soLuong.Value);
                                // Chỉnh sửa mẫu với số lượng khác
                                ThongTinMauModel myUpdate = _LstChiTiet.FirstOrDefault(ct => ct.MaSP == _SelectedMau.MaSP);
                                myUpdate.ThanhTien = (_SelectedMau.ThanhTien / _SelectedMau.SoLuong) * viewModel.soLuong;
                                myUpdate.SoLuong = viewModel.soLuong;
                                TongTien += myUpdate.ThanhTien;
                            }
                            else
                            {
                                MessageBox.Show("Chỉnh sửa thất bại. Số lượng tối đa: " + (maxSoLuong + _SelectedMau.SoLuong), "Thất bại!", MessageBoxButton.OK);
                            }
                        }
                        GetDanhSachVatLieu();
                    }
                }
            }
        }

        // Chọn sản phẩm trong danh sách sản phẩm
        public void SanPham_Click()
        {
            if(_SelectedSanPham!=null)
            {
            HoaDon myHoaDon = DataProvider.Ins.DB.HoaDons.FirstOrDefault(hd => hd.MaHD == _myHoaDon.MaHD);
            int maxSoLuong = check.CheckMauAo(_lstVatLieuAo, _SelectedSanPham.MaSP);

            var viewModel = new ModifySoLuongViewModel(_SelectedSanPham, maxSoLuong);
            Shared.myViewModelForModify = viewModel;
            Shared.requestModify = 3; // hiển thị thông tin sản phẩm và số lượng = 1

            bool? result = Shared.dialogService.ShowDialog(viewModel);
                if (result.HasValue)
                {
                    if (result.Value)
                    {
                        if (viewModel.soLuong > 0)
                        {
                            if (!viewModel.IsRequired || viewModel.soLuong <= maxSoLuong)
                            {
                                ThemSanPham(viewModel.soLuong.Value);
                                //if (_LstChiTiet != null) // Đã có phần tử trong danh sách chi tiết mẫu
                                //{
                                //    // Kiểm tra sản phẩm có trong chi tiết chưa
                                //    ThongTinMauModel myMau = _LstChiTiet.FirstOrDefault(ct => ct.MaSP == _SelectedSanPham.MaSP);
                                //    if (myMau != null) // Đã có
                                //    {
                                //        myMau.SoLuong += viewModel.soLuong;
                                //        myMau.ThanhTien += viewModel.soLuong * _SelectedSanPham.GiaTien;
                                //    }
                                //    else // Chưa có
                                //    {
                                //        // Update danh sach san pham
                                //        LstChiTiet.Add(new ThongTinMauModel
                                //        {
                                //            MaHD = _myHoaDon.MaHD,
                                //            MaSP = _SelectedSanPham.MaSP,
                                //            HinhMoTa = _SelectedSanPham.HinhMoTa,
                                //            TenSP = _SelectedSanPham.TenSP,
                                //            SoLuong = viewModel.soLuong,
                                //            ThanhTien = _SelectedSanPham.GiaTien * viewModel.soLuong
                                //        });
                                //    }
                                //}
                                //else // Danh sách chi tiết mẫu trống
                                //{
                                //    LstChiTiet = new List<ThongTinMauModel>();
                                //    LstChiTiet.Add(new ThongTinMauModel
                                //    {
                                //        MaHD = _myHoaDon.MaHD,
                                //        MaSP = _SelectedSanPham.MaSP,
                                //        HinhMoTa = _SelectedSanPham.HinhMoTa,
                                //        TenSP = _SelectedSanPham.TenSP,
                                //        SoLuong = viewModel.soLuong,
                                //        ThanhTien = _SelectedSanPham.GiaTien * viewModel.soLuong
                                //    });
                                //}
                                //TongTien += (_SelectedSanPham.GiaTien * viewModel.soLuong);

                                //// Update số lượng tồn trong kho vật liệu ảo
                                //_lstVatLieuAo = serviceVL.UpdateVatLieuAo(_lstVatLieuAo, _myHoaDon.MaHD, _SelectedSanPham.MaSP, viewModel.soLuong.Value);
                            }
                            else
                            {
                                MessageBox.Show("Chỉnh sửa thất bại. Số lượng tối đa: " + maxSoLuong, "Thất bại!", MessageBoxButton.OK);
                            }
                        }
                        GetDanhSachVatLieu();
                    }
                }
            }
        }

        void ThemSanPham(int soLuong)
        {
            if (_LstChiTiet != null) // Đã có phần tử trong danh sách chi tiết mẫu
            {
                // Kiểm tra sản phẩm có trong chi tiết chưa
                ThongTinMauModel myMau = _LstChiTiet.FirstOrDefault(ct => ct.MaSP == _SelectedSanPham.MaSP);
                if (myMau != null) // Đã có
                {
                    myMau.SoLuong += soLuong;
                    myMau.ThanhTien += soLuong * _SelectedSanPham.GiaTien;
                }
                else // Chưa có
                {
                    // Update danh sach san pham
                    LstChiTiet.Add(new ThongTinMauModel
                    {
                        MaHD = _myHoaDon.MaHD,
                        MaSP = _SelectedSanPham.MaSP,
                        HinhMoTa = _SelectedSanPham.HinhMoTa,
                        TenSP = _SelectedSanPham.TenSP,
                        SoLuong = soLuong,
                        ThanhTien = _SelectedSanPham.GiaTien * soLuong
                    });
                }
            }
            else // Danh sách chi tiết mẫu trống
            {
                LstChiTiet = new List<ThongTinMauModel>();
                LstChiTiet.Add(new ThongTinMauModel
                {
                    MaHD = _myHoaDon.MaHD,
                    MaSP = _SelectedSanPham.MaSP,
                    HinhMoTa = _SelectedSanPham.HinhMoTa,
                    TenSP = _SelectedSanPham.TenSP,
                    SoLuong = soLuong,
                    ThanhTien = _SelectedSanPham.GiaTien * soLuong
                });
            }
            TongTien += (_SelectedSanPham.GiaTien * soLuong);

            // Update số lượng tồn trong kho vật liệu ảo
            _lstVatLieuAo = serviceVL.UpdateVatLieuAo(_lstVatLieuAo, _myHoaDon.MaHD, _SelectedSanPham.MaSP, soLuong);
        }
        #endregion
    }
}
