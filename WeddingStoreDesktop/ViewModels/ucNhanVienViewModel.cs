using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Interfaces.Dialog;
using WeddingStoreDesktop.Models.SystemModel;
using System.Windows;
using WeddingStoreDesktop.Models.DesktopModel;
using WeddingStoreDesktop.Services.DesktopService;
using WeddingStoreDesktop.Functions;

namespace WeddingStoreDesktop.ViewModels
{
    public class ucNhanVienViewModel : BaseViewModel
    {
        #region Properties
        private List<NhanVien> _LstNhanVien { get; set; }
        public List<NhanVien> LstNhanVien
        {
            get => _LstNhanVien;
            set
            {
                if (_LstNhanVien != value)
                {
                    _LstNhanVien = value;
                    OnPropertyChanged();
                }
            }
        }

        private NhanVien _SelectedNhanVien { get; set; }
        public NhanVien SelectedNhanVien
        {
            get => _SelectedNhanVien;
            set
            {
                _SelectedNhanVien = value;
                OnPropertyChanged();

                if (_SelectedNhanVien != null)
                {
                    GetChamCong();
                }
            }
        }

        private List<ChamCongModel> _LstChamCong { get; set; }
        public List<ChamCongModel> LstChamCong
        {
            get => _LstChamCong;
            set
            {
                if (_LstChamCong != value)
                {
                    _LstChamCong = value;
                    OnPropertyChanged();
                }
            }
        }

        private float _TongTien { get; set; }
        public float TongTien
        {
            get => _TongTien;
            set
            {
                if (_TongTien != value)
                {
                    _TongTien = value;
                    OnPropertyChanged();
                }
            }
        }

        private List<NhanVien> _defaultLstNhanVien { get; set; }

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
                    if (String.IsNullOrEmpty(_keyWord))
                    {
                        LstNhanVien = _defaultLstNhanVien;
                    }
                    else
                    {
                        LstNhanVien = _defaultLstNhanVien.Where(nv => nv.TenNV.ToLower().Contains(_keyWord.ToLower())).ToList();
                    }
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

        // Chọn tháng
        private string _SelectedThang { get; set; }
        public string SelectedThang
        {
            get => _SelectedThang;
            set
            {
                if (_SelectedThang != value)
                {
                    _SelectedThang = value;
                    OnPropertyChanged();
                    GetChamCong();
                }
            }
        }

        private string _Nam { get; set; }
        public string Nam
        {
            get => _Nam;
            set
            {
                _Nam = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Services
        ChamCongService chamCongService = new ChamCongService();
        #endregion

        #region Constructors
        public ucNhanVienViewModel()
        {
            GetData();
            _SelectedThang = "Tháng " + DateTime.Now.Month;
            _Nam = DateTime.Now.Year.ToString();

            RefreshCommand = new ActionCommand(p =>
            {
                SelectedNhanVien = DataProvider.Ins.DB.NhanViens.FirstOrDefault(nv => nv.MaNV == _SelectedNhanVien.MaNV);
            });
            AddCommand = new ActionCommand(p => Them());
            ModifyThongTinCommand = new ActionCommand(p => ModifyThongTin());
            DeleteCommand = new ActionCommand(p => Delete());
            XemTaiKhoanCommand = new ActionCommand(p => XemTaiKhoan());
            PayCommand = new ActionCommand(p => Pay());
            XemChamCongCommand = new ActionCommand(p => GetChamCong());
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand { get; }
        public ICommand PayCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand ModifyThongTinCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand XemTaiKhoanCommand { get; }
        public ICommand XemChamCongCommand { get; }
        #endregion

        #region Methods
        void GetData()
        {
            _LstNhanVien = DataProvider.Ins.DB.NhanViens.ToList();
            _defaultLstNhanVien = _LstNhanVien;
        }
        void GetChamCong()
        {
            if (int.TryParse(_Nam, out int nam))
            {
                string[] strThang = _SelectedThang.Split(' ');
                LstChamCong = chamCongService.GetChamCongByIdNVVaThangNam(_SelectedNhanVien.MaNV, int.Parse(strThang[1]), nam);
                TongTien = 0;
                NhanVien myNV = DataProvider.Ins.DB.NhanViens.FirstOrDefault(nv => nv.MaNV == _SelectedNhanVien.MaNV);
                foreach (var pc in LstChamCong)
                {
                    if (myNV.Luong.HasValue && !string.IsNullOrEmpty(pc.TongThoiGian))
                        TongTien += Tinh.TinhTongTien(pc.TongThoiGian, myNV.Luong.Value);
                }
            }
            else
                MessageBox.Show("Năm không đúng định dạng.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Hand);

        }
        private void Them()
        {
            var viewModel = new ThemNhanVienViewModel(null);
            Shared.myViewModelForModify = viewModel;

            bool? result = Shared.dialogService.ShowDialog(viewModel);
            if (result.HasValue)
            {
                if (result.Value)
                {
                    DataProvider.Ins.DB.NhanViens.Add(viewModel.myNV);
                    DataProvider.Ins.DB.SaveChanges();

                    LstNhanVien = DataProvider.Ins.DB.NhanViens.ToList();
                }
            }
        }
        private void ModifyThongTin()
        {
            var viewModel = new ThemNhanVienViewModel(_SelectedNhanVien);
            Shared.myViewModelForAdd = viewModel;

            bool? result = Shared.dialogService.ShowDialog(viewModel);
            if (result.HasValue)
            {
                if (result.Value)
                {
                    var myUpdate = DataProvider.Ins.DB.NhanViens.FirstOrDefault(nv => nv.MaNV == _SelectedNhanVien.MaNV);

                    myUpdate.TenNV = viewModel.myNV.TenNV;
                    myUpdate.GioiTinh = viewModel.myNV.GioiTinh;
                    myUpdate.NgaySinh = viewModel.myNV.NgaySinh;
                    myUpdate.DiaChi = viewModel.myNV.DiaChi;
                    myUpdate.SoDT = viewModel.myNV.SoDT;
                    myUpdate.Luong = viewModel.myNV.Luong;
                    myUpdate.Avatar = viewModel.myNV.Avatar;

                    DataProvider.Ins.DB.SaveChanges();
                    SelectedNhanVien = DataProvider.Ins.DB.NhanViens.FirstOrDefault(nv => nv.MaNV == _SelectedNhanVien.MaNV);
                    LstNhanVien = DataProvider.Ins.DB.NhanViens.ToList();
                }
                else
                {
                    DataProvider.Ins.RefreshDB();
                    SelectedNhanVien = DataProvider.Ins.DB.NhanViens.FirstOrDefault(nv => nv.MaNV == _SelectedNhanVien.MaNV);
                    LstNhanVien = DataProvider.Ins.DB.NhanViens.ToList();
                }
            }
        }
        private void Delete()
        {
            var result = MessageBox.Show("Xóa nhân viên", "Xóa nhân viên " + _SelectedNhanVien.TenNV + " ?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                // Xóa phân công nhân viên
                List<PhanCong> lstPhanCong = DataProvider.Ins.DB.PhanCongs.Where(pc => pc.MaNV == _SelectedNhanVien.MaNV).ToList();
                foreach (var pc in lstPhanCong)
                {
                    DataProvider.Ins.DB.PhanCongs.Remove(pc);
                    DataProvider.Ins.DB.SaveChanges();
                }
                // Xóa nhân viên
                DataProvider.Ins.DB.NhanViens.Remove(_SelectedNhanVien);
                DataProvider.Ins.DB.SaveChanges();

                // Lấy dữ liệu mới
                LstNhanVien = DataProvider.Ins.DB.NhanViens.ToList();
                SelectedNhanVien = null;
            }
        }
        void XemTaiKhoan()
        {
            var viewModel = new TaiKhoanViewModel();
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
        void Pay()
        {
            string[] strThang = _SelectedThang.Split(' ');
            var viewModel = new ReportHoaDonViewModel(_SelectedNhanVien.MaNV, int.Parse(strThang[1]), 2019);
            Shared.myViewModelForModify = viewModel;

            bool? result = Shared.dialogService.ShowDialog(viewModel);
            if (result.HasValue)
            {
                if (result.Value)
                {
                }
            }
        }
        #endregion
    }
}
