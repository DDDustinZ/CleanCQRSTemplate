using AutoMapper;
using COMPANY_NAME.PRODUCT.UseCases.Blogs;
using FastEndpoints;
using FluentValidation;
using MediatR;
using IMapper = AutoMapper.IMapper;

namespace COMPANY_NAME.PRODUCT.Web.Blogs;

public class GetBlog : Endpoint<GetBlog.Request, GetBlogByIdQuery.Result>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public GetBlog(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }
    
    public record Request(int Id)
    {
        public class Validator : Validator<Request>
        {
            public Validator()
            {
                RuleFor(x => x.Id).GreaterThan(0);
            }
        }
        
        public class Mapper : Profile
        {
            public Mapper()
            {
                CreateMap<Request, GetBlogByIdQuery>();
            }
        }
    };

    public override void Configure()
    {
        Get("/{Id}");
        Group<BlogGroup>();
        Description(builder => builder
            .Produces<ErrorResponse>(404, "application/problem+json"));
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var query = _mapper.Map<GetBlogByIdQuery>(req);
        var result = await _mediator.Send(query, ct);
        await SendOkAsync(result, ct);
    }
} 