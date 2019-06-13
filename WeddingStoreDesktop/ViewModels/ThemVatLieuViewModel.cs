using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Models.SystemModel;
using WeddingStoreDesktop.Interfaces.Dialog;
using WeddingStoreDesktop.Functions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace WeddingStoreDesktop.ViewModels
{
    public class ThemVatLieuViewModel : BaseViewModel, IDialogRequestClose
    {
        #region Properties
        private KhoVatLieu _myVL { get; set; }
        public KhoVatLieu myVL
        {
            get => _myVL;
            set
            {
                if (_myVL != value)
                {
                    _myVL = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _IsNhap { get; set; }
        public bool IsNhap
        {
            get => _IsNhap;
            set
            {
                _IsNhap = value;
                OnPropertyChanged();
                if (_IsNhap)
                    myVL.IsNhap = true;
            }
        }

        private bool _IsCoSan { get; set; }
        public bool IsCoSan
        {
            get => _IsCoSan;
            set
            {
                _IsCoSan = value;
                OnPropertyChanged();
                if (_IsCoSan)
                    myVL.IsNhap = false;
            }
        }
        #endregion

        #region Constructors
        public ThemVatLieuViewModel(KhoVatLieu vl)
        {
            SaveCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true)));
            CancelCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false)));
            ChooseImageCommand = new ActionCommand(p => ChooseImage());
            if (vl == null)
            {
                myVL = new KhoVatLieu
                {
                    MaVL = "VL-" + GetMaxID.GetMaxIdVL(),
                    TenVL = "",
                    SoLuongTon = 1,
                    GiaTien = 0,
                    DonVi = "",
                    AnhMoTa = null
                };
                Title = "Thêm vật liệu mới";
                IsNhap = false;
                IsCoSan = true;
            }
            else
            {
                myVL = vl;
                Title = "Chỉnh sửa thông tin vật liệu";
                if (_myVL.IsNhap.Value)
                {
                    IsNhap = true;
                    IsCoSan = false;
                }
                else
                {
                    IsNhap = false;
                    IsCoSan = true;
                }
            }
        }
        #endregion

        #region Commands
        public ICommand ChooseImageCommand { get; set; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        #endregion

        #region Methods
        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        void ChooseImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Chọn ảnh";
            openFileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                                    "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                                    "Portable Network Graphic (*.png)|*.png";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Image myImage = null;
                myImage = Image.FromFile(openFileDialog.FileName);
                using (MemoryStream mStream = new MemoryStream())
                {
                    myImage.Save(mStream, myImage.RawFormat);
                    myVL.AnhMoTa = mStream.ToArray();
                }
                OnPropertyChanged(nameof(myVL));
            }
        }

        #endregion
    }
}
