using Microsoft.EntityFrameworkCore;
using SoccerPrediction.Context;
using SoccerPrediction.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SoccerPrediction.Repository
{
    public class GenericRepository<T> : IGenericRepository<T>, IDisposable where T : class
    {

        #region Private Fields

        private readonly bool disposeContext = true;
        private object _entities = new object();
        public object Context { get => _entities; set => _entities = value; }
        //ToDo: in jedem neuen Projekt anpassen !!
        internal SPDbContext ContextInternal => (SPDbContext)Context;
        #endregion

        #region Constructor

        public GenericRepository() : this(null, false)
        { }

        public GenericRepository(bool tracking) : this(null, tracking)
        { }

        public GenericRepository(object context, bool tracking = false) : this((SPDbContext)context, tracking)
        { }

        internal GenericRepository(SPDbContext context = null, bool tracking = false)
        {
            if (!(context == null)) Debug.WriteLine($"Create new instance of GenericProvider. ProviderName:{context.Database.ProviderName}");
            else Debug.WriteLine($"Create new instance of GenericProvider without context.");

            if (context != null)
            {
                Context = context;
                disposeContext = false;
            }
            else
            {
                Context = new SPDbContext();
            }

            ContextInternal.ChangeTracker.AutoDetectChangesEnabled = tracking;

            if (tracking) ContextInternal.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
            else ContextInternal.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        #endregion

        #region IGenericRepository

        public virtual async Task<bool> AnyAsync(bool includeDeleted = false)
        {
            if (!includeDeleted)
                return await ((SPDbContext)_entities).Set<T>().Where(e => ((ILogicalDelete)e).DeletedFlag == false).AnyAsync().ConfigureAwait(true);
            else
                return await ((SPDbContext)_entities).Set<T>().AnyAsync().ConfigureAwait(true);
        }

        public virtual void AddOrUpdate(T item)
        {
            Debug.WriteLine($"Add or Update (entity) of Type {typeof(T).ToString()}");
            ((SPDbContext)_entities).Set<T>().Update(item);
        }

        public virtual void Delete(T item)
        {
            Debug.WriteLine($"Delete (entity) of Type {typeof(T).ToString()}");
            ((SPDbContext)_entities).Set<T>().Remove(item);
        }

        public virtual IQueryable<T> GetAll(bool tracking = false)
        {
            Debug.WriteLine($"Calling GetAll. Tracking: {tracking.ToString()}");
            IQueryable<T> query = ((SPDbContext)_entities).Set<T>();
            if (tracking) query = query.AsTracking();
            Debug.WriteLine("Get All - Return Query");
            return query;
        }

        public virtual async Task<List<T>> GetAsync(IQueryable<T> query)
        {
            Debug.WriteLine($"GetAllAsync (Direct return) for T of Type {typeof(T).ToString()}");
            return await query.ToListAsync();
        }

        public virtual T Find(int id)
        {
            Debug.WriteLine($"Find (Direct return) for T of Type {typeof(T).ToString()}");
            return ((SPDbContext)_entities).Set<T>().Find(id);
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, bool tracking)
        {
            Debug.WriteLine($"FindBy for T of Type {typeof(T).ToString()} - Tracking: {tracking.ToString()}");
            IQueryable<T> query = ((SPDbContext)_entities).Set<T>().Where(predicate);
            if (tracking) query = query.AsTracking();
            Debug.WriteLine("FindBy - Return Query");
            return query;
        }

        public virtual async Task<T> FindByAsync(IQueryable<T> query)
        {
            Debug.WriteLine($"FindByAsync for T of Type {typeof(T).ToString()}");
            return await query.SingleOrDefaultAsync();
        }
        public virtual async Task<bool> CreateDbIfNotExist(bool doSeeding)
        {
            bool ret = await ContextInternal.Database.EnsureCreatedAsync();
            if (doSeeding && ret) await Seed.CreatePeopleOnNewDb(ContextInternal).ConfigureAwait(true);
            return ret;
        }

        public virtual int Save()
        {
            Debug.WriteLine(GetStatistic());
            ContextInternal.ChangeTracker.DetectChanges();
            int anz = ContextInternal.SaveChanges();
            Debug.WriteLine($"GenericRepository:Save returned {anz.ToString()} changes saved successfully");
            return anz;
        }

        public virtual async Task<int> SaveAsync()
        {
            Debug.WriteLine(GetStatistic());
            ContextInternal.ChangeTracker.DetectChanges();
            int anz = await ContextInternal.SaveChangesAsync();
            Debug.WriteLine($"GenericRepository:Save returned {anz.ToString()} changes saved successfully");
            return anz;
        }

        public virtual IQueryable<T> SetTracking(bool tracking, IQueryable<T> query)
        {
            Debug.WriteLine($"SetTracking for Query of Type '{typeof(T).ToString()}' to '{tracking.ToString()}' and Return modified Query");
            if (tracking) return query.AsTracking();
            else return query.AsNoTracking();
        }

        #endregion

        #region Helper Methods

        public virtual bool HasContextChanges()
        {
            Debug.WriteLine("GenericRepository:HasContextChanges...");
            Debug.WriteLine("Detecting changes");
            if (ContextInternal == null) return false;
            ContextInternal.ChangeTracker.DetectChanges();
            if (ContextInternal.ChangeTracker.HasChanges())
            {
                Debug.WriteLine("Context:ChangeTracker has changes, returning true");
                return true;
            }
            else
            {
                Debug.WriteLine("Context:ChangeTracker has NO changes, returning false");
                return false;
            }
        }

        protected virtual string GetStatistic<TEntity>() where TEntity : class
        {
            string statistic = "";
            statistic += "Changed: " + ContextInternal.ChangeTracker.Entries<TEntity>().Where(x => x.State == EntityState.Modified).Count().ToString();
            statistic += "New: " + ContextInternal.ChangeTracker.Entries<TEntity>().Where(x => x.State == EntityState.Added).Count().ToString();
            statistic += "Deleted: " + ContextInternal.ChangeTracker.Entries<TEntity>().Where(x => x.State == EntityState.Deleted).Count().ToString();
            Debug.WriteLine(statistic);
            return statistic;
        }

        protected virtual string GetStatistic()
        {
            string statistic = "";
            statistic += "Changed: " + ContextInternal.ChangeTracker.Entries().Where(x => x.State == EntityState.Modified).Count().ToString();
            statistic += "New: " + ContextInternal.ChangeTracker.Entries().Where(x => x.State == EntityState.Added).Count().ToString();
            statistic += "Deleted: " + ContextInternal.ChangeTracker.Entries().Where(x => x.State == EntityState.Deleted).Count().ToString();
            Debug.WriteLine(statistic);
            return statistic;
        }

        public virtual TEntity GetEntityOriginalValue<TEntity>(TEntity entity) where TEntity : class
        {
            return (TEntity)ContextInternal.Entry<TEntity>(entity).OriginalValues.ToObject();
        }

        public virtual TEntity GetEntityDbValue<TEntity>(TEntity entity) where TEntity : class
        {
            return (TEntity)ContextInternal.Entry<TEntity>(entity).GetDatabaseValues().ToObject();
        }

        public virtual void DetachAll()
        {
            Debug.WriteLine("Detach all Entities!!");
            ContextInternal.DetachAll();
        }
        #endregion

        #region IDisposable

        public void Dispose()
        {
            if (disposeContext)
            {
                Debug.WriteLine("Disposing GenericRepository...");
                ContextInternal.Dispose();
                Debug.WriteLine("GenericRepository Disposed");
            }
            else
            {
                Debug.WriteLine("Do not Dispose Context of GenericRepository because its created from another place!!");
            }
        }

        #endregion
    }

}
