using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Models.SystemModel;

namespace WeddingStoreDesktop.ViewModels
{
    public class ucThongTinVatLieuViewModel : BaseViewModel
    {
        private KhoVatLieu _myVL { get; set; }
        public KhoVatLieu myVL
        {
            get => _myVL;
            set
            {
                if(_myVL!=value)
                {
                    _myVL = value;
                    OnPropertyChanged();
                }
            }
        }

        public ucThongTinVatLieuViewModel(string idVL)
        {
            myVL = DataProvider.Ins.DB.KhoVatLieux.FirstOrDefault(vl => vl.MaVL == idVL);
        }
    }
}
