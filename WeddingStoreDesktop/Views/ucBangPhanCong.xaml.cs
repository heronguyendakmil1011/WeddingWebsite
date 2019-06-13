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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeddingStoreDesktop.Converters;
using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Models.SystemModel;

namespace WeddingStoreDesktop.Views
{
    /// <summary>
    /// Interaction logic for ucBangPhanCong.xaml
    /// </summary

    public partial class ucBangPhanCong : UserControl
    {
        ViewModels.ucBangPhanCongViewModel vm;
        public ucBangPhanCong()
        {
            InitializeComponent();
            vm = new ViewModels.ucBangPhanCongViewModel();
            DataContext = vm;
            vm.theoNgay = false;
            vm.theoThang = false;
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
                    if (myNV != null)
                        DragDrop.DoDragDrop(lstNhanVien, myNV.MaNV, DragDropEffects.Move);
                }
            }
        }

        private void nhanVienThamGia_Drop(object sender, DragEventArgs e)
        {
            //var myNV = e.Data.GetData("WeddingStoreData.Models.NhanVienModel") as NhanVien;
            string maNV = (string)e.Data.GetData(DataFormats.StringFormat);
            vm.ThemNhanVienThamGia(maNV);
            lstNhanVienThamGia.Items.Refresh();
        }

        private void nhanVienThamGia_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            vm.XoaNhanVienThamGia();
            lstNhanVienThamGia.Items.Refresh();
        }

        private void theoThang_Checked(object sender, RoutedEventArgs e)
        {
            vm.theoThang = true;
            vm.theoNgay = false;
        }

        private void theoNgay_Checked(object sender, RoutedEventArgs e)
        {
            vm.theoThang = false;
            vm.theoNgay = true;
        }
    }

    [ValueConversion(typeof(bool), typeof(SolidColorBrush))]
    public class BoolToBrushConverter : BoolToValueConverter<Brush> { }
}
