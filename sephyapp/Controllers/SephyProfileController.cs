using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sephyapp.Data;
using sephyapp.Models;
using sephyapp.Models.Domain;
using sephyapp.Models.DTO;

namespace sephyapp.Controllers
{
    [EnableCors("frontend")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SephyProfileController : ControllerBase
    {
        private readonly SephyDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public SephyProfileController(SephyDbContext dbContext,  UserManager<IdentityUser> userManager)
        {
            this._dbContext = dbContext;
            this._userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetSephyProfile()
        {
            var currUser = await _userManager.GetUserAsync(HttpContext.User);
            SephyProfile profile;
            try
            {
                profile = await _dbContext.SephyProfiles.Where(p => 
                    p.User == currUser)
                    .FirstOrDefaultAsync() ?? new SephyProfile()
                {
                    Id =  Guid.Empty,
                    User = currUser,
                    Bio = "",
                    Name = "",
                    ZipCode = "",
                };
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }

            return Ok(profile);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSephyProfile(CreateSephyProfileRequestDTO request)
        {
            var currUser = await _userManager.GetUserAsync(HttpContext.User);
            
            var domainModelSephyProfile = new SephyProfile
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                ZipCode = request.ZipCode,
                Bio = request.Bio,
                User = currUser
            };

            await _dbContext.SephyProfiles.AddAsync(domainModelSephyProfile);
            await _dbContext.SaveChangesAsync();

            return Ok(domainModelSephyProfile);
        }

        /*[HttpPatch]
        public async Task<IActionResult> UpdateSephyProfile(UpdateSephyProfileRequestDTO request)
        {
            var sephyProfile = await _dbContext.SephyProfiles.Where(u => u.Id == request.Id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(u => u.Email, request.Email)
                    .SetProperty(u => u.Name, request.Name)
                    .SetProperty(u => u.AccountType, request.AccountType));

            return Ok(sephyProfile);
        }*/
    }
}
