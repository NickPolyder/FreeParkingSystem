using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FreeParkingSystem.Common.Models;

// ReSharper disable CheckNamespace
namespace FreeParkingSystem.Common.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : IBaseModel, new()
    {
        TEntity GetById(string id);

        TEntity GetOneByFilter(Func<TEntity, bool> filter);

        IEnumerable<TEntity> GetByFilter(Func<TEntity, bool> filter);

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
