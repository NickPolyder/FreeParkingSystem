using System;
using FreeParkingSystem.Common.Models;
using X.PagedList;

// ReSharper disable CheckNamespace
namespace FreeParkingSystem.Common.Repositories
{
    public interface IBasePagedRepository<TEntity> : IBaseRepository<TEntity> where TEntity : IBaseModel
    {

        IPagedList<TEntity> GetByFilter(Func<TEntity, bool> filter, int skip, int take);

        IPagedList<TEntity> GetByFilter<TKey>(Func<TEntity, bool> filter, Func<TEntity, TKey> orderBy, int skip, int take);

        IPagedList<TEntity> GetAll<TKey>(Func<TEntity, TKey> orderBy, int skip, int take);
    }
}
