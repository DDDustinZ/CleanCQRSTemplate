using COMPANY_NAME.PRODUCT.UseCases.Blogs;
using IntegrationTests.Common;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests.Blogs;

public class GetBlogByIdTests(IntegrationDbFixture fixture) : IntegrationDbTestBase(fixture)
{
    private readonly GetBlogByIdHandler _sut = fixture.Services.GetRequiredService<GetBlogByIdHandler>();
    
    [Fact]
    public async Task Handle_ReturnsBlogQuery()
    {
        var request = new GetBlogByIdQuery(2);

        var actual = await _sut.Handle(request, CancellationToken.None);

        await Verify(actual, VerifySettingsFactory.Default);
    }
}