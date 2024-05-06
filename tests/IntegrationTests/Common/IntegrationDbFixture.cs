using System.Data.Common;
using COMPANY_NAME.PRODUCT.Infrastructure.Data;
using FastEndpoints.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;

namespace IntegrationTests.Common;

public class IntegrationDbFixture : AppFixture<Program>
{
    private DbConnection _dbConnection = null!;
    private Respawner _respawner = null!;
    
    protected override async Task SetupAsync()
    {
        var dbContext = Services.GetRequiredService<AppDbContext>();
        await dbContext.Database.EnsureCreatedAsync();
    
        _dbConnection = new SqlConnection(dbContext.Database.GetConnectionString());
        await _dbConnection.OpenAsync();
    
        _respawner = await Respawner.CreateAsync(_dbConnection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.SqlServer,
            SchemasToInclude = ["dbo"],
            WithReseed = true
        });
    }

    protected override void ConfigureApp(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(configBuilder =>
        {
            configBuilder.AddInMemoryCollection(new Dictionary<string, string?>
            {
                { "ConnectionStrings:DbContext", "Data Source=host.docker.internal,1433;Persist Security Info=True;Initial Catalog=DB_NAME_IntegrationTests;User ID=sa;Password=Localp@55;TrustServerCertificate=True" }
            });
        });
    }

    public async Task ResetDb()
    {
        await _respawner.ResetAsync(_dbConnection);
        var cmd = _dbConnection.CreateCommand();
        cmd.CommandText = await File.ReadAllTextAsync("integration-test-seed.sql");
        await cmd.ExecuteNonQueryAsync();
    }

    protected override async Task TearDownAsync()
    {
        await _dbConnection.CloseAsync();
    }
}