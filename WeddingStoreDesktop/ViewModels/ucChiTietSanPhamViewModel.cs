using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Functions;
using WeddingStoreDesktop.Models.DesktopModel;
using WeddingStoreDesktop.Services.DesktopService;

namespace WeddingStoreDesktop.ViewModels
{
    public class ucChiTietSanPhamViewModel : BaseViewModel
    {
        private string _maSP;
        private string _maHD;

        private List<ThongTinChiTietVatLieuModel> _LstThongTinChiTietVatLieu { get; set; }
        public List<ThongTinChiTietVatLieuModel> LstThongTinChiTietVatLieu
        {
            get => _LstThongTinChiTietVatLieu;
            set
            {
                _LstThongTinChiTietVatLieu = value;
                OnPropertyChanged();
            }
        }

        public ucChiTietSanPhamViewModel(string maSP, string maHD)
        {
            _maSP = maSP;
            _maHD = maHD;

            GetData();
        }

        void GetData()
        {
            ThongTinChiTietVatLieuService thongTinVatLieuService = new ThongTinChiTietVatLieuService();
            _LstThongTinChiTietVatLieu = thongTinVatLieuService.GetThongTinVatLieu(_maHD, _maSP);
        }
    }
}
