using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Models.DesktopModel;
using WeddingStoreDesktop.Models.SystemModel;
using WeddingStoreDesktop.Interfaces.Dialog;
using System.Windows;

namespace WeddingStoreDesktop.ViewModels
{
    public class ucThongTinNhanVienViewModel : BaseViewModel
    {
        private string _maNV;
        private NhanVien _myNV { get; set; }
        public NhanVien myNV
        {
            get => _myNV;
            set
            {
                _myNV = value;
                OnPropertyChanged();
            }
        }

        public ucThongTinNhanVienViewModel(string maNV)
        {
            _maNV = maNV;
            GetData();

            ModifyThongTinCommand = new ActionCommand(p => ModifyThongTin());
            DeleteCommand = new ActionCommand(p => Delete());
        }

        public void GetData()
        {
            _myNV = DataProvider.Ins.DB.NhanViens.FirstOrDefault(nv => nv.MaNV == _maNV);
        }

        public ICommand ModifyThongTinCommand { get; }
        public ICommand DeleteCommand { get; }

        private void ModifyThongTin()
        {
            var viewModel = new ThemNhanVienViewModel(_myNV);
            Shared.myViewModelForAdd = viewModel;

            bool? result = Shared.dialogService.ShowDialog(viewModel);
            if (result.HasValue)
            {
                if (result.Value)
                {
                    var myUpdate = DataProvider.Ins.DB.NhanViens.FirstOrDefault(nv => nv.MaNV == _myNV.MaNV);

                    myUpdate.TenNV = viewModel.myNV.TenNV;
                    myUpdate.GioiTinh = viewModel.myNV.GioiTinh;
                    myUpdate.NgaySinh = viewModel.myNV.NgaySinh;
                    myUpdate.DiaChi = viewModel.myNV.DiaChi;
                    myUpdate.SoDT = viewModel.myNV.SoDT;
                    myUpdate.Luong = viewModel.myNV.Luong;
                    myNV.Avatar = viewModel.myNV.Avatar;

                    DataProvider.Ins.DB.SaveChanges();
                    myNV = DataProvider.Ins.DB.NhanViens.FirstOrDefault(nv => nv.MaNV == _myNV.MaNV);
                }
                else
                {
                    DataProvider.Ins.RefreshDB();
                    myNV = DataProvider.Ins.DB.NhanViens.FirstOrDefault(nv => nv.MaNV == _myNV.MaNV);
                }
            }
        }

        private void Delete()
        {
            var result = MessageBox.Show("Xóa nhân viên", "Xóa nhân viên " + myNV.TenNV + " ?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                // Xóa phân công nhân viên
                List<PhanCong> lstPhanCong = DataProvider.Ins.DB.PhanCongs.Where(pc => pc.MaNV == _myNV.MaNV).ToList();
                foreach(var pc in lstPhanCong)
                {
                    DataProvider.Ins.DB.PhanCongs.Remove(pc);
                    DataProvider.Ins.DB.SaveChanges();
                }
                // Xóa nhân viên
                DataProvider.Ins.DB.NhanViens.Remove(_myNV);
                DataProvider.Ins.DB.SaveChanges();

                // Lấy dữ liệu mới
            }
        }
    }
}
