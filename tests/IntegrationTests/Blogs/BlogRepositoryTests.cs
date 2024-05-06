using COMPANY_NAME.PRODUCT.Core.Blogs;
using COMPANY_NAME.PRODUCT.Infrastructure.Data.Repositories;
using IntegrationTests.Common;

namespace IntegrationTests.Blogs;

public class BlogRepositoryTests(IntegrationDbFixture fixture) 
    : GenericRepositoryTests<BlogRepository, Blog, int>(fixture)
{
}