using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingStoreDesktop.Converters
{
    public class ConvertDateTimeToString
    {
        public static string ConverToMyDateFormat(DateTime? date)
        {
            return date.HasValue ? date.Value.ToString("dd/MM/yyyy") : String.Empty;
        }
    }
}
