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

        public async Task<bool> CreateAsync(params TEntity[] entities)
        {
            using var scope = DbCxFactory.CreateScope();
            using var cx = scope.GetRequiredServices();

            var set = cx.Set<TEntity>();
            set.AddRangeAsync(entities).ConfigureAwait(false);

            return await cx.SaveChangesAsync().ConfigureAwait(false) >= entities.Length;
        }

        public async Task<bool> UpdateAsync(params TEntity[] entities)
        {
            using var scope = DbCxFactory.CreateScope();
            using var cx = scope.GetRequiredServices();

            var set = cx.Set<TEntity>();
            set.UpdateRange(entities);

            return await cx.SaveChangesAsync().ConfigureAwait(false) >= entities.Length;
        }

        public async Task<bool> DeleteAsync(params TEntity[] entities)
        {
            using var scope = DbCxFactory.CreateScope();
            using var cx = scope.GetRequiredServices();

            var set = cx.Set<TEntity>();
            set.RemoveRange(entities);

            return await cx.SaveChangesAsync().ConfigureAwait(false) >= entities.Length;
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate,
            params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes)
        {
            using var scope = DbCxFactory.CreateScope();
            using var cx = scope.GetRequiredServices();

            return await AggregateIncludes(cx, includes).FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity[]> QueryAsync(
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

            return await AggregateQuary(cx, filter, select, sort, tracking, skip, take, includes).ToArrayAsync();
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
