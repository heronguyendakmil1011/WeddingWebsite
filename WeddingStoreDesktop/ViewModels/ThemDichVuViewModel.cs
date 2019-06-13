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
    public class ThemDichVuViewModel : BaseViewModel, IDialogRequestClose
    {
        #region Properties Loại dịch vụ
        private List<LoaiDichVu> _LstLoaiDichVu { get; set; }
        public List<LoaiDichVu> LstLoaiDichVu
        {
            get => _LstLoaiDichVu;
            set
            {
                _LstLoaiDichVu = value;
                OnPropertyChanged();
            }
        }

        private LoaiDichVu _SelectedLDV;
        public LoaiDichVu SelectedLDV
        {
            get { return _SelectedLDV; }
            set
            {
                if (_SelectedLDV != value)
                {
                    _SelectedLDV = value;
                    SelectedLDVInDV = value; // gán giá trị hiển thị loại dịch vụ bên dịch vụ
                    SelectedDV = null; // Làm mới bên dịch vụ
                    onActionDV = false; // Ẩn thao tác bên dịch vụ
                    isCreateDV = false; // Hủy thao tác thêm bên dịch vụ
                    isModifyDV = false; // Hủy thao tác chỉnh sửa bên dịch vụ
                    OnPropertyChanged();
                    GetDV(); // Lấy danh sách dịch vụ mới theo loại dịch vụ
                    if (_isModifyLDV && _SelectedLDV != null)
                        myLDV = _SelectedLDV.TenLoaiDV;
                }
            }
        }

        private bool _isCreateLDV;
        public bool isCreateLDV
        {
            get { return _isCreateLDV; }
            set
            {
                _isCreateLDV = value;
                OnPropertyChanged();
            }
        }

        private bool _isModifyLDV;
        public bool isModifyLDV
        {
            get { return _isModifyLDV; }
            set
            {
                _isModifyLDV = value;
                OnPropertyChanged();
            }
        }

        private bool _onActionLDV;
        public bool onActionLDV
        {
            get { return _onActionLDV; }
            set { _onActionLDV = value; OnPropertyChanged(); }
        }

        private string _myLDV;
        public string myLDV
        {
            get { return _myLDV; }
            set { _myLDV = value; OnPropertyChanged(); }
        }
        #endregion

        #region Properties Dịch vụ
        private List<DichVu> _LstDichVu { get; set; }
        public List<DichVu> LstDichVu
        {
            get => _LstDichVu;
            set
            {
                _LstDichVu = value;
                OnPropertyChanged();
            }
        }

        private DichVu _SelectedDV;
        public DichVu SelectedDV
        {
            get { return _SelectedDV; }
            set
            {
                if (_SelectedDV != value)
                {
                    _SelectedDV = value;
                    OnPropertyChanged();
                    GetDV();
                    if (_isModifyDV && _SelectedDV != null)
                        myDV = _SelectedDV.TenDV;
                }
            }
        }

        private LoaiDichVu _SelectedLDVInDV { get; set; }
        public LoaiDichVu SelectedLDVInDV
        {
            get => _SelectedLDVInDV;
            set
            {
                _SelectedLDVInDV = value;
                OnPropertyChanged();
            }
        }

        private bool _isCreateDV;
        public bool isCreateDV
        {
            get { return _isCreateDV; }
            set
            {
                _isCreateDV = value;
                OnPropertyChanged();
            }
        }

        private bool _isModifyDV;
        public bool isModifyDV
        {
            get { return _isModifyDV; }
            set
            {
                _isModifyDV = value;
                OnPropertyChanged();
            }
        }

        private bool _onActionDV;
        public bool onActionDV
        {
            get { return _onActionDV; }
            set { _onActionDV = value; OnPropertyChanged(); }
        }

        private string _myDV;
        public string myDV
        {
            get => _myDV;
            set
            {
                _myDV = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public ThemDichVuViewModel()
        {
            GetLDV();
            GetDV();

            _isCreateLDV = false;
            _isModifyLDV = false;

            CreateLDVCommand = new ActionCommand(p => CreateLDV());
            ModifyLDVCommand = new ActionCommand(p => ModifyLDV());
            DeleteLDVCommand = new ActionCommand(p => DeleteLDV());
            SaveLDVCommand = new ActionCommand(p => SaveLDV());
            CancelLDVCommand = new ActionCommand(p => CancelLDV());

            _isCreateDV = false;
            _isModifyDV = false;

            CreateDVCommand = new ActionCommand(p => CreateDV());
            ModifyDVCommand = new ActionCommand(p => ModifyDV());
            DeleteDVCommand = new ActionCommand(p => DeleteDV());
            SaveDVCommand = new ActionCommand(p => SaveDV());
            CancelDVCommand = new ActionCommand(p => CancelDV());

            DoneCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true)));
        }
        void GetLDV()
        {
            LstLoaiDichVu = DataProvider.Ins.DB.LoaiDichVus.ToList();
        }
        void GetDV()
        {
            if (_SelectedLDV == null)
                LstDichVu = new List<DichVu>();
            else
                LstDichVu = _SelectedLDV.DichVus.ToList();
        }

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;
        #region Command Loại dịch vụ
        public ICommand CreateLDVCommand { get; }
        public ICommand ModifyLDVCommand { get; }
        public ICommand DeleteLDVCommand { get; }
        public ICommand SaveLDVCommand { get; }
        public ICommand CancelLDVCommand { get; }
        #endregion

        #region Method Loại dịch vụ
        void CreateLDV()
        {
            if (!_isCreateLDV)
            {
                isCreateLDV = true;
                myLDV = "";
                onActionLDV = true;
                isModifyLDV = false;
            }
            else
            {
                isCreateLDV = false;
                myLDV = "";
                onActionLDV = false;
            }

        }
        void ModifyLDV()
        {
            if (!_isModifyLDV)
            {
                if (_SelectedLDV != null)
                {
                    isModifyLDV = true;
                    myLDV = _SelectedLDV.TenLoaiDV;
                    isCreateLDV = false;
                    onActionLDV = true;
                }
            }
            else
            {
                isModifyLDV = false;
                myLDV = "";
                onActionLDV = false;
            }
        }
        void DeleteLDV()
        {
            if (_SelectedLDV != null)
            {
                var result = MessageBox.Show("Xóa loại dịch vụ " + _SelectedLDV.TenLoaiDV + " ?", "Xóa loại dịch vụ?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {

                }
            }
        }
        void SaveLDV()
        {
            if (isCreateLDV)
            {
                // Insert Loại dịch vụ
                DataProvider.Ins.DB.LoaiDichVus.Add(new LoaiDichVu
                {
                    MaLoaiDV = "LDV-" + GetMaxID.GetMaxIdLDV(),
                    TenLoaiDV = myLDV
                });
                DataProvider.Ins.DB.SaveChanges();
                GetLDV(); // Lấy dữ liệu mới
                isCreateLDV = false; // tắt thêm mới loại dịch vụ
                isModifyLDV = false; // tắt modify loại dịch vụ
                onActionLDV = false;
            }
            else
            {
                SelectedLDV.TenLoaiDV = myLDV;
                DataProvider.Ins.DB.SaveChanges(); // Update loại dịch vụ
                GetLDV();
                SelectedLDV = null;
                isCreateLDV = false; // tắt thêm mới loại dịch vụ
                isModifyLDV = false; // tắt modify loại dịch vụ
                onActionLDV = false;
            }

        }
        void CancelLDV()
        {
            isCreateLDV = false; // tắt thêm mới loại dịch vụ
            isModifyLDV = false; // tắt modify loại dịch vụ
            onActionLDV = false;
            DataProvider.Ins.RefreshDB();
            GetLDV();
            SelectedLDV = null;
        }

        #endregion

        #region Command Dịch Vụ
        public ICommand CreateDVCommand { get; }
        public ICommand ModifyDVCommand { get; }
        public ICommand DeleteDVCommand { get; }
        public ICommand SaveDVCommand { get; }
        public ICommand CancelDVCommand { get; }
        #endregion

        #region Method Dịch vụ
        void CreateDV()
        {
            if (!_isCreateDV)
            {
                isCreateDV = true;
                myDV = "";
                onActionDV = true;
                isModifyDV = false;
            }
            else
            {
                isCreateDV = false;
                myDV = "";
                onActionDV = false;
            }
        }
        void ModifyDV()
        {
            if (!_isModifyDV)
            {
                if (_SelectedDV != null)
                {
                    isModifyDV = true;
                    myDV = _SelectedDV.TenDV;
                    isCreateDV = false;
                    onActionDV = true;
                }
            }
            else
            {
                isModifyDV = false;
                myDV = "";
                onActionDV = false;
            }
        }
        void DeleteDV()
        {
            if (_SelectedDV != null)
            {
                var result = MessageBox.Show("Xóa loại dịch vụ " + _SelectedDV.TenDV + " ?", "Xóa loại dịch vụ?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {

                }
            }
        }
        void SaveDV()
        {
            if (isCreateDV)
            {
                // Insert Loại dịch vụ
                DataProvider.Ins.DB.DichVus.Add(new DichVu
                {
                    MaDV = "DV-" + GetMaxID.GetMaxIdDV(),
                    TenDV = myDV,
                    LoaiDV=_SelectedLDVInDV.MaLoaiDV
                });
                DataProvider.Ins.DB.SaveChanges();
                GetDV(); // Lấy dữ liệu mới
                isCreateDV = false; // tắt thêm mới loại dịch vụ
                isModifyDV = false; // tắt modify loại dịch vụ
                onActionDV = false;
            }
            else
            {
                SelectedDV.TenDV = myDV;
                SelectedDV.LoaiDV = _SelectedLDVInDV.MaLoaiDV;
                DataProvider.Ins.DB.SaveChanges(); // Update loại dịch vụ
                GetDV();
                SelectedDV = null;
                isCreateDV = false; // tắt thêm mới loại dịch vụ
                isModifyDV = false; // tắt modify loại dịch vụ
                onActionDV = false;
            }
        }
        void CancelDV()
        {
            isCreateDV = false; // tắt thêm mới loại dịch vụ
            isModifyDV = false; // tắt modify loại dịch vụ
            onActionDV = false;
            //DataProvider.Ins.RefreshDB();
            //GetLDV();
            SelectedDV = null;
        }
        #endregion

        public ICommand DoneCommand { get; }
    }
}
