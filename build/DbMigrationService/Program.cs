using COMPANY_NAME.PRODUCT.Infrastructure.Data;
using COMPANY_NAME.PRODUCT.ServiceDefaults;
using DbMigrationService;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

builder.AddSqlServerDbContext<AppDbContext>("db");

var host = builder.Build();
host.Run();