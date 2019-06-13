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
using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Models.DesktopModel;

namespace WeddingStoreDesktop.Views
{
    /// <summary>
    /// Interaction logic for ucPhanCongNhanVien.xaml
    /// </summary>
    public partial class ucPhanCongNhanVien : UserControl
    {
        ViewModels.ucPhanCongNhanVienViewModel vm;
        public ucPhanCongNhanVien(HoaDonKhachHangModel HdKh)
        {
            InitializeComponent();
            vm = new ViewModels.ucPhanCongNhanVienViewModel(HdKh);
            DataContext = vm;
        }
    }
}
