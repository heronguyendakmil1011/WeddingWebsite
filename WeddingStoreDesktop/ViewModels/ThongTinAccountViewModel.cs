using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Models.SystemModel;
using WeddingStoreDesktop.Interfaces.Dialog;
using System.Windows.Input;
using System.Windows;

namespace WeddingStoreDesktop.ViewModels
{
    public class ThongTinAccountViewModel : BaseViewModel, IDialogRequestClose
    {
        private string _maNV;
        private NhanVien _myNhanVien { get; set; }
        public NhanVien myNhanVien
        {
            get => _myNhanVien;
            set
            {
                _myNhanVien = value;
                OnPropertyChanged();
            }
        }

        private TaiKhoan _myTaiKhoan { get; set; }
        public TaiKhoan myTaiKhoan
        {
            get { return _myTaiKhoan; }
            set { _myTaiKhoan = value; }
        }

        private TaiKhoan _firstTaiKhoan;

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        public ThongTinAccountViewModel(string maNV)
        {
            _maNV = maNV;
            GetNhanVien();
            GetAccount();

            SaveCommand = new ActionCommand(p => Save());
            CancelCommand = new ActionCommand(p => Cancel());
            DoneCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true)));
        }

        void GetNhanVien()
        {
            myNhanVien = DataProvider.Ins.DB.NhanViens.FirstOrDefault(nv => nv.MaNV == _maNV);
        }

        void GetAccount()
        {
            myTaiKhoan = DataProvider.Ins.DB.TaiKhoans.FirstOrDefault(tk => tk.MaNV == _maNV);
            _firstTaiKhoan = _myTaiKhoan;
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand DoneCommand { get; }
        public ICommand ChooseImageCommand { get; }

        void Save()
        {
            DataProvider.Ins.DB.SaveChanges();
            MessageBox.Show("Chỉnh sửa tài khoản thành công", "Thành công!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        void Cancel()
        {
            DataProvider.Ins.RefreshDB();
            myTaiKhoan = _firstTaiKhoan;
        }
    }
}
