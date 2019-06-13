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
using WeddingStoreDesktop.ViewModels;

namespace WeddingStoreDesktop.Views
{
    /// <summary>
    /// Interaction logic for ThemChiTietHD.xaml
    /// </summary>
    public partial class ThemChiTietHD : Window, IDialog
    {
        ThemChiTietHDViewModel myVm = Shared.myViewModelForAdd as ThemChiTietHDViewModel;
        public ThemChiTietHD()
        {
            InitializeComponent();
        }

        private void SanPhamHD_Click(object sender, MouseButtonEventArgs e)
        {
            myVm.SanPhamInHD_Click();
            lstChiTiet.Items.Refresh();
        }

        private void SanPham_Click(object sender, MouseButtonEventArgs e)
        {
            myVm.SanPham_Click();
            lstChiTiet.Items.Refresh();
        }
    }
}
