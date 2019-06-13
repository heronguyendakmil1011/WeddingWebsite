using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingStoreDesktop.Services.DesktopService;
using WeddingStoreDesktop.Views;
using WeddingStoreDesktop.Models.DesktopModel;
using WeddingStoreDesktop.Models.SystemModel;
using WeddingStoreDesktop.Models;

namespace WeddingStoreDesktop.ViewModels
{
    public class ucReportLuongNhanVienViewModel
    {
        private ReportViewer _reportViewer;
        private string _maNV;
        private int _Thang;
        private int _Nam;
        public ucReportLuongNhanVienViewModel(ucReportLuongNhanVien ucReport, string maNV, int Thang, int Nam)
        {
            _maNV = maNV;
            _Thang = Thang;
            _reportViewer = ucReport.myReport;
            _Nam = Nam;

            GetData();
        }

        void GetData()
        {
            SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);
            NhanVienPhanCongService nhanVienPC = new NhanVienPhanCongService();
            List<PhanCongNhanVienModel> lstPhanCongNV = nhanVienPC.GetPhanCongByIdNVVaThangVaNam(_maNV, _Thang, _Nam);

            NhanVien myNhanVien = DataProvider.Ins.DB.NhanViens.FirstOrDefault(nv => nv.MaNV == _maNV);

            TimeSpan myTime = TimeSpan.Zero;
            foreach (var pc in lstPhanCongNV)
            {
                if (TimeSpan.TryParse(pc.ThoiGianDen, out TimeSpan tgDen) && TimeSpan.TryParse(pc.ThoiGianDi, out TimeSpan tgDi))
                {
                    if (tgDi >= tgDen)
                    {
                        myTime += tgDi - tgDen;
                    }
                }
            }

            float tongLuong = (myTime.Hours * myNhanVien.Luong.Value) + (myTime.Minutes * (myNhanVien.Luong.Value / 60));

            _reportViewer.LocalReport.DataSources.Clear();
            ReportDataSource nvPhanCong = new ReportDataSource() { Name = "PhanCong", Value = lstPhanCongNV };
            _reportViewer.LocalReport.DataSources.Add(nvPhanCong);
            _reportViewer.LocalReport.EnableExternalImages = true;

            _reportViewer.LocalReport.ReportEmbeddedResource = "WeddingStoreDesktop.Views.reportLuongNhanVien.rdlc";

            ReportParameter[] rptParameters = new ReportParameter[]
            {
                new ReportParameter("TenNV",myNhanVien.TenNV),
                new ReportParameter("GioiTinh",myNhanVien.GioiTinh),
                new ReportParameter("SoDT",myNhanVien.SoDT),
                new ReportParameter("DiaChi",myNhanVien.DiaChi),
                new ReportParameter("Luong",myNhanVien.Luong.ToString()),
                new ReportParameter("Thang",_Thang.ToString()),
                new ReportParameter("Nam",_Nam.ToString()),
                new ReportParameter("TongGioCong",myTime.ToString()),
                new ReportParameter("TongLuong",tongLuong.ToString()),
                new ReportParameter("NVLap",Shared.CurrentTenNV),
            };
            _reportViewer.LocalReport.SetParameters(rptParameters);

            _reportViewer.Refresh();
            _reportViewer.RefreshReport();
        }
    }
}
