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

namespace WeddingStoreDesktop.Views
{
    /// <summary>
    /// Interaction logic for ucHoaDon.xaml
    /// </summary>
    public partial class ucHoaDon : UserControl
    {
        ViewModels.ucHoaDonViewModel vm;
        public ucHoaDon()
        {
            InitializeComponent();
            vm = new ViewModels.ucHoaDonViewModel();
            DataContext = vm;

        }

        void CleanGrid()
        {
            //gridThongTin.Children.Clear();
            gridPhanCong.Children.Clear();
            gridChiTietHoaDon.Children.Clear();
            gridPhatSinh.Children.Clear();
        }

        private void HoaDon_CLick(object sender, SelectionChangedEventArgs e)
        {
            CleanGrid();
            //gridThongTin.Children.Add(new ucThongTinHoaDon(vm.SelectedHDKH.MaHD,vm.SelectedHDKH.MaKH,false));
            if (vm.SelectedHDKH != null)
            {
                gridPhanCong.Children.Add(new ucPhanCongNhanVien(vm.SelectedHDKH));
                gridChiTietHoaDon.Children.Add(new ucChiTietHoaDon(vm.SelectedHDKH.MaHD));
                gridPhatSinh.Children.Add(new ucPhatSinh(vm.SelectedHDKH.MaHD, true));
            }
        }
    }
}
