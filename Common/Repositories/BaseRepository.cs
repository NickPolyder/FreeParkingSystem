using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeParkingSystem.Common.Models;

namespace FreeParkingSystem.Common.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : IBaseModel, new()
    {

        public virtual TEntity GetById(string id)
        {
            throw new NotImplementedException();
        }

        public virtual TEntity GetOneByFilter(Func<TEntity, bool> filter)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<TEntity> GetByFilter(Func<TEntity, bool> filter)
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
