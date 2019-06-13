using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Models.DesktopModel;
using WeddingStoreDesktop.Models.SystemModel;

namespace WeddingStoreDesktop.Services.SystemService
{
    public class VatLieuService
    {
        // Lấy danh sách tất cả vật liệu có số lượng tồn thực tế phù hợp với hóa đơn
        public List<KhoVatLieuAoModel> GetVatLieuCan(DateTime? ntt, DateTime? ntd, string maHD)
        {
            // Danh sách các vật liệu có trong các hóa đơn phù hợp
            List<KhoVatLieu> myLst = new List<KhoVatLieu>();

            DataProvider.Ins.RefreshDB();
            List<KhoVatLieu> lstVatLieu = new List<KhoVatLieu>();
            lstVatLieu = DataProvider.Ins.DB.KhoVatLieux.ToList();

            List<KhoVatLieuAoModel> lstAo = new List<KhoVatLieuAoModel>();
            foreach (var vl in lstVatLieu)
            {
                lstAo.Add(new KhoVatLieuAoModel
                {
                    MaVL = vl.MaVL,
                    TenVL = vl.TenVL,
                    SoLuong = vl.SoLuongTon,
                    GiaTien = vl.GiaTien,
                    IsNhap = vl.IsNhap
                });
            }

            List<HoaDon> lstHoaDon = new List<HoaDon>();
            using (WeddingStoreDBEntities DB = new WeddingStoreDBEntities())
            {
                if (maHD == null)
                    lstHoaDon = DB.HoaDons.Where(hd => hd.TinhTrang == 0).ToList();
                else
                    lstHoaDon = DB.HoaDons.Where(hd => (hd.TinhTrang == 0 && hd.MaHD != maHD)
                                                    || (hd.TinhTrang != 1 && hd.MaHD == maHD)).ToList();

                //HoaDon myHoaDon = DataProvider.Ins.DB.HoaDons.FirstOrDefault(hd => hd.MaHD == maHD);
                //if (myHoaDon.TinhTrang != 1)
                //    lstHoaDon.Add(myHoaDon);

                foreach (var hd in lstHoaDon)
                {
                    bool flag = false;
                    if (hd.NgayTrangTri <= ntt && ntt <= hd.NgayThaoDo || hd.NgayTrangTri <= ntd && ntd <= hd.NgayThaoDo
                            || ntt <= hd.NgayTrangTri && hd.NgayThaoDo <= ntd
                            || hd.NgayTrangTri <= ntt && hd.NgayThaoDo >= ntd
                            || (ntt >= hd.NgayThaoDo && ntt.Value.Subtract(hd.NgayThaoDo.Value).TotalDays <= 3)
                            || (ntd <= hd.NgayTrangTri && hd.NgayTrangTri.Value.Subtract(ntd.Value).TotalDays <= 3))
                        flag = true;

                    if (flag)
                    {
                        foreach (var cthd in hd.ChiTietHoaDons)
                        {
                            foreach (var ctsp in cthd.SanPham.ChiTietSanPhams)
                            {
                                if (!ctsp.KhoVatLieu.IsNhap.Value) // không tính vật liệu nhập
                                {
                                    foreach (var vlAo in lstAo)
                                    {
                                        if (vlAo.MaVL == ctsp.KhoVatLieu.MaVL)
                                        {
                                            vlAo.SoLuong -= (cthd.SoLuong * ctsp.SoLuong);
                                        }
                                    }
                                }
                            }
                        }

                        foreach (var ps in hd.PhatSinhs)
                        {
                            KhoVatLieuAoModel myVL = lstAo.FirstOrDefault(vl => vl.MaVL == ps.MaVL);
                            if (!myVL.IsNhap.Value)
                            {
                                myVL.SoLuong -= ps.SoLuong;
                            }
                        }
                    }
                }
            }

            return lstAo;
        }

        public List<KhoVatLieuAoModel> UpdateVatLieuAo(List<KhoVatLieuAoModel> lstVatLieuAo, string maHD, string maSP, int soLuong)
        {
            DataProvider.Ins.RefreshDB();
            ChiTietHoaDon chiTietHoaDon = DataProvider.Ins.DB.ChiTietHoaDons.FirstOrDefault(cthd => cthd.MaHD == maHD && cthd.MaSP == maSP);
            if (chiTietHoaDon != null)
            {
                foreach (var ct in chiTietHoaDon.SanPham.ChiTietSanPhams)
                {
                    if (!ct.KhoVatLieu.IsNhap.Value)
                    {
                        // Cập nhập số lượng tồn trong danh sách vật liệu ảo
                        foreach (var vl in lstVatLieuAo)
                        {
                            if (vl.MaVL == ct.KhoVatLieu.MaVL)
                            {
                                if (soLuong == 0) // Xóa mẫu
                                {
                                    vl.SoLuong = vl.SoLuong + (ct.SoLuong * chiTietHoaDon.SoLuong);
                                    break;
                                }
                                else // Update với số lượng khác
                                {
                                    vl.SoLuong = vl.SoLuong + (ct.SoLuong * chiTietHoaDon.SoLuong) - (ct.SoLuong * soLuong);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                List<ChiTietSanPham> lstChiTiet = DataProvider.Ins.DB.ChiTietSanPhams.Where(ct => ct.MaSP == maSP).ToList();
                foreach (var ct in lstChiTiet)
                {
                    foreach (var vl in lstVatLieuAo)
                    {
                        if (vl.MaVL == ct.KhoVatLieu.MaVL)
                        {
                            if (soLuong > 0)
                            {
                                vl.SoLuong -= (ct.SoLuong * soLuong);
                                break;
                            }
                        }
                    }
                }
            }
            return lstVatLieuAo;
        }

        public List<KhoVatLieuAoModel> UpdateVatLieuAoWithVatLieu(List<KhoVatLieuAoModel> lstVatLieuAo, int soLuongHT, string maVL, int soLuong)
        {
            foreach (var vl in lstVatLieuAo)
            {
                if (vl.MaVL == maVL)
                {
                    if (soLuong == 0)
                        vl.SoLuong += soLuongHT;
                    else
                        vl.SoLuong = vl.SoLuong + soLuongHT - soLuong;
                }
            }

            return lstVatLieuAo;
        }
    }
}
