using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeddingStoreDesktop.Interfaces.Dialog;
using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Functions;
using System.Windows.Input;
using WeddingStoreDesktop.Models.DesktopModel;
using WeddingStoreDesktop.Services.DesktopService;
using WeddingStoreDesktop.Models.SystemModel;
using System.Windows.Xps.Packaging;
using System.Windows.Documents;
using Microsoft.Win32;
using System.Drawing;
using System.Windows.Xps;
using System.IO;
using WeddingStoreDesktop.Views;
using System.Windows.Controls;

namespace WeddingStoreDesktop.ViewModels
{
    public class ReportHoaDonViewModel : BaseViewModel, IDialogRequestClose
    {
        public string maHD;
        public string maKH;

        public int idRequest;

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        public ReportHoaDonViewModel(string maHD, string maKH)
        {
            idRequest = 1; // Report hóa đơn
            this.maHD = maHD;
            this.maKH = maKH;

            PayCommand = new ActionCommand(p => ahihi());

            SaveCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true)));
            CancelCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false)));
        }

        public string maNV;
        public int thang;
        public int nam;
        public ReportHoaDonViewModel(string maNV,int thang,int nam)
        {
            idRequest = 2;
            this.maNV = maNV;
            this.thang = thang;
            this.nam = nam;

            PayCommand = new ActionCommand(p => ahihi());
            SaveCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true)));
            CancelCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false)));
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand PayCommand { get; }

        //public void ahihi()
        //{
        //    SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
        //    dlg.FileName = String.Format("MyFile{0:ddMMyyyyHHmmss}", DateTime.Now); // Default file name
        //    dlg.DefaultExt = ".xps"; // Default file extension
        //    dlg.Filter = "XPS Documents (.xps)|*.xps"; // Filter files by extension

        //    // Show save file dialog box
        //    Nullable<bool> result = dlg.ShowDialog();

        //    // Process save file dialog box results
        //    if (result == true)
        //    {
        //        // Save document
        //        string filename = dlg.FileName;

        //        // Initialize the xps document structure
        //        FixedDocument fixedDoc = new FixedDocument();
        //        PageContent pageContent = new PageContent();
        //        FixedPage fixedPage = new FixedPage();

        //        // Create the document and set the datacontext
        //        //ReportHoaDon view = new ReportHoaDon();
        //        //view.DataContext = this;

        //        //DangNhap view = new DangNhap();
        //        //view.DataContext = new DangNhapViewModel();
        //        ucReportHoaDon view = new ucReportHoaDon(maHD, maKH, 30000000);
        //        view.UpdateLayout();

        //        // Get the page size of an A4 document
        //        var pageSize = new Size(8 * 80, 11 * 96);

        //        // We just fit it horizontally, so only the width is set here
        //        //view.Height = pageSize.Height;
        //        view.Width = pageSize.Width;
        //        view.UpdateLayout();

        //        //Create first page of document
        //        fixedPage.Children.Add(view);
        //        ((System.Windows.Markup.IAddChild)pageContent).AddChild(fixedPage);
        //        fixedDoc.Pages.Add(pageContent);

        //        // Create the xps file and write it
        //        XpsDocument xpsd = new XpsDocument(filename, FileAccess.ReadWrite);
        //        XpsDocumentWriter xw = XpsDocument.CreateXpsDocumentWriter(xpsd);
        //        xw.Write(fixedDoc);
        //        xpsd.Close();

        //        CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true));
        //    }
        //}

        void ahihi()
        {
            PrintDialog printDlg = new PrintDialog();
            printDlg.PrintVisual(new ucTestReport(maHD),"Hóa đơn");
        }
    }
}
