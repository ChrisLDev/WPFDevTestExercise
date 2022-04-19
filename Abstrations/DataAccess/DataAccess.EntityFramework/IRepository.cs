using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EntityFramework
{
    public interface IRepository<TEntity> 
        where TEntity : class, IEntity
    {
        /// <summary>
        /// Filters the <see cref="DbSet{TEntity}"/> based on a predicate, sorts in ascending order,
        /// skips a number of entities,
        /// returns the specified number of entities. 
        /// Includes specified which related entities to include in the query results
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="select"></param>
        /// <param name="sort"></param>
        /// <param name="tracking"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        TEntity[] Query(
            Expression<Func<TEntity, bool>> filter = null,
            Expression<Func<TEntity, TEntity>> select = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sort = null,
            bool tracking = true,
            int? skip = null,
            int? take = null,
            params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes);

        /// <summary>
        /// Retrieves the first entity from the <see cref="DbSet{TEntity}"/> that satisfies the specified condition
        /// otherwise returns default value
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        TEntity Get(Expression<Func<TEntity, bool>> predicate,
            params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes);

        /// <summary>
        /// Inserts the specified entities into the <see cref="DbSet{TEntity}"/>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        bool Create(params TEntity[] entities);

        /// <summary>
        /// Updates the specified entities in the <see cref="DbSet{TEntity}"/>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        bool Update(params TEntity[] entities);

        /// <summary>
        /// Removes the specified entities from the <see cref="DbSet{TEntity}"/>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        bool Delete(params TEntity[] entities);
    }
}
