using COMPANY_NAME.PRODUCT.Core.Blogs;
using FluentAssertions;
using UnitTests.Common;

namespace UnitTests.Blogs;

public class BlogTests
{
    [Theory]
    [AutoMoqData]
    public void NewBlog_SetsProperties(string name, AuthorName authorName)
    {
        var sut = Blog.NewBlog(name, authorName);

        sut.Id.Should().Be(default);
        sut.Name.Should().Be(name);
        sut.AuthorName.Should().Be(authorName);
    }
}