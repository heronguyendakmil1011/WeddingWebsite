using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Functions;
using WeddingStoreDesktop.Models.DesktopModel;
using WeddingStoreDesktop.Services.DesktopService;
using System.Windows.Input;
using WeddingStoreDesktop.Interfaces.Dialog;
using System.Globalization;
using WeddingStoreDesktop.Models.SystemModel;

namespace WeddingStoreDesktop.ViewModels
{
    public class ucPhanCongNhanVienViewModel : BaseViewModel
    {
        #region Properties
        private HoaDonKhachHangModel _hdKH;

        private List<PhanCongNhanVienModel> _LstPhanCongNgayTrangTri { get; set; }
        public List<PhanCongNhanVienModel> LstPhanCongNgayTrangTri
        {
            get => _LstPhanCongNgayTrangTri;
            set
            {
                if (_LstPhanCongNgayTrangTri != value)
                {
                    _LstPhanCongNgayTrangTri = value;
                    OnPropertyChanged();
                }
            }
        }

        private List<PhanCongNhanVienModel> _LstPhanCongNgayThaoDo { get; set; }
        public List<PhanCongNhanVienModel> LstPhanCongNgayThaoDo
        {
            get => _LstPhanCongNgayThaoDo;
            set
            {
                if (_LstPhanCongNgayThaoDo != value)
                {
                    _LstPhanCongNgayThaoDo = value;
                    OnPropertyChanged();
                }
            }
        }

        private PhanCongNhanVienModel _SelectedNhanVien { get; set; }
        public PhanCongNhanVienModel SelectedNhanVien
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
        #endregion

        #region Constructors
        public ucPhanCongNhanVienViewModel(HoaDonKhachHangModel HdKh)
        {
            _hdKH = HdKh;
            GetData();
            PhanCongCommand = new ActionCommand(p => GoToPhanCong());
            ThemPhanCongCommand = new ActionCommand(p => ThemPhanCong());
        }
        #endregion

        #region Commands
        public ICommand PhanCongCommand { get; }
        public ICommand ThemPhanCongCommand { get; }
        #endregion

        #region Methods
        void GetData()
        {
            NhanVienPhanCongService nhanVienPhanCong = new NhanVienPhanCongService();
            LstPhanCongNgayTrangTri = nhanVienPhanCong.GetPhanCongByNgayTrangTri(_hdKH.MaHD, _hdKH.NgayTrangTri);
            LstPhanCongNgayThaoDo = nhanVienPhanCong.GetPhanCongByNgayThaoDo(_hdKH.MaHD, _hdKH.NgayThaoDo);
        }
        void GoToPhanCong()
        {
            if (_SelectedNhanVien != null)
            {
                var viewModel = new DiemDanhNhanVienViewModel(_SelectedNhanVien);
                bool? result = Shared.dialogService.ShowDialog(viewModel);
                if (result.HasValue)
                {
                    if (result.Value)
                    {
                        DateTime? myDate = DateTime.ParseExact(_SelectedNhanVien.Ngay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        var myUpdate = DataProvider.Ins.DB.PhanCongs.FirstOrDefault(pc => pc.MaNV == _SelectedNhanVien.MaNV
                                                              && pc.Ngay == myDate
                                                              && pc.MaHD == _SelectedNhanVien.MaHD);
                        if (myUpdate != null)
                        {
                            if (!String.IsNullOrEmpty(viewModel.myPCNV.ThoiGianDen))
                            {
                                myUpdate.ThoiGianDen = TimeSpan.Parse(viewModel.myPCNV.ThoiGianDen);
                                DataProvider.Ins.DB.SaveChanges();
                            }
                            if (!String.IsNullOrEmpty(viewModel.myPCNV.ThoiGianDi))
                            {
                                myUpdate.ThoiGianDi = TimeSpan.Parse(viewModel.myPCNV.ThoiGianDi);
                                DataProvider.Ins.DB.SaveChanges();
                            }

                            GetData();
                        }
                    }
                    else
                    {
                        DataProvider.Ins.RefreshDB();
                        GetData();
                    }
                }
            }
        }
        void ThemPhanCong()
        {
            var viewModel = new ThemPhanCongNhanVienViewModel(_hdKH);
            Shared.myViewModelForAdd = viewModel;
            bool? result = Shared.dialogService.ShowDialog(viewModel);
            if (result.HasValue)
            {
                if (result.Value)
                {
                    // Xóa các phân công hiện tại trong hóa đơn
                    foreach (var first in _LstPhanCongNgayTrangTri)
                    {
                        DateTime myNgay = DateTime.ParseExact(_hdKH.NgayTrangTri, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        PhanCong myPC = DataProvider.Ins.DB.PhanCongs.FirstOrDefault(pc => pc.MaHD == _hdKH.MaHD
                                                                 && pc.MaNV == first.MaNV
                                                                 && pc.Ngay == myNgay);
                        DataProvider.Ins.DB.PhanCongs.Remove(myPC);
                        DataProvider.Ins.DB.SaveChanges();
                    }
                    foreach (var first in _LstPhanCongNgayThaoDo)
                    {
                        DateTime myNgay = DateTime.ParseExact(_hdKH.NgayThaoDo, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        PhanCong myPC = DataProvider.Ins.DB.PhanCongs.FirstOrDefault(pc => pc.MaHD == _hdKH.MaHD
                                                                 && pc.MaNV == first.MaNV
                                                                 && pc.Ngay == myNgay);
                        DataProvider.Ins.DB.PhanCongs.Remove(myPC);
                        DataProvider.Ins.DB.SaveChanges();
                    }

                    // Thêm phân công mới vào hóa đơn
                    foreach (var myNew in viewModel.LstPhanCongNgayTrangTri)
                    {
                        //TimeSpan? ahihi = null; // dùng đc
                        DateTime myNgay;
                        if (DateTime.TryParseExact(myNew.Ngay, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out myNgay))
                        {
                            DataProvider.Ins.DB.PhanCongs.Add(new PhanCong
                            {
                                MaHD = myNew.MaHD,
                                MaNV = myNew.MaNV,
                                //Ngay = DateTime.Parse(myNew.Ngay),
                                Ngay = myNgay,
                                ThoiGianDen = !String.IsNullOrEmpty(myNew.ThoiGianDen) ? TimeSpan.Parse(myNew.ThoiGianDen) : TimeSpan.Zero,
                                ThoiGianDi = !String.IsNullOrEmpty(myNew.ThoiGianDi) ? TimeSpan.Parse(myNew.ThoiGianDi) : TimeSpan.Zero
                            });
                        }
                        else
                        {
                            DataProvider.Ins.DB.PhanCongs.Add(new PhanCong
                            {
                                MaHD = myNew.MaHD,
                                MaNV = myNew.MaNV,
                                Ngay = DateTime.Parse(myNew.Ngay),
                                ThoiGianDen = !String.IsNullOrEmpty(myNew.ThoiGianDen) ? TimeSpan.Parse(myNew.ThoiGianDen) : TimeSpan.Zero,
                                ThoiGianDi = !String.IsNullOrEmpty(myNew.ThoiGianDi) ? TimeSpan.Parse(myNew.ThoiGianDi) : TimeSpan.Zero
                            });
                        }
                        DataProvider.Ins.DB.SaveChanges();
                    }
                    foreach (var myNew in viewModel.LstPhanCongNgayThaoDo)
                    {
                        DateTime myNgay;
                        if (DateTime.TryParseExact(myNew.Ngay, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out myNgay))
                            DataProvider.Ins.DB.PhanCongs.Add(new PhanCong
                            {
                                MaHD = myNew.MaHD,
                                MaNV = myNew.MaNV,
                                //Ngay = DateTime.Parse(myNew.Ngay),
                                Ngay = myNgay,
                                ThoiGianDen = !String.IsNullOrEmpty(myNew.ThoiGianDen) ? TimeSpan.Parse(myNew.ThoiGianDen) : TimeSpan.Zero,
                                ThoiGianDi = !String.IsNullOrEmpty(myNew.ThoiGianDi) ? TimeSpan.Parse(myNew.ThoiGianDi) : TimeSpan.Zero
                            });
                        else
                            DataProvider.Ins.DB.PhanCongs.Add(new PhanCong
                            {
                                MaHD = myNew.MaHD,
                                MaNV = myNew.MaNV,
                                Ngay = DateTime.Parse(myNew.Ngay),
                                ThoiGianDen = !String.IsNullOrEmpty(myNew.ThoiGianDen) ? TimeSpan.Parse(myNew.ThoiGianDen) : TimeSpan.Zero,
                                ThoiGianDi = !String.IsNullOrEmpty(myNew.ThoiGianDi) ? TimeSpan.Parse(myNew.ThoiGianDi) : TimeSpan.Zero
                            });
                        DataProvider.Ins.DB.SaveChanges();
                    }

                    GetData();
                }
                else
                {
                    DataProvider.Ins.RefreshDB();
                    GetData();
                }
            }
        }
        #endregion
    }
}
