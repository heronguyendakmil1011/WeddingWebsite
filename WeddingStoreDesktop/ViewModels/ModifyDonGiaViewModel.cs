using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeddingStoreDesktop.Interfaces.Dialog;
using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Functions;
using System.Windows.Input;
using WeddingStoreDesktop.Models.SystemModel;
using WeddingStoreDesktop.Models.DesktopModel;
using WeddingStoreDesktop.Services.DesktopService;

namespace WeddingStoreDesktop.ViewModels
{
    public class ModifyDonGiaViewModel : BaseViewModel, IDialogRequestClose
    {
        #region Properties
        private DonGiaNhapHang _myDG { get; set; }
        public DonGiaNhapHang myDG
        {
            get => _myDG;
            set
            {
                if (_myDG != value)
                {
                    _myDG = value;
                    OnPropertyChanged();
                }
            }
        }

        private NhaCungCap _myNCC { get; set; }
        public NhaCungCap myNCC
        {
            get => _myNCC;
            set
            {
                if (_myNCC != value)
                {
                    _myNCC = value;
                    OnPropertyChanged();
                }
            }
        }

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
        private List<KhoVatLieu> _firstList;
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

        private List<VatLieuNhapModel> _LstChiTietDG { get; set; }
        public List<VatLieuNhapModel> LstChiTietDG
        {
            get => _LstChiTietDG;
            set
            {
                if (_LstChiTietDG != value)
                {
                    _LstChiTietDG = value;
                    OnPropertyChanged();
                }
            }
        }

        private VatLieuNhapModel _SelectedChiTietDG { get; set; }
        public VatLieuNhapModel SelectedChiTietDG
        {
            get => _SelectedChiTietDG;
            set
            {
                if (_SelectedChiTietDG != value)
                {
                    _SelectedChiTietDG = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Constructors
        public ModifyDonGiaViewModel(DonGiaNhapHang dg, NhaCungCap ncc)
        {
            SelectedOption = "All";
            if (dg != null)
            {
                _myDG = dg;
                Title = "Chỉnh sửa hóa đơn nhập";
                GetVatLieuNhap();
            }
            else
            {
                _myDG = new DonGiaNhapHang
                {
                    MaDG = "DG-" + GetMaxID.GetMaxIdDG(),
                    NgayLap = DateTime.Now,
                    TongTien = 0,
                    MaNCC = ncc.MaNCC
                };
                _LstChiTietDG = new List<VatLieuNhapModel>();
            }
            _myNCC = ncc;
            GetVatLieu();

            SaveCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true)));
            CancelCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false)));
        }
        #endregion

        #region Commands
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        #endregion

        #region Methods
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

        void GetVatLieuNhap()
        {
            VatLieuNhapService vatLieuNhap = new VatLieuNhapService();
            _LstChiTietDG = vatLieuNhap.GetVatLieuNhapByIdDonGia(_myDG.MaDG);
        }

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        public void AddVatLieu(string maVL)
        {
            KhoVatLieu myVL = DataProvider.Ins.DB.KhoVatLieux.FirstOrDefault(vl => vl.MaVL == maVL);
            var viewModel = new ModifyChiTietDonGiaViewModel(myVL);
            Shared.myViewModelForModify = viewModel;

            bool? result = Shared.dialogService.ShowDialog(viewModel);
            if (result.HasValue)
            {
                if (result.Value)
                {
                    // kiểm tra vật liệu đã tồn tịa hay chưa
                    if (_LstChiTietDG.Count == 0)
                    {
                        LstChiTietDG.Add(new VatLieuNhapModel
                        {
                            MaVL = myVL.MaVL,
                            TenVL = myVL.TenVL,
                            SoLuong = viewModel.SoLuong,
                            ThanhTien = viewModel.ThanhTien,
                            AnhMoTa = myVL.AnhMoTa
                        });
                    }
                    else
                    {
                        bool isExist = false;
                        foreach (var ct in _LstChiTietDG)
                        {
                            if (ct.MaVL == myVL.MaVL)
                            {
                                isExist = true;
                                ct.SoLuong += viewModel.SoLuong;
                                ct.ThanhTien += viewModel.ThanhTien;
                                break;
                            }
                        }
                        if (!isExist)
                        {
                            _LstChiTietDG.Add(new VatLieuNhapModel
                            {
                                MaVL = myVL.MaVL,
                                TenVL = myVL.TenVL,
                                SoLuong = viewModel.SoLuong,
                                ThanhTien = viewModel.ThanhTien,
                                AnhMoTa = myVL.AnhMoTa
                            });
                        }
                    }
                    myDG.TongTien += viewModel.ThanhTien;
                    OnPropertyChanged("myDG");
                }
            }
        }

        public void ModifyChiTietDG()
        {
            var viewModel = new ModifyChiTietDonGiaViewModel(_SelectedChiTietDG);
            Shared.myViewModelForModify = viewModel;

            bool? result = Shared.dialogService.ShowDialog(viewModel);
            if (result.HasValue)
            {
                if (result.Value)
                {
                    if (viewModel.SoLuong == 0)
                    {
                        LstChiTietDG.Remove(_SelectedChiTietDG);
                        myDG.TongTien -= _SelectedChiTietDG.ThanhTien;
                    }
                    else
                    {
                        foreach (var ct in _LstChiTietDG)
                        {
                            if (ct.MaVL == _SelectedChiTietDG.MaVL)
                            {
                                myDG.TongTien -= _SelectedChiTietDG.ThanhTien;
                                ct.SoLuong = viewModel.SoLuong;
                                ct.ThanhTien = viewModel.ThanhTien;
                                myDG.TongTien += ct.ThanhTien.Value;
                            }
                        }
                    }
                    OnPropertyChanged("myDG");
                }
            }
        }
        #endregion
    }
}
