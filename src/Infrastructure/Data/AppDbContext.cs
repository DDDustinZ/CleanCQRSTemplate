using COMPANY_NAME.PRODUCT.Core.Blogs;
using COMPANY_NAME.PRODUCT.Infrastructure.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace COMPANY_NAME.PRODUCT.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Blog> Blogs => Set<Blog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlogEntityConfiguration).Assembly);
    }
}