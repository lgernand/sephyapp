namespace sephyapp.Models.Domain;

public class Pet
{
    public Guid Id { get; set; }
    public SephyProfile Owner { get; set; }
    public String Species { get; set; }
    public String Breed { get; set; }
    public String Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
}