using COMPANY_NAME.PRODUCT.Core.Blogs;
using COMPANY_NAME.PRODUCT.UseCases.Abstracts;
using MassTransit;
using MediatR;

namespace COMPANY_NAME.PRODUCT.UseCases.Blogs;

public record CreateNewBlogCommand(string Name, string AuthorFirstName, string AuthorLastName) : IRequest<int>;

public class CreateNewBlogHandler : CommandHandler<CreateNewBlogCommand, int>
{
    private readonly IPublishEndpoint _publishEndpoint;

    public CreateNewBlogHandler(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }
    
    public override async Task<int> Handle(CreateNewBlogCommand request, CancellationToken ct)
    {
        var newBlog = Blog.NewBlog(request.Name, new AuthorName(request.AuthorFirstName, request.AuthorLastName));
        UnitOfWork.BlogRepository.Add(newBlog);
        await UnitOfWork.SaveChangesAsync(ct);
        await _publishEndpoint.Publish(new BlogUpdated(newBlog.Id), ct);
        return newBlog.Id;
    }
}