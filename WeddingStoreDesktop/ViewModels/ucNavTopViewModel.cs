using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeddingStoreDesktop.Interfaces.Dialog;
using WeddingStoreDesktop.Views;

namespace WeddingStoreDesktop.ViewModels
{
    public class ucNavTopViewModel : BaseViewModel
    {
        public ucNavTopViewModel()
        {
            LogoutCommand = new ActionCommand(p => Logout());
        }

        public ICommand LogoutCommand { get; }

        private void Logout()
        {
            var dangNhap = new DangNhap();
            dangNhap.ShowDialog();
        }
    }
}
