using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KatmanliSP.Core.Base
{
    public interface IGeneticRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id); //
        Task AddAsync(T entity); //
        void Update(T entity); //
        void Delete(T entity); //
        IQueryable<T> GetAll(); //
        IQueryable<T> Where(Expression<Func<T, bool>> expression); //

        // ihtiyaç halinde aç.
        /*
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task AddRangeAsync(IEnumerable<T> entities);
        void DeleteRange(IEnumerable<T> entities);
        IQueryable<T> Queryable();
        */
    }
}

