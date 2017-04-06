using System;
using System.Linq;
using System.Linq.Expressions;

namespace Tipsters.Data.Interfaces
{
    public interface IRepository<T>
    {
        void InsertOrUpdate(T entity);

        void Delete(T entity);

        T FindByPredicate(Expression<Func<T, bool>> predicate);

        IQueryable<T> Find(Expression<Func<T, bool>> predicate);

        IQueryable<T> GetAll();

        IQueryable<T> Include(string input);
        T GetById(int id);
    }
}
