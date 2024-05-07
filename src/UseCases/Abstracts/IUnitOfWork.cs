using COMPANY_NAME.PRODUCT.Core.Abstracts;

namespace COMPANY_NAME.PRODUCT.UseCases.Abstracts;

public interface IUnitOfWork
{
    public IBlogRepository BlogRepository { get; }
    Task<int> SaveChangesAsync(CancellationToken ct);
}