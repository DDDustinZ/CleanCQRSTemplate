using AutoMapper;
using COMPANY_NAME.PRODUCT.Core.Blogs;
using COMPANY_NAME.PRODUCT.UseCases.Blogs;
using FastEndpoints;
using FluentValidation;
using MediatR;
using IMapper = AutoMapper.IMapper;

namespace COMPANY_NAME.PRODUCT.Web.Blogs;

public class PostBlog : Endpoint<PostBlog.Request>
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public PostBlog(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    public record Request(string Name, string AuthorFirstName, string AuthorLastName)
    {
        public class Validator : Validator<Request>
        {
            public Validator()
            {
                RuleFor(x => x.Name).MinimumLength(2).MaximumLength(Blog.NameMaxLength);
                RuleFor(x => x.AuthorFirstName).MinimumLength(AuthorName.MinLength).MaximumLength(AuthorName.MaxLength);
                RuleFor(x => x.AuthorLastName).MinimumLength(AuthorName.MinLength).MaximumLength(AuthorName.MaxLength);
            }
        }
        
        public class Mapper : Profile
        {
            public Mapper()
            {
                CreateMap<Request, CreateNewBlogCommand>();
            }
        }
    }
    
    public override void Configure()
    {
        Post("");
        Group<BlogGroup>();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var command = _mapper.Map<CreateNewBlogCommand>(req);
        var id = await _mediator.Send(command, ct);
        await SendCreatedAtAsync<GetBlog>(new { id }, null, cancellation: ct);
    }
}