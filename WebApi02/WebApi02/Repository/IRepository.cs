using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi02.Repository
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        Task<T> GetByIdAsync(Guid id);
        Task<int> InsertAsync(Guid id);
        Task<int> UpdateAsync(Guid id);
        Task<int> DeleteAsync(Guid id);
    }
}
