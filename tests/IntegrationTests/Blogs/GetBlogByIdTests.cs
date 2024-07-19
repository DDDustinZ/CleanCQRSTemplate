using COMPANY_NAME.PRODUCT.Core.Exceptions;
using COMPANY_NAME.PRODUCT.UseCases.Blogs;
using FluentAssertions;
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
    
    [Fact]
    public async Task Handle_ReturnsNotFound_WhenNoResultsFromDb()
    {
        var request = new GetBlogByIdQuery(9999);

        var act = async () => await _sut.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<RecordNotFoundException>();
    }
}