using System.Collections.Generic;
using System.Linq;
using FreeParkingSystem.Common.Models;

namespace FreeParkingSystem.Parking.Infastructure.Queries
{
    public class ParkingQuery:BaseQuery<IParkingSpace>
    {
        public string ParkingId { get; set; }

        public string ParkingType { get; set; }

        public bool? HasSpecialParkSpaces { get; set; }

        public override IEnumerable<IParkingSpace> Apply(IEnumerable<IParkingSpace> query)
        {
            if (query == null) return Enumerable.Empty<IParkingSpace>();

            var runQuery = query;

            if (HasSpecialParkSpaces.HasValue)
            {
                runQuery = runQuery.Where(tt => tt.HasSpecialParkSpaces == HasSpecialParkSpaces.Value);
            }

            if (!string.IsNullOrEmpty(ParkingId))
            {
                runQuery = runQuery.Where(tt => tt.Id == ParkingId);
            }

            if (!string.IsNullOrEmpty(ParkingType))
            {
                runQuery = runQuery.Where(tt => tt.ParkingType!=null &&
                                                tt.ParkingType.Text == ParkingType);
            }

            return runQuery;
        }
    }
}