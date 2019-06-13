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

namespace WeddingStoreDesktop.Views
{
    /// <summary>
    /// Interaction logic for DangNhap.xaml
    /// </summary>
    public partial class DangNhap : Window
    {
        ViewModels.DangNhapViewModel vm;
        public DangNhap()
        {
            InitializeComponent();
            vm = new ViewModels.DangNhapViewModel();
            DataContext = vm;

        }

        private void Thoat_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            //vm.ahihi();
        }
    }
}
