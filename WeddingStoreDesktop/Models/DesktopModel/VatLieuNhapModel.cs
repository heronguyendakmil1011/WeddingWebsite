using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingStoreDesktop.Models.DesktopModel
{
    public class VatLieuNhapModel
    {
        public Byte[] AnhMoTa { get; set; }
        public string MaVL { get; set; }
        public string TenVL { get; set; }
        public int? SoLuong { get; set; }
        public float? ThanhTien { get; set; }
    }
}
