using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Functions;
using System.Windows.Input;
using WeddingStoreDesktop.Interfaces.Dialog;
using WeddingStoreDesktop.Models.DesktopModel;
using WeddingStoreDesktop.Services.DesktopService;
using WeddingStoreDesktop.Models.SystemModel;
using System.Windows;

namespace WeddingStoreDesktop.ViewModels
{
    public class ucHoaDonViewModel : BaseViewModel
    {
        #region Properties
        private List<HoaDonKhachHangModel> _LstHoaDonKhachHang { get; set; }
        public List<HoaDonKhachHangModel> LstHoaDonKhachHang
        {
            get => _LstHoaDonKhachHang;
            set
            {
                if (_LstHoaDonKhachHang != value)
                {
                    _LstHoaDonKhachHang = value;
                    OnPropertyChanged();
                }
            }
        }

        private HoaDonKhachHangModel _SelectedHDKH { get; set; }
        public HoaDonKhachHangModel SelectedHDKH
        {
            get => _SelectedHDKH;
            set
            {
                if (_SelectedHDKH != value)
                {
                    _SelectedHDKH = value;
                    OnPropertyChanged();
                    if (_SelectedHDKH != null)
                        GetThongTin();
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
        private string _Thang { get; set; }
        public string Thang
        {
            get => _Thang;
            set
            {
                if (_Thang != value)
                {
                    _Thang = value;
                    OnPropertyChanged();
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
        public List<string> myOption
        {
            get => new List<string>
            {
                "Tên khách hàng",
                "Ngày trang trí",
                "Ngày tháo dở"
            };
        }

        private string _SelectedOption { get; set; }
        public string SelectedOption
        {
            get => _SelectedOption;
            set
            {
                if (_SelectedOption != value)
                {
                    _SelectedOption = value;
                    OnPropertyChanged();
                    myKeyWord = null;
                }
            }
        }

        private string _myKeyWord { get; set; }
        public string myKeyWord
        {
            get => _myKeyWord;
            set
            {
                if (_myKeyWord != value)
                {
                    _myKeyWord = value;
                    OnPropertyChanged();
                }
            }
        }

        private HoaDon _myHD { get; set; }
        public HoaDon myHD
        {
            get => _myHD;
            set
            {
                _myHD = value;
                OnPropertyChanged();
            }
        }

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
        #endregion

        #region Services
        private HoaDonKhachHangService hdKHService = new HoaDonKhachHangService();
        #endregion

        #region Constructors
        public ucHoaDonViewModel()
        {
            _SelectedOption = "Tên khách hàng";
            _Nam = DateTime.Now.Year.ToString();
            _Thang = "Tháng " + DateTime.Now.Month.ToString();

            SearchCommand = new ActionCommand(p => GetData());
            ModifyThongTinCommand = new ActionCommand(p => ModifyThongTin());
            ThanhToanCommand = new ActionCommand(p => ThanhToan());
            DeleteCommand = new ActionCommand(p => Delete());
            RefreshCommand = new ActionCommand(p => Refresh());
            ChangeTinhTrangTo1Command = new ActionCommand(p => ChangeTinhTrang1());
            ChangeTinhTrangTo2Command = new ActionCommand(p => ChangeTinhTrang2());
            ChangeTinhTrangTo3Command = new ActionCommand(p => ChangeTinhTrang3());
        }
        #endregion

        #region Commands
        public ICommand SearchCommand { get; }
        public ICommand ModifyThongTinCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ThanhToanCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand ChangeTinhTrangTo1Command { get; }
        public ICommand ChangeTinhTrangTo2Command { get; }
        public ICommand ChangeTinhTrangTo3Command { get; }
        #endregion

        #region Methods
        void GetData()
        {
            // LstHoaDonKhachHang = hdKHService.GetHoaDonKhachHang();
            if (int.TryParse(_Nam, out int nam))
            {
                string[] strThang = _Thang.Split(' ');
                LstHoaDonKhachHang = hdKHService.GetHoaDonKhachHangByThangNam(int.Parse(strThang[1]), nam);

                switch (_SelectedOption)
                {
                    case "Tên khách hàng":
                        if (_myKeyWord != null && _myKeyWord != "")
                        {
                            LstHoaDonKhachHang = LstHoaDonKhachHang.Where(hd => hd.TenKH.ToLower().Contains(_myKeyWord.ToLower())).ToList();
                        }
                        break;
                    case "Ngày trang trí":
                        if (_myKeyWord != null && _myKeyWord != "")
                        {
                            LstHoaDonKhachHang = LstHoaDonKhachHang.Where(hd => hd.NgayTrangTri.Equals(_myKeyWord)).ToList();
                        }
                        break;
                    case "Ngày tháo dở":
                        if (_myKeyWord != null && _myKeyWord != "")
                        {
                            LstHoaDonKhachHang = LstHoaDonKhachHang.Where(hd => hd.NgayThaoDo.Equals(_myKeyWord)).ToList();
                        }
                        break;
                }
            }
            else
            {
                MessageBox.Show("Năm không đúng định dạng.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
        }
        void GetThongTin()
        {
            if (_SelectedHDKH != null)
            {
                myHD = DataProvider.Ins.DB.HoaDons.FirstOrDefault(hd => hd.MaHD == _SelectedHDKH.MaHD);
                myKH = DataProvider.Ins.DB.KhachHangs.FirstOrDefault(kh => kh.MaKH == _SelectedHDKH.MaKH);
            }
        }
        private void ModifyThongTin()
        {
            var viewModel = new ModifyThongTinViewModel(myHD, myKH);
            Shared.myViewModelForModify = viewModel;

            bool? result = Shared.dialogService.ShowDialog(viewModel);
            if (result.HasValue)
            {
                if (result.Value)
                {
                    if (viewModel.myHD.NgayTrangTri < viewModel.myHD.NgayLap || viewModel.myHD.NgayThaoDo < viewModel.myHD.NgayLap
                        || viewModel.myHD.NgayThaoDo < viewModel.myHD.NgayTrangTri)
                    {
                        MessageBox.Show("Chỉnh sửa thông tin thất bại", "Fail!!", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        // Update thông tin hóa đơn
                        HoaDon hoaDon = DataProvider.Ins.DB.HoaDons.FirstOrDefault(hd => hd.MaHD == _myHD.MaHD);
                        //hoaDon.NgayLap = viewModel.myHD.NgayLap;
                        //hoaDon.NgayTrangTri = viewModel.myHD.NgayTrangTri;
                        //hoaDon.NgayThaoDo = viewModel.myHD.NgayThaoDo;
                        //hoaDon.TongTien = viewModel.myHD.TongTien;

                        DataProvider.Ins.DB.SaveChanges();

                        // Kiểm tra ngày trang trí có bị thay đổi không, nếu có update phân công ngày trang trí
                        if (viewModel.ngayTT != hoaDon.NgayTrangTri)
                        {
                            List<PhanCong> lstPhanCong = DataProvider.Ins.DB.PhanCongs.Where(pc => pc.MaHD == _myHD.MaHD
                                                                                      && pc.Ngay.CompareTo(viewModel.ngayTT) == 0).ToList();
                            if (lstPhanCong != null)
                            {
                                foreach (var pc in lstPhanCong)
                                {
                                    //pc.Ngay = hoaDon.NgayTrangTri.Value;
                                    DataProvider.Ins.DB.PhanCongs.Add(new PhanCong
                                    {
                                        MaHD = pc.MaHD,
                                        MaNV = pc.MaNV,
                                        Ngay = hoaDon.NgayTrangTri.Value,
                                        ThoiGianDen = pc.ThoiGianDen,
                                        ThoiGianDi = pc.ThoiGianDi
                                    });
                                    DataProvider.Ins.DB.PhanCongs.Remove(pc);
                                    DataProvider.Ins.DB.SaveChanges();
                                }
                            }
                        }

                        // Kiểm tra ngày tháo dở có bị thay đổi không, nếu có update phân công ngày tháo dở
                        if (viewModel.ngayTD != hoaDon.NgayThaoDo)
                        {
                            List<PhanCong> lstPhanCong = DataProvider.Ins.DB.PhanCongs.Where(pc => pc.MaHD == _myHD.MaHD
                                                                                      && pc.Ngay.CompareTo(viewModel.ngayTD) == 0).ToList();
                            if (lstPhanCong != null)
                            {
                                foreach (var pc in lstPhanCong)
                                {
                                    DataProvider.Ins.DB.PhanCongs.Add(new PhanCong
                                    {
                                        MaHD = pc.MaHD,
                                        MaNV = pc.MaNV,
                                        Ngay = hoaDon.NgayThaoDo.Value,
                                        ThoiGianDen = pc.ThoiGianDen,
                                        ThoiGianDi = pc.ThoiGianDi
                                    });
                                    DataProvider.Ins.DB.PhanCongs.Remove(pc);
                                    DataProvider.Ins.DB.SaveChanges();
                                }
                            }
                        }

                        // Update thông tin khách hàng
                        KhachHang khachHang = DataProvider.Ins.DB.KhachHangs.FirstOrDefault(kh => kh.MaKH == _myKH.MaKH);
                        khachHang.TenKH = viewModel.myKH.TenKH;
                        khachHang.SoDT = viewModel.myKH.SoDT;
                        khachHang.DiaChi = viewModel.myKH.DiaChi;

                        DataProvider.Ins.DB.SaveChanges();

                        GetThongTin();
                        LstHoaDonKhachHang = hdKHService.GetHoaDonKhachHang();
                    }
                }
                else
                {
                    DataProvider.Ins.RefreshDB();
                    GetData();
                    GetThongTin();
                }
            }
        }
        private void ThanhToan()
        {
            var viewModel = new ReportHoaDonViewModel(myHD.MaHD, myKH.MaKH);
            Shared.myViewModelForModify = viewModel;
            bool? result = Shared.dialogService.ShowDialog(viewModel);
            if (result.HasValue)
            {
                if (result.Value)
                {

                }
                else
                {

                }
            }
        }
        private void Delete()
        {
            var result = MessageBox.Show("Bạn có muốn xóa hóa đơn " + myKH.TenKH + " ?", "Xóa hóa đơn?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    // Xóa phát sinh
                    List<PhatSinh> lstPhatSinh = DataProvider.Ins.DB.PhatSinhs.Where(ps => ps.MaHD == _myHD.MaHD).ToList();
                    foreach (var ps in lstPhatSinh)
                    {
                        DataProvider.Ins.DB.PhatSinhs.Remove(ps);
                        DataProvider.Ins.DB.SaveChanges();
                    }

                    // Xóa chi tiết hóa đơn
                    List<ChiTietHoaDon> lstChiTiet = DataProvider.Ins.DB.ChiTietHoaDons.Where(ct => ct.MaHD == _myHD.MaHD).ToList();
                    foreach (var ct in lstChiTiet)
                    {
                        DataProvider.Ins.DB.ChiTietHoaDons.Remove(ct);
                        DataProvider.Ins.DB.SaveChanges();
                    }

                    // Xóa phân công
                    List<PhanCong> lstPhanCong = DataProvider.Ins.DB.PhanCongs.Where(pc => pc.MaHD == _myHD.MaHD).ToList();
                    foreach (var pc in lstPhanCong)
                    {
                        DataProvider.Ins.DB.PhanCongs.Remove(pc);
                        DataProvider.Ins.DB.SaveChanges();
                    }

                    // Xóa hóa đơn
                    DataProvider.Ins.DB.HoaDons.Remove(_myHD);
                    DataProvider.Ins.DB.SaveChanges();

                    // Xóa khách hàng
                    DataProvider.Ins.DB.KhachHangs.Remove(_myKH);
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Xóa dữ liệu thành công!", "Success!!!", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                    LstHoaDonKhachHang = hdKHService.GetHoaDonKhachHang();
                    SelectedHDKH = null;
                }
                catch
                {
                }
            }
        }
        private void Refresh()
        {
            DataProvider.Ins.RefreshDB();
            GetThongTin();
        }

        // Thay đổi tình trạng hóa đơn --> Chưa trang trí --> Cộng số lượng tồn của vật liệu
        private void ChangeTinhTrang1()
        {
            var result = MessageBox.Show("Bạn muốn thay đổi tình trạng hóa đơn thành chưa trang trí?", "Thông báo!", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (result == MessageBoxResult.Yes)
            {
                if (_myHD.TinhTrang == 1) // Đã trang trí
                {
                    UpdateKhoVatLieu(false);
                }
                HoaDon myHoaDon = DataProvider.Ins.DB.HoaDons.FirstOrDefault(hd => hd.MaHD == _myHD.MaHD);
                myHoaDon.TinhTrang = 0;
                DataProvider.Ins.DB.SaveChanges();

                myHD.TinhTrang = 0;
                OnPropertyChanged(nameof(myHD));
            }
        }

        // Thay đổi tình trạng hóa đơn --> Đã trang trí --> Trừ số lượng tồn của vật liệu
        private void ChangeTinhTrang2()
        {
            var result = MessageBox.Show("Bạn muốn thay đổi tình trạng hóa đơn thành đã trang trí?", "Thông báo!", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (result == MessageBoxResult.Yes)
            {
                if (_myHD.TinhTrang == 0 || _myHD.TinhTrang == 2)
                {
                    UpdateKhoVatLieu(true);
                }

                HoaDon myHoaDon = DataProvider.Ins.DB.HoaDons.FirstOrDefault(hd => hd.MaHD == _myHD.MaHD);
                myHoaDon.TinhTrang = 1;
                DataProvider.Ins.DB.SaveChanges();

                myHD.TinhTrang = 1;
                OnPropertyChanged(nameof(myHD));
            }
        }

        // Thay đổi tình trạng hóa đơn --> Đã tháo dở --> Cộng số lượng tồn của vật liệu
        private void ChangeTinhTrang3()
        {
            var result = MessageBox.Show("Bạn muốn thay đổi tình trạng hóa đơn thành đã tháo dở?", "Thông báo!", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (result == MessageBoxResult.Yes)
            {
                if (_myHD.TinhTrang == 1) // Đã trang trí
                {
                    UpdateKhoVatLieu(false);
                }

                HoaDon myHoaDon = DataProvider.Ins.DB.HoaDons.FirstOrDefault(hd => hd.MaHD == _myHD.MaHD);
                myHoaDon.TinhTrang = 2;
                DataProvider.Ins.DB.SaveChanges();

                myHD.TinhTrang = 2;
                OnPropertyChanged(nameof(myHD));
            }
        }

        void UpdateKhoVatLieu(bool type)
        {
            // type: true ==> - vật liệu tồn (chưa trang trí, đã tháo dở ==> đã trang trí)
            // type: false ==> + vật liệu tồn (đã trang trí ==> chưa trang trí, đã tháo dở)
            if (type)
            {
                // Trừ các vật liệu trong chi tiết hóa đơn
                foreach (var cthd in _myHD.ChiTietHoaDons)
                {
                    foreach (var ctsp in cthd.SanPham.ChiTietSanPhams)
                    {
                        KhoVatLieu myVatLieu = DataProvider.Ins.DB.KhoVatLieux.FirstOrDefault(vl => vl.MaVL == ctsp.MaVL);
                        if (!myVatLieu.IsNhap.Value)
                        {
                            myVatLieu.SoLuongTon -= (ctsp.SoLuong * cthd.SoLuong);
                            DataProvider.Ins.DB.SaveChanges();
                        }
                    }
                }
                // Trừ các vật liệu trong danh sách phát sinh
                foreach (var ps in _myHD.PhatSinhs)
                {
                    if (!ps.KhoVatLieu.IsNhap.Value)
                    {
                        KhoVatLieu myVatLieu = DataProvider.Ins.DB.KhoVatLieux.FirstOrDefault(vl => vl.MaVL == ps.MaVL);
                        myVatLieu.SoLuongTon -= ps.SoLuong;
                    }
                }
            }
            else
            {
                // Cộng các vật liệu trong chi tiết hóa đơn
                foreach (var cthd in _myHD.ChiTietHoaDons)
                {
                    foreach (var ctsp in cthd.SanPham.ChiTietSanPhams)
                    {
                        KhoVatLieu myVatLieu = DataProvider.Ins.DB.KhoVatLieux.FirstOrDefault(vl => vl.MaVL == ctsp.MaVL);
                        if (!myVatLieu.IsNhap.Value)
                        {
                            myVatLieu.SoLuongTon += (ctsp.SoLuong * cthd.SoLuong);
                            DataProvider.Ins.DB.SaveChanges();
                        }
                    }
                }
                // Cộng các vật liệu trong danh sách phát sinh
                foreach (var ps in _myHD.PhatSinhs)
                {
                    if (!ps.KhoVatLieu.IsNhap.Value)
                    {
                        KhoVatLieu myVatLieu = DataProvider.Ins.DB.KhoVatLieux.FirstOrDefault(vl => vl.MaVL == ps.MaVL);
                        myVatLieu.SoLuongTon += ps.SoLuong;
                    }
                }

            }
        }
        #endregion
    }
}
