using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingStoreDesktop.Models.SystemModel;

namespace WeddingStoreDesktop.Models.DesktopModel
{
    public class SanPhamByDichVuModel
    {
        public string MaDV { get; set; }
        public string TenDV { get; set; }
        public string LoaiDV { get; set; }
        public List<SanPham> lstSanPham { get; set; } = new List<SanPham>();
    }
}
