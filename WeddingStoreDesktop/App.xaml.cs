using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WeddingStoreDesktop.Interfaces.Dialog;
using WeddingStoreDesktop.ViewModels;
using WeddingStoreDesktop.Views;

namespace WeddingStoreDesktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IDialogService dialogService = new DialogService(MainWindow);

            dialogService.Register<ModifyThongTinViewModel, ModifyThongTin>();
            dialogService.Register<ReportHoaDonViewModel, ReportHoaDon>();
            dialogService.Register<ModifySoLuongViewModel, ModifySoLuong>();
            dialogService.Register<ThemChiTietHDViewModel, ThemChiTietHD>();
            dialogService.Register<ThemPhatSinhViewModel, ThemPhatSinh>();
            dialogService.Register<ThemSanPhamViewModel, ThemSanPham>();
            dialogService.Register<ThemNhanVienViewModel, ThemNhanVien>();
            dialogService.Register<ThemVatLieuViewModel, ThemVatLieu>();
            dialogService.Register<ModifyNhaCungCapViewModel, ModifyNhaCungCap>();
            dialogService.Register<ModifyDonGiaViewModel, ModifyDonGiaNhap>();
            dialogService.Register<ModifyChiTietDonGiaViewModel, ModifyChiTietDonGia>();
            dialogService.Register<ThemDichVuViewModel, ThemDichVu>();
            dialogService.Register<TaiKhoanViewModel, DanhSachTaiKhoan>();
            dialogService.Register<ThongTinAccountViewModel, ThongTinAccount>();
            dialogService.Register<DiemDanhNhanVienViewModel, DiemDanhNhanVien>();
            dialogService.Register<ThemPhanCongNhanVienViewModel, ThemPhanCongNhanVien>();

            Shared.dialogService = dialogService;

            var view = new DangNhap();
            view.ShowDialog();
        }
    }
}
