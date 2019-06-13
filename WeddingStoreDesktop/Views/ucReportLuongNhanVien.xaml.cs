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
    /// Interaction logic for ucReportLuongNhanVien.xaml
    /// </summary>
    public partial class ucReportLuongNhanVien : UserControl
    {
        public ucReportLuongNhanVien(string maNV,int thang,int nam)
        {
            InitializeComponent();
            DataContext = new ucReportLuongNhanVienViewModel(this, maNV, thang, nam);
        }
    }
}
