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
using WeddingStoreDesktop.Services.DesktopService;

namespace WeddingStoreDesktop.ViewModels
{
    public class ucHoaDonNhapViewModel : BaseViewModel
    {
        #region Properties
        private List<NhaCungCap> _LstNhaCungCap { get; set; }
        public List<NhaCungCap> LstNhaCungCap
        {
            get => _LstNhaCungCap;
            set
            {
                if (_LstNhaCungCap != value)
                {
                    _LstNhaCungCap = value;
                    OnPropertyChanged();
                }
            }
        }

        private NhaCungCap _SelectedNhaCungCap { get; set; }
        public NhaCungCap SelectedNhaCungCap
        {
            get => _SelectedNhaCungCap;
            set
            {
                if (_SelectedNhaCungCap != value)
                {
                    _SelectedNhaCungCap = value;
                    OnPropertyChanged();
                    GetDonGiaByNCC();
                    //if (_SelectedNhaCungCap != null)
                    //    GetDonGiaByNCC();
                    //else
                    //{
                    //    GetNCC();
                    //    //GetDonGiaByNCC();
                    //}

                    SelectedDG = null;
                }
            }
        }

        private List<DonGiaNhapHang> _LstDonGiaNhapHang { get; set; }
        public List<DonGiaNhapHang> LstDonGiaNhapHang
        {
            get => _LstDonGiaNhapHang;
            set
            {
                if (_LstDonGiaNhapHang != value)
                {
                    _LstDonGiaNhapHang = value;
                    OnPropertyChanged();
                }
            }
        }

        private DonGiaNhapHang _SelectedDG { get; set; }
        public DonGiaNhapHang SelectedDG
        {
            get => _SelectedDG;
            set
            {
                if (_SelectedDG != value)
                {
                    _SelectedDG = value;
                    OnPropertyChanged();
                    GetChiTietDG();
                }
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

        public List<string> LstThang
        {
            get => new List<string>
            {
                "All",
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
                    GetDonGiaByNCC();
                }
            }
        }
        #endregion

        #region Constructors
        public ucHoaDonNhapViewModel()
        {
            GetNCC();
            AddNhaCungCapCommand = new ActionCommand(p => AddNhaCungCap());
            ModifyNhaCungCapCommand = new ActionCommand(p => ModifyNhaCungCap());
            DeleteNhaCungCapCommand = new ActionCommand(p => DeleteNhaCungCap());

            AddDonGiaNhapCommand = new ActionCommand(p => AddDonGiaNhap());
            ModifyDonGiaNhapCommand = new ActionCommand(p => ModifyDonGiaNhap());
            DeleteDonGiaNhapCommand = new ActionCommand(p => DeleteDonGiaNhap());

            _Nam = DateTime.Now.Year.ToString();
            _SelectedThang = "Tháng " + DateTime.Now.Month;
        }
        #endregion

        #region Commands
        public ICommand AddNhaCungCapCommand { get; }
        public ICommand ModifyNhaCungCapCommand { get; }
        public ICommand DeleteNhaCungCapCommand { get; }

        public ICommand AddDonGiaNhapCommand { get; }
        public ICommand ModifyDonGiaNhapCommand { get; }
        public ICommand DeleteDonGiaNhapCommand { get; }
        #endregion

        #region Methods
        void GetNCC()
        {
            LstNhaCungCap = DataProvider.Ins.DB.NhaCungCaps.ToList();
        }

        void GetDonGiaByNCC()
        {
            if (int.TryParse(_Nam, out int myNam))
            {
                if (_SelectedNhaCungCap != null)
                {
                    if (_SelectedThang.Equals("All"))
                        LstDonGiaNhapHang = DataProvider.Ins.DB.DonGiaNhapHangs.Where(dg => dg.MaNCC == _SelectedNhaCungCap.MaNCC
                                                                                        && dg.NgayLap.Value.Year == myNam).ToList();
                    else
                    {
                        string[] strThang = _SelectedThang.Split(' ');
                        int myThang = int.Parse(strThang[1]);
                        LstDonGiaNhapHang = DataProvider.Ins.DB.DonGiaNhapHangs.Where(dg => dg.MaNCC == _SelectedNhaCungCap.MaNCC
                                                                                        && dg.NgayLap.Value.Month == myThang
                                                                                        && dg.NgayLap.Value.Year == myNam).ToList();
                    }
                }
                else
                    LstDonGiaNhapHang = new List<DonGiaNhapHang>();
            }
            else
                MessageBox.Show("Không đúng format năm.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Hand);
        }

        void GetChiTietDG()
        {
            if (_SelectedDG != null)
            {
                VatLieuNhapService vatLieuNhap = new VatLieuNhapService();
                LstChiTietDG = vatLieuNhap.GetVatLieuNhapByIdDonGia(_SelectedDG.MaDG);
            }
            else
                LstChiTietDG = new List<VatLieuNhapModel>();
        }
        private void AddNhaCungCap()
        {
            var viewModel = new ModifyNhaCungCapViewModel(null);
            Shared.myViewModelForAdd = viewModel;

            bool? result = Shared.dialogService.ShowDialog(viewModel);
            if (result.HasValue)
            {
                if (result.Value)
                {
                    DataProvider.Ins.DB.NhaCungCaps.Add(viewModel.myNCC);
                    DataProvider.Ins.DB.SaveChanges();
                    GetNCC();
                    SelectedNhaCungCap = viewModel.myNCC;
                }
            }
        }

        private void ModifyNhaCungCap()
        {
            if (_SelectedNhaCungCap != null)
            {
                var viewModel = new ModifyNhaCungCapViewModel(_SelectedNhaCungCap);
                Shared.myViewModelForAdd = viewModel;

                bool? result = Shared.dialogService.ShowDialog(viewModel);
                if (result.HasValue)
                {
                    if (result.Value)
                    {
                        DataProvider.Ins.DB.SaveChanges(); // Update nhà cung cấp
                    }
                    else
                    {
                        DataProvider.Ins.RefreshDB();
                        GetNCC();
                    }
                }
            }
        }

        private void DeleteNhaCungCap()
        {
            if (_SelectedNhaCungCap != null)
            {
                var result = MessageBox.Show("Xóa nhà cung cấp " + _SelectedNhaCungCap.TenNCC + " ?", "Xóa nhà cung cấp??", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Xóa các chi tiết đơn giá --> xóa đơn giá --> xóa nhà cung cấp

                    //List<DonGiaNhapHang> lstDonGia = DataProvider.Ins.DB.DonGiaNhapHangs.Where(dg => dg.MaNCC == _SelectedNhaCungCap.MaNCC).ToList();
                    //foreach (var dg in lstDonGia)

                    NhaCungCap myNCC = DataProvider.Ins.DB.NhaCungCaps.FirstOrDefault(ncc => ncc.MaNCC == _SelectedNhaCungCap.MaNCC);
                    List<DonGiaNhapHang> lstDonGia = myNCC.DonGiaNhapHangs.ToList();
                    foreach (var dg in lstDonGia)
                    {
                        List<ChiTietDonGiaNhapHang> lstChiTiet = dg.ChiTietDonGiaNhapHangs.ToList();
                        foreach (var ct in lstChiTiet)
                        {
                            // xóa chi tiết đơn giá
                            DataProvider.Ins.DB.ChiTietDonGiaNhapHangs.Remove(ct);
                            DataProvider.Ins.DB.SaveChanges();
                        }
                        // Xóa đơn giá
                        DataProvider.Ins.DB.DonGiaNhapHangs.Remove(dg);
                        DataProvider.Ins.DB.SaveChanges();
                    }
                    // Xóa nhà cung cấp
                    DataProvider.Ins.DB.NhaCungCaps.Remove(myNCC);
                    DataProvider.Ins.DB.SaveChanges();

                    GetNCC();
                    SelectedNhaCungCap = null;
                }
            }
        }

        private void AddDonGiaNhap()
        {
            if (_SelectedNhaCungCap != null)
            {
                var viewModel = new ModifyDonGiaViewModel(null, _SelectedNhaCungCap);
                Shared.myViewModelForAdd = viewModel;

                bool? result = Shared.dialogService.ShowDialog(viewModel);
                if (result.HasValue)
                {
                    if (result.Value)
                    {
                        if (viewModel.LstChiTietDG != null && viewModel.LstChiTietDG.Count != 0)
                        {
                            // Thêm đơn giá nhập hàng mới
                            DataProvider.Ins.DB.DonGiaNhapHangs.Add(viewModel.myDG);
                            DataProvider.Ins.DB.SaveChanges();

                            // Thêm các vật liệu nhập vào danh sách chi tiết đơn giá
                            foreach (var ct in viewModel.LstChiTietDG)
                            {
                                DataProvider.Ins.DB.ChiTietDonGiaNhapHangs.Add(new ChiTietDonGiaNhapHang
                                {
                                    MaDG = viewModel.myDG.MaDG,
                                    MaVL = ct.MaVL,
                                    SoLuong = ct.SoLuong,
                                    ThanhTien = ct.ThanhTien
                                });
                                DataProvider.Ins.DB.SaveChanges();
                                // Cộng số lượng tồn của vật liệu trong kho
                                KhoVatLieu myVatLieu = DataProvider.Ins.DB.KhoVatLieux.FirstOrDefault(vl => vl.MaVL == ct.MaVL);
                                myVatLieu.SoLuongTon += ct.SoLuong;
                                DataProvider.Ins.DB.SaveChanges();
                            }
                            GetDonGiaByNCC();
                            GetChiTietDG();
                        }
                    }
                }
            }
        }

        private void ModifyDonGiaNhap()
        {
            if (_SelectedNhaCungCap != null && _SelectedDG != null)
            {
                var viewModel = new ModifyDonGiaViewModel(_SelectedDG, _SelectedNhaCungCap);
                Shared.myViewModelForAdd = viewModel;

                bool? result = Shared.dialogService.ShowDialog(viewModel);
                if (result.HasValue)
                {
                    if (result.Value)
                    {
                        // Xóa chi tiết đơn giá cũ
                        List<ChiTietDonGiaNhapHang> lstChiTiet = DataProvider.Ins.DB.ChiTietDonGiaNhapHangs.Where(ct => ct.MaDG == _SelectedDG.MaDG).ToList();
                        foreach (var ct in lstChiTiet)
                        {
                            DataProvider.Ins.DB.ChiTietDonGiaNhapHangs.Remove(ct);
                            DataProvider.Ins.DB.SaveChanges();
                        }

                        // Thêm chi tiết đơn giá
                        foreach (var ct in viewModel.LstChiTietDG)
                        {
                            DataProvider.Ins.DB.ChiTietDonGiaNhapHangs.Add(new ChiTietDonGiaNhapHang
                            {
                                MaDG = viewModel.myDG.MaDG,
                                MaVL = ct.MaVL,
                                SoLuong = ct.SoLuong,
                                ThanhTien = ct.ThanhTien
                            });
                            DataProvider.Ins.DB.SaveChanges();
                        }

                        GetDonGiaByNCC();
                        GetChiTietDG();
                    }
                }
            }
        }

        private void DeleteDonGiaNhap()
        {
            if (_SelectedDG != null)
            {
                var result = MessageBox.Show("Xóa đơn giá nhập ngày " + _SelectedDG.NgayLap + " của nhà cung cấp" + _SelectedNhaCungCap.TenNCC + " ?"
                            , "Xóa nhà đơn giá??", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Xóa các chi tiết đơn giá
                    List<ChiTietDonGiaNhapHang> lstChiTiet = DataProvider.Ins.DB.ChiTietDonGiaNhapHangs.Where(ct => ct.MaDG == _SelectedDG.MaDG).ToList();
                    foreach (var ct in lstChiTiet)
                    {
                        DataProvider.Ins.DB.ChiTietDonGiaNhapHangs.Remove(ct);
                        DataProvider.Ins.DB.SaveChanges();
                    }

                    // Xóa đơn giá
                    DataProvider.Ins.DB.DonGiaNhapHangs.Remove(_SelectedDG);
                    DataProvider.Ins.DB.SaveChanges();

                    GetDonGiaByNCC();
                    SelectedDG = null;
                }
            }
        }
        #endregion

    }
}
