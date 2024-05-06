using System.Net;
using COMPANY_NAME.PRODUCT.Core.Exceptions;
using COMPANY_NAME.PRODUCT.UseCases.Blogs;
using COMPANY_NAME.PRODUCT.Web.Blogs;
using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;
using IntegrationTests.Common;
using Moq;
using UnitTests.Common;

namespace IntegrationTests.EndpointProcessors;

public class RecordNotFoundPostProcessorTests(EndpointFixture fixture) : TestBase<EndpointFixture>
{
    [Theory]
    [AutoMoqData]
    public async Task Endpoint_ReturnsNotFound_WhenRecordNotFoundExceptionThrown(
        GetBlog.Request request,
        RecordNotFoundException exception)
    {
        fixture.MapperMock
            .Setup(x => x.Map<GetBlogByIdQuery>(It.IsAny<object>()))
            .Throws(exception);
        
        var (response, actual) = await fixture.Client.GETAsync<GetBlog, GetBlog.Request, InternalErrorResponse>(request);
        
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        actual.Status.Should().Be("Record Not Found!");
        actual.Code.Should().Be(404);
        actual.Reason.Should().Be(exception.Message);
        actual.Note.Should().BeNull();
    }
}