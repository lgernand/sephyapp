using sephyapp.Models.Domain;

namespace sephyapp.Models.DTO;

public class PetDTO
{
    public PetDTO(Pet entity)
    {
        Name = entity.Name;
        Species = entity.Species;
        Breed = entity.Breed;
        Gender = entity.Gender;
        DateOfBirth = entity.DateOfBirth;
    }
    
    public String Name { get; set; }
    public String Species { get; set; }
    public String Breed { get; set; }
    public String Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
}