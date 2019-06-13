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
using WeddingStoreDesktop.Interfaces.Dialog;
using WeddingStoreDesktop.ViewModels;

namespace WeddingStoreDesktop.Views
{
    /// <summary>
    /// Interaction logic for ThemPhatSinh.xaml
    /// </summary>
    public partial class ThemPhatSinh : Window, IDialog
    {
        ThemPhatSinhViewModel myVm;
        public ThemPhatSinh()
        {
            InitializeComponent();
            myVm = Shared.myViewModelForAdd as ThemPhatSinhViewModel;
        }

        private void DSVatLieu_Click(object sender, MouseButtonEventArgs e)
        {
            myVm.AddCommand.Execute(null);
            lstPhatSinh.Items.Refresh();
        }

        private void PhatSinh_Click(object sender, MouseButtonEventArgs e)
        {
            myVm.ModifyCommand.Execute(null);
            lstPhatSinh.Items.Refresh();
        }
    }
}
