using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreeParkingSystem.Common.Helpers;
using FreeParkingSystem.Common.Models;

namespace FreeParkingSystem.Parking.Infastructure
{
    public static class TestData
    {
        private static List<IParkingSpace> _parkingSpaces;

        public static IEnumerable<IParkingSpace> GetParkingSpaces()
        {
            CreateParkingSpaces();

            return _parkingSpaces;
        }

        private static void CreateParkingSpaces()
        {
            if (!(_parkingSpaces != null && _parkingSpaces.Count > 0))
            {
                _parkingSpaces = new List<IParkingSpace>();


                var rand = new Random();
                var iterations = rand.Next(20, 200);
                for (int index = 0; index < iterations; index++)
                {
                    var createdAt = DateTimeOffset.Now.AddDays(rand.Next(-20, 300));
                    var floors = rand.Next(1, 6);
                    _parkingSpaces.Add(new ParkingSpace
                    {
                        Id = Guid.NewGuid().ToString(),
                        CreatedBy = User.SysAdmin,
                        CreatedAt = createdAt,
                        UpdatedAt = createdAt.AddDays(rand.Next(0, 600)),
                        Description = $"Description {index}",
                        Floors = floors,
                        HasSpecialParkSpaces = rand.NextBool(),
                        IsDeleted = rand.NextBool(),
                        MaxHeightOfVehicle = (1 + rand.NextDouble()) * 10,
                        ParkSpaces = floors * rand.Next(20, 200),
                        WidthOfParkSpot = (1 + rand.NextDouble()) * 4,
                        ParkingType = ParkingType.Free,
                        Position = new Position(rand.NextDouble() * 10000, rand.NextDouble() * 10000,
                            Guid.NewGuid().ToString()),
                    });
                }
            }
        }
    }
}
