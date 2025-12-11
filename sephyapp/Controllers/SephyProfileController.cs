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
    public class SephyProfileController(SephyDbContext dbContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleService)
        : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetSephyProfile()
        {
            var currUser = await userManager.GetUserAsync(HttpContext.User);
            SephyProfile profile;
            try
            {
                profile = await dbContext.SephyProfiles.Where(p => 
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

            var dto = new SephyProfileDto()
            {
                Id = profile.Id.ToString(),
                Bio = profile.Bio,
                Name = profile.Name,
                ZipCode = profile.ZipCode,
                Role = userManager.GetRolesAsync(currUser).Result.FirstOrDefault()
            };
            
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSephyProfile(SephyProfileDto request)
        {
            var currUser = await userManager.GetUserAsync(HttpContext.User);

            var domainModelSephyProfile = new SephyProfile
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                ZipCode = request.ZipCode,
                Bio = request.Bio,
                User = currUser
            };

            await dbContext.SephyProfiles.AddAsync(domainModelSephyProfile);
            await dbContext.SaveChangesAsync();

            var dto = new SephyProfileDto()
            {
                Id = domainModelSephyProfile.Id.ToString(),
                Bio = domainModelSephyProfile.Bio,
                Name = domainModelSephyProfile.Name,
                ZipCode = domainModelSephyProfile.ZipCode,
                Role = userManager.GetRolesAsync(currUser).Result.FirstOrDefault()
            };
            
            return Ok(dto);
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetTags(string profileId)
        {
            var tags = await dbContext.SephyProfiles
                .Include(p => p.Tags)
                .FirstOrDefaultAsync(p => p.Id ==  Guid.Parse(profileId));
            
            return Ok(tags);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> AddTagToProfile(string tagId)
        { 
            var currUser = await userManager.GetUserAsync(HttpContext.User);
            var profile = await dbContext.SephyProfiles.Where(p => 
                p.User == currUser)
                .FirstOrDefaultAsync();

            var tag = await dbContext.Tags.Where(t =>
                t.Id == Guid.Parse(tagId)).FirstOrDefaultAsync();

            if (tag == null || profile == null)
            {
                return BadRequest();
            }

            profile.Tags.Add(tag);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateNewTag([FromQuery]String tagName)
        {
            var tag = new Tag()
            {
                Id = Guid.NewGuid(),
                Name = tagName
            };
            
            dbContext.Tags.Add(tag);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
