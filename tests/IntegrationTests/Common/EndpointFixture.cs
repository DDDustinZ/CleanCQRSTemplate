using AutoMapper;
using FastEndpoints.Testing;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace IntegrationTests.Common;

[DisableWafCache]
public class EndpointFixture : AppFixture<Program>
{
    public readonly Mock<IMapper> MapperMock = new();
    public readonly Mock<IMediator> MediatorMock = new();
    
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IMapper>(_ => MapperMock.Object);
        services.AddScoped<IMediator>(_ => MediatorMock.Object);
    }
}