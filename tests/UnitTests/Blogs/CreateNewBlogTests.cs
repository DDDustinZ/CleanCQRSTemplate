using AutoFixture.Xunit2;
using COMPANY_NAME.PRODUCT.Core.Abstracts;
using COMPANY_NAME.PRODUCT.Core.Blogs;
using COMPANY_NAME.PRODUCT.UseCases.Abstracts;
using COMPANY_NAME.PRODUCT.UseCases.Blogs;
using FluentAssertions;
using Moq;
using UnitTests.Common;

namespace UnitTests.Blogs;

public class CreateNewBlogTests
{
    [Theory]
    [AutoMoqData]
    public async Task Handle_SavesBlogToDatabase(
        [Frozen] Mock<IUnitOfWork> unitOfWorkMock,
        [Frozen] Mock<IBlogRepository> blogRepositoryMock,
        CreateNewBlogCommand request,
        CreateNewBlogHandler sut)
    {
        var actual = await sut.Handle(request, CancellationToken.None);

        actual.Should().Be(default);
        blogRepositoryMock.Verify(x => x.Add(It.Is<Blog>(blog =>
            blog.Id == default && 
            blog.Name == request.Name && 
            blog.AuthorName.First == request.AuthorFirstName &&
            blog.AuthorName.Last == request.AuthorLastName)));
        unitOfWorkMock.Verify(x => x.SaveChangesAsync(CancellationToken.None));
    }
}