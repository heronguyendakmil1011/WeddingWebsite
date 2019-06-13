using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WeddingStoreDesktop.Interfaces.Dialog;
using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Models.DesktopModel;
using WeddingStoreDesktop.Models.SystemModel;
using WeddingStoreDesktop.Services;
using WeddingStoreDesktop.Views;

namespace WeddingStoreDesktop.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private List<TinhNangModel> _lstTinhNang { get; set; }
        public List<TinhNangModel> LstTinhNang
        {
            get => _lstTinhNang;
            set
            {
                _lstTinhNang = value;
                OnPropertyChanged();
            }
        }

        private TinhNangModel _tinhNang { get; set; }
        public TinhNangModel tinhNang
        {
            get => _tinhNang;
            set
            {
                if (_tinhNang != value)
                {
                    _tinhNang = value;
                    OnPropertyChanged();
                }
            }
        }

        private NhanVien _myNhanVien;
        public NhanVien myNhanVien
        {
            get { return _myNhanVien; }
            set { _myNhanVien = value; OnPropertyChanged(); }
        }

        private TaiKhoan _myTaiKhoan;

        public HomeViewModel(TaiKhoan tk)
        {
            _myTaiKhoan = tk;
            var currentWindow = GetCurrentWindow();
            currentWindow.Close();
            GetData();
            GetNhanVien();

            AccountCommand = new ActionCommand(p => XemAccount());
        }

        void GetData()
        {
            MockTinhNangRepository tinhNang = new MockTinhNangRepository();
            _lstTinhNang = tinhNang.GetTinhNang();
        }

        void GetNhanVien()
        {
            myNhanVien = DataProvider.Ins.DB.NhanViens.FirstOrDefault(nv => nv.MaNV == _myTaiKhoan.MaNV);
            Shared.CurrentTenNV = myNhanVien.TenNV;
        }

        public void CheckDashBroad()
        {
            Window home = new HomePage(_tinhNang.id);
            home.Show();
        }

        public ICommand AccountCommand { get; }

        void XemAccount()
        {
            var viewModel = new ThongTinAccountViewModel(_myTaiKhoan.MaNV);
            bool? result = Shared.dialogService.ShowDialog(viewModel);
            if (result.HasValue)
            {
                if (result.Value)
                {

                }
            }
        }
    }
}
