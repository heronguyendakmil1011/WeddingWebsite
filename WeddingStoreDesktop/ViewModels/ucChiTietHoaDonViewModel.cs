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
using System.Windows.Data;

namespace WeddingStoreDesktop.ViewModels
{
    public class ucChiTietHoaDonViewModel : BaseViewModel
    {
        #region Properties
        private HoaDon _myHoaDon = new HoaDon();

        private List<ThongTinMauModel> _LstChiTiet { get; set; }
        public List<ThongTinMauModel> LstChiTiet
        {
            get => _LstChiTiet;
            set
            {
                if (_LstChiTiet != value)
                {
                    _LstChiTiet = value;
                    OnPropertyChanged("LstChiTiet");
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

        private List<KhoVatLieuAoModel> _lstVatLieuAo = new List<KhoVatLieuAoModel>();
        #endregion

        #region Services
        private ThongTinMauService thongTinMauService;
        private VatLieuService serviceVL = new VatLieuService();
        Check check = new Check();
        #endregion

        #region Constructors
        public ucChiTietHoaDonViewModel(string maHD)
        {
            Shared.IsChangedAo = false;
            using (WeddingStoreDBEntities DB = new WeddingStoreDBEntities())
            {
                _myHoaDon = DB.HoaDons.FirstOrDefault(hd => hd.MaHD == maHD);
            }
            GetData();
            GetDanhSachVatLieu();

            RefreshCommand = new ActionCommand(p => Refresh());
            ModifyCommand = new ActionCommand(p => ModifySoLuong());
            AddCommand = new ActionCommand(p => AddSanPham());
            DeleteCommand = new ActionCommand(p => DeleteSanPham());
            ChangedKhoAoCommand = new ActionCommand(p => ChangedKhoAo());
        }
        #endregion

        #region Commands
        public ICommand ModifyCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand ChangedKhoAoCommand { get; }
        #endregion

        #region Methods
        void GetData()
        {
            thongTinMauService = new ThongTinMauService();
            _LstChiTiet = thongTinMauService.GetChiTietByIdHD(_myHoaDon.MaHD);
        }
        void ChangedKhoAo()
        {
            if (Shared.IsChangedAo)
            {
                Shared.IsChangedAo = false;
                GetDanhSachVatLieu();
            }
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
        void Refresh()
        {
            DataProvider.Ins.RefreshDB();
            GetData();
            GetDanhSachVatLieu();
            OnPropertyChanged(nameof(LstChiTiet));
        }
        private void ModifySoLuong()
        {
            if (SelectedMau == null)
            {
                MessageBox.Show("Chọn sản phẩm cần chỉnh sửa pleaseeeeeee!");
            }
            else
            {
                int maxSoLuong = check.CheckMauAo(_lstVatLieuAo, _SelectedMau.MaSP);

                var viewModel = new ModifySoLuongViewModel(_SelectedMau, maxSoLuong);
                Shared.requestModify = 1;
                Shared.myViewModelForModify = viewModel;

                bool? result = Shared.dialogService.ShowDialog(viewModel);
                if (result.HasValue)
                {
                    if (result.Value && viewModel.soLuong.HasValue)
                    {
                        if (!viewModel.IsRequired || viewModel.soLuong <= (maxSoLuong + _SelectedMau.SoLuong.Value))
                        {
                            Shared.IsChangedAo = true;
                            ChiTietHoaDon myUpdate = DataProvider.Ins.DB.ChiTietHoaDons.FirstOrDefault(ct => ct.MaHD == _myHoaDon.MaHD
                                                                                          && ct.MaSP == _SelectedMau.MaSP);

                            HoaDon myHoaDon = DataProvider.Ins.DB.HoaDons.FirstOrDefault(hd => hd.MaHD == _myHoaDon.MaHD);
                            myHoaDon.TongTien -= myUpdate.ThanhTien; // trừ tiền trong hóa đơn

                            if (viewModel.soLuong == 0) // Xóa
                            {
                                // Cập nhập chi tiết hóa đơn
                                DataProvider.Ins.DB.ChiTietHoaDons.Remove(myUpdate);
                                DataProvider.Ins.DB.SaveChanges();
                            }
                            else // Cập nhập tổng tiền mới của hóa đơn
                            {
                                // Cập nhập chi tiết hóa đơn
                                myUpdate.ThanhTien = (myUpdate.ThanhTien / myUpdate.SoLuong) * viewModel.soLuong;
                                myUpdate.SoLuong = viewModel.soLuong;

                                myHoaDon.TongTien += myUpdate.ThanhTien;
                                DataProvider.Ins.DB.SaveChanges();
                            }

                            // Cập nhập số lượng tồn của vật liệu có trong chi tiết sản phẩm
                            if (_myHoaDon.TinhTrang == 1) // Đã trang trí
                                CapNhapSoLuongTon(viewModel.soLuong.Value);
                            // Cập nhập số lượng tồn trong danh sách vật liệu ảo
                            _lstVatLieuAo = serviceVL.UpdateVatLieuAo(_lstVatLieuAo, _myHoaDon.MaHD, _SelectedMau.MaSP, viewModel.soLuong.Value);
                        }
                        else
                        {
                            MessageBox.Show("Chỉnh sửa số lượng thất bại. Số lượng tối đa: " + maxSoLuong + _SelectedMau.SoLuong.Value, "Thất bại!", MessageBoxButton.OK);
                        }
                    }
                    else
                    {
                        DataProvider.Ins.RefreshDB();
                    }
                    LstChiTiet = thongTinMauService.GetChiTietByIdHD(_myHoaDon.MaHD);
                    GetDanhSachVatLieu();
                }
            }
        }
        private void AddSanPham()
        {
            var viewModel = new ThemChiTietHDViewModel(_myHoaDon.MaHD);
            Shared.myViewModelForAdd = viewModel;

            bool? result = Shared.dialogService.ShowDialog(viewModel);
            if (result.HasValue)
            {
                if (result.Value)
                {
                    Shared.IsChangedAo = true;
                    HoaDon myHoaDon = DataProvider.Ins.DB.HoaDons.FirstOrDefault(hd => hd.MaHD == _myHoaDon.MaHD);
                    // Update Danh sách sản phẩm
                    List<ChiTietHoaDon> myLst = DataProvider.Ins.DB.ChiTietHoaDons.Where(ct => ct.MaHD == _myHoaDon.MaHD).ToList();
                    // Xóa các chi tiết hiện tại
                    foreach (var ct in myLst)
                    {
                        myHoaDon.TongTien -= ct.ThanhTien;
                        DataProvider.Ins.DB.ChiTietHoaDons.Remove(ct);
                        DataProvider.Ins.DB.SaveChanges();
                    }
                    // Thêm các chi tiết mới
                    foreach (var myCt in viewModel.LstChiTiet)
                    {
                        DataProvider.Ins.DB.ChiTietHoaDons.Add(new ChiTietHoaDon
                        {
                            MaHD = _myHoaDon.MaHD,
                            MaSP = myCt.MaSP,
                            SoLuong = myCt.SoLuong,
                            ThanhTien = myCt.ThanhTien
                        });
                        myHoaDon.TongTien += myCt.ThanhTien;
                        DataProvider.Ins.DB.SaveChanges();
                    }

                    LstChiTiet = thongTinMauService.GetChiTietByIdHD(_myHoaDon.MaHD);
                    GetDanhSachVatLieu();
                }
            }
        }
        private void DeleteSanPham()
        {
            if (_SelectedMau != null)
            {
                var result = MessageBox.Show("Xóa mẫu " + _SelectedMau.TenSP + " ?", "Xóa?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Shared.IsChangedAo = true;
                    ChiTietHoaDon myChiTiet = DataProvider.Ins.DB.ChiTietHoaDons.FirstOrDefault(ct => ct.MaHD == _myHoaDon.MaHD && ct.MaSP == _SelectedMau.MaSP);
                    if (_myHoaDon.TinhTrang == 1) // Đã trang trí
                    {
                        // Update số lượng tồn trong trong kho vật liệu thật --> Cộng số lượng tồn
                        foreach (var ct in myChiTiet.SanPham.ChiTietSanPhams)
                        {
                            ct.KhoVatLieu.SoLuongTon += (ct.SoLuong * myChiTiet.SoLuong);
                        }
                    }
                    DataProvider.Ins.DB.ChiTietHoaDons.Remove(myChiTiet);
                    DataProvider.Ins.DB.SaveChanges();

                    HoaDon myHoaDon = DataProvider.Ins.DB.HoaDons.FirstOrDefault(hd => hd.MaHD == _myHoaDon.MaHD);
                    myHoaDon.TongTien -= myChiTiet.ThanhTien;
                    DataProvider.Ins.DB.SaveChanges();

                    LstChiTiet = thongTinMauService.GetChiTietByIdHD(_myHoaDon.MaHD);
                    SelectedMau = null;
                    GetDanhSachVatLieu();
                }
            }
        }
        private void CapNhapSoLuongTon(int soLuongMoi)
        {
            SanPham mySanPham = DataProvider.Ins.DB.SanPhams.FirstOrDefault(sp => sp.MaSP == _SelectedMau.MaSP);
            foreach (var ct in mySanPham.ChiTietSanPhams)
            {
                if (!ct.KhoVatLieu.IsNhap.Value) // Chỉ cập nhập những vật liệu có sẵn
                {
                    if (soLuongMoi == 0)
                        ct.KhoVatLieu.SoLuongTon += (ct.SoLuong * _SelectedMau.SoLuong);
                    else
                        ct.KhoVatLieu.SoLuongTon = ct.KhoVatLieu.SoLuongTon + (ct.SoLuong * _SelectedMau.SoLuong) - (ct.SoLuong * soLuongMoi);
                    DataProvider.Ins.DB.SaveChanges();
                }
            }
        }
        #endregion
    }
}
