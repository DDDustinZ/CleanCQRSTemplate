using System.Reflection;
using COMPANY_NAME.PRODUCT.Infrastructure.Data;
using COMPANY_NAME.PRODUCT.Web;
using COMPANY_NAME.PRODUCT.Web.EndpointProcessors;
using FastEndpoints;
using FastEndpoints.Swagger;
using JasperFx.Core;
using Lamar.Microsoft.DependencyInjection;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

var assemblies = new[]
{
    Assembly.Load("COMPANY_NAME.PRODUCT.Web"),
    Assembly.Load("COMPANY_NAME.PRODUCT.UseCases"),
    Assembly.Load("COMPANY_NAME.PRODUCT.Core"),
    Assembly.Load("COMPANY_NAME.PRODUCT.Infrastructure")
};

builder.Host.UseLamar((_, registry) =>
{
    registry.Scan(scanner =>
    {
        assemblies.ForEach(scanner.Assembly);
        scanner.LookForRegistries();
        scanner.SingleImplementationsOfInterface();
    });
});
builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument();
builder.Services.AddAutoMapper(assemblies);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbContext"));
});
builder.AddLogging();
builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy(), ["live"]);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseDefaultExceptionHandler();
app.UseFastEndpoints(options =>
{
    options.Endpoints.RoutePrefix = "api";
    options.Endpoints.Configurator = ep =>
    {
        ep.PostProcessor<RecordNotFoundPostProcessor>(Order.After);
        ep.Description(routeHandlerBuilder => routeHandlerBuilder.ProducesProblemFE<InternalErrorResponse>(500));
    };
});
app.UseSwaggerGen();
app.MapHealthChecks("/health");
app.MapHealthChecks("/alive", new HealthCheckOptions
{
    Predicate = r => r.Tags.Contains("live")
});

app.Run();

// Needed to reference in test fixtures AppFixture<Program>
// ReSharper disable once ClassNeverInstantiated.Global
public partial class Program { }