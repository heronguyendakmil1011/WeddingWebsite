using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingStoreDesktop.Models.DesktopModel;

namespace WeddingStoreDesktop.Services
{
    public class MockTinhNangRepository
    {
        private List<TinhNangModel> _lstTinhNang;

        public MockTinhNangRepository()
        {
            _lstTinhNang = new List<TinhNangModel>()
            {
                new TinhNangModel{id=1,icon="/WeddingStoreDesktop;component/Images/TinhNang/them.png",ChucNang="Thêm"},
                new TinhNangModel{id=2,icon="/WeddingStoreDesktop;component/Images/TinhNang/hoadon.png",ChucNang="Hóa Đơn"},
                new TinhNangModel{id=3,icon="/WeddingStoreDesktop;component/Images/TinhNang/nhanvien.png",ChucNang="Nhân Viên"},
                new TinhNangModel{id=4,icon="/WeddingStoreDesktop;component/Images/TinhNang/vatlieu.png",ChucNang="Vật Liệu"},
                new TinhNangModel{id=6,icon="/WeddingStoreDesktop;component/Images/TinhNang/bangphancong.png",ChucNang="Bảng Phân Công"},
                new TinhNangModel{id=7,icon="/WeddingStoreDesktop;component/Images/TinhNang/mautrangtri.png",ChucNang="Mẫu Trang Trí"},
                new TinhNangModel{id=10,icon="/WeddingStoreDesktop;component/Images/TinhNang/hoadonnhap.png",ChucNang="Hóa Đơn Nhập"},
                new TinhNangModel{id=9,icon="/WeddingStoreDesktop;component/Images/TinhNang/thongke.png",ChucNang="Thống Kê"},
            };
        }

        public List<TinhNangModel> GetTinhNang()
        {
            return _lstTinhNang;
        }
    }
}
