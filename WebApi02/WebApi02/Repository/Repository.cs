using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi02.Model;
using Microsoft.EntityFrameworkCore;

namespace WebApi02.Repository
{
    public class Repository<T> : IRepository<T> where T : class, IModel
    {
        protected ContenerContext context;

        public Repository(ContenerContext _context)
        {
            this.context = _context;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var paraborrar = await this.context.Set<T>().SingleOrDefaultAsync(i => i.Id == id);
            this.context.Set<T>().Remove(paraborrar);
            return await this.context.SaveChangesAsync();
        }

        public IQueryable<T> GetAll()
        {
            return this.context.Set<T>();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await this.context.Set<T>().SingleOrDefaultAsync(i => i.Id == id);
        }

        public async Task<int> InsertAsync(Guid id)
        {
            var parainsertar = await this.context.Set<T>().SingleOrDefaultAsync(i => i.Id == id);
            this.context.Set<T>().Add(parainsertar);
            return await this.context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(Guid id)
        {
            var paraactualizar = await this.context.Set<T>().SingleOrDefaultAsync(i => i.Id == id);
            this.context.Set<T>().Update(paraactualizar);
            return await this.context.SaveChangesAsync();
        }
    }
}
