using DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework
{
    public class GenericRepository<TContext, TEntity> : IRepository<TEntity>
        where TContext : DbContext
        where TEntity : class, IEntity
    {
        protected readonly IServiceScopeFactory<TContext> DbCxFactory;

        public GenericRepository(IServiceScopeFactory<TContext> dbCxFactory) =>
            DbCxFactory = dbCxFactory;

        public bool Create(params TEntity[] entities)
        {
            using var scope = DbCxFactory.CreateScope();
            using var cx = scope.GetRequiredServices();

            var set = cx.Set<TEntity>();
            set.AddRange(entities);

            return cx.SaveChanges() >= entities.Length;
        }

        public bool Update(params TEntity[] entities)
        {
            using var scope = DbCxFactory.CreateScope();
            using var cx = scope.GetRequiredServices();

            var set = cx.Set<TEntity>();
            set.UpdateRange(entities);

            return cx.SaveChanges() >= entities.Length;
        }

        public bool Delete(params TEntity[] entities)
        {
            using var scope = DbCxFactory.CreateScope();
            using var cx = scope.GetRequiredServices();

            var set = cx.Set<TEntity>();
            set.RemoveRange(entities);

            return cx.SaveChanges() >= entities.Length;
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate,
            params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes)
        {
            using var scope = DbCxFactory.CreateScope();
            using var cx = scope.GetRequiredServices();

            return AggregateIncludes(cx, includes).FirstOrDefault(predicate);
        }

        public TEntity[] Query(
            Expression<Func<TEntity, bool>> filter = null,
            Expression<Func<TEntity, TEntity>> select = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sort = null,
            bool tracking = true,
            int? skip = null,
            int? take = null,
            params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes)
        {
            using var scope = DbCxFactory.CreateScope();
            using var cx = scope.GetRequiredServices();

            return AggregateQuary(cx, filter, select, sort, tracking, skip, take, includes).ToArray();
        }

        private static IQueryable<TEntity> AggregateQuary(
            TContext cx,
            Expression<Func<TEntity, bool>> filter = null,
            Expression<Func<TEntity, TEntity>> select = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sort = null,
            bool tracking = true,
            int? skip = null,
            int? take = null,
            params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes)
        {
            var items = AggregateIncludes(cx, includes);

            if (filter != null)
            {
                items = items.Where(filter);
            }

            if (select != null)
            {
                items = items.Select(select);
            }

            if (sort != null)
            {
                items = sort(items);
            }

            if (!tracking)
            {
                items.AsNoTracking();
            }

            if (skip.HasValue)
            {
                items = items.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                items = items.Take(take.Value);
            }

            return items;
        }

        private static IQueryable<TEntity> AggregateIncludes(
            TContext cx,
            params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes) =>
                includes.Aggregate((IQueryable<TEntity>)cx.Set<TEntity>(),
                    (current, include) => include(current)).AsQueryable();
    }
}
