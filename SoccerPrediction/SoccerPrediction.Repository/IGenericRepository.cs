using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SoccerPrediction.Repository
{
    public interface IGenericRepository<T>
    {
        object Context { get; set; }

        Task<bool> AnyAsync(bool includeDeleted = false);
        void AddOrUpdate(T item);
        void Delete(T item);
        IQueryable<T> GetAll(bool tracking = false);
        Task<List<T>> GetAsync(IQueryable<T> query);
        T Find(int id);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, bool tracking);
        Task<T> FindByAsync(IQueryable<T> query);
        Task<bool> CreateDbIfNotExist(bool doSeeding);
        int Save();
        Task<int> SaveAsync();

        IQueryable<T> SetTracking(bool tracking, IQueryable<T> query);
    }
}
