using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            List<SephyUser> users;
            try
            {
                users = dbContext.SephyUsers.ToList();
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }

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

        [HttpPatch]
        public IActionResult UpdateSephyUser(UpdateSephyUserRequestDTO request)
        {
            var sephyUser = dbContext.SephyUsers.Where(u => u.Id == request.Id)
                .ExecuteUpdate(setters => setters
                    .SetProperty(u => u.Email, request.Email)
                    .SetProperty(u => u.Name, request.Name)
                    .SetProperty(u => u.AccountType, request.AccountType));

            return Ok(sephyUser);
        }
    }
}
