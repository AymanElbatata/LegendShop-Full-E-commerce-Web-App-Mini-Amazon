using AymanStore.DAL.Entities;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AymanStore.BLL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(int? id);
        IEnumerable<T> GetAll();
        int Add(T obj);
        int Update(T obj);
        //int Delete(T obj);

        IEnumerable<T> GetAllCustomized(
        Expression<Func<T, bool>> filter = null,
        params Expression<Func<T, object>>[] includes);
    }
}
