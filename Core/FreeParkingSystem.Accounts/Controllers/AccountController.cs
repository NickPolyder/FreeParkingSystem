using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreeParkingSystem.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeParkingSystem.Accounts.Controllers
{
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {

        // GET: api/Account/5
        [HttpGet("{id}", Name = "Get")]
        public User Get(string id)
        {
            var rand = new Random();
            var dateTime = DateTime.Today.AddDays(rand.Next(-100, 100));
            var user = new User
            {
                Id = id,
                Active = true,
                CreatedAt = dateTime,
                UpdatedAt = dateTime,
                CreatedBy = Common.Models.User.SysAdmin,
                FirstName = "First Name",
                LastName = "Last Name",
                Email = "email@email.com",
                Phone = "0030634423424",
            };
            user.AddRole(Role.Administrator());
            return user;
        }

        // POST: api/Account
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Account/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Account/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
