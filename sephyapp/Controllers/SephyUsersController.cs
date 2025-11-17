using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sephyapp.Data;
using sephyapp.Models;
using sephyapp.Models.Domain;

namespace sephyapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SephyUsersController : ControllerBase
    {
        private readonly SephyDbContext dbContext;

        public SephyUsersController(SephyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllSephyUsers()
        {
            var users = dbContext.SephyUsers.ToList();
            
            return Ok(users);
        }

        [HttpPost]
        public IActionResult AddSephyUser(AddSephyUserRequestDTO request)
        {
            var domainModelSephyUser = new SephyUser
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                AccountType = request.AccountType
            };

            dbContext.SephyUsers.Add(domainModelSephyUser);
            dbContext.SaveChanges();

            return Ok(domainModelSephyUser);
        }
    }
}
