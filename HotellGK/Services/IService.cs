using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotellGK.Services
{
    public interface IService<T>
    {
        void Add(T entity);
        List<T> GetAll();
        void Update(int id, T entity);
        void Delete(int id);
    }
}
