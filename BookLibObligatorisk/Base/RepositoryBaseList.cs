using BookLibObligatorisk.Interfaces;

namespace BookLibObligatorisk.Base
{
    /// <summary>
    /// Base class for repositories using a List. Implements IRepository<T> where T is a class that implements IHasId, IValidateable and IUpdateable<T>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RepositoryBaseList<T> : IRepository<T> where T : class, IHasId, IValidateable, IUpdateable<T>
    {
        protected readonly List<T> _list;
        protected int _nextId;

        public RepositoryBaseList()
        {
            _list = new List<T>();
            _nextId = 1;
        }

        /// <inheritdoc />
        public T Add(T entity)
        {
            entity.Validate();
            entity.Id = _nextId++;
            _list.Add(entity);
            return entity;
        }

        /// <inheritdoc />
        public T? Delete(int id)
        {
            T? entityToDelete = _list.FirstOrDefault(e => e.Id == id);
            if (entityToDelete == null)
            {
                return null;
            }
            _list.Remove(entityToDelete);
            return entityToDelete;
        }

        /// <inheritdoc />
        public IEnumerable<T> Get()
        {
            return _list;
        }

        /// <inheritdoc />
        public T? GetById(int id)
        {
            return _list.FirstOrDefault(e => e.Id == id);
        }

        /// <inheritdoc />
        public T? Update(int id, T data)
        {
            data.Validate();
            T? entityToUpdate = _list.FirstOrDefault(e => e.Id == id);
            if (entityToUpdate == null)
            {
                return null;
            }
            return entityToUpdate.Update(data);
        }
    }
}
