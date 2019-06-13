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
using WeddingStoreDesktop.Services.DesktopService;
using WeddingStoreDesktop.Models.SystemModel;
using WeddingStoreDesktop.Services.SystemService;

namespace WeddingStoreDesktop.ViewModels
{
    public class ucPhatSinhViewModel : BaseViewModel
    {
        #region Properties
        private HoaDon _myHoaDon = new HoaDon();
        private List<ThongTinPhatSinhModel> _LstPhatSinh { get; set; }
        public List<ThongTinPhatSinhModel> LstPhatSinh
        {
            get => _LstPhatSinh;
            set
            {
                if (_LstPhatSinh != value)
                {
                    _LstPhatSinh = value;
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

        private List<KhoVatLieuAoModel> _lstVatLieuAo { get; set; }
        #endregion

        #region Services
        Check check = new Check();
        VatLieuService serviceVL = new VatLieuService();
        private ThongTinPhatSinhService thongTinPSService;
        #endregion

        #region Constructors
        public ucPhatSinhViewModel(string maHD)
        {
            Shared.IsChangedAo = false;
            _myHoaDon = DataProvider.Ins.DB.HoaDons.FirstOrDefault(hd => hd.MaHD == maHD);
            GetData();
            GetDanhSachVatLieu();

            RefreshCommand = new ActionCommand(p => Refresh());
            ModifyCommand = new ActionCommand(p => ModifySoLuong());
            AddCommand = new ActionCommand(p => AddPhatSinh());
            DeleteCommand = new ActionCommand(p => DeletePhatSinh());
            ChangedKhoAoCommand = new ActionCommand(p => ChangedKhoAo());
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand { get; }
        public ICommand ModifyCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ChangedKhoAoCommand { get; }
        #endregion

        #region Methods
        void GetData()
        {
            thongTinPSService = new ThongTinPhatSinhService();
            _LstPhatSinh = thongTinPSService.GetThongTinPhatSinh(_myHoaDon.MaHD);
        }
        void GetDanhSachVatLieu()
        {
            _lstVatLieuAo = new List<KhoVatLieuAoModel>();
            _lstVatLieuAo = serviceVL.GetVatLieuCan(_myHoaDon.NgayTrangTri, _myHoaDon.NgayThaoDo, _myHoaDon.MaHD);
        }
        void ChangedKhoAo()
        {
            if (Shared.IsChangedAo)
            {
                Shared.IsChangedAo = false;
                GetDanhSachVatLieu();
            }
        }
        private void ModifySoLuong()
        {
            if (SelectedPhatSinh == null)
            {
                MessageBox.Show("Chọn vật liệu pleaseeeeeee!!!!");
            }
            else
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
                        Shared.IsChangedAo = true;

                        PhatSinh myPhatSinh = DataProvider.Ins.DB.PhatSinhs.FirstOrDefault(ps => ps.MaHD == _myHoaDon.MaHD && ps.MaVL == _SelectedPhatSinh.MaVL);
                        HoaDon myHoaDon = DataProvider.Ins.DB.HoaDons.Find(_myHoaDon.MaHD);
                        myHoaDon.TongTien -= myPhatSinh.ThanhTien;
                        if (viewModel.soLuong == 0)
                        {
                            DataProvider.Ins.DB.PhatSinhs.Remove(myPhatSinh);
                            DataProvider.Ins.DB.SaveChanges();

                            if (myHoaDon.TinhTrang == 1 && viewModel.IsRequired)
                            {
                                KhoVatLieu myVatLieu = DataProvider.Ins.DB.KhoVatLieux.FirstOrDefault(vl => vl.MaVL == _SelectedPhatSinh.MaVL);
                                myVatLieu.SoLuongTon += _SelectedPhatSinh.SoLuong;
                                DataProvider.Ins.DB.SaveChanges();
                            }
                        }
                        else
                        {
                            if (!viewModel.IsRequired || viewModel.soLuong <= (maxSoLuong + _SelectedPhatSinh.SoLuong))
                            {
                                KhoVatLieu myVl = DataProvider.Ins.DB.KhoVatLieux.FirstOrDefault(vl => vl.MaVL == _SelectedPhatSinh.MaVL);
                                if (_myHoaDon.TinhTrang == 1 && viewModel.IsRequired)
                                {
                                    myVl.SoLuongTon = myVl.SoLuongTon + _SelectedPhatSinh.SoLuong - viewModel.soLuong;
                                }
                                myPhatSinh.ThanhTien = myVl.GiaTien * viewModel.soLuong;
                                myPhatSinh.SoLuong = viewModel.soLuong;

                                myHoaDon.TongTien += myPhatSinh.ThanhTien;
                                DataProvider.Ins.DB.SaveChanges();
                            }
                            else
                            {
                                MessageBox.Show("Chỉnh sửa số lượng thất bại. Số lượng tối đa: " + (maxSoLuong + _SelectedPhatSinh.SoLuong, "Thất bại!", MessageBoxButton.OK));
                            }
                        }
                    }
                    else
                    {
                        DataProvider.Ins.RefreshDB();
                    }
                    LstPhatSinh = thongTinPSService.GetThongTinPhatSinh(_myHoaDon.MaHD);
                    GetDanhSachVatLieu();
                }
            }
        }
        void Refresh()
        {
            DataProvider.Ins.RefreshDB();
            GetData();
            GetDanhSachVatLieu();
            OnPropertyChanged(nameof(LstPhatSinh));
        }
        void AddPhatSinh()
        {
            var viewModel = new ThemPhatSinhViewModel(_myHoaDon.MaHD);
            Shared.myViewModelForAdd = viewModel;

            bool? result = Shared.dialogService.ShowDialog(viewModel);
            if (result.HasValue)
            {
                if (result.Value)
                {
                    Shared.IsChangedAo = true;
                    HoaDon myHoaDon = DataProvider.Ins.DB.HoaDons.Find(_myHoaDon.MaHD);
                    // Xóa các phát sinh hiện có trong hóa đơn
                    List<PhatSinh> lstPhatSinh = DataProvider.Ins.DB.PhatSinhs.Where(ps => ps.MaHD == _myHoaDon.MaHD).ToList();
                    if (lstPhatSinh != null)
                    {
                        // Xóa các phát sinh hiện có trong hóa đơn
                        foreach (var ps in lstPhatSinh)
                        {
                            myHoaDon.TongTien -= ps.ThanhTien;
                            DataProvider.Ins.DB.PhatSinhs.Remove(ps);
                            DataProvider.Ins.DB.SaveChanges();
                        }

                        if (_myHoaDon.TinhTrang == 1) // Đã trang trí
                        {
                            // Update số lượng tồn trong kho thật
                            foreach (var ps in lstPhatSinh)
                            {


                                KhoVatLieu myVL = DataProvider.Ins.DB.KhoVatLieux.FirstOrDefault(vl => vl.MaVL == ps.MaVL);
                                if (!myVL.IsNhap.Value)
                                {
                                    myVL.SoLuongTon += ps.SoLuong;
                                    DataProvider.Ins.DB.SaveChanges();
                                }

                            }
                        }
                    }

                    // Thêm phát sinh mới
                    foreach (var ps in viewModel.LstPhatSinh)
                    {
                        DataProvider.Ins.DB.PhatSinhs.Add(new PhatSinh
                        {
                            MaHD = _myHoaDon.MaHD,
                            MaVL = ps.MaVL,
                            SoLuong = ps.SoLuong,
                            ThanhTien = ps.ThanhTien
                        });
                        myHoaDon.TongTien += ps.ThanhTien;
                        DataProvider.Ins.DB.SaveChanges();
                    }

                    if (_myHoaDon.TinhTrang == 1)
                    {
                        DataProvider.Ins.RefreshDB();
                        foreach (var ps in viewModel.LstPhatSinh)
                        {
                            if (!ps.IsNhap)
                            {
                                KhoVatLieu myVL = DataProvider.Ins.DB.KhoVatLieux.FirstOrDefault(vl => vl.MaVL == ps.MaVL);
                                myVL.SoLuongTon -= ps.SoLuong;
                                DataProvider.Ins.DB.SaveChanges();
                            }
                        }
                    }

                }
                LstPhatSinh = thongTinPSService.GetThongTinPhatSinh(_myHoaDon.MaHD);
                GetDanhSachVatLieu();
            }
        }

        void DeletePhatSinh()
        {
            if (_SelectedPhatSinh != null)
            {
                var result = MessageBox.Show("Xóa phát sinh " + _SelectedPhatSinh.TenVL + " ?", "Xóa phát sinh?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Shared.IsChangedAo = true;
                    HoaDon myHoaDon = DataProvider.Ins.DB.HoaDons.Find(_myHoaDon.MaHD);
                    PhatSinh myPhatSinh = DataProvider.Ins.DB.PhatSinhs.FirstOrDefault(ps => ps.MaHD == _myHoaDon.MaHD && ps.MaVL == _SelectedPhatSinh.MaVL);
                    myHoaDon.TongTien -= myPhatSinh.ThanhTien;
                    DataProvider.Ins.DB.PhatSinhs.Remove(myPhatSinh);
                    DataProvider.Ins.DB.SaveChanges();

                    if (_myHoaDon.TinhTrang == 1)
                        UpdateKhoVatLieu(0);

                    LstPhatSinh = thongTinPSService.GetThongTinPhatSinh(_myHoaDon.MaHD);
                    SelectedPhatSinh = null;
                    GetDanhSachVatLieu();
                }
            }
        }

        void UpdateKhoVatLieu(int soLuongMoi)
        {
            KhoVatLieu myVL = DataProvider.Ins.DB.KhoVatLieux.FirstOrDefault(vl => vl.MaVL == _SelectedPhatSinh.MaVL);
            if (!myVL.IsNhap.Value)
            {
                if (soLuongMoi == 0)
                {
                    myVL.SoLuongTon += _SelectedPhatSinh.SoLuong;
                }
                else
                {
                    myVL.SoLuongTon = myVL.SoLuongTon + _SelectedPhatSinh.SoLuong - soLuongMoi;
                }
                DataProvider.Ins.DB.SaveChanges();
            }
            //GetDanhSachVatLieu();
        }
        #endregion
    }
}
