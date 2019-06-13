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
    /// Interaction logic for ucPhatSinh.xaml
    /// </summary>
    public partial class ucPhatSinh : UserControl
    {
        ViewModels.ucPhatSinhViewModel vm;
        public ucPhatSinh(string maHD,bool canEdit)
        {
            InitializeComponent();
            vm = new ViewModels.ucPhatSinhViewModel(maHD);
            DataContext = vm;

            if (!canEdit)
                moreOption.Visibility = Visibility.Collapsed;
        }

        private void PhatSinh_Click(object sender, MouseButtonEventArgs e)
        {
            vm.ModifyCommand.Execute(null);
            lstPhatSinh.Items.Refresh();
        }
    }
}
