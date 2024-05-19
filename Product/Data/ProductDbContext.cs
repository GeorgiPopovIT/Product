using Microsoft.EntityFrameworkCore;
using ProductShop.Data.Models;

namespace ProductShop.Data;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> dbContextOptions)
        :base(dbContextOptions)
    {  }

    public DbSet<Models.Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    public DbSet<Shop> Shops { get; set; }

}
