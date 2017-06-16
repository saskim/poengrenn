using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Poengrenn.DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Find the property name for the specified type
        /// </summary>
        /// <param name="findPropertyType"></param>
        /// <returns></returns>
        string FindPropertyName(Type findPropertyType);

        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        TEntity GetByID(params object[] ids);

        TEntity Insert(TEntity entity);

        TEntity Update(TEntity entityToUpdate);

        bool UpdateRange(IList<TEntity> entitiesToUpdate);

        bool Delete(object id);

        bool Delete(TEntity entityToDelete);

        bool DeleteRange(IList<TEntity> entitiesToDelete);

        bool Any(Expression<Func<TEntity, bool>> filter = null);
    }
}