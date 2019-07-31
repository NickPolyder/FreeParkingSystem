using System;
using System.Collections.Generic;

namespace FreeParkingSystem.Common
{
	public interface IRepository<TBusinessModel> : IDisposable
	{
		TBusinessModel Get(int id);

		IEnumerable<TBusinessModel> GetAll();

		TBusinessModel Add(TBusinessModel entity);

		IEnumerable<TBusinessModel> AddRange(IEnumerable<TBusinessModel> entities);

		TBusinessModel Update(TBusinessModel entity);

		IEnumerable<TBusinessModel> UpdateRange(IEnumerable<TBusinessModel> entities);

		void Delete(int id);

		void DeleteRange(IEnumerable<int> ids);
	}
}