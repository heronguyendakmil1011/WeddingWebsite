using System;
using System.Collections.Generic;
using System.ComponentModel;
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

using WeddingStoreDesktop.ViewModels;

namespace WeddingStoreDesktop.Views
{
    /// <summary>
    /// Interaction logic for ucThem.xaml
    /// </summary>
    public partial class ucThem : UserControl
    {
        private ucThemViewModel vm;
        public ucThem()
        {
            InitializeComponent();
            vm = new ucThemViewModel();
            DataContext = vm;
        }

        private void SanPham_Click(object sender, MouseButtonEventArgs e)
        {
            vm.SanPhamClick();
            lstMau.Items.Refresh();
        }

        private void VatLieu_Click(object sender, MouseButtonEventArgs e)
        {
            vm.VatLieuClick();
            lstPhatSinh.Items.Refresh();
        }

        private void Mau_Click(object sender, MouseButtonEventArgs e)
        {
            vm.MauClick();
            lstMau.Items.Refresh();
        }

        private void PhatSinh_Click(object sender, MouseButtonEventArgs e)
        {
            vm.PhatSinhClick();
            lstPhatSinh.Items.Refresh();
        }

        private void Refresh_CLick(object sender, RoutedEventArgs e)
        {
            lstMau.Items.Refresh();
            lstPhatSinh.Items.Refresh();
            vm.RefreshCommand.Execute(null);
        }

        private void SaveHDKH_Click(object sender, RoutedEventArgs e)
        {
            lstMau.Items.Refresh();
            lstPhatSinh.Items.Refresh();
            vm.SaveHDKHCommand.Execute(null);
        }
    }
}
