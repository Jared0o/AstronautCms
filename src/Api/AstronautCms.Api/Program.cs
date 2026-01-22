using AstronautCms.Api;
using AstronautCms.Shared.Abstract.Modules;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo { Title = "AstronautCms", Version = "v1" });
});
builder.Services.AddHealthChecks();

var modules = ModuleLoader.LoadModules();
foreach (var module in modules)
{
    module.Register(builder.Services, builder.Configuration);
    builder.Services.AddSingleton<IModule>(module);

}


var app = builder.Build();

using var scope = app.Services.CreateScope();
var dbContexts = scope.ServiceProvider.GetServices<DbContext>().ToArray();
var logger = scope.ServiceProvider.GetService<ILogger<Program>>();
var cancellationToken = app.Lifetime.ApplicationStopping;

if (dbContexts.Length == 0)
{
    logger?.LogInformation("Brak zarejestrowanych DbContextów do migracji.");
}

foreach (var dbContext in dbContexts)
{
    try
    {
        logger?.LogInformation("Applying migrations for {Context}...", dbContext.GetType().FullName);
        await dbContext.Database.MigrateAsync(cancellationToken);
        logger?.LogInformation("Migrations applied for {Context}.", dbContext.GetType().FullName);
    }
    catch (Exception ex)
    {
        logger?.LogError(ex, "Failed to migrate {Context}.", dbContext.GetType().FullName);
        throw;
    }
}
    

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/health");

app.UseHttpsRedirection();

var registeredModules = app.Services.GetServices<IModule>().ToArray();
foreach (var module in registeredModules)
{
    module.Use(app);
}

app.MapGet("/", () => "Hello from AstronautCms").WithTags("Base");

await app.RunAsync();
