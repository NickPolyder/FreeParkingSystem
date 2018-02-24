using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FreeParkingSystem.Common.Models;

namespace FreeParkingSystem.Common.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : IBaseModel
    {

        public virtual TEntity GetById(string id)
        {
            throw new NotImplementedException();
        }

        public virtual TEntity GetOneByFilter(Func<TEntity, bool> filter)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<TEntity> GetByFilter<TKey>(Func<TEntity, bool> filter, Func<TEntity, TKey> orderPredicate = null)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<TEntity> GetAll<TKey>(Func<TEntity, TKey> orderPredicate = null)
        {
            throw new NotImplementedException();
        }

        #region CRUD

        public virtual void Insert(TEntity item)
        {
            throw new NotImplementedException();
        }

        public virtual Task InsertAsync(TEntity item)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(TEntity item)
        {
            throw new NotImplementedException();
        }

        public virtual Task UpdateAsync(TEntity item)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(TEntity item)
        {
            throw new NotImplementedException();
        }

        public virtual Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public virtual Task DeleteAsync(TEntity item)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
