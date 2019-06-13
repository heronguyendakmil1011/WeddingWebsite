using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace WeddingStoreDesktop.Converters
{
    [ValueConversion(typeof(bool),typeof(Visibility))]
    public class BoolToVisibilityConverter : BoolToValueConverter<Visibility>
    {
    }
}
