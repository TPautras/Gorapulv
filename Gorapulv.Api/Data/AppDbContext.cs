// Gorapulv.Api/Data/AppDbContext.cs
using Gorapulv.Api.Models;
using Microsoft.EntityFrameworkCore;
namespace Gorapulv.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :
            base(options) { }
        public DbSet<Article> Articles => Set<Article>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}