using Microsoft.EntityFrameworkCore;

namespace JwtAuthenticationApi.Model
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<UserDetail> UserDetails { get; set; }
    }
}
