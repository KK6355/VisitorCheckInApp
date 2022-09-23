using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VisitorCheckInApp.Models;

namespace VisitorCheckInApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Visitors> Visitors { get; set; }
        public DbSet<Staff> Staff { get; set; }
    }
}