namespace sephyapp.Models.Domain
{
    public class SephyProfile
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string AccountType { get; set; }
    }
}
