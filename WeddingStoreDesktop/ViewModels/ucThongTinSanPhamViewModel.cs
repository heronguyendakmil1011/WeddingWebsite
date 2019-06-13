using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Models.SystemModel;

namespace WeddingStoreDesktop.ViewModels
{
    public class ucThongTinSanPhamViewModel : BaseViewModel
    {
        #region Properties
        private string _maSP;
        private SanPham _mySanPham { get; set; }
        public SanPham mySanPham
        {
            get => _mySanPham;
            set
            {
                if (_mySanPham != value)
                {
                    _mySanPham = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _myTenDichVu { get; set; }
        public string myTenDichVu
        {
            get => _myTenDichVu;
            set
            {
                _myTenDichVu = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructors
        public ucThongTinSanPhamViewModel(string maSP)
        {
            _maSP = maSP;

            GetData();
        }
        #endregion

        #region Commands
        #endregion

        #region Methods
        void GetData()
        {
            _mySanPham = DataProvider.Ins.DB.SanPhams.FirstOrDefault(sp => sp.MaSP == _maSP);
            _myTenDichVu = DataProvider.Ins.DB.DichVus.FirstOrDefault(dv => dv.MaDV == _mySanPham.DichVu).TenDV;
        }
        #endregion
    }
}
