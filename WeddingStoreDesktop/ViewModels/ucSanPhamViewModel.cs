using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WeddingStoreDesktop.Functions;
using WeddingStoreDesktop.Interfaces.Dialog;
using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Models.DesktopModel;
using WeddingStoreDesktop.Models.SystemModel;
using WeddingStoreDesktop.Services;
using WeddingStoreDesktop.Services.DesktopService;
using WeddingStoreDesktop.Services.SystemService;

namespace WeddingStoreDesktop.ViewModels
{
    public class ucSanPhamViewModel : BaseViewModel
    {
        #region Properties
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

        private List<SanPham> _myLstSanPham { get; set; }

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

        private List<LoaiDichVu> _LstLoaiDichVu { get; set; }
        public List<LoaiDichVu> LstLoaiDichVu
        {
            get => _LstLoaiDichVu;
            set
            {
                _LstLoaiDichVu = value;
                OnPropertyChanged();
            }
        }

        public List<DichVu> _LstDichVu { get; set; }
        public List<DichVu> LstDichVu
        {
            get => _LstDichVu;
            set
            {
                if (_LstDichVu != value)
                {
                    _LstDichVu = value;
                    OnPropertyChanged();
                }
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
                    LstDichVu = DataProvider.Ins.DB.DichVus.Where(dv => dv.LoaiDV == SelectedLDV.MaLoaiDV).ToList();
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

        // lưu text search
        private string _keyWord { get; set; }
        public string keyWord
        {
            get => _keyWord;
            set
            {
                if (_keyWord != value)
                {
                    _keyWord = value;
                    OnPropertyChanged();
                    SearchSanPham();
                }
            }
        }
        #endregion

        #region Constructors
        public ucSanPhamViewModel()
        {
            GetData();

            AddCommand = new ActionCommand(p => Add());
            ModifyCommand = new ActionCommand(p => Modify());
            DeleteCommand = new ActionCommand(p => Delete());
            RefreshCommand = new ActionCommand(p => Refresh());
            DanhSachCommand = new ActionCommand(p => DanhSach());
        }
        #endregion

        #region Commands
        public ICommand AddCommand { get; }
        public ICommand ModifyCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand DanhSachCommand { get; }
        #endregion

        #region Methods

        void GetData()
        {
            LstLoaiDichVu = DataProvider.Ins.DB.LoaiDichVus.ToList();
            LstDichVu = DataProvider.Ins.DB.DichVus.ToList();
            SelectedLDV = null;
            SelectedDV = null;
            keyWord = "";
            GetSanPham();
        }

        void GetSanPham()
        {
            if (_selectedLDV == null)
            {
                if (_selectedDV == null)
                    LstSanPham = DataProvider.Ins.DB.SanPhams.ToList();
                else
                    LstSanPham = DataProvider.Ins.DB.SanPhams.Where(sp => sp.DichVu == _selectedDV.MaDV).ToList();
            }
            else
            {
                SanPhamService sanPhamService = new SanPhamService();
                if (_selectedDV == null)
                {
                    if (_keyWord == null || _keyWord == "")
                        LstSanPham = sanPhamService.GetByIDLoaiDV(_selectedLDV.MaLoaiDV);
                    else
                        LstSanPham = sanPhamService.GetByIDLoaiDV(_selectedLDV.MaLoaiDV)
                                            .Where(sp => sp.TenSP.ToLower().Contains(_keyWord.ToLower())).ToList();
                }
                else
                {
                    if (_keyWord == null || _keyWord == "")
                        LstSanPham = sanPhamService.GetByIDDichVu(_selectedDV.MaDV);
                    else
                        LstSanPham = sanPhamService.GetByIDDichVu(_selectedDV.MaDV)
                                            .Where(sp => sp.TenSP.ToLower().Contains(_keyWord.ToLower())).ToList();
                }
            }
            _myLstSanPham = _LstSanPham;

            //SanPhamByDichVuRepository sanPhamByDichVu = new SanPhamByDichVuRepository();
            //_LstSanPhamByDV = sanPhamByDichVu.GetAll();
        }

        void SearchSanPham()
        {
            if (_myLstSanPham == null || _myLstSanPham.Count == 0)
                GetSanPham();

            if (_keyWord == null || _keyWord.Equals(""))
            {
                LstSanPham = _myLstSanPham;
            }
            else
            {
                LstSanPham = _myLstSanPham.Where(sp => sp.TenSP.ToLower().Contains(_keyWord.ToLower())).ToList();
            }
        }
        private void Add()
        {
            var viewModel = new ThemSanPhamViewModel(null);
            Shared.myViewModelForAdd = viewModel;

            bool? result = Shared.dialogService.ShowDialog(viewModel);
            if (result.HasValue)
            {
                if (result.Value)
                {
                    // Thêm thông tin sản phẩm
                    DataProvider.Ins.DB.SanPhams.Add(viewModel.mySanPham);
                    DataProvider.Ins.DB.SaveChanges();

                    //Thêm chi tiết sản phẩm
                    foreach (var ct in viewModel.LstChiTiet)
                    {
                        ChiTietSanPham myChiTiet = new ChiTietSanPham
                        {
                            MaSP = viewModel.mySanPham.MaSP,
                            MaVL = ct.MaVL,
                            SoLuong = ct.SoLuong,
                            GiaTien = ct.ThanhTien
                        };
                        DataProvider.Ins.DB.ChiTietSanPhams.Add(myChiTiet);
                        DataProvider.Ins.DB.SaveChanges();
                    }

                    LstSanPham = DataProvider.Ins.DB.SanPhams.ToList();

                    SelectedSanPham = viewModel.mySanPham;
                }
                else
                {
                    // thông báo thêm không thành công
                }
            }
        }

        private void Modify()
        {
            if (_SelectedSanPham != null)
            {
                var viewModel = new ThemSanPhamViewModel(_SelectedSanPham);
                Shared.myViewModelForAdd = viewModel;

                bool? result = Shared.dialogService.ShowDialog(viewModel);
                if (result.HasValue)
                {
                    if (result.Value)
                    {
                        // Update thông tin sản phẩm
                        var myUpdate = DataProvider.Ins.DB.SanPhams.FirstOrDefault(sp => sp.MaSP == _SelectedSanPham.MaSP);
                        if (myUpdate != null)
                        {
                            myUpdate.TenSP = viewModel.mySanPham.TenSP;
                            myUpdate.HinhMoTa = viewModel.mySanPham.HinhMoTa;
                            myUpdate.GiaTien = viewModel.mySanPham.GiaTien;

                            DataProvider.Ins.DB.SaveChanges();
                        }

                        // Xóa các chi tiết hiện có của sản phẩm
                        List<ChiTietSanPham> myDelete = DataProvider.Ins.DB.ChiTietSanPhams.Where(ct => ct.MaSP == _SelectedSanPham.MaSP).ToList();
                        // Đã có chi tiết sản phẩm
                        if (myDelete != null)
                        {
                            foreach (var ct in myDelete)
                            {
                                DataProvider.Ins.DB.ChiTietSanPhams.Remove(ct);
                                DataProvider.Ins.DB.SaveChanges();
                            }
                        }

                        // Thêm chi tiết sản phẩm mới
                        foreach (var ct in viewModel.LstChiTiet)
                        {
                            ChiTietSanPham myChiTiet = new ChiTietSanPham
                            {
                                MaSP = viewModel.mySanPham.MaSP,
                                MaVL = ct.MaVL,
                                SoLuong = ct.SoLuong,
                                GiaTien = ct.ThanhTien
                            };
                            DataProvider.Ins.DB.ChiTietSanPhams.Add(myChiTiet);
                            DataProvider.Ins.DB.SaveChanges();
                        }

                        LstSanPham = DataProvider.Ins.DB.SanPhams.ToList();
                    }
                    else
                    {
                        Refresh();
                    }
                }
            }
        }

        private void Delete()
        {
            var result = MessageBox.Show("Bạn có muốn xóa mẫu " + _SelectedSanPham.TenSP + " không?", "Thông báo!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                SanPhamService sanPhamService = new SanPhamService();
                sanPhamService.DeleteSanPham(_SelectedSanPham);

                LstSanPham = DataProvider.Ins.DB.SanPhams.ToList();
            }
        }

        private void Refresh()
        {
            DataProvider.Ins.RefreshDB();
            GetData();
        }
        private void DanhSach()
        {
            var viewModel = new ThemDichVuViewModel();
            Shared.myViewModelForAdd = viewModel;

            bool? result = Shared.dialogService.ShowDialog(viewModel);
            if (result.HasValue)
            {
                if (result.Value)
                {
                    GetData();
                }
            }
        }
        #endregion
    }
}
