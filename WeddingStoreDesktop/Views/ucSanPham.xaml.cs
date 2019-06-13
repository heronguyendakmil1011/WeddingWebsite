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

namespace WeddingStoreDesktop.Views
{
    /// <summary>
    /// Interaction logic for ucSanPham.xaml
    /// </summary>
    public partial class ucSanPham : UserControl
    {
        ViewModels.ucSanPhamViewModel vm;
        public ucSanPham()
        {
            InitializeComponent();
            vm = new ViewModels.ucSanPhamViewModel();
            DataContext = vm;
            //lvSanPham.ItemsSource = vm.LstSanPham;
            //CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvSanPham.ItemsSource);
            //PropertyGroupDescription myGroup = new PropertyGroupDescription("DichVu");
            //view.GroupDescriptions.Add(myGroup);
        }

        private void SanPham_Click(object sender, MouseButtonEventArgs e)
        {
            gridDetail.Children.Clear();
            gridDetail.Children.Add(new ucThongTinSanPham(vm.SelectedSanPham.MaSP));
        }
    }
}
