using FastEndpoints;

namespace COMPANY_NAME.PRODUCT.Web.Blogs;

public sealed class BlogGroup : Group
{
    public BlogGroup()
    {
        Configure("blog", ep =>
        {
            ep.AllowAnonymous();
        });
    }
}