using Microsoft.EntityFrameworkCore;
using UrlShortner.models;

namespace UrlShortner.data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public DbSet<UrlMapping> UrlMappings { get; set; }
    }
}