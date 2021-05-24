using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace crowdfunding_application.Models.Services
{
    public interface IGenericService<T> where T : ModelBase
    {
        Task<T> Create(T entity);
        Task<T> Update(int id, T entity);
        Task<T> Update(T entity);
        Task<bool> Delete(int? id);
        Task<IEnumerable<T>> GetJoin(Func<T, bool> func, params Expression<Func<T, object>>[] expressions);
        //Task<List<T>> GetJoin(Func<T, bool> func);
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int? id);
      
    }
}
