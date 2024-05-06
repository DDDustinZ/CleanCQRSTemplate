using COMPANY_NAME.PRODUCT.Core.Exceptions;
using COMPANY_NAME.PRODUCT.UseCases.Abstracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace COMPANY_NAME.PRODUCT.UseCases.Blogs;

public record GetBlogByIdQuery(int Id) : IRequest<GetBlogByIdQuery.Result>
{
    public record Result(string Name, string AuthorLastName);
};

public class GetBlogByIdHandler : QueryHandler<GetBlogByIdQuery, GetBlogByIdQuery.Result>
{
    public override async Task<GetBlogByIdQuery.Result> Handle(GetBlogByIdQuery request, CancellationToken cancellationToken)
    {
         var result = await ReadContext.Blogs
             .Where(x => x.Id == request.Id)
             .Select(x => new GetBlogByIdQuery.Result(x.Name, x.AuthorName.Last))
             .FirstOrDefaultAsync(cancellationToken: cancellationToken);

         if (result == null)
         {
             throw new RecordNotFoundException(request.Id, $"No blog found for Id: {request.Id}");
         }
         
         return result;
    }
}