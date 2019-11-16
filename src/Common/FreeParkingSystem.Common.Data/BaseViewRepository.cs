using System;
using System.Collections.Generic;
using FreeParkingSystem.Common.Data.Models;
using FreeParkingSystem.Common.ExtensionMethods;
using Microsoft.EntityFrameworkCore;

namespace FreeParkingSystem.Common.Data
{
	public abstract class BaseViewRepository<TBusinessObject, TEntity> : BaseRepository<TBusinessObject, TEntity> where TEntity : class, IEntity where TBusinessObject : class
	{
		protected BaseViewRepository(DbContext dbContext, IMap<TEntity, TBusinessObject> mapper) : base(dbContext, mapper)
		{
		}

		public override TBusinessObject Add(TBusinessObject entity)
		{
			throw new InvalidOperationException(Resources.Errors.Views_InvalidOperation.WithArgs(Resources.Errors.Views_AddOperation));
		}

		public override IEnumerable<TBusinessObject> AddRange(IEnumerable<TBusinessObject> entities)
		{
			throw new InvalidOperationException(Resources.Errors.Views_InvalidOperation.WithArgs(Resources.Errors.Views_AddRangeOperation));
		}

		public override TBusinessObject Update(TBusinessObject entity)
		{
			throw new InvalidOperationException(Resources.Errors.Views_InvalidOperation.WithArgs(Resources.Errors.Views_UpdateOperation));
		}

		public override IEnumerable<TBusinessObject> UpdateRange(IEnumerable<TBusinessObject> entities)
		{
			throw new InvalidOperationException(Resources.Errors.Views_InvalidOperation.WithArgs(Resources.Errors.Views_UpdateRangeOperation));
		}

		public override void Delete(int id)
		{
			throw new InvalidOperationException(Resources.Errors.Views_InvalidOperation.WithArgs(Resources.Errors.Views_DeleteOperation));
		}

		public override void DeleteRange(IEnumerable<int> ids)
		{
			throw new InvalidOperationException(Resources.Errors.Views_InvalidOperation.WithArgs(Resources.Errors.Views_DeleteRangeOperation));
		}
	}
}