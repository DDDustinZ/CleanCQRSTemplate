using COMPANY_NAME.PRODUCT.Core.Abstracts;
using COMPANY_NAME.PRODUCT.Core.Blogs;

namespace COMPANY_NAME.PRODUCT.Infrastructure.Data.Repositories;

public class BlogRepository : GenericRepository<Blog, int>, IBlogRepository
{
    public BlogRepository(AppDbContext context) : base(context)
    {
    }
}