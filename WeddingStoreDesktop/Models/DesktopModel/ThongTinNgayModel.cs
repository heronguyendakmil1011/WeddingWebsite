using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingStoreDesktop.Models.DesktopModel
{
    public class ThongTinNgayModel
    {
        public string MaHD { get; set; }
        public string TenKH { get; set; }
        public bool type { get; set; }  // true: trang trí
                                        // false: tháo dở
    }
}
