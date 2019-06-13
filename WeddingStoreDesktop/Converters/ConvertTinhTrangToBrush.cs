using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace WeddingStoreDesktop.Converters
{
    [ValueConversion(typeof(int), typeof(SolidColorBrush))]
    public class ConvertTinhTrangToBrush : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int tinhTrang = (int)value;
            if (tinhTrang == 0)
                return new SolidColorBrush(Colors.Aqua);
            else
            {
                if (tinhTrang == 1)
                    return new SolidColorBrush(Colors.Green);
                else
                    if (tinhTrang == 2)
                    return new SolidColorBrush(Colors.Gray);
            }
            return new SolidColorBrush(Colors.GreenYellow);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
