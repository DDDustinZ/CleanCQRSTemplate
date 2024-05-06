using COMPANY_NAME.PRODUCT.Core.Blogs;
using COMPANY_NAME.PRODUCT.UseCases.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace COMPANY_NAME.PRODUCT.Infrastructure.Data;

public class ReadContext : IReadContext
{
    private readonly AppDbContext _appDbContext;

    public ReadContext(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public IQueryable<Blog> Blogs => PrepareQuery(_appDbContext.Blogs);

    private static IQueryable<TEntity> PrepareQuery<TEntity>(IQueryable<TEntity> dbSet) where TEntity : class
    {
        return dbSet.IgnoreAutoIncludes().AsNoTracking();
    }
}