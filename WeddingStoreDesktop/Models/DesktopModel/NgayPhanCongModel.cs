using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingStoreDesktop.Models.SystemModel;

namespace WeddingStoreDesktop.Models.DesktopModel
{
    public class NgayPhanCongModel : List<HoaDon>
    {
        //public string Ngay { get; set; }
        //public int HDCount { get; set; }

        //public NgayPhanCong(string ngay, int hDCount, List<HoaDonModel> lstHD)
        //{
        //    Ngay = ngay;
        //    HDCount = HDCount;
        //    this.AddRange(lstHD);
        //}

        //public string Ngay { get; set; }
        //public List<HoaDonModel> LstHD { get; set; }

        public string Ngay { get; set; }
        public List<ThongTinNgayModel> LstThongTinNgay { get; set; }
    }
}
