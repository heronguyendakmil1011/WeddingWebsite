using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingStoreDesktop.Models.DesktopModel;
using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Models.SystemModel;

namespace WeddingStoreDesktop.Services.DesktopService
{
    public class LuongNhanVienService
    {
        public List<LuongNhanVienModel> GetLuongNVByThang(int thang, int nam)
        {
            List<LuongNhanVienModel> myLst = new List<LuongNhanVienModel>();

            List<PhanCong> lstPhanCong = DataProvider.Ins.DB.PhanCongs.Where(pc => pc.Ngay.Month == thang
                                                                                && pc.Ngay.Year == nam).ToList();
            foreach (var pc in lstPhanCong)
            {
                if (pc.ThoiGianDen.HasValue && pc.ThoiGianDi.HasValue
                    && pc.ThoiGianDen.Value.CompareTo(TimeSpan.Zero) != 0
                    && pc.ThoiGianDi.Value.CompareTo(TimeSpan.Zero) != 0)
                {
                    TimeSpan tongThoiGian = pc.ThoiGianDi.Value.Subtract(pc.ThoiGianDen.Value);
                    if (myLst.Count == 0)
                    {
                        myLst.Add(new LuongNhanVienModel
                        {
                            Avata = pc.NhanVien.Avatar,
                            MaNV = pc.MaNV,
                            TenNV = pc.NhanVien.TenNV,
                            TongThoiGian = tongThoiGian,
                            TongTien = (tongThoiGian.Hours * pc.NhanVien.Luong.Value) + (tongThoiGian.Minutes * (pc.NhanVien.Luong.Value / 60))
                        });
                    }
                    else
                    {
                        bool isExist = false;
                        foreach (var myNV in myLst)
                        {
                            if (pc.MaNV == myNV.MaNV)
                            {
                                myNV.TongThoiGian += tongThoiGian;
                                myNV.TongTien += (tongThoiGian.Hours * pc.NhanVien.Luong.Value) + (tongThoiGian.Minutes * (pc.NhanVien.Luong.Value / 60));
                                isExist = true;
                                break;
                            }
                        }
                        if (!isExist)
                        {
                            myLst.Add(new LuongNhanVienModel
                            {
                                Avata = pc.NhanVien.Avatar,
                                MaNV = pc.MaNV,
                                TenNV = pc.NhanVien.TenNV,
                                TongThoiGian = tongThoiGian,
                                TongTien = (tongThoiGian.Hours * pc.NhanVien.Luong.Value) + (tongThoiGian.Minutes * (pc.NhanVien.Luong.Value / 60))
                            });
                        }
                    }
                }
            }

            return myLst;
        }
    }
}
