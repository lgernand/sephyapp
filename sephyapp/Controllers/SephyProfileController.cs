using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
        public async Task<IActionResult> GetAllSephyProfiles()
        {
            List<SephyProfile> users;
            try
            {
                users = await _dbContext.SephyProfiles.ToListAsync();
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }

            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> AddSephyProfile(AddSephyProfileRequestDTO request)
        {
            var domainModelSephyProfile = new SephyProfile
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                AccountType = request.AccountType
            };

            await _dbContext.SephyProfiles.AddAsync(domainModelSephyProfile);
            await _dbContext.SaveChangesAsync();

            return Ok(domainModelSephyProfile);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateSephyProfile(UpdateSephyProfileRequestDTO request)
        {
            var sephyProfile = await _dbContext.SephyProfiles.Where(u => u.Id == request.Id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(u => u.Email, request.Email)
                    .SetProperty(u => u.Name, request.Name)
                    .SetProperty(u => u.AccountType, request.AccountType));

            return Ok(sephyProfile);
        }
    }
}
