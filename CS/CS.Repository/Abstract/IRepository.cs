using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS.Repository.Abstract
{
    public interface IRepository<T>
    {
        void Add(T item);
        List<T> GetAll();
    }
}
