using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WeddingStoreDesktop.Interfaces.Dialog;
using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Models.SystemModel;
using WeddingStoreDesktop.Views;

namespace WeddingStoreDesktop.ViewModels
{
    public class ModifyThongTinViewModel : BaseViewModel, IDialogRequestClose
    {
        private KhachHang _myKH { get; set; }
        public KhachHang myKH
        {
            get => _myKH;
            set
            {
                if (_myKH != value)
                {
                    _myKH = value;
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
                if (_myHD != value)
                {
                    _myHD = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateTime ngayTT;
        public DateTime ngayTD;
        public ModifyThongTinViewModel(HoaDon hd, KhachHang kh)
        {
            myHD = hd;
            myKH = kh;

            ngayTT = myHD.NgayTrangTri.Value;
            ngayTD = myHD.NgayThaoDo.Value;

            SaveCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true)));
            CancelCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false)));
        }

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
    }
}
