namespace sephyapp.Models;

public class AddPetRequestDTO
{
    public String Species { get; set; }
    public String Breed { get; set; }
    public String Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
}