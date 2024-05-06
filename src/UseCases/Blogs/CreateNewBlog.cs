using COMPANY_NAME.PRODUCT.Core.Blogs;
using COMPANY_NAME.PRODUCT.UseCases.Abstracts;
using MediatR;

namespace COMPANY_NAME.PRODUCT.UseCases.Blogs;

public record CreateNewBlogCommand(string Name, string AuthorFirstName, string AuthorLastName) : IRequest<int>;

public class CreateNewBlogHandler : CommandHandler<CreateNewBlogCommand, int>
{
    public override async Task<int> Handle(CreateNewBlogCommand request, CancellationToken cancellationToken)
    {
        var newBlog = Blog.NewBlog(request.Name, new AuthorName(request.AuthorFirstName, request.AuthorLastName));
        UnitOfWork.BlogRepository.Add(newBlog);
        await UnitOfWork.SaveChangesAsync();
        return newBlog.Id;
    }
}