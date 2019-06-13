using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using WeddingStoreDesktop.Interfaces.Dialog;
using WeddingStoreDesktop.ViewModels;

namespace WeddingStoreDesktop.Views
{
    /// <summary>
    /// Interaction logic for ReportHoaDon.xaml
    /// </summary>
    public partial class ReportHoaDon : Window, IDialog
    {
        ReportHoaDonViewModel vm;
        public ReportHoaDon()
        {
            InitializeComponent();
            vm = Shared.myViewModelForModify as ReportHoaDonViewModel;
            switch (vm.idRequest)
            {
                case 1:
                    ahihi.Children.Add(new ucTestReport(vm.maHD));
                    break;
                case 2:
                    ahihi.Children.Add(new ucReportLuongNhanVien(vm.maNV, vm.thang, vm.nam));
                    break;
            }
        }

        //private void Pay_Click(object sender, RoutedEventArgs e)
        //{
        //    //Grid testGrid = myGrid;
        //    //ahihi.Children.Remove(myGrid);
        //    //vm.ahihi(testGrid);

        //    PrintDialog dlg = new PrintDialog();

        //    //FixedDocument fixedDoc = new FixedDocument();
        //    //PageContent pageContent = new PageContent();
        //    //FixedPage fixedPage = new FixedPage();
        //    //var pageSize = new Size(8 * 96, 11 * 96);

        //    //myGrid.Width = pageSize.Width;
        //    //myGrid.UpdateLayout();

        //    ////Create first page of document
        //    ////fixedPage.Children.Add(view);
        //    //ahihi.Children.Remove(myGrid);
        //    //fixedPage.Children.Add(myGrid);
        //    //((System.Windows.Markup.IAddChild)pageContent).AddChild(fixedPage);
        //    //fixedDoc.Pages.Add(pageContent);

        //    //dlg.PrintVisual(myGrid, "Hóa đơn " + vm.myKH.TenKH);
        //}
    }
}
