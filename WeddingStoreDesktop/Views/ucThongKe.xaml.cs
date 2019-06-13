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
    /// Interaction logic for ucThongKe.xaml
    /// </summary>
    public partial class ucThongKe : UserControl
    {
        ViewModels.ucThongKeViewModel vm;
        public ucThongKe()
        {
            InitializeComponent();
            vm = new ViewModels.ucThongKeViewModel();
            DataContext = vm;
        }

        private void thang_Checked(object sender, RoutedEventArgs e)
        {
            vm.op = true;
            gridTongQuat.Children.Clear();
            gridBieuDo.Children.Clear();
        }

        private void custom_Checked(object sender, RoutedEventArgs e)
        {
            vm.op = false;
            gridTongQuat.Children.Clear();
            gridBieuDo.Children.Clear();
        }

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            gridBieuDo.Children.Clear();
            gridTongQuat.Children.Clear();
            //if (vm.op)
            //{
            //    gridBieuDo.Children.Add(new ucBieuDoHoaDon(vm.Thang));
            //    gridTongQuat.Children.Add(new ucDoanhThuTongQuat(vm.Thang));
            //}
            //else
            //{
            //    gridBieuDo.Children.Add(new ucBieuDoHoaDon(vm.FromThang, vm.ToThang));
            //    gridTongQuat.Children.Add(new ucDoanhThuTongQuat(vm.FromThang, vm.ToThang));
            //}

            if (vm.op) // theo tháng
            {
                if (vm.Thang != null && int.TryParse(vm.nam, out int myNam))
                {
                    gridBieuDo.Children.Add(new ucBieuDoDoanhThu(vm.Thang, myNam));
                    gridTongQuat.Children.Add(new ucDoanhThuTongQuat(vm.Thang, myNam));
                }
                else
                    MessageBox.Show("Sai cú pháp. Mời nhập lại!!", "Fail!!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else // theo custom
            {
                if (vm.FromThang != null && vm.ToThang != null && int.TryParse(vm.nam, out int myNam))
                {
                    gridBieuDo.Children.Add(new ucBieuDoDoanhThu(vm.FromThang, vm.ToThang, myNam));
                    gridTongQuat.Children.Add(new ucDoanhThuTongQuat(vm.FromThang, vm.ToThang, myNam));
                }
                else
                    MessageBox.Show("Sai cú pháp. Mời nhập lại!!", "Fail!!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }
    }
}
