using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingStoreDesktop.Functions;
using WeddingStoreDesktop.Interfaces.Dialog;
using System.Windows.Input;
using System.Globalization;
using WeddingStoreDesktop.Models.SystemModel;
using WeddingStoreDesktop.Command;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using WeddingStoreDesktop.Models;

namespace WeddingStoreDesktop.ViewModels
{
    public class ThemNhanVienViewModel : BaseViewModel, IDialogRequestClose
    {
        #region Properties
        private NhanVien _myNV { get; set; }
        public NhanVien myNV
        {
            get => _myNV;
            set
            {
                if (_myNV != value)
                {
                    _myNV = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _checkNam { get; set; }
        public bool checkNam
        {
            get => _checkNam;
            set
            {
                if (_checkNam != value)
                {
                    _checkNam = value;
                    OnPropertyChanged();
                    if (_checkNam)
                        myNV.GioiTinh = "Nam";
                }
            }
        }

        private bool _checkNu { get; set; }
        public bool checkNu
        {
            get => _checkNu;
            set
            {
                if (_checkNu != value)
                {
                    _checkNu = value;
                    OnPropertyChanged();
                    if (_checkNu)
                        myNV.GioiTinh = "Nữ";
                }
            }
        }
        #endregion

        #region Constructors
        public ThemNhanVienViewModel(NhanVien nv)
        {
            if (nv == null)
            {
                GetData();
                Title = "Thêm nhân viên";
            }
            else
            {
                //DateTime dt = DateTime.ParseExact(nv.NgaySinh.ToString(), "dd/M/yyyy", CultureInfo.InvariantCulture);
                _myNV = nv;
                Title = "Chỉnh sửa thông tin nhân viên";
                if (_myNV.GioiTinh.Trim().Equals("Nam"))
                {
                    checkNam = true;
                    checkNu = false;
                }
                else
                {
                    checkNam = false;
                    checkNu = true;
                }
                //_myNV.NgaySinh= dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            }
            SaveCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true)));
            //SaveCommand = new ActionCommand(p => Save());
            CancelCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false)));
            ChooseImageCommand = new RelayCommand<Image>((p) => { return true; }, (p) => { ChooseImage(); });
        }
        #endregion

        #region Commands
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand ChooseImageCommand { get; set; }
        #endregion

        #region Methods
        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;
        void GetData()
        {
            _myNV = new NhanVien
            {
                MaNV = "NV-" + GetMaxID.GetMaxIdNV(),
                TenNV = "",
                GioiTinh = "",
                NgaySinh = null,
                DiaChi = "",
                SoDT = "",
                Luong = 0
            };
        }
        void Save()
        {
            DataProvider.Ins.DB.SaveChanges();
            CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true));
        }
        void ChooseImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Chọn ảnh";
            openFileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                                    "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                                    "Portable Network Graphic (*.png)|*.png";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //imgPhoto.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                //mySanPham.HinhMoTa = openFileDialog.FileName;
                Image myImage = null;
                myImage = Image.FromFile(openFileDialog.FileName);
                using (MemoryStream mStream = new MemoryStream())
                {
                    myImage.Save(mStream, myImage.RawFormat);
                    myNV.Avatar = mStream.ToArray();
                }
                OnPropertyChanged(nameof(myNV));
            }
        }
        #endregion
    }
}
