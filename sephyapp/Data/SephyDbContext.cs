using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using sephyapp.Models.Domain;

namespace sephyapp.Data
{
    public class SephyDbContext : IdentityDbContext
    {
        private IConfiguration _config;

        public SephyDbContext(IConfiguration config, DbContextOptions options) : base(options)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("AZURE_SQL_CONNECTIONSTRING"));
        }

        public DbSet<SephyProfile> SephyProfiles { get; set; }
    }
}
