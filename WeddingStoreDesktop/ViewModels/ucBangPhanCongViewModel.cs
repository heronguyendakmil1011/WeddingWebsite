using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Models.DesktopModel;
using WeddingStoreDesktop.Functions;
using System.Windows.Input;
using WeddingStoreDesktop.Interfaces.Dialog;
using WeddingStoreDesktop.Models.SystemModel;
using WeddingStoreDesktop.Services.DesktopService;
using System.Windows;
using System.Globalization;

namespace WeddingStoreDesktop.ViewModels
{
    public class ucBangPhanCongViewModel : BaseViewModel
    {
        #region Properties
        private List<NgayPhanCongModel> _LstNgay { get; set; }
        public List<NgayPhanCongModel> LstNgay
        {
            get => _LstNgay;
            set
            {
                if (_LstNgay != value)
                {
                    _LstNgay = value;
                    OnPropertyChanged();
                }
            }
        }

        // Chọn ngày
        private NgayPhanCongModel _SelectedNgay { get; set; }
        public NgayPhanCongModel SelectedNgay
        {
            get => _SelectedNgay;
            set
            {
                if (_SelectedNgay != value)
                {
                    _SelectedNgay = value;
                    OnPropertyChanged();
                    LstPhanCongNhanVien = null;
                }
            }
        }

        // Chọn hóa đơn trong ngày
        private ThongTinNgayModel _SelectedThongTinNgay { get; set; }
        public ThongTinNgayModel SelectedThongTinNgay
        {
            get => _SelectedThongTinNgay;
            set
            {
                if (_SelectedThongTinNgay != value)
                {
                    _SelectedThongTinNgay = value;
                    OnPropertyChanged();
                    GetPhanCongNhanVien();
                }
            }
        }

        // Danh sách phân công nhân viên theo hóa đơn
        private List<PhanCongNhanVienModel> _LstPhanCongNhanVien { get; set; }
        public List<PhanCongNhanVienModel> LstPhanCongNhanVien
        {
            get => _LstPhanCongNhanVien;
            set
            {
                if (_LstPhanCongNhanVien != value)
                {
                    _LstPhanCongNhanVien = value;
                    OnPropertyChanged();
                }
            }
        }

        // Chọn nhân viên trong danh sách phân công
        private PhanCongNhanVienModel _SelectedPhanCong { get; set; }
        public PhanCongNhanVienModel SelectedPhanCong
        {
            get => _SelectedPhanCong;
            set
            {
                if (_SelectedPhanCong != value)
                {
                    _SelectedPhanCong = value;
                    OnPropertyChanged();
                }
            }
        }

        // Danh sách tất cả nhân viên hiện có
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

        // CHọn nhân viên trong danh sách tất cả nhân viên hiện có
        private NhanVien _SelectedNhanVien { get; set; }
        public NhanVien SelectedNhanVien
        {
            get => _SelectedNhanVien;
            set
            {
                if (_SelectedNhanVien != value)
                {
                    _SelectedNhanVien = value;
                    OnPropertyChanged();
                }
            }
        }

        // Danh sách các tháng
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

        // Nhập ngày
        private string _Ngay { get; set; }
        public string Ngay
        {
            get => _Ngay;
            set
            {
                if (_Ngay != value)
                {
                    _Ngay = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _theoThang { get; set; }
        public bool theoThang
        {
            get => _theoThang;
            set
            {
                if (_theoThang != value)
                {
                    _theoThang = value;
                    OnPropertyChanged();
                    LstNgay = null;
                    LstPhanCongNhanVien = null;
                }
            }
        }

        private bool _theoNgay { get; set; }
        public bool theoNgay
        {
            get => _theoNgay;
            set
            {
                _theoNgay = value;
                OnPropertyChanged();
                LstNgay = null;
                LstPhanCongNhanVien = null;
            }
        }
        #endregion

        #region Services
        #endregion

        #region Constructors
        public ucBangPhanCongViewModel()
        {
            GetNhanVien();
            SearchCommand = new ActionCommand(p => GetData());
        }
        #endregion

        #region Commands

        public ICommand SearchCommand { get; }
        #endregion

        #region Methods
        void GetData()
        {
            NgayPhanCongService ngayPhanCong = new NgayPhanCongService();
            if (theoThang)
            {
                if (_Thang != null)
                {
                    string[] myThang = _Thang.Split(' ');
                    LstNgay = ngayPhanCong.GetNgayPhanCongByThang(int.Parse(myThang[1]));
                }
            }
            else
            {
                DateTime myNgay;
                if (DateTime.TryParse(_Ngay, out myNgay))
                {
                    LstNgay = ngayPhanCong.GetNgayPhanCongByNgayXacDinh(myNgay);
                }
                else
                    MessageBox.Show("Sai cú pháp, mời nhập lại!!", "Fail!!!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        void GetNhanVien()
        {
            _LstNhanVien = DataProvider.Ins.DB.NhanViens.ToList();
        }

        void GetPhanCongNhanVien()
        {
            if (SelectedThongTinNgay != null && SelectedNgay != null)
            {

                NhanVienPhanCongService nhanVienPhanCong = new NhanVienPhanCongService();
                LstPhanCongNhanVien = nhanVienPhanCong.GetPhanCongNhanVienByIdHDVaNgay(SelectedThongTinNgay.MaHD, SelectedNgay.Ngay);
            }
        }

        public void ThemNhanVienThamGia(string maNV)
        {
            if (_SelectedNgay != null && _SelectedThongTinNgay != null)
            {
                DateTime myNgay = DateTime.ParseExact(_SelectedNgay.Ngay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                PhanCong myPhanCong = DataProvider.Ins.DB.PhanCongs.FirstOrDefault(pc => pc.MaNV == maNV
                                                                    && pc.MaHD == _SelectedThongTinNgay.MaHD
                                                                    && pc.Ngay == myNgay);
                if (myPhanCong == null)
                {
                    DataProvider.Ins.DB.PhanCongs.Add(new PhanCong
                    {
                        MaHD = _SelectedThongTinNgay.MaHD,
                        MaNV = maNV,
                        Ngay = myNgay,
                        ThoiGianDen = null,
                        ThoiGianDi = null
                    });
                    DataProvider.Ins.DB.SaveChanges();
                    GetPhanCongNhanVien();
                }
            }
            //bool isExist = false;
            //if (SelectedThongTinNgay != null)
            //{
            //    // Kiểm tra đã tồn tại nhân viên hay chưa
            //    foreach (var myPC in LstPhanCongNhanVien)
            //    {
            //        if (myPC.MaNV == myNV.MaNV)
            //        {
            //            isExist = true;
            //            break;
            //        }
            //    }
            //    if (!isExist)
            //        LstPhanCongNhanVien.Add(
            //            new PhanCongNhanVienModel
            //            {
            //                MaHD = SelectedThongTinNgay.MaHD,
            //                Ngay = SelectedNgay.Ngay,
            //                Avatar = myNV.Avatar,
            //                MaNV = myNV.MaNV,
            //                TenNV = myNV.TenNV,
            //                ThoiGianDen = "",
            //                ThoiGianDi = ""
            //            });
            //}
        }

        public void XoaNhanVienThamGia()
        {
            if (_SelectedPhanCong != null)
            {
                DateTime myNgay = DateTime.ParseExact(_SelectedNgay.Ngay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                PhanCong myPhanCong = DataProvider.Ins.DB.PhanCongs.FirstOrDefault(pc => pc.MaNV == _SelectedPhanCong.MaNV
                                                                    && pc.MaHD == _SelectedThongTinNgay.MaHD
                                                                    && pc.Ngay == myNgay);
                DataProvider.Ins.DB.PhanCongs.Remove(myPhanCong);
                DataProvider.Ins.DB.SaveChanges();
                GetPhanCongNhanVien();
            }
        }
        #endregion
    }
}
