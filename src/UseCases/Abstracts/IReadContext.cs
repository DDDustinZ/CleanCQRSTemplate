using COMPANY_NAME.PRODUCT.Core.Blogs;

namespace COMPANY_NAME.PRODUCT.UseCases.Abstracts;

public interface IReadContext
{
    public IQueryable<Blog> Blogs { get; }
}