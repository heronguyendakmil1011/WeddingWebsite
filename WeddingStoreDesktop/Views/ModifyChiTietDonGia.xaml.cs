using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WeddingStoreDesktop.Interfaces.Dialog;
using WeddingStoreDesktop.Models;

namespace WeddingStoreDesktop.Views
{
    /// <summary>
    /// Interaction logic for ModifyChiTietDonGia.xaml
    /// </summary>
    public partial class ModifyChiTietDonGia : Window,IDialog
    {
        ViewModels.ModifyChiTietDonGiaViewModel vm;
        public ModifyChiTietDonGia()
        {
            InitializeComponent();
            vm = Shared.myViewModelForModify as ViewModels.ModifyChiTietDonGiaViewModel;
            gridThongTin.Children.Add(new ucThongTinVatLieu(vm.maVL));
        }
    }
}
