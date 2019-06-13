using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingStoreDesktop.Functions
{
    public class Tinh
    {
        public static string TinhGioCong(string thoiGianDen, string thoiGianDi)
        {
            TimeSpan gioCong = DateTime.Parse(thoiGianDi).Subtract(DateTime.Parse(thoiGianDen));
            return gioCong.ToString();
        }

        public static float TinhTongTien(string gioCong, float luong)
        {
            string[] gc = gioCong.Split(':');
            return (luong * float.Parse(gc[0])) + (luong / 60 * float.Parse(gc[1]));
        }
    }
}
