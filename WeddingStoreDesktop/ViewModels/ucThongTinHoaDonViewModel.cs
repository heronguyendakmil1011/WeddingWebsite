using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingStoreDesktop.Models;
using System.Windows.Input;
using WeddingStoreDesktop.Views;
using WeddingStoreDesktop.Interfaces.Dialog;
using MaterialDesignThemes.Wpf;
using WeddingStoreDesktop.Models.SystemModel;

namespace WeddingStoreDesktop.ViewModels
{
    public class ucThongTinHoaDonViewModel : BaseViewModel
    {
        private HoaDon _myHD { get; set; }
        public HoaDon myHD
        {
            get => _myHD;
            set
            {
                _myHD = value;
                OnPropertyChanged();
            }
        }

        private KhachHang _myKH { get; set; }
        public KhachHang myKH
        {
            get => _myKH;
            set
            {
                _myKH = value;
                OnPropertyChanged();
            }
        }

        public ucThongTinHoaDonViewModel(string maHD, string maKH)
        {
            GetData(maHD, maKH);
        }

        void GetData(string maHD, string maKH)
        {
            _myHD = DataProvider.Ins.DB.HoaDons.FirstOrDefault(hd => hd.MaHD == maHD);

            _myKH = DataProvider.Ins.DB.KhachHangs.FirstOrDefault(kh => kh.MaKH == maKH);
        }

        public ICommand ThanhToanCommand { get; }
    }
}
