using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WeddingStoreDesktop.Functions;
using WeddingStoreDesktop.Interfaces.Dialog;
using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Models.SystemModel;

namespace WeddingStoreDesktop.ViewModels
{
    public class TaiKhoanViewModel : BaseViewModel, IDialogRequestClose
    {
        private List<NhanVien> _LstNhanVien { get; set; }
        public List<NhanVien> LstNhanVien
        {
            get => _LstNhanVien;
            set
            {
                if (_LstNhanVien != value)
                {
                    _LstNhanVien = value;
                    OnPropertyChanged();
                }
            }
        }

        private NhanVien _SelectedNhanVien { get; set; }
        public NhanVien SelectedNhanVien
        {
            get => _SelectedNhanVien;
            set
            {
                if (_SelectedNhanVien != value)
                {
                    _SelectedNhanVien = value;
                    OnPropertyChanged();
                    GetTaiKhoan();
                }
            }
        }

        private List<NhanVien> _myLstNhanVien { get; set; }

        private string _keyWord { get; set; }
        public string keyWord
        {
            get => _keyWord;
            set
            {
                if (_keyWord != value)
                {
                    _keyWord = value;
                    OnPropertyChanged();
                    SearchNhanVien();
                }
            }
        }

        private TaiKhoan _myTaiKhoan { get; set; }
        public TaiKhoan myTaiKhoan
        {
            get => _myTaiKhoan;
            set
            {
                _myTaiKhoan = value;
                OnPropertyChanged();
            }
        }

        private bool _isCreate;
        public bool isCreate
        {
            get { return _isCreate; }
            set { _isCreate = value; OnPropertyChanged(); }
        }

        private bool _isModify;
        public bool isModify
        {
            get { return _isModify; }
            set { _isModify = value; OnPropertyChanged(); }
        }

        public TaiKhoanViewModel()
        {
            GetNhanVien();

            CreateCommand = new ActionCommand(p => Create());
            SaveCommand = new ActionCommand(p => Save());
            DeleteCommand = new ActionCommand(p => Delete());
            CancelCommand = new ActionCommand(p => Cancel());

            DoneCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true)));
        }

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        void GetNhanVien()
        {
            LstNhanVien = DataProvider.Ins.DB.NhanViens.ToList();
            _myLstNhanVien = _LstNhanVien;
            SelectedNhanVien = null;
        }
        void SearchNhanVien()
        {
            if (String.IsNullOrEmpty(_keyWord))
            {
                LstNhanVien = _myLstNhanVien;
            }
            else
            {
                LstNhanVien = _myLstNhanVien.Where(nv => nv.TenNV.ToLower().Contains(_keyWord.ToLower())).ToList();
            }
        }
        void GetTaiKhoan()
        {
            if (_SelectedNhanVien != null)
            {
                myTaiKhoan = DataProvider.Ins.DB.TaiKhoans.FirstOrDefault(tk => tk.MaNV == _SelectedNhanVien.MaNV);
                if (myTaiKhoan == null)
                {
                    myTaiKhoan = new TaiKhoan();
                    myTaiKhoan.ID = "TK-" + GetMaxID.GetMaxIdTK();
                    myTaiKhoan.UserName = "";
                    myTaiKhoan.PassWord = "";
                    myTaiKhoan.MaNV = _SelectedNhanVien.MaNV;

                    isCreate = true;
                    isModify = false;
                }
                else
                {
                    isModify = true;
                    isCreate = false;
                }
            }
        }

        public ICommand CreateCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand DoneCommand { get; }

        void Create()
        {
            if (_SelectedNhanVien != null)
            {
                TaiKhoan myTK = DataProvider.Ins.DB.TaiKhoans.FirstOrDefault(tk => tk.MaNV == _SelectedNhanVien.MaNV);
                if (myTK == null)
                {
                    if (!String.IsNullOrEmpty(myTaiKhoan.UserName) && !String.IsNullOrEmpty(myTaiKhoan.PassWord))
                    {
                        DataProvider.Ins.DB.TaiKhoans.Add(myTaiKhoan);
                        DataProvider.Ins.DB.SaveChanges();
                        GetNhanVien();
                    }
                    else
                        MessageBox.Show("UserName hoặc PassWord không được để trống", "Fail!!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        void Save()
        {
            TaiKhoan myTK = DataProvider.Ins.DB.TaiKhoans.FirstOrDefault(tk => tk.MaNV == _SelectedNhanVien.MaNV);
            if (myTK != null)
            {
                DataProvider.Ins.DB.SaveChanges();
                GetNhanVien();
                MessageBox.Show("Success!!", "Tạo tài khoản nhân viên " + _SelectedNhanVien.TenNV + " thành công.", MessageBoxButton.OK);
            }
        }
        void Delete()
        {
            if (_SelectedNhanVien != null)
            {
                TaiKhoan myTK = DataProvider.Ins.DB.TaiKhoans.FirstOrDefault(tk => tk.MaNV == _SelectedNhanVien.MaNV);
                if (myTK != null)
                {
                    DataProvider.Ins.DB.TaiKhoans.Remove(myTaiKhoan);
                    DataProvider.Ins.DB.SaveChanges();
                    GetNhanVien();
                    MessageBox.Show("Success!!", "Xóa tài khoản nhân viên " + _SelectedNhanVien.TenNV + " thành công.", MessageBoxButton.OK);
                }
            }
        }
        void Cancel()
        {
            DataProvider.Ins.RefreshDB();
            SelectedNhanVien = null;
        }
    }
}
