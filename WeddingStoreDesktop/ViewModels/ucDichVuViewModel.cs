using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeddingStoreData.Models;
using WeddingStoreServices;

namespace WeddingStoreDesktop.ViewModels
{
    public class ucDichVuViewModel : BaseViewModel
    {
        private List<LoaiDichVuModel> _LstLoaiDichVu { get; set; }
        public List<LoaiDichVuModel> LstLoaiDichVu
        {
            get => _LstLoaiDichVu;
            set
            {
                if (_LstLoaiDichVu != value)
                {
                    _LstLoaiDichVu = value;
                    OnPropertyChanged();

                }
            }
        }

        private List<DichVuModel> _LstDichVu { get; set; }
        public List<DichVuModel> LstDichVu
        {
            get => _LstDichVu;
            set
            {
                if (_LstDichVu != value)
                {
                    _LstDichVu = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
