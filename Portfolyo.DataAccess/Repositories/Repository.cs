using Microsoft.EntityFrameworkCore;
using Portfolyo.DataAccess.Context;
using Portfolyo.Entities.Repositories;
using System.Linq.Expressions;

namespace Portfolyo.DataAccess.Repositories
{
    internal class Repository<T> : IRepository<T>
        where T : class
    {
        private readonly ApplicationDbContext applicationDbContext;

        public Repository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await applicationDbContext.Set<T>().AddAsync(entity, cancellationToken);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
        {
          return  await applicationDbContext.Set<T>().AnyAsync(expression, cancellationToken);
        }

        public IQueryable<T> GetAll()
        {
            return  applicationDbContext.Set<T>().AsNoTracking().AsQueryable(); 
        }

        public async Task<T> GetByIdAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
        {
           return await applicationDbContext.Set<T>().Where(expression).FirstOrDefaultAsync(cancellationToken);
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> expression)
        {
            return applicationDbContext.Set<T>().AsNoTracking().Where(expression).AsQueryable();
        }

        public void Remove(T entity)
        {
            applicationDbContext.Remove(entity);
        }

        public void Update(T entity)
        {
            applicationDbContext.Update(entity);
        }
    }
}
