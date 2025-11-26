using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sephyapp.Data;
using sephyapp.Models;
using sephyapp.Models.Domain;
namespace sephyapp.Controllers;

[EnableCors("frontend")]
[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
[ApiController]
[Authorize]
public class PetController : ControllerBase
{
    [HttpGet]
    [Authorize]
    public IActionResult GetPets()
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult AddPet(AddPetRequestDTO pet)
    {
        return Ok();
    }
}