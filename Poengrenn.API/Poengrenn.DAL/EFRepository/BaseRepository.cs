using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Poengrenn.DAL.Interfaces;

namespace Poengrenn.DAL.EFRepository
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal DbContext _context;
        internal IDbSet<TEntity> _dbSet;

        public BaseRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();

            _context.Configuration.ProxyCreationEnabled = false;
            _context.Configuration.LazyLoadingEnabled = false;
        }

        public BaseRepository(DbContext context, IDbSet<TEntity> dbSet)
        {
            _context = context;
            _dbSet = dbSet;

            _context.Configuration.ProxyCreationEnabled = false;
            _context.Configuration.LazyLoadingEnabled = false;
        }

        public string FindPropertyName(Type findPropertyType)
        {
            var props = typeof(TEntity).GetProperties();
            var prop = props.FirstOrDefault(p => p.PropertyType == findPropertyType);
            return prop?.Name;
        }

        /// <summary>
        /// Return entity with filter based on a predicate, if no predicate is given all elements will be returned. </para>
        /// Sorts the elements according to a key.
        /// Includes the spesified related objects.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        /// <summary>
        /// Find entity by id
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public virtual TEntity GetByID(params object[] ids)
        {
            return _dbSet.Find(ids);
        }

        /// <summary>
        /// Returns the entity if add was successful, else null
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual TEntity Insert(TEntity entity)
        {
            var newEntity = _dbSet.Add(entity);
            var affRows = _context.SaveChanges();
            if (affRows == 0) _dbSet.Remove(entity);
            return (affRows > 0) ? newEntity : null;
        }

        /// <summary>
        /// Updates entity and returns the entity if update was successful, else null
        /// </summary>
        /// <param name="entityToUpdate"></param>
        /// <returns></returns>
        public virtual TEntity Update(TEntity entityToUpdate)
        {
            var updatedEntity = _dbSet.Attach(entityToUpdate);
            _context.Entry(updatedEntity).State = EntityState.Modified;
            var affRows = _context.SaveChanges();
            return (affRows > 0) ? updatedEntity : null;
        }

        /// <summary>
        /// Returns the entity if update was successful, else null
        /// </summary>
        /// <param name="entityToUpdate"></param>
        /// <returns></returns>
        public virtual bool UpdateRange(IList<TEntity> entitiesToUpdate)
        {
            foreach (var entityToUpdate in entitiesToUpdate)
            {
                var updatedEntity = _dbSet.Attach(entityToUpdate);
                _context.Entry(entityToUpdate).State = EntityState.Modified;
            }
            var affRows = _context.SaveChanges();
            return (affRows > 0);
        }

        /// <summary>
        /// Returns true if entity with id was deleted successfylly
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Delete(object id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            return Delete(entityToDelete);
        }

        /// <summary>
        /// Returns true if entity was deleted successfylly
        /// </summary>
        /// <param name="entityToDelete"></param>
        /// <returns></returns>
        public virtual bool Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
            var affRows = _context.SaveChanges();
            return affRows > 0;
        }

        public bool DeleteRange(IList<TEntity> entitiesToDelete)
        {
            foreach (var entityToDelete in entitiesToDelete)
            {
                if (_context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    _dbSet.Attach(entityToDelete);
                }
                _dbSet.Remove(entityToDelete);
            }
            var affRows = _context.SaveChanges();
            return affRows > 0;
        }

        public virtual bool Any(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = _dbSet;
            if (filter != null)
                query = query.Where(filter);

            return query.Any();
        }
    }
}
