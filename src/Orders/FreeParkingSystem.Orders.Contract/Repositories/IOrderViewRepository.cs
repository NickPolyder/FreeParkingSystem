using System.Collections.Generic;
using FreeParkingSystem.Common;

namespace FreeParkingSystem.Orders.Contract.Repositories
{
	public interface IOrderViewRepository: IRepository<OrderView>
	{
		IEnumerable<OrderView> GetActiveLeases(int userId);
		IEnumerable<OrderView> GetActiveLeasesByParking(int parkingSiteId, int userId);
	}
}