using Microsoft.EntityFrameworkCore;

namespace Inventive.Data;

public class InventiveContext(DbContextOptions<InventiveContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder) => base.OnModelCreating(modelBuilder);
}
