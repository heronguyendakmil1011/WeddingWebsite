using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeddingStoreDesktop.Interfaces;
using WeddingStoreDesktop.Models;
using WeddingStoreDesktop.Models.SystemModel;

namespace WeddingStoreDesktop.Services
{
    public class KhoVatLieuxervice : IBaseService<KhoVatLieu>
    {
        public List<KhoVatLieu> GetAll()
        {
            return DataProvider.Ins.DB.KhoVatLieux.ToList();
        }
    }
}
