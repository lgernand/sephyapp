namespace sephyapp.Models.Domain;

public class Tag
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<SephyProfile> SephyProfiles { get; }
}