using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Drawing;
using System.Resources;

namespace WeddingStoreDesktop.Converters
{
    [ValueConversion(typeof(string), typeof(string))]
    public class ConvertToImage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //return "/WeddingStoreDesktop;component/Images/Avatar/NV-1_avatar.jpg" + value;
            string format = value as string;
            if (!string.IsNullOrEmpty(format)) // kiểm tra có phải thêm nhân viên mới ko
            {
                if (WeddingStoreData.WeddingStoreResource.ResourceManager.GetObject(format) == null)
                {

                    return "/WeddingStoreDesktop;component/Images/noimage.png";

                }
                return WeddingStoreData.WeddingStoreResource.ResourceManager.GetObject(format);
            }
            return WeddingStoreData.WeddingStoreResource.noimage;

            //if (!string.IsNullOrEmpty(format)) // kiểm tra có phải thêm nhân viên mới ko
            //{
            //    return "pack://application:,,,/WeddingStoreData;component/Images/noimage.png";
            //}
            //return "pack://application:,,,/WeddingStoreData;component/Images/Avatar/" + format;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
