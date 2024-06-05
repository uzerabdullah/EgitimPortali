using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EgitimPortali.Models
{
    public class EgitimlerContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public EgitimlerContext(DbContextOptions<EgitimlerContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

        public DbSet<Educations> Educations { get; set; }
        public DbSet<Teacher> Teachers { get; set; }


    }
}
