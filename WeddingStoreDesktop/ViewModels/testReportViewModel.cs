using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Models.DesktopModel;
using WeddingStoreDesktop.Models.SystemModel;
using WeddingStoreDesktop.Services.DesktopService;
using WeddingStoreDesktop.Views;

namespace WeddingStoreDesktop.ViewModels
{
    public class testReportViewModel
    {
        private ucTestReport _ucahihi;
        private LocalReport _Report;
        private ReportViewer _reportViewer;
        private string _maHD;

        public testReportViewModel(ucTestReport ahihi, string maHD)
        {
            _maHD = maHD;
            _ucahihi = ahihi;
            _reportViewer = ahihi.myReport;

            GetData();
        }
        private static string _path = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())));
        public static string ContentStart = _path + @"\Views\reportHoaDon.rdlc";

        void GetData()
        {
            SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);
            ThongTinMauService ttMau = new ThongTinMauService();
            List<ThongTinMauModel> LstThongTinMau = ttMau.GetChiTietByIdHD(_maHD);

            ThongTinPhatSinhService ttPhatSinh = new ThongTinPhatSinhService();
            List<ThongTinPhatSinhModel> LstThongTinPhatSinh = ttPhatSinh.GetThongTinPhatSinh(_maHD);

            HoaDon myHD = DataProvider.Ins.DB.HoaDons.FirstOrDefault(hd => hd.MaHD == _maHD);

            _reportViewer.LocalReport.DataSources.Clear();
            var rpds_model = new ReportDataSource() { Name = "SanPham", Value = LstThongTinMau };
            var ps_model = new ReportDataSource() { Name = "PhatSinh", Value = LstThongTinPhatSinh };

            _reportViewer.LocalReport.DataSources.Add(rpds_model);
            _reportViewer.LocalReport.DataSources.Add(ps_model);
            _reportViewer.LocalReport.EnableExternalImages = true;

            //_reportViewer.LocalReport.ReportPath = ContentStart;
            _reportViewer.LocalReport.ReportEmbeddedResource = "WeddingStoreDesktop.Views.reportHoaDon.rdlc";

            // Parameters
            ReportParameter[] rptParameters = new ReportParameter[]
            {
                new ReportParameter("TenKH",myHD.KhachHang.TenKH),
                new ReportParameter("DiaChi",myHD.KhachHang.DiaChi),
                new ReportParameter("SoDT",myHD.KhachHang.SoDT),
                new ReportParameter("MaHD",myHD.MaHD),
                new ReportParameter("NgayLap",myHD.NgayLap.Value.ToString()),
                new ReportParameter("TongTien",myHD.TongTien.Value.ToString()),
                new ReportParameter("TenNV",Shared.CurrentTenNV)
            };
            _reportViewer.LocalReport.SetParameters(rptParameters);

            // Refresh
            //_reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            _reportViewer.Refresh();
            _reportViewer.RefreshReport();
        }
    }
}
