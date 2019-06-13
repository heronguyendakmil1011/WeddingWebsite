using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Interfaces.Dialog;
using WeddingStoreDesktop.Models.SystemModel;
using WeddingStoreDesktop.Functions;

namespace WeddingStoreDesktop.ViewModels
{
    public class ModifyNhaCungCapViewModel : BaseViewModel, IDialogRequestClose
    {
        private NhaCungCap _myNCC;
        public NhaCungCap myNCC
        {
            get => _myNCC;
            set
            {
                if (_myNCC != value)
                {
                    _myNCC = value;
                    OnPropertyChanged();
                }
            }
        }

        public ModifyNhaCungCapViewModel(NhaCungCap ncc)
        {
            if (ncc != null)
            {
                _myNCC = ncc;
                Title = "Chỉnh sửa nhà cung cấp";
            }
            else
            {
                _myNCC = new NhaCungCap
                {
                    MaNCC = "NCC-" + GetMaxID.GetMaxIdNCC(),
                    TenNCC = "",
                    SoDT = "",
                    DiaChi = ""
                };
                Title = "Thêm nhà cung cấp";
            }
            SaveCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true)));
            CancelCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false)));
        }

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
    }
}
