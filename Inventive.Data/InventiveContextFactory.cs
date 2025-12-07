using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Inventive.Data;

/// <summary>
///     Design-time factory for EF Core migrations.
///     This is ONLY used by dotnet ef tools at design time - NOT at runtime.
///     Allows EF tools to instantiate InventiveContext without needing startup project configuration.
/// </summary>
public class InventiveContextFactory : IDesignTimeDbContextFactory<InventiveContext>
{
    public InventiveContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<InventiveContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Inventive;Username=postgres;Password=postgres");

        return new InventiveContext(optionsBuilder.Options);
    }
}
