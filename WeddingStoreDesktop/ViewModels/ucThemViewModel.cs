using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Functions;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows;
using System.ComponentModel;
using System.Windows.Data;
using WeddingStoreDesktop.Interfaces.Dialog;
using WeddingStoreDesktop.Models.SystemModel;
using WeddingStoreDesktop.Models.DesktopModel;
using WeddingStoreDesktop.Services.SystemService;
using WeddingStoreDesktop.Services.DesktopService;

namespace WeddingStoreDesktop.ViewModels
{
    public class ucThemViewModel : BaseViewModel
    {
        #region Properties

        // Sản phẩm
        private List<SanPham> _myLstSanPham { get; set; }
        private List<SanPham> _lstSanPham { get; set; }
        public List<SanPham> LstSanPham
        {
            get => _lstSanPham;
            set
            {
                _lstSanPham = value;
                OnPropertyChanged();
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
        private string _keyWordSanPham { get; set; }
        public string keyWordSanPham
        {
            get => _keyWordSanPham;
            set
            {
                if (_keyWordSanPham != value)
                {
                    _keyWordSanPham = value;
                    OnPropertyChanged();
                    SearchSanPham();
                }
            }
        }

        //private List<SanPham> _firstLstSanPham { get; set; }
        //public List<SanPham> firstLstSanPham
        //{
        //    get => _firstLstSanPham;
        //    set
        //    {
        //        if (_firstLstSanPham != value)
        //        {
        //            _firstLstSanPham = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}

        // Loại dịch Vụ
        private List<LoaiDichVu> _lstLoaiDichVu { get; set; }
        public List<LoaiDichVu> LstLoaiDichVu
        {
            get => _lstLoaiDichVu;
            set
            {
                _lstLoaiDichVu = value;
                OnPropertyChanged();
            }
        }
        private LoaiDichVu _selectedLDV { get; set; }
        public LoaiDichVu SelectedLDV
        {
            get
            {
                return _selectedLDV;

            }
            set
            {
                if (_selectedLDV != value)
                {
                    _selectedLDV = value;
                    OnPropertyChanged();
                    GetSanPham();
                }

                if (_selectedLDV != null)
                    GetDichVu();
            }
        }

        // Dịch vụ
        public List<DichVu> _lstDichVu { get; set; }
        public List<DichVu> LstDichVu
        {
            get => _lstDichVu;
            set
            {
                if (_lstDichVu != value)
                {
                    _lstDichVu = value;
                    OnPropertyChanged();
                }
            }
        }
        private DichVu _selectedDV { get; set; }
        public DichVu SelectedDV
        {
            get => _selectedDV;
            set
            {
                if (_selectedDV != value)
                {
                    _selectedDV = value;
                    OnPropertyChanged();
                    GetSanPham();
                }
            }
        }

        // Vật liệu
        public List<string> LstOption
        {
            get => new List<string> { "All", "Hàng nhập", "Hàng có sẵn" };
        }
        private string _SelectedOption { get; set; }
        public string SelectedOption
        {
            get => _SelectedOption;
            set
            {
                _SelectedOption = value;
                OnPropertyChanged();
                GetVatLieu();
            }
        }
        private KhoVatLieu _SelectedVatLieu { get; set; }
        public KhoVatLieu SelectedVatLieu
        {
            get => _SelectedVatLieu;
            set
            {
                if (_SelectedVatLieu != value)
                {
                    _SelectedVatLieu = value;
                    OnPropertyChanged();
                }
            }
        }
        private List<KhoVatLieu> _myLstVatLieu { get; set; }
        private List<KhoVatLieu> _lstVatLieu { get; set; }
        public List<KhoVatLieu> LstVatLieu
        {
            get => _lstVatLieu;
            set
            {
                _lstVatLieu = value;
                OnPropertyChanged();
            }
        }
        private string _keyWordVatLieu { get; set; }
        public string keyWordVatLieu
        {
            get => _keyWordVatLieu;
            set
            {
                if (_keyWordVatLieu != value)
                {
                    _keyWordVatLieu = value;
                    OnPropertyChanged();
                    SearchVatLieu();
                }
            }
        }

        // Thông tin mẫu
        private List<ThongTinMauModel> _lstThongTinMau { get; set; }
        public List<ThongTinMauModel> LstThongTinMau
        {
            get => _lstThongTinMau;
            set
            {
                if (_lstThongTinMau != value)
                {
                    _lstThongTinMau = value;
                    OnPropertyChanged();
                }
            }
        }

        private float? _TongTienMau { get; set; }
        public float? TongTienMau
        {
            get => _TongTienMau;
            set
            {
                if (_TongTienMau != value)
                {
                    _TongTienMau = value;
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

        // Phát sinh
        private List<ThongTinPhatSinhModel> _lstThongTinPhatSinh { get; set; }
        public List<ThongTinPhatSinhModel> LstThongTinPhatSinh
        {
            get => _lstThongTinPhatSinh;
            set
            {
                if (_lstThongTinPhatSinh != value)
                {
                    _lstThongTinPhatSinh = value;
                    OnPropertyChanged();
                }
            }
        }

        private float? _TongTienPhatSinh { get; set; }
        public float? TongTienPhatSinh
        {
            get => _TongTienPhatSinh;
            set
            {
                if (_TongTienPhatSinh != value)
                {
                    _TongTienPhatSinh = value;
                    OnPropertyChanged();
                }
            }
        }

        private ThongTinPhatSinhModel _SelectedPhatSinh { get; set; }
        public ThongTinPhatSinhModel SelectedPhatSinh
        {
            get => _SelectedPhatSinh;
            set
            {
                if (_SelectedPhatSinh != value)
                {
                    _SelectedPhatSinh = value;
                    OnPropertyChanged();
                }
            }
        }

        // Hóa đơn
        private HoaDon _myHD { get; set; }
        public HoaDon myHD
        {
            get => _myHD;
            set
            {
                _myHD = value;
                OnPropertyChanged();
                isSaveHDKH = false;
            }
        }

        // Khách hàng
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

        // Danh sách vật liệu
        List<KhoVatLieuAoModel> _lstVatLieuAo { get; set; }
        bool isSaveHDKH = false;

        #endregion

        #region services
        Check check = new Check();
        VatLieuService serviceVL = new VatLieuService();
        #endregion

        #region Constructors
        public ucThemViewModel()
        {
            SelectedOption = "All";
            GetData();
            RefreshSanPhamCommand = new ActionCommand(p => RefreshSanPham());
            SaveCommand = new ActionCommand(p => Save());
            RefreshCommand = new ActionCommand(p => Refresh());
            SaveHDKHCommand = new ActionCommand(p => GetDanhSachVatLieu());
        }
        #endregion

        #region Commands
        public ICommand RefreshSanPhamCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand SaveHDKHCommand { get; }
        public ICommand SaveCommand { get; }
        #endregion

        #region Methods

        void RefreshSanPham()
        {
            SelectedLDV = null;
            SelectedDV = null;
            keyWordSanPham = "";
            GetSanPham();
        }

        void GetData()
        {
            GetSanPham();
            GetVatLieu();

            myHD = new HoaDon
            {
                MaHD = "HD-" + GetMaxID.GetMaxIdHD(),
                MaKH = "KH-" + GetMaxID.GetMaxIdKH(),
                NgayLap = DateTime.Now,
                NgayTrangTri = null,
                NgayThaoDo = null,
                TongTien = 0,
                TinhTrang = 0
            };

            myKH = new KhachHang
            {
                MaKH = "KH-" + GetMaxID.GetMaxIdKH(),
                TenKH = "",
                DiaChi = "",
                SoDT = ""
            };

            //_lstVatLieu = DataProvider.Ins.DB.KhoVatLieux.ToList();
            //_firstLstVatLieu = _lstVatLieu;
            LstThongTinMau = new List<ThongTinMauModel>();
            _TongTienMau = 0;
            LstThongTinPhatSinh = new List<ThongTinPhatSinhModel>();
            _TongTienPhatSinh = 0;

            LstLoaiDichVu = DataProvider.Ins.DB.LoaiDichVus.ToList();
            LstDichVu = DataProvider.Ins.DB.DichVus.ToList();
        }

        public void GetSanPham()
        {
            SanPhamService sanPham = new SanPhamService();
            if (_selectedLDV == null)
            {
                if (_selectedDV == null)
                    LstSanPham = DataProvider.Ins.DB.SanPhams.ToList();
                else
                    LstSanPham = DataProvider.Ins.DB.SanPhams.Where(sp => sp.DichVu == _selectedDV.MaDV).ToList();
            }
            else
            {
                if (_selectedDV == null)
                {
                    if (_keyWordSanPham == null || _keyWordSanPham == "")
                        LstSanPham = sanPham.GetByIDLoaiDV(_selectedLDV.MaLoaiDV);
                    else
                        LstSanPham = sanPham.GetByIDLoaiDV(_selectedLDV.MaLoaiDV)
                                            .Where(sp => sp.TenSP.ToLower().Contains(_keyWordSanPham.ToLower())).ToList();
                }
                else
                {
                    if (_keyWordSanPham == null || _keyWordSanPham == "")
                        LstSanPham = sanPham.GetByIDDichVu(_selectedDV.MaDV);
                    else
                        LstSanPham = sanPham.GetByIDDichVu(_selectedDV.MaDV)
                                            .Where(sp => sp.TenSP.ToLower().Contains(_keyWordSanPham.ToLower())).ToList();
                }
            }
            _myLstSanPham = _lstSanPham;
        }

        void GetVatLieu()
        {
            switch (_SelectedOption)
            {
                case "All":
                    _myLstVatLieu = DataProvider.Ins.DB.KhoVatLieux.ToList();
                    break;
                case "Hàng nhập":
                    _myLstVatLieu = DataProvider.Ins.DB.KhoVatLieux.Where(vl => vl.IsNhap == true).ToList();
                    break;
                case "Hàng có sẵn":
                    _myLstVatLieu = DataProvider.Ins.DB.KhoVatLieux.Where(vl => vl.IsNhap == false).ToList();
                    break;
            }
            SearchVatLieu();
        }
        void SearchVatLieu()
        {
            if (String.IsNullOrEmpty(_keyWordVatLieu))
            {
                LstVatLieu = _myLstVatLieu;
            }
            else
            {
                LstVatLieu = _myLstVatLieu.Where(vl => vl.TenVL.ToLower().Contains(_keyWordVatLieu.ToLower())).ToList();
            }
        }

        public void GetDichVu()
        {
            LstDichVu = DataProvider.Ins.DB.DichVus.Where(dv => dv.LoaiDV == SelectedLDV.MaLoaiDV).ToList();
        }

        void SearchSanPham()
        {
            if (_myLstSanPham.Count == 0)
                GetSanPham();

            if (_keyWordSanPham == null || _keyWordSanPham.Equals(""))
            {
                LstSanPham = _myLstSanPham;
            }
            else
            {
                LstSanPham = _myLstSanPham.Where(sp => sp.TenSP.ToLower().Contains(_keyWordSanPham.ToLower())).ToList();
            }
        }
        void GetDanhSachVatLieu()
        {
            if (_myHD.NgayTrangTri.HasValue && _myHD.NgayThaoDo.HasValue && _myHD.NgayLap.HasValue)
            {
                // Làm mới danh sách mẫu và danh sách phát sinh
                LstThongTinMau.Clear();
                LstThongTinPhatSinh.Clear();
                isSaveHDKH = true;
                // Lấy danh sách vật liệu ảo
                _lstVatLieuAo = new List<KhoVatLieuAoModel>();
                _lstVatLieuAo = serviceVL.GetVatLieuCan(_myHD.NgayTrangTri, _myHD.NgayThaoDo, null);
            }
            else
                MessageBox.Show("Mời nhập thông tin!!", "Information!", MessageBoxButton.OK);
        }

        public void SanPhamClick()
        {
            if (_SelectedSanPham != null)
            {
                if (isSaveHDKH)
                {
                    int maxSoLuong = check.CheckMauAo(_lstVatLieuAo, _SelectedSanPham.MaSP);
                    var viewModel = new ModifySoLuongViewModel(_SelectedSanPham, maxSoLuong);
                    Shared.myViewModelForModify = viewModel;
                    Shared.requestModify = 3;

                    bool? result = Shared.dialogService.ShowDialog(viewModel);
                    if (result.HasValue)
                    {
                        if (result.Value)
                        {
                            if (!viewModel.IsRequired) // tất cả vật liệu đều là hàng nhập
                            {
                                ThemSanPham(viewModel.soLuong.Value);
                            }
                            else
                            {
                                if (viewModel.soLuong <= maxSoLuong)
                                {
                                    // Update số lượng tồn trong danh sách vật liệu ảo
                                    foreach (var ct in _SelectedSanPham.ChiTietSanPhams)
                                    {
                                        if (!ct.KhoVatLieu.IsNhap.Value) // Không phải vật liệu nhập
                                        {
                                            foreach (var vl in _lstVatLieuAo)
                                            {
                                                if (ct.KhoVatLieu.MaVL == vl.MaVL)
                                                {
                                                    vl.SoLuong -= (viewModel.soLuong * ct.SoLuong);
                                                    break;
                                                }
                                            }
                                        }
                                    }

                                    ThemSanPham(viewModel.soLuong.Value);
                                }
                                else
                                {
                                    MessageBox.Show("Không thể thêm. Số lượng tối đa: " + maxSoLuong, "Error!", MessageBoxButton.OK);
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Save thông tin trước khi thêm!", "Information!!", MessageBoxButton.OK);
                }
            }
        }

        public void VatLieuClick()
        {
            if (_SelectedVatLieu != null)
            {
                if (isSaveHDKH)
                {
                    int maxSoLuong = check.CheckVatLieuAo(_lstVatLieuAo, _SelectedVatLieu.MaVL);
                    var viewModel = new ModifySoLuongViewModel(_SelectedVatLieu, maxSoLuong);
                    Shared.myViewModelForModify = viewModel;
                    Shared.requestModify = 4;

                    bool? result = Shared.dialogService.ShowDialog(viewModel);
                    if (result.HasValue)
                    {
                        if (result.Value && viewModel.soLuong.HasValue && viewModel.soLuong.Value > 0)
                        {
                            if (!_SelectedVatLieu.IsNhap.Value) // không phải vật liệu nhập
                            {
                                foreach (var vl in _lstVatLieuAo)
                                {
                                    if (vl.MaVL == _SelectedVatLieu.MaVL)
                                    {
                                        // phù hợp
                                        if (maxSoLuong >= viewModel.soLuong)
                                        {
                                            vl.SoLuong -= viewModel.soLuong;
                                            UpdatePhatSinh(viewModel.soLuong.Value);
                                        }
                                        else // không phù hợp
                                        {
                                            maxSoLuong = vl.SoLuong.Value;
                                            MessageBox.Show("Không thể thêm. Số lượng tối đa: " + maxSoLuong, "Error!", MessageBoxButton.OK);
                                        }
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                ThongTinPhatSinhModel myPhatSinh = new ThongTinPhatSinhModel
                                {
                                    AnhMoTa = _SelectedVatLieu.AnhMoTa,
                                    MaHD = _myHD.MaHD,
                                    MaVL = _SelectedVatLieu.MaVL,
                                    TenVL = _SelectedVatLieu.TenVL,
                                    IsNhap = _SelectedVatLieu.IsNhap.Value,
                                    SoLuong = viewModel.soLuong.Value,
                                    ThanhTien = _SelectedVatLieu.GiaTien * viewModel.soLuong.Value
                                };
                                TongTienPhatSinh += myPhatSinh.ThanhTien;
                                LstThongTinPhatSinh.Add(myPhatSinh);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Save thông tin trước khi thêm!", "Information!!", MessageBoxButton.OK);
                }
            }
        }

        public void MauClick()
        {
            if (_SelectedMau != null)
            {
                int maxSoLuong = check.CheckMauAo(_lstVatLieuAo, _SelectedMau.MaSP);

                var viewModel = new ModifySoLuongViewModel(_SelectedMau, maxSoLuong);
                Shared.myViewModelForModify = viewModel;
                Shared.requestModify = 1;

                bool? result = Shared.dialogService.ShowDialog(viewModel);
                if (result.HasValue)
                {
                    if (result.Value)
                    {
                        if (viewModel.soLuong == 0)
                        {
                            if (viewModel.IsRequired)
                                UpdateVatLieuAo(_SelectedMau.MaSP, _SelectedMau.SoLuong.Value, viewModel.soLuong.Value);
                            LstThongTinMau.Remove(_SelectedMau);

                            TongTienMau -= _SelectedMau.ThanhTien;
                        }
                        else
                        {
                            if (viewModel.IsRequired)
                            {
                                if (viewModel.soLuong <= (maxSoLuong + _SelectedMau.SoLuong))
                                {
                                    UpdateVatLieuAo(_SelectedMau.MaSP, _SelectedMau.SoLuong.Value, viewModel.soLuong.Value);

                                    ThongTinMauModel myUpdate = _lstThongTinMau.FirstOrDefault(ct => ct.MaSP == _SelectedMau.MaSP);
                                    float giaTien = _SelectedMau.ThanhTien.Value / _SelectedMau.SoLuong.Value;
                                    TongTienMau = TongTienMau - _SelectedMau.ThanhTien + (giaTien * viewModel.soLuong.Value);
                                    myUpdate.ThanhTien = giaTien * viewModel.soLuong.Value;
                                    myUpdate.SoLuong = viewModel.soLuong.Value;
                                }
                                else
                                {
                                    MessageBox.Show("Chỉnh sửa số lượng thất bại. Số lượng tối đa: " + (maxSoLuong + _SelectedMau.SoLuong), "Thất bại", MessageBoxButton.OK, MessageBoxImage.Hand);
                                }
                            }
                            else
                            {
                                ThongTinMauModel myUpdate = _lstThongTinMau.FirstOrDefault(ct => ct.MaSP == _SelectedMau.MaSP);
                                float giaTien = _SelectedMau.ThanhTien.Value / _SelectedMau.SoLuong.Value;
                                TongTienMau = TongTienMau - _SelectedMau.ThanhTien + (giaTien * viewModel.soLuong.Value);
                                myUpdate.ThanhTien = giaTien * viewModel.soLuong.Value;
                                myUpdate.SoLuong = viewModel.soLuong.Value;
                            }
                        }
                    }
                }
            }
        }

        public void PhatSinhClick()
        {
            if (_SelectedPhatSinh != null)
            {
                int maxSoLuong = check.CheckVatLieuAo(_lstVatLieuAo, _SelectedPhatSinh.MaVL);
                var viewModel = new ModifySoLuongViewModel(_SelectedPhatSinh, maxSoLuong);
                Shared.myViewModelForModify = viewModel;
                Shared.requestModify = 2;

                bool? result = Shared.dialogService.ShowDialog(viewModel);
                if (result.HasValue)
                {
                    if (result.Value)
                    {
                        //KhoVatLieu myVL = DataProvider.Ins.DB.KhoVatLieux.FirstOrDefault(vl => vl.MaVL == _SelectedPhatSinh.MaVL);
                        //foreach (var vl in _lstVatLieuAo)
                        //{
                        //    if (vl.MaVL == _SelectedPhatSinh.MaVL)
                        //    {
                        //        if (viewModel.soLuong <= _SelectedPhatSinh.SoLuong) // nhỏ hơn
                        //        {
                        //            ThongTinPhatSinhModel myPhatSinh = _lstThongTinPhatSinh.FirstOrDefault(ps => ps.MaVL == _SelectedPhatSinh.MaVL);
                        //            if (viewModel.soLuong == 0)
                        //            {
                        //                if (!myVL.IsNhap.Value)
                        //                    // Update số lượng tồn
                        //                    vl.SoLuong += _SelectedPhatSinh.SoLuong.Value;

                        //                LstThongTinPhatSinh.Remove(myPhatSinh);
                        //                TongTienPhatSinh -= myPhatSinh.ThanhTien;
                        //            }
                        //            else
                        //            {
                        //                if (!myVL.IsNhap.Value)
                        //                    // Update số lượng tồn
                        //                    vl.SoLuong += (_SelectedPhatSinh.SoLuong - viewModel.soLuong);

                        //                TongTienPhatSinh -= myPhatSinh.ThanhTien;
                        //                myPhatSinh.ThanhTien = (myPhatSinh.ThanhTien / myPhatSinh.SoLuong) * viewModel.soLuong;
                        //                myPhatSinh.SoLuong = viewModel.soLuong;
                        //                TongTienPhatSinh += myPhatSinh.ThanhTien;
                        //            }
                        //        }
                        //        else // lớn hơn
                        //        {
                        //            if (viewModel.soLuong <= maxSoLuong) // phù hợp
                        //            {
                        //                if (!myVL.IsNhap.Value)
                        //                    // Update số lượng tồn
                        //                    vl.SoLuong = vl.SoLuong - _SelectedPhatSinh.SoLuong + viewModel.soLuong;

                        //                ThongTinPhatSinhModel myPhatSinh = _lstThongTinPhatSinh.FirstOrDefault(ps => ps.MaVL == _SelectedPhatSinh.MaVL);
                        //                TongTienPhatSinh -= myPhatSinh.ThanhTien;
                        //                myPhatSinh.ThanhTien = (myPhatSinh.ThanhTien / myPhatSinh.SoLuong) * viewModel.soLuong;
                        //                myPhatSinh.SoLuong = viewModel.soLuong;
                        //                TongTienPhatSinh += myPhatSinh.ThanhTien;
                        //            }
                        //            else // không phù hợp
                        //            {
                        //                maxSoLuong = vl.SoLuong.Value;
                        //                MessageBox.Show("Không thể chỉnh sửa. Số lượng tối đa: " + maxSoLuong, "Error!", MessageBoxButton.OK);
                        //            }
                        //        }
                        //        break;
                        //    }
                        //}

                        if (viewModel.soLuong == 0)
                        {
                            if (viewModel.IsRequired)
                                UpdateVatLieuAoWithVatLieu(_SelectedPhatSinh.MaVL, _SelectedPhatSinh.SoLuong.Value, viewModel.soLuong.Value);

                            LstThongTinPhatSinh.Remove(_SelectedPhatSinh);
                            TongTienPhatSinh -= _SelectedPhatSinh.ThanhTien;
                        }
                        else
                        {
                            if (!viewModel.IsRequired)
                            {
                                ThongTinPhatSinhModel myUpdate = _lstThongTinPhatSinh.FirstOrDefault(ps => ps.MaVL == _SelectedPhatSinh.MaVL);
                                float giaTien = _SelectedPhatSinh.ThanhTien.Value / _SelectedPhatSinh.SoLuong.Value;
                                TongTienPhatSinh = TongTienPhatSinh - (_SelectedPhatSinh.ThanhTien) + (giaTien * viewModel.soLuong.Value);
                                myUpdate.ThanhTien = giaTien * viewModel.soLuong.Value;
                                myUpdate.SoLuong = viewModel.soLuong.Value;
                            }
                            else
                            {
                                if (viewModel.soLuong <= (maxSoLuong + _SelectedPhatSinh.SoLuong))
                                {
                                    UpdateVatLieuAoWithVatLieu(_SelectedPhatSinh.MaVL, _SelectedPhatSinh.SoLuong.Value, viewModel.soLuong.Value);

                                    ThongTinPhatSinhModel myUpdate = _lstThongTinPhatSinh.FirstOrDefault(ps => ps.MaVL == _SelectedPhatSinh.MaVL);
                                    float giaTien = _SelectedPhatSinh.ThanhTien.Value / _SelectedPhatSinh.SoLuong.Value;
                                    TongTienPhatSinh = TongTienPhatSinh - (_SelectedPhatSinh.ThanhTien) + (giaTien * viewModel.soLuong.Value);
                                    myUpdate.ThanhTien = giaTien * viewModel.soLuong.Value;
                                    myUpdate.SoLuong = viewModel.soLuong.Value;
                                }
                                else
                                {
                                    MessageBox.Show("Chỉnh sửa số lượng thất bại. Số lượng tối đa: " + (maxSoLuong + _SelectedPhatSinh.SoLuong), "Thất bại!", MessageBoxButton.OK, MessageBoxImage.Hand);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Save()
        {
            // Kiểm tra thông tin
            if (String.IsNullOrEmpty(myKH.TenKH) || String.IsNullOrEmpty(myKH.DiaChi) || myHD.TongTien.Value <= 0 || myHD.NgayThaoDo < myHD.NgayTrangTri)
            {
                MessageBox.Show("Thêm dữ liệu thất bại!!", "Error!!!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var result = MessageBox.Show("Bạn muốn thêm hóa đơn mới?", "Thêm mới!!", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.OK)
                {
                    try
                    {
                        //Thêm thông tin khách hàng
                        DataProvider.Ins.DB.KhachHangs.Add(myKH);
                        DataProvider.Ins.DB.SaveChanges();

                        //Thêm thông tin Hóa đơn
                        DataProvider.Ins.DB.HoaDons.Add(myHD);
                        DataProvider.Ins.DB.SaveChanges();

                        // Thêm chi tiết hóa đơn
                        foreach (var ct in _lstThongTinMau)
                        {
                            DataProvider.Ins.DB.ChiTietHoaDons.Add(new ChiTietHoaDon
                            {
                                MaHD = ct.MaHD,
                                MaSP = ct.MaSP,
                                SoLuong = ct.SoLuong,
                                ThanhTien = ct.ThanhTien
                            });
                            DataProvider.Ins.DB.SaveChanges();
                        }

                        // Thêm phát sinh
                        foreach (var ps in _lstThongTinPhatSinh)
                        {
                            DataProvider.Ins.DB.PhatSinhs.Add(new PhatSinh
                            {
                                MaHD = ps.MaHD,
                                MaVL = ps.MaVL,
                                SoLuong = ps.SoLuong,
                                ThanhTien = ps.ThanhTien
                            });
                            DataProvider.Ins.DB.SaveChanges();
                        }
                        MessageBox.Show("Thêm dữ liệu thành công!!", "Success!!!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        Refresh();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Thêm dữ liệu thất bại!!", "Error!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
        private void UpdatePhatSinh(int soLuong)
        {
            if (_lstThongTinPhatSinh != null)
            {
                ThongTinPhatSinhModel myPhatSinh = _lstThongTinPhatSinh.FirstOrDefault(ps => ps.MaVL == _SelectedVatLieu.MaVL);
                if (myPhatSinh != null)
                {
                    TongTienPhatSinh -= myPhatSinh.ThanhTien;
                    myPhatSinh.SoLuong += soLuong;
                    myPhatSinh.ThanhTien += _SelectedVatLieu.GiaTien * soLuong;
                    TongTienPhatSinh += myPhatSinh.ThanhTien;
                }
                else
                {
                    LstThongTinPhatSinh.Add(new ThongTinPhatSinhModel
                    {
                        MaHD = myHD.MaHD,
                        AnhMoTa = _SelectedVatLieu.AnhMoTa,
                        TenVL = _SelectedVatLieu.TenVL,
                        MaVL = _SelectedVatLieu.MaVL,
                        SoLuong = soLuong,
                        ThanhTien = _SelectedVatLieu.GiaTien * soLuong
                    });
                    TongTienPhatSinh += (_SelectedVatLieu.GiaTien * soLuong);
                }
            }
            else
            {
                LstThongTinPhatSinh.Add(new ThongTinPhatSinhModel
                {
                    MaHD = myHD.MaHD,
                    AnhMoTa = _SelectedVatLieu.AnhMoTa,
                    TenVL = _SelectedVatLieu.TenVL,
                    MaVL = _SelectedVatLieu.MaVL,
                    SoLuong = soLuong,
                    ThanhTien = _SelectedVatLieu.GiaTien * soLuong
                });
                TongTienPhatSinh += (_SelectedVatLieu.GiaTien * soLuong);
            }
        }
        private void Refresh()
        {
            isSaveHDKH = false;
            LstThongTinMau.Clear();
            LstThongTinPhatSinh.Clear();
            GetData();
        }

        private void UpdateVatLieuAo(string maSP, int SoLuongHT, int soLuongMoi)
        {
            List<ChiTietSanPham> lstChiTiet = DataProvider.Ins.DB.ChiTietSanPhams.Where(ct => ct.MaSP == _SelectedMau.MaSP).ToList();
            foreach (var ct in lstChiTiet)
            {
                if (!ct.KhoVatLieu.IsNhap.Value)
                {
                    foreach (var vl in _lstVatLieuAo)
                    {
                        if (vl.MaVL == ct.KhoVatLieu.MaVL)
                        {
                            if (soLuongMoi == 0)
                                vl.SoLuong += (SoLuongHT * ct.SoLuong);
                            else
                                vl.SoLuong = vl.SoLuong - (SoLuongHT * ct.SoLuong) + (soLuongMoi * ct.SoLuong);
                        }
                    }
                }
            }
        }
        private void UpdateVatLieuAoWithVatLieu(string maVL, int SoLuongHT, int soLuongMoi)
        {
            foreach (var vl in _lstVatLieuAo)
            {
                if (vl.MaVL == maVL)
                {
                    if (!vl.IsNhap.Value)
                    {
                        if (soLuongMoi == 0)
                            vl.SoLuong += SoLuongHT;
                        else
                            vl.SoLuong = vl.SoLuong - SoLuongHT + soLuongMoi;
                    }
                }
            }
        }
        void ThemSanPham(int soLuong)
        {
            if (_lstThongTinMau != null)
            {
                ThongTinMauModel myMau = _lstThongTinMau.FirstOrDefault(m => m.MaSP == _SelectedSanPham.MaSP);
                if (myMau != null) //Đã tồn tại
                {
                    TongTienMau -= myMau.ThanhTien;
                    myMau.SoLuong += soLuong;
                    myMau.ThanhTien += _SelectedSanPham.GiaTien * soLuong;
                    TongTienMau += myMau.ThanhTien;
                }
                else
                {
                    LstThongTinMau.Add(new ThongTinMauModel
                    {
                        HinhMoTa = _SelectedSanPham.HinhMoTa,
                        MaSP = _SelectedSanPham.MaSP,
                        TenSP = _SelectedSanPham.TenSP,
                        MaHD = myHD.MaHD,
                        SoLuong = soLuong,
                        ThanhTien = _SelectedSanPham.GiaTien * soLuong
                    });
                    TongTienMau += (_SelectedSanPham.GiaTien * soLuong);
                }
            }
            else
            {
                LstThongTinMau.Add(new ThongTinMauModel
                {
                    HinhMoTa = _SelectedSanPham.HinhMoTa,
                    MaSP = _SelectedSanPham.MaSP,
                    TenSP = _SelectedSanPham.TenSP,
                    MaHD = myHD.MaHD,
                    SoLuong = soLuong,
                    ThanhTien = _SelectedSanPham.GiaTien * soLuong
                });
                TongTienMau += (_SelectedSanPham.GiaTien * soLuong);
            }
        }
        #endregion
    }
}
