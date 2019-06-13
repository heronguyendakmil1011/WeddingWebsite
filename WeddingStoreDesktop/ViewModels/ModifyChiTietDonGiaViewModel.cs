using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeddingStoreDesktop.Interfaces.Dialog;
using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Models.DesktopModel;
using WeddingStoreDesktop.Models.SystemModel;

namespace WeddingStoreDesktop.ViewModels
{
    public class ModifyChiTietDonGiaViewModel : BaseViewModel, IDialogRequestClose
    {
        public string maVL;

        private int _SoLuong { get; set; }
        public int SoLuong
        {
            get => _SoLuong;
            set
            {
                if (_SoLuong != value)
                {
                    _SoLuong = value;
                    OnPropertyChanged();
                }
            }
        }

        private float _ThanhTien { get; set; }
        public float ThanhTien
        {
            get => _ThanhTien;
            set
            {
                if (_ThanhTien != value)
                {
                    _ThanhTien = value;
                    OnPropertyChanged();
                }
            }
        }

        public ModifyChiTietDonGiaViewModel(VatLieuNhapModel vl)
        {
            maVL = vl.MaVL;
            SoLuong = vl.SoLuong.Value;
            ThanhTien = vl.ThanhTien.Value;

            SaveCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true)));
            CancelCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false)));
        }

        public ModifyChiTietDonGiaViewModel(KhoVatLieu vl)
        {
            maVL = vl.MaVL;
            SoLuong = 1;
            ThanhTien = 0;

            SaveCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true)));
            CancelCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false)));
        }

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
    }
}
