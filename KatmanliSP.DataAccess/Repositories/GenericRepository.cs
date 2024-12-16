using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using KatmanliSP.Core.Entities;
using KatmanliSP.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using KatmanliSP.Core.Base;

namespace KatmanliSP.DataAccess.Repositories
{
    public class GenericRepository<T>(AppDbContext context) : IGeneticRepository<T> where T : class
    {
        private DbSet<T> _dbSet = context.Set<T>(); // _dbSet üzerinden context set edebilme imkanı sağlar. bu sayede crud metot gövde doldururuz.

        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

        public void Delete(T entity) => _dbSet.Remove(entity);

        public IQueryable<T> GetAll() => _dbSet.AsQueryable().AsNoTracking();

        public Task<IEnumerable<T>> GetAllAsync() => throw new NotImplementedException();

        public async Task<T> GetByIdAsync(int id) => await _dbSet.FindAsync(); // ???
        
        public void Update(T entity) => _dbSet.Update(entity); // execute update?

        public IQueryable<T> Where(Expression<Func<T, bool>> expression) => throw new NotImplementedException(); // TODO: güncelle -DEFAULT-
    }
}
