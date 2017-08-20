using System;
using FreeParkingSystem.Common.Models;
using X.PagedList;

namespace FreeParkingSystem.Common.Repositories
{
    public abstract class BasePagedRepository<TEntity> : BaseRepository<TEntity>, IBasePagedRepository<TEntity> where TEntity : IBaseModel, new()
    {
        public virtual IPagedList<TEntity> GetByFilter(Func<TEntity, bool> filter, int skip, int take)
        {
            throw new NotImplementedException();
        }

        public virtual IPagedList<TEntity> GetByFilter<TKey>(Func<TEntity, bool> filter, Func<TEntity, TKey> orderBy, int skip, int take)
        {
            throw new NotImplementedException();
        }
    }
}
