using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using WeddingStoreDesktop.Command;
using WeddingStoreDesktop.Interfaces.Dialog;
using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Models.SystemModel;
using WeddingStoreDesktop.Views;

namespace WeddingStoreDesktop.ViewModels
{
    public class DangNhapViewModel : BaseViewModel
    {
        private string _UserName { get; set; }
        public string UserName
        {
            get => _UserName;
            set
            {
                if (_UserName != value)
                {
                    _UserName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _Password { get; set; }
        public string Password
        {
            get => _Password;
            set
            {
                if (_Password != value)
                {
                    _Password = value;
                    OnPropertyChanged();
                }
            }
        }

        public DangNhapViewModel()
        {
            DangNhapCommand = new ActionCommand(p => DangNhap());
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
        }

        public ICommand DangNhapCommand { get; }
        public ICommand PasswordChangedCommand { get; set; }

        private void DangNhap()
        {
            // Check Here!!
            TaiKhoan myAcount = DataProvider.Ins.DB.TaiKhoans.FirstOrDefault(tk => tk.UserName == _UserName && tk.PassWord == _Password);
            if (myAcount!=null)
            {
                var home = new HomePage(myAcount);
                home.ShowDialog();
            }
            else
            {
                MessageBox.Show("Sai tên tài khoản hoặc mật khẩu","Đăng nhập thất bại!",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

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
        //        ucNhanVien view = new ucNhanVien();
        //        view.DataContext = new ucNhanVienViewModel();
        //        view.UpdateLayout();

        //        // Get the page size of an A4 document
        //        var pageSize = new Size(8 * 96, 11 * 96);

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
        //    }
        //}
    }
}
