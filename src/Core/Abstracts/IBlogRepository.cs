using COMPANY_NAME.PRODUCT.Core.Blogs;

namespace COMPANY_NAME.PRODUCT.Core.Abstracts;

public interface IBlogRepository
{
    void Add(Blog newBlog);
}