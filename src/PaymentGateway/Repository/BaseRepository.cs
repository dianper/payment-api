namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Repository.Interfaces;
    using Repository.Models;

    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly PaymentContext context;
        private readonly DbSet<T> dbSet;

        public BaseRepository(PaymentContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllByExpressionAsync(Expression<Func<T, bool>> expression)
        {
            return await this.dbSet.AsNoTracking().Where(expression).ToListAsync();
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            return this.dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> InsertAsync(T model)
        {
            await this.dbSet.AddAsync(model);
            await this.context.SaveChangesAsync();

            return model;
        }
    }
}