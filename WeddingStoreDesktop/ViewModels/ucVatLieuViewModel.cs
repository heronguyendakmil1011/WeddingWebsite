using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WeddingStoreDesktop.Interfaces.Dialog;
using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Models.SystemModel;
using WeddingStoreDesktop.Services.SystemService;

namespace WeddingStoreDesktop.ViewModels
{
    public class ucVatLieuViewModel : BaseViewModel
    {
        #region Properties
        private List<KhoVatLieu> _LstVatLieu { get; set; }
        public List<KhoVatLieu> LstVatLieu
        {
            get => _LstVatLieu;
            set
            {
                if (_LstVatLieu != value)
                {
                    _LstVatLieu = value;
                    OnPropertyChanged();
                }
            }
        }

        private KhoVatLieu _SelectedVatLieu { get; set; }
        public KhoVatLieu SelectedVatLieu
        {
            get => _SelectedVatLieu;
            set
            {
                if (_SelectedVatLieu != value)
                {
                    _SelectedVatLieu = value;
                    OnPropertyChanged();
                }
            }
        }
        private List<KhoVatLieu> _firstList;
        private string _myKeyWord { get; set; }
        public string myKeyWord
        {
            get => _myKeyWord;
            set
            {
                _myKeyWord = value;
                OnPropertyChanged();
                SearchVatLieu();
            }
        }
        public List<string> LstOption
        {
            get => new List<string> { "All", "Hàng nhập", "Hàng có sẵn" };
        }
        private string _SelectedOption { get; set; }
        public string SelectedOption
        {
            get => _SelectedOption;
            set
            {
                _SelectedOption = value;
                OnPropertyChanged();
                GetData();
            }
        }
        #endregion

        #region Constructors
        public ucVatLieuViewModel()
        {
            SelectedOption = "All";
            GetData();
            AddCommand = new ActionCommand(p => Them());
            ModifyCommand = new ActionCommand(p => Modify());
            DeleteCommand = new ActionCommand(p => Delete());
            RefreshCommand = new ActionCommand(p => Refresh());
        }
        #endregion

        #region Commands
        public ICommand AddCommand { get; }
        public ICommand ModifyCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand RefreshCommand { get; }
        #endregion

        #region Methods
        void GetData()
        {
            //MockVatLieuRepository vatLieu = new MockVatLieuRepository();
            //_LstVatLieu = await vatLieu.GetVatLieu();

            //LstVatLieu = DataProvider.Ins.DB.KhoVatLieux.ToList();
            //_firstList = _LstVatLieu;

            switch (_SelectedOption)
            {
                case "All":
                    _firstList = DataProvider.Ins.DB.KhoVatLieux.ToList();
                    break;
                case "Hàng nhập":
                    _firstList = DataProvider.Ins.DB.KhoVatLieux.Where(vl => vl.IsNhap == true).ToList();
                    break;
                case "Hàng có sẵn":
                    _firstList = DataProvider.Ins.DB.KhoVatLieux.Where(vl => vl.IsNhap == false).ToList();
                    break;
            }
            SearchVatLieu();
        }

        void SearchVatLieu()
        {
            if (String.IsNullOrEmpty(_myKeyWord))
            {
                LstVatLieu = _firstList;
            }
            else
            {
                LstVatLieu = _firstList.Where(vl => vl.TenVL.ToLower().Contains(_myKeyWord.ToLower())).ToList();
            }
        }

        private void Them()
        {
            var viewModel = new ThemVatLieuViewModel(null);
            Shared.myViewModelForAdd = viewModel;

            bool? result = Shared.dialogService.ShowDialog(viewModel);
            if (result.HasValue)
            {
                if (result.Value)
                {
                    if (int.TryParse(viewModel.myVL.SoLuongTon.Value.ToString(), out int mySoLuong)
                        && float.TryParse(viewModel.myVL.GiaTien.Value.ToString(), out float myGiaTien))
                    {
                        DataProvider.Ins.DB.KhoVatLieux.Add(viewModel.myVL);
                        DataProvider.Ins.DB.SaveChanges();
                    }
                    else
                        MessageBox.Show("Fail!", "Thêm vật liệu mới thất bại.", MessageBoxButton.OK, MessageBoxImage.Error);
                    GetData();
                }
            }
        }

        private void Modify()
        {
            if (_SelectedVatLieu != null)
            {
                KhoVatLieu myFirstVL = _SelectedVatLieu;
                var viewModel = new ThemVatLieuViewModel(myFirstVL);
                Shared.myViewModelForAdd = viewModel;

                bool? result = Shared.dialogService.ShowDialog(viewModel);
                if (result.HasValue)
                {
                    if (result.Value)
                    {
                        if (int.TryParse(viewModel.myVL.SoLuongTon.Value.ToString(), out int mySoLuong)
                        && float.TryParse(viewModel.myVL.GiaTien.Value.ToString(), out float myGiaTien))
                        {
                            KhoVatLieu myUpdate = DataProvider.Ins.DB.KhoVatLieux.FirstOrDefault(vl => vl.MaVL == viewModel.myVL.MaVL);
                            if (myUpdate != null)
                            {
                                myUpdate.TenVL = viewModel.myVL.TenVL;
                                myUpdate.SoLuongTon = viewModel.myVL.SoLuongTon;
                                myUpdate.DonVi = viewModel.myVL.DonVi;
                                myUpdate.GiaTien = viewModel.myVL.GiaTien;
                                myUpdate.AnhMoTa = viewModel.myVL.AnhMoTa;
                                myUpdate.IsNhap = viewModel.IsNhap;

                                DataProvider.Ins.DB.SaveChanges();
                            }

                            MessageBox.Show("Chỉnh sửa thông tin vật liệu!", "Chỉnh sửa thông tin vật liệu thành công", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                        else
                        {
                            //SelectedVatLieu = myFirstVL;
                            MessageBox.Show("Chỉnh sửa thông tin vật liệu!", "Chỉnh sửa thông tin vật liệu thất bại", MessageBoxButton.OK, MessageBoxImage.Error);
                            DataProvider.Ins.RefreshDB();
                        }
                        GetData();
                    }
                    else
                    {
                        Refresh();
                    }
                }
            }
            else
            {
                MessageBox.Show("Chọn vật liệu broooo!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Delete()
        {
            if (_SelectedVatLieu != null)
            {
                var result = MessageBox.Show("Bạn có muốn xóa " + _SelectedVatLieu.TenVL + " không?", "Xóa vật liệu", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    var myDelete = DataProvider.Ins.DB.KhoVatLieux.FirstOrDefault(vl => vl.MaVL == SelectedVatLieu.MaVL);
                    if (myDelete != null)
                    {
                        // Xóa trong chi tiết sản phẩm
                        List<ChiTietSanPham> lstChiTietSanPham = myDelete.ChiTietSanPhams.ToList();
                        foreach (var ctSP in lstChiTietSanPham)
                        {
                            DataProvider.Ins.DB.ChiTietSanPhams.Remove(ctSP);
                        }
                        // Xóa trong phát sinh
                        List<PhatSinh> lstPhatSinh = myDelete.PhatSinhs.ToList();
                        foreach (var ps in lstPhatSinh)
                        {
                            DataProvider.Ins.DB.PhatSinhs.Remove(ps);
                        }
                        // Xóa trong chi tiết đơn giá nhập hàng
                        List<ChiTietDonGiaNhapHang> lstChiTietDGNhapHang = myDelete.ChiTietDonGiaNhapHangs.ToList();
                        foreach (var ctDG in lstChiTietDGNhapHang)
                        {
                            DataProvider.Ins.DB.ChiTietDonGiaNhapHangs.Remove(ctDG);
                        }
                        // Xóa vật liệu
                        DataProvider.Ins.DB.KhoVatLieux.Remove(myDelete);
                        DataProvider.Ins.DB.SaveChanges();
                        LstVatLieu = DataProvider.Ins.DB.KhoVatLieux.ToList();
                        SelectedVatLieu = null;
                    }
                }
            }
        }

        private void Refresh()
        {
            DataProvider.Ins.RefreshDB();
            GetData();
        }
        #endregion
    }
}
