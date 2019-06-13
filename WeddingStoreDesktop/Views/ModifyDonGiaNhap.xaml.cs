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
    /// Interaction logic for ModifyDonGiaNhap.xaml
    /// </summary>
    public partial class ModifyDonGiaNhap : Window, IDialog
    {
        ViewModels.ModifyDonGiaViewModel vm;
        public ModifyDonGiaNhap()
        {
            InitializeComponent();
            vm = Shared.myViewModelForAdd as ViewModels.ModifyDonGiaViewModel;
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

        private void ChiTiet_Drop(object sender, DragEventArgs e)
        {
            string maVL = (string)e.Data.GetData(DataFormats.StringFormat);
            //vm.LstChiTiet.Add(new Models.ThongTinVatLieuChiTietMau
            //{
            //    MaVL = myVl.MaVL,
            //    TenVL = myVl.TenVL,
            //    SoLuong = 2,
            //    DonVi = myVl.DonVi,
            //    ThanhTien = 2 * myVl.GiaTien
            //});
            vm.AddVatLieu(maVL);
            dgChiTiet.Items.Refresh();
        }

        private void chiTiet_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            vm.ModifyChiTietDG();
            dgChiTiet.Items.Refresh();
        }
    }
}
