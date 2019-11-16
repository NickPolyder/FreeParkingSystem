using FreeParkingSystem.Common;
using FreeParkingSystem.Common.Data;
using FreeParkingSystem.Orders.Contract;
using FreeParkingSystem.Orders.Contract.Repositories;
using FreeParkingSystem.Orders.Data.Models;

namespace FreeParkingSystem.Orders.Data.Repositories
{
	public class OrdersRepository : BaseRepository<Order, DbOrder>, IOrderRepository
	{
		public OrdersRepository(OrdersDbContext dbContext, IMap<DbOrder, Order> mapper) : base(dbContext, mapper)
		{
		}
	}
}