using Microsoft.EntityFrameworkCore;
using sephyapp.Models.Domain;

namespace sephyapp.Data
{
    public class SephyDbContext : DbContext
    {
        private IConfiguration _config;

        public SephyDbContext(IConfiguration config)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("AZURE_SQL_CONNECTIONSTRING "));
        }

        public DbSet<SephyUser> SephyUsers { get; set; }
    }
}
