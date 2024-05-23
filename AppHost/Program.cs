var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddSqlServer("dbserver").AddDatabase("db");

builder.AddProject<Projects.Web>("web")
    .WithReference(db);

builder.AddProject<Projects.DbMigrationService>("migrations")
    .WithReference(db);

builder.Build().Run();