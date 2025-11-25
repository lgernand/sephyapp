using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sephyapp.Data;

namespace sephyapp.Controllers
{
    [EnableCors("frontend")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly SephyDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(SephyDbContext dbContext, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoleNames()
        {
            var allRoles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();

            return Ok(allRoles);
        }
    }
}