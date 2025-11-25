using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sephyapp.Data;
using sephyapp.Models;

namespace sephyapp.Controllers
{
    [EnableCors("frontend")]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly SephyDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(SephyDbContext dbContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public async Task<IActionResult> GetRoleNames()
        {
            var allRoles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();

            return Ok(allRoles);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddUserToRole(RegisterRequestDTO request)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var roleResponse = await _userManager.AddToRoleAsync(currentUser, request.RoleName);
            
            return Ok(roleResponse);
        }
    }
}