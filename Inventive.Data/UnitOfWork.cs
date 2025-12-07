using Inventive.Core.Interfaces;
using Inventive.Core.Interfaces.Repositories;
using Inventive.Data.Repositories;

namespace Inventive.Data;

/// <summary>
/// Implements Unit of Work pattern for managing database transactions.
/// Provides centralized access to all repositories sharing the same DbContext instance.
/// </summary>
public sealed class UnitOfWork(InventiveContext context) : IUnitOfWork
{
    /// <summary>
    /// Lazy-loaded Equipment repository.
    /// All repositories share the same DbContext for transactional consistency.
    /// </summary>
    public IEquipmentRepository Equipment =>
        field ??= new EquipmentRepository(context);

    /// <summary>
    /// Commits all pending changes to the database in a single atomic transaction.
    /// Triggers audit field population in InventiveContext.SaveChangesAsync().
    /// </summary>
    public async Task<int> CommitAsync(CancellationToken cancellationToken = default) => await context.SaveChangesAsync(cancellationToken);

    public void Dispose()
    {
        context.Dispose();
        GC.SuppressFinalize(this);
    }
}
