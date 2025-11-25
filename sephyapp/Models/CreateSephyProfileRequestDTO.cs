namespace sephyapp.Models
{
    public class CreateSephyProfileRequestDTO
    {
        public required string Name { get; set; }
        public required string ZipCode { get; set; }
        public required string Bio { get; set; }
    }
}
