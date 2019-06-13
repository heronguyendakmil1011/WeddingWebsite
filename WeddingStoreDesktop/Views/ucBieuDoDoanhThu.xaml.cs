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
using System.Windows.Shapes;

using WeddingStoreDesktop.ViewModels;

namespace WeddingStoreDesktop.Views
{
    /// <summary>
    /// Interaction logic for ucBieuDoDoanhThu.xaml
    /// </summary>
    public partial class ucBieuDoDoanhThu : UserControl
    {
        ucBieuDoDoanhThuViewModel vm;
        public ucBieuDoDoanhThu(string thang,int nam)
        {
            InitializeComponent();
            vm = new ucBieuDoDoanhThuViewModel(thang,nam);
            DataContext = vm;
        }

        public ucBieuDoDoanhThu(string from,string to,int nam)
        {
            InitializeComponent();
            vm = new ucBieuDoDoanhThuViewModel(from,to,nam);
            DataContext = vm;
        }
    }
}
