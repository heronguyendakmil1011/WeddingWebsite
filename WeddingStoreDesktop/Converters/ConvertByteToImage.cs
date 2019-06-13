using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WeddingStoreDesktop.Converters
{
    [ValueConversion(typeof(byte[]),typeof(Image))]
    public class ConvertByteToImage : IValueConverter
    {
        //public static Image ConvertToImage(System.Data.Linq.Binary iBinary)
        //{
        //    var arrayBinary = iBinary.ToArray();
        //    Image rImage = null;

        //    using (MemoryStream ms = new MemoryStream(arrayBinary))
        //    {
        //        rImage = Image.FromStream(ms);
        //    }
        //    return rImage;
        //}

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Image returnImage = null;
            ImageSource my = null;

            // conver object to binary
            byte[] arrBinary = Converters.ConvertObjectToBinary.ObjectToByteArray(value);

            using (MemoryStream ms = new MemoryStream(value as byte[]))
            {
                returnImage = Image.FromStream(ms);
            }
            return returnImage;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
