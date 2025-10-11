using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MvcFreelan.Models.Mypays;

namespace MvcFreelan.Models.Freelan
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Mypay> Mypays { get; set; }

        public DbSet<MypayType> MypayTypes { get; set; }
    }
}
