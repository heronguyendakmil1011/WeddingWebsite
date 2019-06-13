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
    /// Interaction logic for ucDoanhThuTongQuat.xaml
    /// </summary>
    public partial class ucDoanhThuTongQuat : UserControl
    {
        ViewModels.ucDoanhThuTongQuatViewModel vm;
        public ucDoanhThuTongQuat(string thang, int nam)
        {
            InitializeComponent();
            vm = new ViewModels.ucDoanhThuTongQuatViewModel(thang, nam);
            DataContext = vm;
        }

        public ucDoanhThuTongQuat(string from, string to, int nam)
        {
            InitializeComponent();
            vm = new ViewModels.ucDoanhThuTongQuatViewModel(from, to, nam);
            DataContext = vm;
        }
    }
}
