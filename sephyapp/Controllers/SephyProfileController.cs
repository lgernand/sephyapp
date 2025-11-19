using Microsoft.AspNetCore.Cors;
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
    public class SephyProfileController : ControllerBase
    {
        private readonly SephyDbContext dbContext;

        public SephyProfileController(SephyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [EnableCors("frontend")]
        public IActionResult GetAllSephyProfiles()
        {
            List<SephyProfile> users;
            try
            {
                users = dbContext.SephyProfiles.ToList();
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }

            return Ok(users);
        }

        [HttpPost]
        public IActionResult AddSephyProfile(AddSephyProfileRequestDTO request)
        {
            var domainModelSephyProfile = new SephyProfile
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                AccountType = request.AccountType
            };

            dbContext.SephyProfiles.Add(domainModelSephyProfile);
            dbContext.SaveChanges();

            return Ok(domainModelSephyProfile);
        }

        [HttpPatch]
        public IActionResult UpdateSephyProfile(UpdateSephyProfileRequestDTO request)
        {
            var SephyProfile = dbContext.SephyProfiles.Where(u => u.Id == request.Id)
                .ExecuteUpdate(setters => setters
                    .SetProperty(u => u.Email, request.Email)
                    .SetProperty(u => u.Name, request.Name)
                    .SetProperty(u => u.AccountType, request.AccountType));

            return Ok(SephyProfile);
        }
    }
}
