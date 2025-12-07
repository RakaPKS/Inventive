using Inventive.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventive.Data;

public class InventiveContext(DbContextOptions<InventiveContext> options) : DbContext(options)
{
    /// <summary>
    ///     Well-known System user ID for system actions (migrations, background jobs, etc.)
    ///     This user will be seeded in the database during Feature 02 migration.
    /// </summary>
    private static readonly Guid SystemUserId = new("00000000-0000-0000-0000-000000000001");

    // DbSets
    public DbSet<Equipment> Equipment => Set<Equipment>();

    /// <summary>
    ///     Automatically populates audit fields for all entities inheriting from AuditableEntity.
    ///     Uses hybrid approach: Guid for operational queries, string for immutable compliance.
    ///     Phase 1: Uses System user (00000001) as default (no auth yet)
    ///     Phase 2: Will use ICurrentUserService to capture real user IDs and emails from JWT claims
    /// </summary>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTimeOffset.UtcNow;
                    entry.Entity.CreatedById = SystemUserId; // Phase 1: System user
                    entry.Entity.CreatedBy = "System"; // Phase 1: Placeholder
#pragma warning disable S125 // Remove commented out code - Phase 2 implementation placeholder
                    // Phase 2: entry.Entity.CreatedById = _currentUser?.UserId ?? SystemUserId;
                    // Phase 2: entry.Entity.CreatedBy = _currentUser?.Email ?? "System";
#pragma warning restore S125
                    break;

                case EntityState.Modified:
                    entry.Entity.ModifiedAt = DateTimeOffset.UtcNow;
                    entry.Entity.ModifiedById = SystemUserId; // Phase 1: System user
                    entry.Entity.ModifiedBy = "System"; // Phase 1: Placeholder
#pragma warning disable S125 // Remove commented out code - Phase 2 implementation placeholder
                    // Phase 2: entry.Entity.ModifiedById = _currentUser?.UserId ?? SystemUserId;
                    // Phase 2: entry.Entity.ModifiedBy = _currentUser?.Email ?? "System";
#pragma warning restore S125
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Equipment configuration
        modelBuilder.Entity<Equipment>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.Description)
                .HasMaxLength(1000);

            entity.Property(e => e.Status)
                .IsRequired(); // Stored as int (default) for better performance

            entity.Property(e => e.Length)
                .IsRequired()
                .HasPrecision(10, 2);

            entity.Property(e => e.Width)
                .IsRequired()
                .HasPrecision(10, 2);

            entity.Property(e => e.Height)
                .IsRequired()
                .HasPrecision(10, 2);

            entity.Property(e => e.Weight)
                .IsRequired()
                .HasPrecision(10, 2);

            entity.Property(e => e.CreatedAt)
                .IsRequired();

            entity.Property(e => e.CreatedById);

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(200);

            entity.Property(e => e.ModifiedById);

            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(200);

            // FK relationships will be added in Feature 02 when User entity is created:
#pragma warning disable S125 // Remove commented out code - Feature 02 implementation placeholder
            // entity.HasOne(e => e.CreatedByUser)
            //     .WithMany()
            //     .HasForeignKey(e => e.CreatedById)
            //     .OnDelete(DeleteBehavior.NoAction);
            //
            // entity.HasOne(e => e.ModifiedByUser)
            //     .WithMany()
            //     .HasForeignKey(e => e.ModifiedById)
            //     .OnDelete(DeleteBehavior.NoAction);
#pragma warning restore S125

            // Soft delete
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            // Global query filter - automatically exclude soft-deleted equipment
            entity.HasQueryFilter(e => !e.IsDeleted);

            // Indexes for common queries
            entity.HasIndex(e => e.Name);
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.IsDeleted);
        });
    }
}
