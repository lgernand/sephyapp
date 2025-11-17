namespace sephyapp.Models.Domain
{
    public class SephyUser
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string AccountType { get; set; }
    }
}
