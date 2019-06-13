using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingStoreDesktop.Models.DesktopModel
{
    public class LuongNhanVienModel
    {
        public Byte[] Avata { get; set; }
        public string MaNV { get; set; }
        public string TenNV { get; set; }
        public TimeSpan TongThoiGian { get; set; }
        public float TongTien { get; set; }
    }
}
