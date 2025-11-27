using Microsoft.AspNetCore.Identity;

namespace sephyapp.Models.Domain;

public class Pet
{
    public Guid Id { get; set; }
    public IdentityUser Owner { get; set; }
    public String Name { get; set; }
    public String Species { get; set; }
    public String Breed { get; set; }
    public String Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
}