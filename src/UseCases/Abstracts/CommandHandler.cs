using Lamar;
using MediatR;

namespace COMPANY_NAME.PRODUCT.UseCases.Abstracts;

public abstract class CommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse> where TCommand : IRequest<TResponse>
{
    [SetterProperty]
    public required IUnitOfWork UnitOfWork { get; init; }
    public abstract Task<TResponse> Handle(TCommand request, CancellationToken cancellationToken);
}