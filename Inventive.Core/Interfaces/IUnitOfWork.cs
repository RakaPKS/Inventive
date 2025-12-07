using Inventive.Core.Interfaces.Repositories;

namespace Inventive.Core.Interfaces;

/// <summary>
///     Manages database transactions and provides access to repositories.
///     All repositories accessed through UnitOfWork share the same DbContext instance,
///     ensuring atomic multi-repository operations.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    ///     Repository for Equipment entity operations
    /// </summary>
    public IEquipmentRepository Equipment { get; }


    /// <summary>
    ///     Commits all pending changes to the database in a single transaction.
    ///     Returns the number of state entries written to the database.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Number of entities saved</returns>
    public Task<int> CommitAsync(CancellationToken cancellationToken = default);

    // Future repositories will be added here as features are developed:
#pragma warning disable S125 // Remove commented out code - Phase 2 implementation placeholder
    // IReservationRepository Reservations { get; }
    // IAuditLogRepository AuditLogs { get; }
    // INotificationRepository Notifications { get; }
#pragma warning restore S125
}
