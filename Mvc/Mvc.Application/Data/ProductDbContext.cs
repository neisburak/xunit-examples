using Microsoft.EntityFrameworkCore;
using Mvc.Application.Models;

namespace Mvc.Application.Data;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();
}
