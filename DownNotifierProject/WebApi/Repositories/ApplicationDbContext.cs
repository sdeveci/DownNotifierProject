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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // Seed data
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser { Id = 1, UserName = "sadik", Password = "sadik123" },
                new ApplicationUser { Id = 2, UserName = "artun", Password = "artun123" },
                new ApplicationUser { Id = 3, UserName = "berna", Password = "berna123" }
            );
        }
    }
}
