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
using WeddingStoreDesktop.Functions;
using WeddingStoreDesktop.Models.SystemModel;

namespace WeddingStoreDesktop.Views
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Window
    {
        private ViewModels.HomeViewModel vm;
        public HomePage(TaiKhoan myTaiKhoan)
        {
            InitializeComponent();
            vm = new ViewModels.HomeViewModel(myTaiKhoan);
            this.DataContext = vm;

            InitializeView();
        }

        public HomePage(int idTinhNang)
        {
            InitializeComponent();
            vm = new ViewModels.HomeViewModel(null);
            this.DataContext = vm;

            InitializeView();

            gridMain.Children.Clear();
            UserControl uc = CheckTinhNang.Check(idTinhNang);
            gridMain.Children.Add(uc);

            InitializeView();
        }

        void InitializeView()
        {

            btnCloseMenu.Visibility = Visibility.Collapsed;
            btnOpenMenu.Visibility = Visibility.Visible;

            lstMenuName.Visibility = Visibility.Collapsed;
            lstMenu.Visibility = Visibility.Visible;
        }

        private void btnOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            btnCloseMenu.Visibility = Visibility.Visible;
            btnOpenMenu.Visibility = Visibility.Collapsed;

            lstMenuName.Visibility = Visibility.Visible;
            lstMenu.Visibility = Visibility.Collapsed;
        }

        private void btnCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            btnCloseMenu.Visibility = Visibility.Collapsed;
            btnOpenMenu.Visibility = Visibility.Visible;

            lstMenuName.Visibility = Visibility.Collapsed;
            lstMenu.Visibility = Visibility.Visible;
        }

        private void itemSelected(object sender, SelectionChangedEventArgs e)
        {
            UserControl uc = null;
            gridMain.Children.Clear();
            uc = CheckTinhNang.Check(vm.tinhNang.id);
            gridMain.Children.Add(uc);
        }
    }
}
