using System;
using System.Collections.Generic;
using FreeParkingSystem.Common.Models;
using X.PagedList;

namespace FreeParkingSystem.Common.Services
{
    public interface IStoreService<TEntity> where TEntity : IBaseModel
    {

        IServiceResult<TEntity> Find(string id);

        IServiceResult<TEntity> Find(Func<TEntity, bool> predicate);

        IServiceResult<IEnumerable<TEntity>> Get<TKey>(Func<TEntity, bool> wherePredicate = null, Func<TEntity, TKey> orderPredicate = null);

        IServiceResult<IPagedList<TEntity>> Get<TKey>(int skip, int take, Func<TEntity, TKey> orderPredicate, Func<TEntity, bool> wherePredicate = null);
    }
}
