using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi02.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

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
            return await context.Set<T>().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> GetNoTrackedByIdAsync(Guid id)
        {
            return await context.Set<T>().AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> InsertAsync(T entity)
        {
            this.context.Set<T>().Add(entity);
            await this.context.SaveChangesAsync();
            return entity;
        }

        public async Task<int> UpdateAsync(T entity)
        {
            this.context.Set<T>().Update(entity);
            return await this.context.SaveChangesAsync();
        }
        public ContenerContext Context
        {
            get
            {
                return context;
            }
        }
        


    }
}
