using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Models.DesktopModel;
using WeddingStoreDesktop.Converters;

namespace WeddingStoreDesktop.Services.DesktopService
{
    public class NgayPhanCongService
    {
        public List<NgayPhanCongModel> GetNgayPhanCongByThang(int thang)
        {
            List<NgayPhanCongModel> myLst = new List<NgayPhanCongModel>();

            // Lấy theo ngày trang trí
            var queryTrangTri = from hd in DataProvider.Ins.DB.HoaDons
                                join kh in DataProvider.Ins.DB.KhachHangs on hd.MaKH equals kh.MaKH
                                where hd.NgayTrangTri.Value.Month == thang
                                select new { hd.NgayTrangTri, hd.MaHD, kh.TenKH };
            foreach (var my in queryTrangTri)
            {
                if (myLst.Count == 0)
                {
                    List<ThongTinNgayModel> myLstThongTinNgay = new List<ThongTinNgayModel>();
                    myLstThongTinNgay.Add(new ThongTinNgayModel
                    {
                        MaHD = my.MaHD,
                        TenKH = my.TenKH,
                        type = true // ngày trang trí
                    });
                    myLst.Add(new NgayPhanCongModel
                    {
                        Ngay = ConvertDateTimeToString.ConverToMyDateFormat(my.NgayTrangTri),
                        LstThongTinNgay = myLstThongTinNgay
                    });
                }
                else
                {
                    bool isExist = false;
                    foreach (var myNgay in myLst)
                    {
                        if (myNgay.Ngay == ConvertDateTimeToString.ConverToMyDateFormat(my.NgayTrangTri))
                        {
                            myNgay.LstThongTinNgay.Add(new ThongTinNgayModel
                            {
                                MaHD = my.MaHD,
                                TenKH = my.TenKH,
                                type = true // ngày trang trí
                            });
                            isExist = true;
                            break;
                        }
                    }
                    if (!isExist)
                    {
                        List<ThongTinNgayModel> myLstThongTinNgay = new List<ThongTinNgayModel>();
                        myLstThongTinNgay.Add(new ThongTinNgayModel
                        {
                            MaHD = my.MaHD,
                            TenKH = my.TenKH,
                            type = true // ngày trang trí
                        });
                        myLst.Add(new NgayPhanCongModel
                        {
                            Ngay = ConvertDateTimeToString.ConverToMyDateFormat(my.NgayTrangTri),
                            LstThongTinNgay = myLstThongTinNgay
                        });
                    }
                }
            }

            // Lấy theo ngày tháo dở
            var queryThaoDo = from hd in DataProvider.Ins.DB.HoaDons
                              join kh in DataProvider.Ins.DB.KhachHangs on hd.MaKH equals kh.MaKH
                              where hd.NgayThaoDo.Value.Month == thang
                              select new { hd.NgayThaoDo, hd.MaHD, kh.TenKH };
            foreach (var my in queryThaoDo)
            {
                if (myLst.Count == 0)
                {
                    List<ThongTinNgayModel> myLstThongTinNgay = new List<ThongTinNgayModel>();
                    myLstThongTinNgay.Add(new ThongTinNgayModel
                    {
                        MaHD = my.MaHD,
                        TenKH = my.TenKH,
                        type = true // ngày tháo dở
                    });
                    myLst.Add(new NgayPhanCongModel
                    {
                        Ngay = ConvertDateTimeToString.ConverToMyDateFormat(my.NgayThaoDo),
                        LstThongTinNgay = myLstThongTinNgay
                    });
                }
                else
                {
                    bool isExist = false; // kiểm tra ngày đã xuất hiện trong danh sách chưa
                    foreach (var myNgay in myLst)
                    {
                        if (myNgay.Ngay == ConvertDateTimeToString.ConverToMyDateFormat(my.NgayThaoDo)) // ngày đã xuất hiện trong danh sách
                        {
                            myNgay.LstThongTinNgay.Add(new ThongTinNgayModel
                            {
                                MaHD = my.MaHD,
                                TenKH = my.TenKH,
                                type = false // ngày tháo dở
                            });
                            isExist = true;
                            break;
                        }
                    }
                    if (!isExist) // ngày chưa xuất hiện trong danh sách
                    {
                        List<ThongTinNgayModel> myLstThongTinNgay = new List<ThongTinNgayModel>();
                        myLstThongTinNgay.Add(new ThongTinNgayModel
                        {
                            MaHD = my.MaHD,
                            TenKH = my.TenKH,
                            type = true // ngày tháo dở
                        });
                        myLst.Add(new NgayPhanCongModel
                        {
                            Ngay = ConvertDateTimeToString.ConverToMyDateFormat(my.NgayThaoDo),
                            LstThongTinNgay = myLstThongTinNgay
                        });
                    }
                }
            }

            return myLst;
        }

        public List<NgayPhanCongModel> GetNgayPhanCongByNgayXacDinh(DateTime ngay)
        {
            List<NgayPhanCongModel> myLst = new List<NgayPhanCongModel>();

            // Lấy danh sách theo ngày trang trí
            var queryTrangTri = from hd in DataProvider.Ins.DB.HoaDons
                                join kh in DataProvider.Ins.DB.KhachHangs on hd.MaKH equals kh.MaKH
                                where hd.NgayTrangTri.Value == ngay
                                select new { hd.NgayTrangTri, hd.MaHD, kh.TenKH };

            foreach (var my in queryTrangTri)
            {
                if (myLst.Count == 0)
                {
                    List<ThongTinNgayModel> myLstThongTinNgay = new List<ThongTinNgayModel>();
                    myLstThongTinNgay.Add(new ThongTinNgayModel
                    {
                        MaHD = my.MaHD,
                        TenKH = my.TenKH,
                        type = true // ngày trang trí
                    });
                    myLst.Add(new NgayPhanCongModel
                    {
                        Ngay = ConvertDateTimeToString.ConverToMyDateFormat(my.NgayTrangTri),
                        LstThongTinNgay = myLstThongTinNgay
                    });
                }
                else
                {
                    bool isExist = false;
                    foreach (var myNgay in myLst)
                    {
                        if (myNgay.Ngay == ConvertDateTimeToString.ConverToMyDateFormat(my.NgayTrangTri))
                        {
                            myNgay.LstThongTinNgay.Add(new ThongTinNgayModel
                            {
                                MaHD = my.MaHD,
                                TenKH = my.TenKH,
                                type = true // ngày trang trí
                            });
                            isExist = true;
                            break;
                        }
                    }
                    if (!isExist)
                    {
                        List<ThongTinNgayModel> myLstThongTinNgay = new List<ThongTinNgayModel>();
                        myLstThongTinNgay.Add(new ThongTinNgayModel
                        {
                            MaHD = my.MaHD,
                            TenKH = my.TenKH,
                            type = true // ngày trang trí
                        });
                        myLst.Add(new NgayPhanCongModel
                        {
                            Ngay = ConvertDateTimeToString.ConverToMyDateFormat(my.NgayTrangTri),
                            LstThongTinNgay = myLstThongTinNgay
                        });
                    }
                }
            }

            // Lấy danh sách theo ngày tháo dở
            var queryThaoDo = from hd in DataProvider.Ins.DB.HoaDons
                              join kh in DataProvider.Ins.DB.KhachHangs on hd.MaKH equals kh.MaKH
                              where hd.NgayThaoDo.Value == ngay
                              select new { hd.NgayThaoDo, hd.MaHD, kh.TenKH };

            foreach (var my in queryThaoDo)
            {
                if (myLst.Count == 0)
                {
                    List<ThongTinNgayModel> myLstThongTinNgay = new List<ThongTinNgayModel>();
                    myLstThongTinNgay.Add(new ThongTinNgayModel
                    {
                        MaHD = my.MaHD,
                        TenKH = my.TenKH,
                        type = true // ngày tháo dở
                    });
                    myLst.Add(new NgayPhanCongModel
                    {
                        Ngay = ConvertDateTimeToString.ConverToMyDateFormat(my.NgayThaoDo),
                        LstThongTinNgay = myLstThongTinNgay
                    });
                }
                else
                {
                    bool isExist = false; // kiểm tra ngày đã xuất hiện trong danh sách chưa
                    foreach (var myNgay in myLst)
                    {
                        if (myNgay.Ngay == ConvertDateTimeToString.ConverToMyDateFormat(my.NgayThaoDo)) // ngày đã xuất hiện trong danh sách
                        {
                            myNgay.LstThongTinNgay.Add(new ThongTinNgayModel
                            {
                                MaHD = my.MaHD,
                                TenKH = my.TenKH,
                                type = false // ngày tháo dở
                            });
                            isExist = true;
                            break;
                        }
                    }
                    if (!isExist) // ngày chưa xuất hiện trong danh sách
                    {
                        List<ThongTinNgayModel> myLstThongTinNgay = new List<ThongTinNgayModel>();
                        myLstThongTinNgay.Add(new ThongTinNgayModel
                        {
                            MaHD = my.MaHD,
                            TenKH = my.TenKH,
                            type = true // ngày tháo dở
                        });
                        myLst.Add(new NgayPhanCongModel
                        {
                            Ngay = ConvertDateTimeToString.ConverToMyDateFormat(my.NgayThaoDo),
                            LstThongTinNgay = myLstThongTinNgay
                        });
                    }
                }
            }

            return myLst;
        }
    }
}
