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
    /// Interaction logic for ucThongTinSanPham.xaml
    /// </summary>
    public partial class ucThongTinSanPham : UserControl
    {
        ViewModels.ucThongTinSanPhamViewModel vm;
        public ucThongTinSanPham(string maSP)
        {
            InitializeComponent();
            vm = new ViewModels.ucThongTinSanPhamViewModel(maSP);
            DataContext = vm;
        }
    }
}
