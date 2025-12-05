using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sephyapp.Data;
using sephyapp.Models;
using sephyapp.Models.Domain;
using sephyapp.Models.DTO;

namespace sephyapp.Controllers;

[EnableCors("frontend")]
[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
[ApiController]
[Authorize]
public class PetController : ControllerBase
{
    private readonly SephyDbContext _dbContext;
    private readonly UserManager<IdentityUser> _userManager;

    public PetController(SephyDbContext dbContext, UserManager<IdentityUser> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetPets()
    {
        var currUser = await _userManager.GetUserAsync(HttpContext.User);

        var pets = await _dbContext.Pets.Where(pet => pet.Owner == currUser).ToListAsync();
        
        return Ok(pets);
    }

    [HttpPost]
    public async Task<IActionResult> AddPet(PetDTO request)
    {
        var currUser = await _userManager.GetUserAsync(HttpContext.User);
            
        var domainModelPet = new Pet
        {
            Id = Guid.NewGuid(),
            Owner = currUser,
            Name = request.Name,
            Species = request.Species,
            Breed = request.Breed,
            Gender = request.Gender,
            DateOfBirth =  request.DateOfBirth,
        };

        await _dbContext.Pets.AddAsync(domainModelPet);
        await _dbContext.SaveChangesAsync();

        return Ok(domainModelPet);
    }
}