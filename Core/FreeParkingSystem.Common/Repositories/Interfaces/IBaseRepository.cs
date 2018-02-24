using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FreeParkingSystem.Common.Models;

// ReSharper disable CheckNamespace
namespace FreeParkingSystem.Common.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : IBaseModel
    {
        TEntity GetById(string id);

        TEntity GetOneByFilter(Func<TEntity, bool> filter);

        IEnumerable<TEntity> GetByFilter<TKey>(Func<TEntity, bool> filter, Func<TEntity, TKey> orderPredicate = null);

        IEnumerable<TEntity> GetAll<TKey>(Func<TEntity, TKey> orderPredicate = null);

        void Insert(TEntity item);

        Task InsertAsync(TEntity item);

        void Update(TEntity item);

        Task UpdateAsync(TEntity item);

        void Delete(string id);

        void Delete(TEntity item);

        Task DeleteAsync(string id);

        Task DeleteAsync(TEntity item);

    }
}
