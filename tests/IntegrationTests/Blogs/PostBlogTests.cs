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

public class PostBlogTests(EndpointFixture fixture) : TestBase<EndpointFixture>
{
    [Fact]
    public async Task PostBlog_ReturnsBadRequest_WhenInvalid()
    {
        var request = new PostBlog.Request("", "", "");
        
        var (response, actual) = await fixture.Client.POSTAsync<PostBlog.Request, ErrorResponse>("/api/blog", request);
        
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        actual.Errors.Count.Should().Be(3);
        actual.Errors.Keys.Should().Equal("name", "authorFirstName", "authorLastName");
    }
    
    [Theory]
    [AutoMoqData]
    public async Task PostBlog_MapsAndSendsCommand(
        PostBlog.Request request,
        CreateNewBlogCommand command,
        int expected)
    {
        fixture.MapperMock
            .Setup(x => x.Map<CreateNewBlogCommand>(request))
            .Returns(command);
        fixture.MediatorMock
            .Setup(x => x.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);
        
        var (response, _) = await fixture.Client.POSTAsync<PostBlog.Request, EmptyResponse>("/api/blog", request);
        
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location.Should().Be($"/api/blog/{expected}");
    }
}