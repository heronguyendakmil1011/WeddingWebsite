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

using WeddingStoreDesktop.Converters;

namespace WeddingStoreDesktop.Views
{
    /// <summary>
    /// Interaction logic for ucVatLieu.xaml
    /// </summary>
    public partial class ucVatLieu : UserControl
    {
        ViewModels.ucVatLieuViewModel vm;
        public ucVatLieu()
        {
            InitializeComponent();
            vm = new ViewModels.ucVatLieuViewModel();
            DataContext = vm;
        }
    }
}
