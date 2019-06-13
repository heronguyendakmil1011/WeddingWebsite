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

using WeddingStoreDesktop.ViewModels;
using WeddingStoreDesktop.Interfaces.Dialog;

namespace WeddingStoreDesktop.Views
{
    /// <summary>
    /// Interaction logic for ModifySoLuong.xaml
    /// </summary>
    public partial class ModifySoLuong : Window, IDialog
    {
        ModifySoLuongViewModel myVm = Shared.myViewModelForModify as ModifySoLuongViewModel;
        public ModifySoLuong()
        {
            InitializeComponent();
            switch(Shared.requestModify)
            {
                case 1:
                    gridThongTin.Children.Add(new ucThongTinSanPham(myVm.thongTinMau.MaSP));
                    break;
                case 2:
                    gridThongTin.Children.Add(new ucThongTinVatLieu(myVm.thongTinPhatSinh.MaVL));
                    break;
                case 3:
                    gridThongTin.Children.Add(new ucThongTinSanPham(myVm.sanPham.MaSP));
                    break;
                case 4:
                    gridThongTin.Children.Add(new ucThongTinVatLieu(myVm.vatLieu.MaVL));
                    break;
                case 5:
                    gridThongTin.Children.Add(new ucThongTinVatLieu(myVm.vlChiTietMau.MaVL));
                    break;
            }
            txtSoLuong.SelectAll();
            txtSoLuong.Focus();
        }
    }
}
