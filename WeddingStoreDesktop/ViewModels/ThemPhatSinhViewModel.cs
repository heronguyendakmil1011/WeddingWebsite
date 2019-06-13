using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingStoreDesktop.Interfaces.Dialog;
using System.Windows.Input;
using System.Windows;
using WeddingStoreDesktop.Models.SystemModel;
using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Models.DesktopModel;
using WeddingStoreDesktop.Services.DesktopService;
using WeddingStoreDesktop.Functions;
using WeddingStoreDesktop.Services.SystemService;

namespace WeddingStoreDesktop.ViewModels
{
    public class ThemPhatSinhViewModel : BaseViewModel, IDialogRequestClose
    {
        #region Properties
        public string maHD;
        HoaDon _myHoaDon = new HoaDon();
        private List<ThongTinPhatSinhModel> _LstPhatSinh { get; set; }
        public List<ThongTinPhatSinhModel> LstPhatSinh
        {
            get => _LstPhatSinh;
            set
            {
                _LstPhatSinh = value;
                OnPropertyChanged();
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
                GetData();
            }
        }

        private List<KhoVatLieuAoModel> _lstVatLieuAo = new List<KhoVatLieuAoModel>();
        #endregion

        #region Constructors
        public ThemPhatSinhViewModel(string maHD)
        {
            this.maHD = maHD;
            SelectedOption = "All";
            using (WeddingStoreDBEntities DB = new WeddingStoreDBEntities())
            {
                _myHoaDon = DB.HoaDons.FirstOrDefault(hd => hd.MaHD == maHD);
            }

            _LstPhatSinh = thongTinPSService.GetThongTinPhatSinh(maHD);
            GetData();
            GetDanhSachVatLieu();

            AddCommand = new ActionCommand(p => AddPhatSinh());
            ModifyCommand = new ActionCommand(p => ModifySoLuong());
            DoneCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true)));
            CancelCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false)));
        }
        #endregion

        #region Services
        Check check = new Check();
        VatLieuService serviceVL = new VatLieuService();
        private ThongTinPhatSinhService thongTinPSService = new ThongTinPhatSinhService();
        #endregion

        #region Commands
        public ICommand AddCommand { get; }
        public ICommand ModifyCommand { get; }
        public ICommand DoneCommand { get; }
        public ICommand CancelCommand { get; }
        #endregion

        #region Methods
        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        void GetData()
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

        void GetDanhSachVatLieu()
        {
            _lstVatLieuAo = serviceVL.GetVatLieuCan(_myHoaDon.NgayTrangTri, _myHoaDon.NgayThaoDo, _myHoaDon.MaHD);

            //if (_LstPhatSinh != null || _LstPhatSinh.Count != 0)
            //{
            //    foreach (var ps in _LstPhatSinh)
            //    {
            //        foreach (var vl in _lstVatLieuAo)
            //        {
            //            if (!ps.IsNhap)
            //            {
            //                if (ps.MaVL == vl.MaVL)
            //                {
            //                    vl.SoLuong -= ps.SoLuong;
            //                    break;
            //                }
            //            }
            //        }
            //    }
            //}
        }

        private void AddPhatSinh()
        {
            if (_SelectedVatLieu == null)
            {
                MessageBox.Show("Chọn vật liệu pleaseeeeeee!!!!");
            }
            else
            {
                int maxSoLuong = check.CheckVatLieuAo(_lstVatLieuAo, _SelectedVatLieu.MaVL);

                var viewModel = new ModifySoLuongViewModel(_SelectedVatLieu, maxSoLuong);
                Shared.myViewModelForModify = viewModel;
                Shared.requestModify = 4; // hiển thị thông tin vật liệu và số lượng = 1

                bool? result = Shared.dialogService.ShowDialog(viewModel);
                if (result.HasValue)
                {
                    if (result.Value)
                    {
                        if (!viewModel.IsRequired || viewModel.soLuong <= maxSoLuong)
                        {
                            if (_LstPhatSinh != null)
                            {
                                // kiểm tra vật liệu có trong danh sách phát sinh chưa
                                ThongTinPhatSinhModel myTTPhatSinh = _LstPhatSinh.FirstOrDefault(ps => ps.MaVL == _SelectedVatLieu.MaVL);
                                if (myTTPhatSinh != null) //đã có
                                {
                                    myTTPhatSinh.SoLuong += viewModel.soLuong;
                                    myTTPhatSinh.ThanhTien += (_SelectedVatLieu.GiaTien * viewModel.soLuong);
                                }
                                else
                                {
                                    LstPhatSinh.Add(new ThongTinPhatSinhModel
                                    {
                                        AnhMoTa = _SelectedVatLieu.AnhMoTa,
                                        TenVL = _SelectedVatLieu.TenVL,
                                        MaHD = maHD,
                                        MaVL = _SelectedVatLieu.MaVL,
                                        SoLuong = viewModel.soLuong,
                                        ThanhTien = _SelectedVatLieu.GiaTien * viewModel.soLuong
                                    });
                                }
                            }
                            else
                            {
                                LstPhatSinh.Add(new ThongTinPhatSinhModel
                                {
                                    AnhMoTa = _SelectedVatLieu.AnhMoTa,
                                    TenVL = _SelectedVatLieu.TenVL,
                                    MaHD = maHD,
                                    MaVL = _SelectedVatLieu.MaVL,
                                    SoLuong = viewModel.soLuong,
                                    ThanhTien = _SelectedVatLieu.GiaTien * viewModel.soLuong
                                });
                            }

                            // Update số lượng tồn trong danh sách vật liệu ảo
                            if (!_SelectedVatLieu.IsNhap.Value)
                                _lstVatLieuAo = serviceVL.UpdateVatLieuAoWithVatLieu(_lstVatLieuAo, 0, _SelectedVatLieu.MaVL, viewModel.soLuong.Value);
                        }
                        else
                        {
                            MessageBox.Show("Chỉnh sửa thất bại. Số lượng tối đa: " + maxSoLuong, "Thất bại!", MessageBoxButton.OK);
                        }
                    }
                }
            }
        }
        private void ModifySoLuong()
        {
            if (_SelectedVatLieu != null)
            {
                int maxSoLuong = check.CheckVatLieuAo(_lstVatLieuAo, _SelectedPhatSinh.MaVL);

                var viewModel = new ModifySoLuongViewModel(_SelectedPhatSinh, maxSoLuong);
                Shared.myViewModelForModify = viewModel;
                Shared.requestModify = 2; // hiển thị thông tin vật liệu và số lượng trong chi tiết

                bool? result = Shared.dialogService.ShowDialog(viewModel);
                if (result.HasValue)
                {
                    if (result.Value)
                    {
                        if (viewModel.soLuong == 0) // Xóa phát sinh
                        {
                            // Update số lượng tồn trong danh sách vật liệu ảo
                            if (!_SelectedPhatSinh.IsNhap)
                                _lstVatLieuAo = serviceVL.UpdateVatLieuAoWithVatLieu(_lstVatLieuAo, _SelectedPhatSinh.SoLuong.Value, _SelectedPhatSinh.MaVL, viewModel.soLuong.Value);

                            LstPhatSinh.Remove(_SelectedPhatSinh);
                        }
                        else // Update số lượng mới
                        {
                            if (!viewModel.IsRequired || viewModel.soLuong <= (maxSoLuong + _SelectedPhatSinh.SoLuong))
                            {
                                // Update số lượng tồn trong danh sách vật liệu ảo
                                if (!_SelectedPhatSinh.IsNhap)
                                    _lstVatLieuAo = serviceVL.UpdateVatLieuAoWithVatLieu(_lstVatLieuAo, _SelectedPhatSinh.SoLuong.Value, _SelectedPhatSinh.MaVL, viewModel.soLuong.Value);

                                SelectedPhatSinh.ThanhTien = (_SelectedPhatSinh.ThanhTien / _SelectedPhatSinh.SoLuong) * viewModel.soLuong;
                                SelectedPhatSinh.SoLuong = viewModel.soLuong;
                            }
                            else
                            {
                                MessageBox.Show("Chỉnh sửa thất bại. Số lượng tối đa: " + (maxSoLuong + _SelectedPhatSinh.SoLuong), "Thất bại!", MessageBoxButton.OK);
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}
