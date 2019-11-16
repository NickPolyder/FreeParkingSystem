using FreeParkingSystem.Common;
using FreeParkingSystem.Common.Data;
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
	}
}