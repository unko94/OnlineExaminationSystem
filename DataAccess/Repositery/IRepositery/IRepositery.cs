using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositery.IRepositery
{
    public interface IRepositery<T>
    {
        void Create(T entity);
        T Get(Expression<Func<T,bool>>? filter, string? include = null);
        List<T> GetAll(Expression<Func<T, bool>>? filter = null, string? include =null);
        void Delete(T entity);
        void DeleteAll(List<T> entity);
    }
}
