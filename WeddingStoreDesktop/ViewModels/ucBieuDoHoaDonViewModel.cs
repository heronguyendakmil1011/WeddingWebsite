using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Functions;
using WeddingStoreDesktop.Models.SystemModel;
using WeddingStoreDesktop.Models.DesktopModel;
using WeddingStoreDesktop.Services.DesktopService;

namespace WeddingStoreDesktop.ViewModels
{
    public class ucBieuDoHoaDonViewModel : BaseViewModel
    {
        private List<HoaDon> _LstHoaDon { get; set; }
        public List<HoaDon> LstHoaDon
        {
            get => _LstHoaDon;
            set
            {
                if(_LstHoaDon!=value)
                {
                    _LstHoaDon = value;
                    OnPropertyChanged();
                }
            }
        }

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

        private string _thang;
        private string _from;
        private string _to;

        public Dictionary<string, float> dicVatLieuNhap = new Dictionary<string, float>();
        public Dictionary<string, float> dicLuongNhanVien = new Dictionary<string, float>();
        public Dictionary<string, float> dicHoaDon = new Dictionary<string, float>();

        public ucBieuDoHoaDonViewModel(string thang)
        {
            _thang = thang;
            GetData();
        }

        public ucBieuDoHoaDonViewModel(string fromThang,string toThang)
        {
            _from = fromThang;
            _to = toThang;

            GetByCustom();
        }

        void GetData()
        {
            _LstHoaDon = DataProvider.Ins.DB.HoaDons.ToList();
        }

        void GetByCustom()
        {
            if (_from == null || _to == null)
            {
                MessageBox.Show("Chọn tháng pleaseeeeeeee");
            }
            else
            {
                string[] fromThang = _from.Split(' ');
                int from = int.Parse(fromThang[1]);

                string[] toThang = _to.Split(' ');
                int to = int.Parse(toThang[1]);

                if (from > to)
                    MessageBox.Show("Nooooo!!! Wrong");
                else
                {
                    for (int i = from; i <= to; i++)
                    {
                        List<HoaDon> lstHoaDon = DataProvider.Ins.DB.HoaDons.Where(hd => hd.NgayLap.Value.Month == i).ToList();
                        float tien = 0;
                        foreach(var hd in lstHoaDon)
                        {
                            tien += hd.TongTien.Value;
                        }
                        dicHoaDon.Add("Tháng " + i, tien);
                        tien = 0;

                        LuongNhanVienService luongNV = new LuongNhanVienService();
                        List<LuongNhanVienModel> lstLuongNV = luongNV.GetLuongNVByThang(i);
                        foreach (var luong in lstLuongNV)
                            tien += luong.TongTien;
                        dicLuongNhanVien.Add("Tháng " + i, tien);
                        tien = 0;

                        VatLieuNhapService vatLieuNhap = new VatLieuNhapService();
                        List<VatLieuNhapModel> lstVatLieuNhap = vatLieuNhap.GetVatLieuNhapByThang(i);
                        foreach (var vl in lstVatLieuNhap)
                            tien += vl.ThanhTien.Value;
                        dicVatLieuNhap.Add("Tháng " + i, tien);
                    }
                }
            }
        }
    }
}
