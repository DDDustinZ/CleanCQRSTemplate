using System.Reflection;
using COMPANY_NAME.PRODUCT.Infrastructure.Data;
using COMPANY_NAME.PRODUCT.Web.EndpointProcessors;
using FastEndpoints;
using FastEndpoints.Swagger;
using JasperFx.Core;
using Lamar.Microsoft.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.SystemConsole.Themes;

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
builder.Services.AddSerilog(lc =>
{
    lc.ReadFrom.Configuration(builder.Configuration);
    lc.Enrich.FromLogContext();
    lc.Enrich.WithMachineName();
    lc.Enrich.WithProcessId();
    lc.Enrich.WithThreadId();
    lc.WriteTo.Conditional(_ => !builder.Environment.IsDevelopment(), x => x.Console(new JsonFormatter()));
    lc.WriteTo.Conditional(_ => builder.Environment.IsDevelopment(), x => x.Console(theme: ConsoleTheme.None));
});

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseDefaultExceptionHandler();
app.UseFastEndpoints(options =>
{
    options.Endpoints.RoutePrefix = "api";
    options.Endpoints.Configurator = ep =>
    {
        ep.PostProcessor<RecordNotFoundPostProcessor>(Order.After);
    };
});
app.UseSwaggerGen();

app.Run();

// Needed to reference in test fixtures AppFixture<Program>
// ReSharper disable once ClassNeverInstantiated.Global
public partial class Program { }