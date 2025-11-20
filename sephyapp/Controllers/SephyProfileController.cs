using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sephyapp.Data;
using sephyapp.Models;
using sephyapp.Models.Domain;

namespace sephyapp.Controllers
{
    [EnableCors("frontend")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SephyProfileController : ControllerBase
    {
        private readonly SephyDbContext _dbContext;

        public SephyProfileController(SephyDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllSephyProfiles()
        {
            List<SephyProfile> users;
            try
            {
                users = _dbContext.SephyProfiles.ToList();
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

            _dbContext.SephyProfiles.Add(domainModelSephyProfile);
            _dbContext.SaveChanges();

            return Ok(domainModelSephyProfile);
        }

        [HttpPatch]
        public IActionResult UpdateSephyProfile(UpdateSephyProfileRequestDTO request)
        {
            var SephyProfile = _dbContext.SephyProfiles.Where(u => u.Id == request.Id)
                .ExecuteUpdate(setters => setters
                    .SetProperty(u => u.Email, request.Email)
                    .SetProperty(u => u.Name, request.Name)
                    .SetProperty(u => u.AccountType, request.AccountType));

            return Ok(SephyProfile);
        }
    }
}
