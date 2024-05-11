using DownNotifier.API.Entities;
using Microsoft.EntityFrameworkCore;


namespace DownNotifier.API.Repositories
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<TargetApp> TargetApp { get; set; }
    }
}
