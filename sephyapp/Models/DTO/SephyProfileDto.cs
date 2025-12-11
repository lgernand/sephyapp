using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using sephyapp.Models.Domain;

namespace sephyapp.Models.DTO
{
    public class SephyProfileDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? ZipCode { get; set; }
        public string? Bio { get; set; }
        public string? Role { get; set; }
    }
}
