using System.Data;
using Microsoft.EntityFrameworkCore;

namespace FreeParkingSystem.Common.Data
{
	public class CommonFunctionsRepository : ICommonFunctionsRepository
	{
		private readonly DbContext _dbContext;

		public CommonFunctionsRepository(DbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public bool HasActiveLeaseOnAParkingSite(int parkingSiteId)
		{
			const string query =
				"SELECT COUNT(1) FROM [Order] ord JOIN [ParkingSpot] spot ON ord.ParkingSpotId = spot.Id " +
				"WHERE spot.ParkingSiteId = @parkingSiteId AND ord.LeaseEndDate IS NULL";

			var connection = _dbContext.Database.GetDbConnection();

			using (var command = connection.CreateCommand())
			{
				command.CommandText = query;
				var parameter = command.CreateParameter();
				parameter.ParameterName = "@parkingSiteId";
				parameter.DbType = DbType.Int32;
				parameter.Value = parkingSiteId;
				command.Parameters.Add(parameter);
				connection.Open();
				var result = command.ExecuteScalar();
				connection.Close();
				return result != null && result is int ordersAgainstParkingSite && ordersAgainstParkingSite >= 1;
			}
		}

		public bool HasActiveLeaseOnAParkingSpot(int parkingSpotId)
		{
			const string query = "SELECT COUNT(1) FROM [Order] ord " +
								 "WHERE ord.ParkingSpotId = @parkingSpotId AND ord.LeaseEndDate IS NULL";

			var connection = _dbContext.Database.GetDbConnection();

			using (var command = connection.CreateCommand())
			{
				command.CommandText = query;
				var parameter = command.CreateParameter();
				parameter.ParameterName = "@parkingSpotId";
				parameter.DbType = DbType.Int32;
				parameter.Value = parkingSpotId;
				command.Parameters.Add(parameter);
				connection.Open();
				var result = command.ExecuteScalar();
				connection.Close();

				return result != null && result is int ordersAgainstParkingSpot && ordersAgainstParkingSpot >= 1;
			}
		}

		public bool ParkingSpotExists(int parkingSpotId)
		{
			const string query = "SELECT COUNT(1) FROM [ParkingSpot] " +
								 "WHERE Id = @parkingSpotId AND IsAvailable = 1";

			var connection = _dbContext.Database.GetDbConnection();

			using (var command = connection.CreateCommand())
			{
				command.CommandText = query;
				var parameter = command.CreateParameter();
				parameter.ParameterName = "@parkingSpotId";
				parameter.DbType = DbType.Int32;
				parameter.Value = parkingSpotId;
				command.Parameters.Add(parameter);
				connection.Open();
				var result = command.ExecuteScalar();
				connection.Close();

				return result != null && result is int ordersAgainstParkingSpot && ordersAgainstParkingSpot >= 1;
			}
		}

		public bool IsOwnerOfParkingSpot(int parkingSpotId, int userId)
		{
			const string query = "SELECT COUNT(1) FROM [ParkingSpot] spot " +
								 "JOIN [ParkingSite] site ON spot.ParkingSiteId = site.Id " +
								 "WHERE spot.Id = @parkingSpotId AND site.OwnerId = @ownerId";

			var connection = _dbContext.Database.GetDbConnection();

			using (var command = connection.CreateCommand())
			{
				command.CommandText = query;
				var parkingSpotParameter = command.CreateParameter();
				parkingSpotParameter.ParameterName = "@parkingSpotId";
				parkingSpotParameter.DbType = DbType.Int32;
				parkingSpotParameter.Value = parkingSpotId;
				command.Parameters.Add(parkingSpotParameter);
				var ownerIdParameter = command.CreateParameter();
				ownerIdParameter.ParameterName = "@ownerId";
				ownerIdParameter.DbType = DbType.Int32;
				ownerIdParameter.Value = userId;
				command.Parameters.Add(ownerIdParameter);

				connection.Open();
				var result = command.ExecuteScalar();
				connection.Close();

				return result != null && result is int isOwner && isOwner >= 1;
			}
		}
	}
}