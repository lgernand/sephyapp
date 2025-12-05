using Microsoft.AspNetCore.Identity;

namespace sephyapp.Models.Domain
{
    public class SephyProfile
    {
        public Guid Id { get; set; }
        public IdentityUser? User { get; set; }
        public required string? Name { get; set; }
        public required string? Bio { get; set; }
        public required string? ZipCode { get; set; }
    }
}
