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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WeddingStoreDesktop.Views
{
    /// <summary>
    /// Interaction logic for ucThongTinHoaDon.xaml
    /// </summary>
    public partial class ucThongTinHoaDon : UserControl
    {
        ViewModels.ucThongTinHoaDonViewModel vm;
        public ucThongTinHoaDon(string maHD,string maKH)
        {
            InitializeComponent();
            vm = new ViewModels.ucThongTinHoaDonViewModel(maHD, maKH);

            DataContext = vm;
        }

        private void ucThongTin_Load(object sender, RoutedEventArgs e)
        {
            DoubleAnimation animation1 = new DoubleAnimation(0,1,TimeSpan.FromSeconds(2));
            DoubleAnimation animation2 = new DoubleAnimation(0,1,TimeSpan.FromSeconds(2));

            grbKhachHang.BeginAnimation(OpacityProperty, animation1);
            grbHoaDon.BeginAnimation(OpacityProperty, animation2);
        }
    }
}
