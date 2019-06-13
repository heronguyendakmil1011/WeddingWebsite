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

using WeddingStoreDesktop.ViewModels;

namespace WeddingStoreDesktop.Views
{
    /// <summary>
    /// Interaction logic for ucReportHoaDon.xaml
    /// </summary>
    public partial class ucReportHoaDon : UserControl
    {
        ucReportHoaDonViewModel vm;
        public ucReportHoaDon(string maHD,string maKH,float tienDua)
        {
            InitializeComponent();
            vm = new ucReportHoaDonViewModel(maHD, maKH, tienDua);
            DataContext = vm;
        }
    }
}
