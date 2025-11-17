namespace sephyapp.Models
{
    public class AddSephyUserRequestDTO
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string AccountType { get; set; }
    }
}
