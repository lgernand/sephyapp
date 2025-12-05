using sephyapp.Models.Domain;

namespace sephyapp.Models.DTO;

public class PetDTO
{
    public String Id { get; set; }
    public String Name { get; set; }
    public String Species { get; set; }
    public String Breed { get; set; }
    public String Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
}