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
    /// Interaction logic for DashBroad.xaml
    /// </summary>
    public partial class DashBroad : Window
    {
        private ViewModels.HomeViewModel vm;
        public DashBroad()
        {
            InitializeComponent();
            vm = new ViewModels.HomeViewModel();
            this.DataContext = vm;
        }

        private void itemSelected(object sender, SelectionChangedEventArgs e)
        {
            vm.CheckDashBroad();
            this.Close();
        }
    }
}
