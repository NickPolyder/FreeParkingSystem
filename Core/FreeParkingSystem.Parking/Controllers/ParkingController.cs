using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreeParkingSystem.Common.Models;
using FreeParkingSystem.Parking.Infastructure;
using FreeParkingSystem.Parking.Infastructure.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeParkingSystem.Parking.Controllers
{
    [Produces("application/json")]
    [Route("api/Parking")]
    public class ParkingController : Controller
    {
        
        public IEnumerable<ParkingSpace> Get([FromQuery]ParkingQuery query)
        {
            return query?.Apply(TestData.GetParkingSpaces()).Cast<ParkingSpace>() ?? TestData.GetParkingSpaces().Cast<ParkingSpace>();
        }
    }
}