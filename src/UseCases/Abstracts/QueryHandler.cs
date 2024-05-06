using Lamar;
using MediatR;

namespace COMPANY_NAME.PRODUCT.UseCases.Abstracts;

public abstract class QueryHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse> where TCommand : IRequest<TResponse>
{
    [SetterProperty]
    public required IReadContext ReadContext { get; init; }
    public abstract Task<TResponse> Handle(TCommand request, CancellationToken cancellationToken);
}