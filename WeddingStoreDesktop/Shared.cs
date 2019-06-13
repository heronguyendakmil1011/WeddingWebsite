using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingStoreDesktop.Interfaces.Dialog;

namespace WeddingStoreDesktop
{
    public class Shared
    {
        public static IDialogService dialogService;
        public static object myViewModelForModify;
        public static object myViewModelForAdd;
        public static int requestModify;
        public static string CurrentTenNV;
        public static bool IsChangedAo;
    }
}
