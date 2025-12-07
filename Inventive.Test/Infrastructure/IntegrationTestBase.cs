using Inventive.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.PostgreSql;

namespace Inventive.Test.Infrastructure;

public abstract class IntegrationTestBase<TProgram> : IAsyncLifetime where TProgram : class
{
    private readonly PostgreSqlContainer _postgresContainer = new PostgreSqlBuilder()
        .WithDatabase("inventive_test")
        .WithUsername("test")
        .WithPassword("test")
        .Build();

    private WebApplicationFactory<TProgram> Factory { get; set; } = null!;
    protected HttpClient Client { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        await _postgresContainer.StartAsync();

        Factory = new WebApplicationFactory<TProgram>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    // Remove the existing DbContext registration
                    services.RemoveAll<DbContextOptions<InventiveContext>>();
                    services.RemoveAll<InventiveContext>();

                    // Add test database context
                    services.AddDbContext<InventiveContext>(options =>
                    {
                        options.UseNpgsql(_postgresContainer.GetConnectionString());
                    });

                    // Ensure application parts include the API assembly for internal controller discovery
                    services.AddMvc()
                        .AddApplicationPart(typeof(TProgram).Assembly);

                    // Build service provider and apply migrations
                    var serviceProvider = services.BuildServiceProvider();
                    using var scope = serviceProvider.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<InventiveContext>();
                    context.Database.Migrate();
                });
            });

        Client = Factory.CreateClient();
    }

    public async Task DisposeAsync()
    {
        await _postgresContainer.DisposeAsync();
        await Factory.DisposeAsync();
        Client.Dispose();
    }

    protected async Task<InventiveContext> GetDbContextAsync()
    {
        var scope = Factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<InventiveContext>();
        return await Task.FromResult(context);
    }
}
