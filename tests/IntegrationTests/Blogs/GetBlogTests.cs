using System.Net;
using COMPANY_NAME.PRODUCT.UseCases.Blogs;
using COMPANY_NAME.PRODUCT.Web.Blogs;
using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;
using IntegrationTests.Common;
using Moq;
using UnitTests.Common;

namespace IntegrationTests.Blogs;

public class GetBlogTests(EndpointFixture fixture) : TestBase<EndpointFixture>
{
    [Fact]
    public async Task GetBlog_ReturnsBadRequest_WhenIdInvalid()
    {
        var request = new GetBlog.Request(-1);
        
        var (response, actual) = await fixture.Client.GETAsync<GetBlog.Request, ErrorResponse>("/api/blog/{id}", request);
        
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        actual.Errors.Count.Should().Be(1);
        actual.Errors.Keys.Should().Equal("id");
    }
    
    [Theory]
    [AutoMoqData]
    public async Task GetBlog_MapsAndSendsQuery(
        GetBlog.Request request,
        GetBlogByIdQuery query,
        GetBlogByIdQuery.Result expected)
    {
        fixture.MapperMock
            .Setup(x => x.Map<GetBlogByIdQuery>(request))
            .Returns(query);
        fixture.MediatorMock
            .Setup(x => x.Send(query, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);
        
        var (response, actual) = await fixture.Client.GETAsync<GetBlog.Request, GetBlogByIdQuery.Result>("/api/blog/{id}", request);
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        actual.Should().Be(expected);
    }
}