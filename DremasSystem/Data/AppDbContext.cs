
using DremasSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace DremasSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Dream> Dreams { get; set; }
    }
}

