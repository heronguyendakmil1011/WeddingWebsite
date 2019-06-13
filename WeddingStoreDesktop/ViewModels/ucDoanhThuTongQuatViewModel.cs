using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Functions;
using System.Windows.Input;
using WeddingStoreDesktop.Interfaces.Dialog;
using System.Windows;
using WeddingStoreDesktop.Models.DesktopModel;
using WeddingStoreDesktop.Services.DesktopService;
using WeddingStoreDesktop.Models.SystemModel;

namespace WeddingStoreDesktop.ViewModels
{
    public class ucDoanhThuTongQuatViewModel : BaseViewModel
    {
        #region Properties
        private List<LuongNhanVienModel> _LstLuongNhanVien { get; set; }
        public List<LuongNhanVienModel> LstLuongNhanVien
        {
            get => _LstLuongNhanVien;
            set
            {
                if (_LstLuongNhanVien != value)
                {
                    _LstLuongNhanVien = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<VatLieuNhapModel> _LstVatLieuNhap { get; set; }
        public List<VatLieuNhapModel> LstVatLieuNhap
        {
            get => _LstVatLieuNhap;
            set
            {
                if (_LstVatLieuNhap != value)
                {
                    _LstVatLieuNhap = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<HoaDon> _LstHoaDon { get; set; }
        public List<HoaDon> LstHoaDon
        {
            get => _LstHoaDon;
            set
            {
                if (_LstHoaDon != value)
                {
                    _LstHoaDon = value;
                    OnPropertyChanged();
                }
            }
        }

        private float _TongTienHD { get; set; }
        public float TongTienHD
        {
            get => _TongTienHD;
            set
            {
                _TongTienHD = value;
                OnPropertyChanged();
            }
        }

        public float _TongTienNhap { get; set; }
        public float TongTienNhap
        {
            get => _TongTienNhap;
            set
            {
                if (_TongTienNhap != value)
                {
                    _TongTienNhap = value;
                    OnPropertyChanged();
                }
            }
        }

        private float _TongLuongNV { get; set; }
        public float TongLuongNV
        {
            get => _TongLuongNV;
            set
            {
                if (_TongLuongNV != value)
                {
                    _TongLuongNV = value;
                    OnPropertyChanged();
                }
            }
        }

        private float _DoanhThu { get; set; }
        public float DoanhThu
        {
            get => _DoanhThu;
            set
            {
                if (_DoanhThu != value)
                {
                    _DoanhThu = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Constructors
        public ucDoanhThuTongQuatViewModel(string thang, int nam)
        {
            string[] strThang = thang.Split(' ');
            int myThang = int.Parse(strThang[1]);

            GetData(myThang, nam);
        }

        public ucDoanhThuTongQuatViewModel(string from, string to, int nam)
        {
            GetByCustom(from, to, nam);
        }
        #endregion

        #region Commands
        #endregion

        #region Methods
        void GetByCustom(string from, string to, int nam)
        {
            {
                string[] fromThang = from.Split(' ');
                int myFrom = int.Parse(fromThang[1]);

                string[] toThang = to.Split(' ');
                int myTo = int.Parse(toThang[1]);

                if (myFrom > myTo)
                    MessageBox.Show("Nooooo!!! Wrong");
                else
                {
                    _TongLuongNV = 0;
                    _TongTienNhap = 0;
                    _TongTienHD = 0;
                    for (int i = myFrom; i <= myTo; i++)
                    {
                        GetData(i, nam);
                    }
                }
            }
        }

        void GetData(int thang, int nam)
        {
            // New Get luong nhan vien
            LuongNhanVienService luongNVService = new LuongNhanVienService();
            List<LuongNhanVienModel> lstLuong = luongNVService.GetLuongNVByThang(thang, nam);
            if (_LstLuongNhanVien == null)
                LstLuongNhanVien = lstLuong;
            else
            {
                foreach (var nv in lstLuong)
                {
                    bool isExist = false;
                    foreach (var myNV in LstLuongNhanVien)
                    {
                        if (myNV.MaNV == nv.MaNV)
                        {
                            myNV.TongThoiGian += nv.TongThoiGian;
                            myNV.TongTien += nv.TongTien;
                            isExist = true;
                            break;
                        }
                    }
                    if (!isExist)
                    {
                        LstLuongNhanVien.Add(nv);
                    }
                }
            }
            foreach (var nv in lstLuong)
            {
                TongLuongNV += nv.TongTien;
            }

            // Get Vat Lieu Nhap
            VatLieuNhapService vatLieuNhap = new VatLieuNhapService();
            List<VatLieuNhapModel> lstVatLieu = vatLieuNhap.GetVatLieuNhapByThang(thang, nam);
            if (_LstVatLieuNhap == null)
                LstVatLieuNhap = lstVatLieu;
            else
            {
                foreach (var vl in lstVatLieu)
                {
                    bool isExist = false;
                    foreach (var myVL in LstVatLieuNhap)
                    {
                        if (myVL.MaVL == vl.MaVL)
                        {
                            myVL.ThanhTien += vl.ThanhTien;
                            myVL.SoLuong += vl.SoLuong;
                            isExist = true;
                            break;
                        }
                    }
                    if (!isExist)
                        LstVatLieuNhap.Add(vl);
                }
            }

            List<DonGiaNhapHang> lstDonGia = DataProvider.Ins.DB.DonGiaNhapHangs.Where(dg => dg.NgayLap.Value.Month == thang
                                                                                    && dg.NgayLap.Value.Year == nam).ToList();
            foreach (var dg in lstDonGia)
            {
                TongTienNhap += dg.TongTien.Value;
            }

            List<HoaDon> lstHD = DataProvider.Ins.DB.HoaDons.Where(hd => hd.NgayLap.Value.Month == thang
                                                                && hd.NgayLap.Value.Year == nam).ToList();
            if (_LstHoaDon == null)
            {
                _LstHoaDon = lstHD;
            }
            else
                LstHoaDon.AddRange(lstHD);
            foreach (var hd in lstHD)
                TongTienHD += hd.TongTien.Value;

            DoanhThu = TongTienHD - TongLuongNV - TongTienNhap;
        }
        #endregion
    }
}
