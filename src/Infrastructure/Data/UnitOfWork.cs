using COMPANY_NAME.PRODUCT.Core.Abstracts;
using COMPANY_NAME.PRODUCT.UseCases.Abstracts;

namespace COMPANY_NAME.PRODUCT.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;

    public UnitOfWork(AppDbContext dbContext, IBlogRepository blogRepository)
    {
        _dbContext = dbContext;
        
        BlogRepository = blogRepository;
    }

    public IBlogRepository BlogRepository { get; }
    
    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }
}