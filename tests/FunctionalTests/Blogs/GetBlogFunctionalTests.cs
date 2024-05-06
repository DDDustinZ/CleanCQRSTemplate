using System.Net;
using COMPANY_NAME.PRODUCT.UseCases.Blogs;
using COMPANY_NAME.PRODUCT.Web.Blogs;
using FastEndpoints;
using FluentAssertions;
using FunctionalTests.Common;
using IntegrationTests.Common;

namespace FunctionalTests.Blogs;

public class GetBlogFunctionalTests(FunctionalDbFixture fixture) : FunctionalDbTestBase(fixture)
{
    private readonly FunctionalDbFixture _fixture = fixture;

    [Fact]
    public async Task GetBlog_ReturnsRecord()
    {
        var request = new GetBlog.Request(2);
        
        var (response, actual) = await _fixture.Client.GETAsync<GetBlog, GetBlog.Request, GetBlogByIdQuery.Result>(request);
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        await Verify(actual, VerifySettingsFactory.Default);
    }
}