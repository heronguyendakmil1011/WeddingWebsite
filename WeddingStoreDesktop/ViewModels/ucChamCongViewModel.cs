using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Functions;
using WeddingStoreDesktop.Models.DesktopModel;
using WeddingStoreDesktop.Services.DesktopService;
using WeddingStoreDesktop.Models.SystemModel;

namespace WeddingStoreDesktop.ViewModels
{
    public class ucChamCongViewModel : BaseViewModel
    {
        private string _maNV;

        private List<ChamCongModel> _LstChamCong { get; set; }
        public List<ChamCongModel> LstChamCong
        {
            get => _LstChamCong;
            set
            {
                if (_LstChamCong != value)
                {
                    _LstChamCong = value;
                    OnPropertyChanged();
                }
            }
        }

        private ChamCongModel _SelectedChamCong { get; set; }
        public ChamCongModel SelectedChamCong
        {
            get => _SelectedChamCong;
            set
            {
                if (_SelectedChamCong != value)
                {
                    _SelectedChamCong = value;
                    OnPropertyChanged();
                }
            }
        }

        private float _TongTien { get; set; }
        public float TongTien
        {
            get => _TongTien;
            set
            {
                if (_TongTien != value)
                {
                    _TongTien = value;
                    OnPropertyChanged();
                }
            }
        }

        public ucChamCongViewModel(string maNV)
        {
            _maNV = maNV;
            GetData();
        }

        void GetData()
        {
            ChamCongService chamCongService = new ChamCongService();
            _LstChamCong = chamCongService.GetChamCongByIdNV(_maNV);
            _TongTien = 0;

            NhanVien myNV = DataProvider.Ins.DB.NhanViens.FirstOrDefault(nv => nv.MaNV == _maNV);

            foreach (var pc in LstChamCong)
            {
                if (myNV.Luong.HasValue && !string.IsNullOrEmpty(pc.TongThoiGian))
                    TongTien += Tinh.TinhTongTien(pc.TongThoiGian, myNV.Luong.Value);
            }
        }
    }
}
