using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeddingStoreDesktop.Models.SystemModel;
using WeddingStoreDesktop.Interfaces.Dialog;
using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Models.DesktopModel;
using System.Windows.Input;
using System.Windows;

namespace WeddingStoreDesktop.ViewModels
{
    public class DiemDanhNhanVienViewModel : BaseViewModel, IDialogRequestClose
    {
        private PhanCongNhanVienModel _myPCNV;
        public PhanCongNhanVienModel myPCNV
        {
            get { return _myPCNV; }
            set { _myPCNV = value; OnPropertyChanged(); }
        }

        private TimeSpan _tongThoiGian;
        public TimeSpan tongThoiGian
        {
            get { return _tongThoiGian; }
            set { _tongThoiGian = value; OnPropertyChanged(); }
        }

        private DateTime _myTime;
        public DateTime myTime
        {
            get => _myTime;
            set
            {
                _myTime = value;
                OnPropertyChanged();
            }
        }

        private bool _isDen { get; set; }
        public bool isDen
        {
            get => _isDen;
            set
            {
                _isDen = value;
                OnPropertyChanged();
            }
        }

        private bool _isDi { get; set; }
        public bool isDi
        {
            get => _isDi;
            set
            {
                _isDi = value;
                OnPropertyChanged();
            }
        }

        public DiemDanhNhanVienViewModel(PhanCongNhanVienModel pcNV)
        {
            myPCNV = pcNV;
            if (String.IsNullOrEmpty(myPCNV.ThoiGianDen) || TimeSpan.Parse(myPCNV.ThoiGianDen).CompareTo(TimeSpan.Zero) == 0)
            {
                isDen = true;
                isDi = false;
            }
            else
            {
                isDen = false;
                isDi = true;
            }

            if (!String.IsNullOrEmpty(myPCNV.ThoiGianDen) && !String.IsNullOrEmpty(myPCNV.ThoiGianDi)
                    && TimeSpan.Parse(myPCNV.ThoiGianDen) != TimeSpan.Zero && TimeSpan.Parse(myPCNV.ThoiGianDi) != TimeSpan.Zero)
                tongThoiGian = TimeSpan.Parse(myPCNV.ThoiGianDi) - TimeSpan.Parse(myPCNV.ThoiGianDen);
            else
                tongThoiGian = TimeSpan.Zero;

            SaveCommand = new ActionCommand(p => Save());
            DoneCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true)));
            CancelCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false)));
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand DoneCommand { get; }

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        void Save()
        {
            if (_myTime != null)
            {
                if (_isDen) // thêm thời gian đến
                {
                    if (!String.IsNullOrEmpty(myPCNV.ThoiGianDi) && TimeSpan.Parse(myPCNV.ThoiGianDi).CompareTo(TimeSpan.Zero) != 0) // kiểm tra đã điểm danh đến chưa
                    {
                        // kiểm tra thời gian có hợp lệ hay không, -1 tức đi < _myTime (thời gian đến)
                        if (TimeSpan.Parse(myPCNV.ThoiGianDi).CompareTo(_myTime.TimeOfDay) == -1)
                            MessageBox.Show("Thời gian đến phải nhỏ hơn thời gian đi", "Fail!!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        else
                            myPCNV.ThoiGianDen = _myTime.TimeOfDay.ToString();
                    }
                    else
                        myPCNV.ThoiGianDen = _myTime.TimeOfDay.ToString();
                }
                else // Thêm thời gian đi
                {
                    if (!String.IsNullOrEmpty(myPCNV.ThoiGianDen) && TimeSpan.Parse(myPCNV.ThoiGianDen).CompareTo(TimeSpan.Zero) != 0)
                    {
                        // kiểm tra thời gian có hợp lệ hay không, 1 tức đến > _myTime (thời gian đi)
                        if (TimeSpan.Parse(myPCNV.ThoiGianDen).CompareTo(_myTime.TimeOfDay) == 1)
                            MessageBox.Show("Thời gian đi phải lớn hơn thời gian đén", "Fail!!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        else
                            myPCNV.ThoiGianDi = _myTime.TimeOfDay.ToString();
                    }
                    else
                        myPCNV.ThoiGianDi = _myTime.TimeOfDay.ToString();
                }

                if (!String.IsNullOrEmpty(myPCNV.ThoiGianDen) && !String.IsNullOrEmpty(myPCNV.ThoiGianDi)
                    && TimeSpan.Parse(myPCNV.ThoiGianDen).CompareTo(TimeSpan.Zero) != 0
                    && TimeSpan.Parse(myPCNV.ThoiGianDi).CompareTo(TimeSpan.Zero) != 0)
                    tongThoiGian = TimeSpan.Parse(myPCNV.ThoiGianDi) - TimeSpan.Parse(myPCNV.ThoiGianDen);
            }

        }
    }
}
