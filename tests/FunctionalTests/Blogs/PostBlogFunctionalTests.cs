using System.Net;
using COMPANY_NAME.PRODUCT.Infrastructure.Data;
using COMPANY_NAME.PRODUCT.Web.Blogs;
using FastEndpoints;
using FluentAssertions;
using FunctionalTests.Common;
using Microsoft.Extensions.DependencyInjection;

namespace FunctionalTests.Blogs;

public class PostBlogFunctionalTests(FunctionalDbFixture fixture) : FunctionalDbTestBase(fixture)
{
    private readonly FunctionalDbFixture _fixture = fixture;
    private readonly AppDbContext _dbContext = fixture.Services.GetRequiredService<AppDbContext>();
    
    [Fact]
    public async Task PostBlog_CreatesNewBlog()
    {
        var request = new PostBlog.Request(Fake.Company.CompanyName(), Fake.Name.FirstName(), Fake.Name.LastName());
        
        var response = await _fixture.Client.POSTAsync<PostBlog, PostBlog.Request>(request);
        
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        _dbContext.ChangeTracker.Clear();
        var locationId = int.Parse(response.Headers.Location!.ToString().Split("/").Last());
        var newBlog = await _dbContext.Blogs.FindAsync(locationId);
        newBlog!.Name.Should().Be(request.Name);
        newBlog.AuthorName.First.Should().Be(request.AuthorFirstName);
        newBlog.AuthorName.Last.Should().Be(request.AuthorLastName);
    }
}