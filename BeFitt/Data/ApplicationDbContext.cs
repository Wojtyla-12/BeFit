using BeFitt.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BeFitt.Data
{
    public class ApplicationDbContext : IdentityDbContext<Uzytkownicy>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        public DbSet<Typy> Typy { get; set; }
        public DbSet<Sesja> Sesja { get; set; }
        public DbSet<Trening_K> Trening_K { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "d4869507-2c1b-4f7d-b5df-1234567890ab",
                Name = "Admin",
                NormalizedName = "ADMIN"
            });
        }
    }
}
