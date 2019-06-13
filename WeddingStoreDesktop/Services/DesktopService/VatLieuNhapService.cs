using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Models.DesktopModel;
using WeddingStoreDesktop.Models.SystemModel;

namespace WeddingStoreDesktop.Services.DesktopService
{
    public class VatLieuNhapService
    {
        public List<VatLieuNhapModel> GetVatLieuNhapByIdDonGia(string maDG)
        {
            List<VatLieuNhapModel> myLst = new List<VatLieuNhapModel>();

            List<ChiTietDonGiaNhapHang> lstChiTiet = new List<ChiTietDonGiaNhapHang>();
            lstChiTiet = DataProvider.Ins.DB.ChiTietDonGiaNhapHangs.Where(ct => ct.MaDG == maDG).ToList();
            foreach (var myDG in lstChiTiet)
            {
                myLst.Add(new VatLieuNhapModel
                {
                    AnhMoTa = myDG.KhoVatLieu.AnhMoTa,
                    MaVL = myDG.MaVL,
                    TenVL = myDG.KhoVatLieu.TenVL,
                    SoLuong = myDG.SoLuong,
                    ThanhTien = myDG.ThanhTien
                });
            }

            return myLst;
        }

        public List<VatLieuNhapModel> GetVatLieuNhapByThang(int thang, int nam)
        {
            List<VatLieuNhapModel> myLst = new List<VatLieuNhapModel>();

            List<DonGiaNhapHang> lstDonGia = DataProvider.Ins.DB.DonGiaNhapHangs.Where(dg => dg.NgayLap.Value.Month == thang
                                                                                    && dg.NgayLap.Value.Year == nam).ToList();
            foreach (var dg in lstDonGia)
            {
                foreach (var ct in dg.ChiTietDonGiaNhapHangs)
                {
                    if (myLst.Count == 0) // Danh sách chưa có vật liệu
                    {
                        myLst.Add(new VatLieuNhapModel
                        {
                            AnhMoTa = ct.KhoVatLieu.AnhMoTa,
                            MaVL = ct.MaVL,
                            TenVL = ct.KhoVatLieu.TenVL,
                            SoLuong = ct.SoLuong,
                            ThanhTien = ct.ThanhTien
                        });
                    }
                    else
                    {
                        bool isExist = false;
                        foreach (var myVl in myLst)
                        {
                            if (myVl.MaVL == ct.MaVL) // đã có vật liệu trong danh sách
                            {
                                // Update số lượng và thành tiền
                                myVl.SoLuong += ct.SoLuong;
                                myVl.ThanhTien += ct.ThanhTien;

                                isExist = true; // bật cờ đã xuất hiện
                                break;
                            }
                        }
                        if (!isExist)
                        {
                            myLst.Add(new VatLieuNhapModel
                            {
                                AnhMoTa = ct.KhoVatLieu.AnhMoTa,
                                MaVL = ct.MaVL,
                                TenVL = ct.KhoVatLieu.TenVL,
                                SoLuong = ct.SoLuong,
                                ThanhTien = ct.ThanhTien
                            });
                        }
                    }
                }
            }

            return myLst;
        }
    }
}
