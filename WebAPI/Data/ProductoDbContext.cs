using Microsoft.EntityFrameworkCore;
using WebAPI.Modelos;

namespace WebAPI.Data
{
    public class ProductoDbContext : DbContext
    {
        public ProductoDbContext(DbContextOptions<ProductoDbContext> options) : base(options) { }

        public DbSet<producto> Productos { get; set; }
    }
}
