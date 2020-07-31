using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi02.Model;

namespace WebApi02.Repository
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        Task<T> GetByIdAsync(Guid id);
        Task<T> GetNoTrackedByIdAsync(Guid id);
        Task<T> InsertAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(Guid id);
    }
}
