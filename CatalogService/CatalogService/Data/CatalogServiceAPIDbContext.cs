using CatalogService.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Data
{
    public class CatalogServiceAPIDbContext: DbContext
    {
        public CatalogServiceAPIDbContext(DbContextOptions options): base(options)
        {
            
        }
        public DbSet<Category> Category { get; set; }
        public DbSet<Item> Item { get; set; }
    }
}
