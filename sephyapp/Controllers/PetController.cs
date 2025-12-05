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
public class PetController(SephyDbContext dbContext, UserManager<IdentityUser> userManager)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPets()
    {
        var currUser = await userManager.GetUserAsync(HttpContext.User);

        var pets = await dbContext.Pets.Where(pet => pet.Owner == currUser).ToListAsync();

        List<PetDTO> dtoList = new List<PetDTO>();
        foreach (var pet in pets)
        {
            var dto = new PetDTO()
            {
                Id = pet.Id.ToString(),
                Name = pet.Name,
                Species = pet.Species,
                Breed = pet.Breed,
                Gender = pet.Gender,
                DateOfBirth = pet.DateOfBirth,
            };
            
            dtoList.Add(dto);
        }
        
        return Ok(dtoList);
    }

    [HttpPost]
    public async Task<IActionResult> AddPet(PetDTO request)
    {
        var currUser = await userManager.GetUserAsync(HttpContext.User);
            
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

        await dbContext.Pets.AddAsync(domainModelPet);
        await dbContext.SaveChangesAsync();

        var dto = new PetDTO()
        {
            Id = domainModelPet.Id.ToString(),
            Name = domainModelPet.Name,
            Species = domainModelPet.Species,
            Gender = domainModelPet.Gender,
            DateOfBirth = domainModelPet.DateOfBirth,
            Breed = domainModelPet.Breed
        };
        
        return Ok(dto);
    }
}