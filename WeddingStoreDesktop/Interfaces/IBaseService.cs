using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingStoreDesktop.Interfaces
{
    public interface IBaseService<T>
    {
        List<T> GetAll();
    }
}
