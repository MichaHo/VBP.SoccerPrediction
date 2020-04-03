using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SoccerPrediction.Repository
{
    public static class QueryableExtension
    {
        public static IQueryable<TEntity> Include<TEntity>(this IQueryable<TEntity> queryable, string prop) where TEntity : class
        {
            return EntityFrameworkQueryableExtensions.Include<TEntity>(queryable, navigationPropertyPath: prop);
        }

        public static IQueryable<TEntity> Include<TEntity, TProperty>(this IQueryable<TEntity> queryable, Expression<Func<TEntity, TProperty>> predicate)
            where TEntity : class
            where TProperty : class
        {
            return EntityFrameworkQueryableExtensions.Include<TEntity, TProperty>(queryable, predicate);
        }

    }
}
