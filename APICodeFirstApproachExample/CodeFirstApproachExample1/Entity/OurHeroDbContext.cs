using CodeFirstApproachExample1.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace CodeFirstApproachExample1.Entity
{
    public class OurHeroDbContext : DbContext
    {
        public OurHeroDbContext(DbContextOptions<OurHeroDbContext> options) : base(options)
        {

        }

        public DbSet<OurHero> OurHeros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<OurHero>().HasKey(x => x.Id);
            modelBuilder.Entity<OurHero>().HasData(new OurHero
            {
                Id = 1,
                FirstName = "System",
                LastName = "",
                isActive = true
            });

        }

    }
}

  