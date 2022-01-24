using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PublicForum2.Domain.Interfaces.Repository
{
    public interface IBaseRepository<T>  : IDisposable where T : class
    {
        T Add(T entity);
        T Update(T entity);
        void Delete(T entity);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        Task<List<T>> ListAsync(Expression<Func<T, bool>> expression);
    }
}
