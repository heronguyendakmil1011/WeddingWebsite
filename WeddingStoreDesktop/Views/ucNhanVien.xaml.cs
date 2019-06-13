using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for ucNhanVien.xaml
    /// </summary>
    public partial class ucNhanVien : UserControl
    {
        ViewModels.ucNhanVienViewModel vm;
        public ucNhanVien()
        {
            InitializeComponent();
            vm = new ViewModels.ucNhanVienViewModel();
            DataContext = vm;
        }

        void CleanGrid()
        {
            gridThongTinNhanVien.Children.Clear();
            gridChamCong.Children.Clear();
        }
    }
}
