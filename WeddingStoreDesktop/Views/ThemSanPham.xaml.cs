using Microsoft.Win32;
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
using WeddingStoreDesktop.Models.SystemModel;

namespace WeddingStoreDesktop.Views
{
    /// <summary>
    /// Interaction logic for ThemSanPham.xaml
    /// </summary>
    public partial class ThemSanPham : Window, IDialog
    {
        ViewModels.ThemSanPhamViewModel vm;
        public ThemSanPham()
        {
            InitializeComponent();
            vm = Shared.myViewModelForAdd as ViewModels.ThemSanPhamViewModel;
        }

        private void vatLieu_MouseLeftDown(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = (ListViewItem)sender;
            KhoVatLieu myVl = (KhoVatLieu)item.Content;
        }

        private void vatLieu_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (e.Source != null)
                {
                    KhoVatLieu myVL = (KhoVatLieu)lstVatLieu.SelectedItem;
                    if (myVL != null)
                        DragDrop.DoDragDrop(lstVatLieu, myVL.MaVL, DragDropEffects.Move);
                }
            }
        }

        private void lstChiTiet_Drop(object sender, DragEventArgs e)
        {
            //var myVl = e.Data.GetData("WeddingStoreDesktop.Models.SystemModel.KhoVatLieu") as KhoVatLieu;

            string maVL = (string)e.Data.GetData(DataFormats.StringFormat);
            vm.TestDrag(maVL);
            gridChiTiet.Items.Refresh();
        }

        private void vatLieuChiTietMau_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            vm.vatLieuChiTietMauDoubleClick();
            gridChiTiet.Items.Refresh();
        }

        private void ChooseImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            vm.ChooseAvata();
        }
    }
}
