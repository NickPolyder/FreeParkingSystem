using System.Collections.Generic;
using System.Linq;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.Data;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Orders.Contract;
using FreeParkingSystem.Orders.Contract.Repositories;
using FreeParkingSystem.Orders.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FreeParkingSystem.Orders.Data.Repositories
{
	public class OrderViewRepository: BaseViewRepository<OrderView, DbOrderView>, IOrderViewRepository
	{
		public OrderViewRepository(OrdersDbContext dbContext, IMap<DbOrderView, OrderView> mapper) : base(dbContext, mapper)
		{
		}

		public IEnumerable<OrderView> GetActiveLeases(int userId)
		{
			var orders = GetDataSetWithIncludes()
				.Where(order => !order.LeaseEndDate.HasValue
				                && !order.IsCancelled
				                && (order.TenantId == userId || order.OwnerId == userId));

			return Mapper.Map(orders);
		}

		public IEnumerable<OrderView> GetActiveLeasesByParking(int parkingSiteId, int userId)
		{
			var orders = GetDataSetWithIncludes()
				.Where(order => order.ParkingSiteId == parkingSiteId 
				                && !order.LeaseEndDate.HasValue 
								&& !order.IsCancelled
				                && (order.TenantId == userId || order.OwnerId == userId));

			return Mapper.Map(orders);
		}
	}
}