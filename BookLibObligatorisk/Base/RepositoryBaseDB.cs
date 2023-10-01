using BookLibObligatorisk.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookLibObligatorisk.Base
{
    /// <summary>
    /// Base class for repositories using a Database. Implements IRepository<T> where T is a class that implements IHasId, IValidateable and IUpdateable<T>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RepositoryBaseDB<T> : IRepository<T> where T : class, IHasId, IValidateable, IUpdateable<T>
    {
        protected DbContext _dbContext;

        /// <summary>
        /// Constructor for RepositoryBaseDB. Takes a DbContext as parameter.
        /// </summary>
        /// <param name="dbContext"></param>
        public RepositoryBaseDB(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public T Add(T entity)
        {
            entity.Validate();
            entity.Id = 0;
            _dbContext.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        /// <inheritdoc />
        public T? Delete(int id)
        {
            T? entityToDelete = _dbContext.Find<T>(id);
            if (entityToDelete != null)
            {
                _dbContext.Remove(entityToDelete);
                _dbContext.SaveChanges();
            }
            return entityToDelete;
        }

        /// <inheritdoc />
        public IEnumerable<T> Get()
        {
            return _dbContext.Set<T>();
        }

        /// <inheritdoc />
        public T? GetById(int id)
        {
            return _dbContext.Find<T>(id);
        }

        /// <inheritdoc />
        public T? Update(int id, T entity)
        {
            entity.Validate();
            T? entityToUpdate = _dbContext.Find<T>(id);
            if (entityToUpdate == null)
            {
                return null;
            }
            T updatedEntity = entityToUpdate.Update(entity);
            _dbContext.SaveChanges();
            return updatedEntity;
        }
    }
}
