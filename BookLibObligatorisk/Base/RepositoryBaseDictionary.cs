using BookLibObligatorisk.Interfaces;

namespace BookLibObligatorisk.Base
{
    /// <summary>
    /// Base class for repositories using a Dictionary. Implements IRepository<T> where T is a class that implements IHasId, IValidateable and IUpdateable<T>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RepositoryBaseDictionary<T> : IRepository<T> where T : class, IHasId, IValidateable, IUpdateable<T>
    {
        protected readonly Dictionary<int, T> _dictionary;
        protected int _nextId;
        public RepositoryBaseDictionary()
        {
            _dictionary = new Dictionary<int, T>();
            _nextId = 1;
        }

        /// <inheritdoc />
        public T Add(T entity)
        {
            entity.Validate();
            entity.Id = _nextId++;
            _dictionary.Add(entity.Id, entity);
            return entity;
        }

        /// <inheritdoc />
        public T? Delete(int id)
        {
            T? entityToDelete = _dictionary.GetValueOrDefault(id);
            if (entityToDelete == null)
            {
                return null;
            }
            _dictionary.Remove(id);
            return entityToDelete;
        }

        /// <inheritdoc />
        public IEnumerable<T> Get()
        {
            return _dictionary.Values;
        }

        /// <inheritdoc />
        public T? GetById(int id)
        {
            return _dictionary.GetValueOrDefault(id);
        }

        /// <inheritdoc />
        public T? Update(int id, T entity)
        {
            entity.Validate();
            T? entityToUpdate = _dictionary.GetValueOrDefault(id);
            if (entityToUpdate == null)
            {
                return null;
            }
            return entityToUpdate.Update(entity);
        }
    }
}
