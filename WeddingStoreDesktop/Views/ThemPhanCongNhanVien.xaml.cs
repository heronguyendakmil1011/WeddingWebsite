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
using WeddingStoreDesktop.ViewModels;

namespace WeddingStoreDesktop.Views
{
    /// <summary>
    /// Interaction logic for ThemPhanCongNhanVien.xaml
    /// </summary>
    public partial class ThemPhanCongNhanVien : Window, IDialog
    {
        ThemPhanCongNhanVienViewModel vm;
        public ThemPhanCongNhanVien()
        {
            InitializeComponent();
            vm = Shared.myViewModelForAdd as ThemPhanCongNhanVienViewModel;
        }

        private void TrangTri_Drop(object sender, DragEventArgs e)
        {
            string maNV = (string)e.Data.GetData(DataFormats.StringFormat);
            vm.ThemNhanVienThamGiaTrangTri(maNV);
            lstNhanVienThamGiaTrangTri.Items.Refresh();
        }

        private void ThaoDo_Drop(object sender, DragEventArgs e)
        {
            string maNV = (string)e.Data.GetData(DataFormats.StringFormat);
            vm.ThemNhanVienThamGiaThaoDo(maNV);
            lstNhanVienThamGiaThaoDo.Items.Refresh();
        }

        private void nhanVien_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = (ListViewItem)sender;
            NhanVien myNV = (NhanVien)item.Content;
        }

        private void nhanVien_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (e.Source != null)
                {
                    NhanVien myNV = (NhanVien)lstNhanVien.SelectedItem;
                    DragDrop.DoDragDrop(lstNhanVien, myNV.MaNV, DragDropEffects.Move);
                }
            }
        }

        private void trangTri_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            vm.XoaPhanCongTrangTri();
            lstNhanVienThamGiaTrangTri.Items.Refresh();
        }

        private void thaoDo_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            vm.XoaPhanCongThaoDo();
            lstNhanVienThamGiaThaoDo.Items.Refresh();
        }
    }
}
