using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeddingStoreDesktop.Models;
using System.Windows.Input;
using WeddingStoreDesktop.Interfaces.Dialog;
using Microsoft.Win32;
using WeddingStoreDesktop.Functions;
using WeddingStoreDesktop.Models.SystemModel;
using WeddingStoreDesktop.Models.DesktopModel;
using WeddingStoreDesktop.Services.DesktopService;
using System.Drawing;
using System.IO;

namespace WeddingStoreDesktop.ViewModels
{
    public class ThemSanPhamViewModel : BaseViewModel, IDialogRequestClose
    {
        #region Properties
        private SanPham _mySanPham { get; set; }
        public SanPham mySanPham
        {
            get => _mySanPham;
            set
            {
                if (_mySanPham != value)
                {
                    _mySanPham = value;
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

        private DichVu _SelectedDV { get; set; }
        public DichVu SelectedDV
        {
            get => _SelectedDV;
            set
            {
                if (_SelectedDV != value)
                {
                    _SelectedDV = value;
                    OnPropertyChanged();
                    mySanPham.DichVu = _SelectedDV.MaDV;
                }
            }
        }

        private string _myKeyWord { get; set; }
        public string myKeyWord
        {
            get => _myKeyWord;
            set
            {
                _myKeyWord = value;
                OnPropertyChanged();
                SearchVatLieu();
            }
        }
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
        private List<KhoVatLieu> _firstList;
        private List<KhoVatLieu> _LstVatLieu { get; set; }
        public List<KhoVatLieu> LstVatLieu
        {
            get => _LstVatLieu;
            set
            {
                if (_LstVatLieu != value)
                {
                    _LstVatLieu = value;
                    OnPropertyChanged();
                }
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

        // Danh sách chi tiết sản phẩm tạm thời khi đang hoạt động
        private List<ThongTinVatLieuChiTietMauModel> _LstChiTiet { get; set; }
        public List<ThongTinVatLieuChiTietMauModel> LstChiTiet
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

        private ThongTinVatLieuChiTietMauModel _SelectedChiTiet { get; set; }
        public ThongTinVatLieuChiTietMauModel SelectedChiTiet
        {
            get => _SelectedChiTiet;
            set
            {
                if (_SelectedChiTiet != value)
                {
                    _SelectedChiTiet = value;
                    OnPropertyChanged();
                }
            }
        }

        private float _GiaVon { get; set; }
        public float GiaVon
        {
            get => _GiaVon;
            set
            {
                if (_GiaVon != value)
                {
                    _GiaVon = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Functions
        Check check = new Check();
        #endregion

        #region Constructors
        public ThemSanPhamViewModel(SanPham sp)
        {
            GetData();

            if (sp == null)
            {
                _mySanPham = new SanPham
                {
                    MaSP = "SP-" + GetMaxID.GetMaxIdSP(),
                    TenSP = "",
                    DichVu = "",
                    HinhMoTa = null,
                    GiaTien = 0
                };
                _LstChiTiet = new List<ThongTinVatLieuChiTietMauModel>();
                Title = "Thêm sản phẩm mới";
                _GiaVon = 0;
            }
            else
            {
                _mySanPham = sp;
                ThongTinVatLieuChiTietMauService thongTinVLChiTietMau = new ThongTinVatLieuChiTietMauService();
                _LstChiTiet = thongTinVLChiTietMau.GetByIDSanPham(sp.MaSP);
                _SelectedDV = _LstDichVu.FirstOrDefault(dv => dv.MaDV == _mySanPham.DichVu);
                Title = "Chỉnh sửa thông tin sản phẩm";
                foreach (var ct in _LstChiTiet)
                {
                    _GiaVon += ct.ThanhTien.Value;
                }
            }

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

        void GetData()
        {
            _LstVatLieu = DataProvider.Ins.DB.KhoVatLieux.ToList();
            _LstDichVu = DataProvider.Ins.DB.DichVus.ToList();
            SelectedOption = "All";
        }
        void GetVatLieu()
        {
            switch (_SelectedOption)
            {
                case "All":
                    _firstList = DataProvider.Ins.DB.KhoVatLieux.ToList();
                    break;
                case "Hàng nhập":
                    _firstList = DataProvider.Ins.DB.KhoVatLieux.Where(vl => vl.IsNhap == true).ToList();
                    break;
                case "Hàng có sẵn":
                    _firstList = DataProvider.Ins.DB.KhoVatLieux.Where(vl => vl.IsNhap == false).ToList();
                    break;
            }
            SearchVatLieu();
        }
        void SearchVatLieu()
        {
            if (String.IsNullOrEmpty(_myKeyWord))
            {
                LstVatLieu = _firstList;
            }
            else
            {
                LstVatLieu = _firstList.Where(vl => vl.TenVL.ToLower().Contains(_myKeyWord.ToLower())).ToList();
            }
        }

        public void TestDrag(string maVL)
        {
            KhoVatLieu myVL = DataProvider.Ins.DB.KhoVatLieux.FirstOrDefault(vl => vl.MaVL == maVL);
            var viewModel = new ModifySoLuongViewModel(myVL);
            Shared.myViewModelForModify = viewModel;
            Shared.requestModify = 4; // Hiển thị thông tin vật liệu và số lượng = 1

            bool? result = Shared.dialogService.ShowDialog(viewModel);
            if (result.HasValue)
            {
                if (result.Value && viewModel.soLuong.HasValue)
                {
                    // Check vật liệu đã tồn tại hay chưa
                    if (LstChiTiet.Count > 0)
                    {
                        bool isExist = false;
                        foreach (var vl in LstChiTiet)
                        {
                            if (vl.MaVL == myVL.MaVL)
                            {
                                vl.SoLuong += viewModel.soLuong;
                                vl.ThanhTien += (myVL.GiaTien * viewModel.soLuong);
                                isExist = true;
                                break;
                            }
                        }
                        if (!isExist)
                        {
                            LstChiTiet.Add(new ThongTinVatLieuChiTietMauModel
                            {
                                MaVL = myVL.MaVL,
                                TenVL = myVL.TenVL,
                                SoLuong = viewModel.soLuong,
                                DonVi = myVL.DonVi,
                                ThanhTien = viewModel.soLuong * myVL.GiaTien
                            });
                        }
                    }
                    else
                    {
                        LstChiTiet.Add(new ThongTinVatLieuChiTietMauModel
                        {
                            MaVL = myVL.MaVL,
                            TenVL = myVL.TenVL,
                            SoLuong = viewModel.soLuong,
                            DonVi = myVL.DonVi,
                            ThanhTien = viewModel.soLuong * myVL.GiaTien
                        });
                    }
                    GiaVon += myVL.GiaTien.Value * viewModel.soLuong.Value;
                }
            }
        }

        public void vatLieuChiTietMauDoubleClick()
        {
            if (_SelectedChiTiet != null)
            {
                var viewModel = new ModifySoLuongViewModel(SelectedChiTiet);
                Shared.myViewModelForModify = viewModel;
                Shared.requestModify = 5; // Hiển thị thông tin vật liệu và số lượng hiện có

                bool? result = Shared.dialogService.ShowDialog(viewModel);
                if (result.HasValue)
                {
                    if (result.Value)
                    {
                        if (viewModel.soLuong == 0)
                        {
                            LstChiTiet.Remove(SelectedChiTiet);
                        }
                        else
                        {
                            foreach (var ct in LstChiTiet)
                            {
                                if (ct.MaVL == SelectedChiTiet.MaVL)
                                {
                                    GiaVon -= ct.ThanhTien.Value;
                                    ct.ThanhTien = (ct.ThanhTien / ct.SoLuong) * viewModel.soLuong;
                                    GiaVon += ct.ThanhTien.Value;
                                    ct.SoLuong = viewModel.soLuong;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {

                    }
                }
            }
        }

        public void ChooseAvata()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Chọn ảnh";
            openFileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                                    "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                                    "Portable Network Graphic (*.png)|*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                Image ahihi = null;
                ahihi = Image.FromFile(openFileDialog.FileName);
                using (MemoryStream mStream = new MemoryStream())
                {
                    ahihi.Save(mStream, ahihi.RawFormat);
                    mySanPham.HinhMoTa = mStream.ToArray();
                }
                OnPropertyChanged(nameof(mySanPham));
                //imgPhoto.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                //mySanPham.HinhMoTa = openFileDialog.FileName;
            }
        }
        #endregion
    }
}
