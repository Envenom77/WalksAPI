using Microsoft.EntityFrameworkCore;
using Walks.API.Models.Domain;

namespace Walks.API.Data
{
    public class WalksDBContext : DbContext
    {
        public WalksDBContext(DbContextOptions options) : base(options)
        {
            
        }


        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
    }
}
