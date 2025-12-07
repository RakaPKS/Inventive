using Inventive.Core.Enums;
using Inventive.Core.Models;

namespace Inventive.Core.Interfaces.Repositories;

/// <summary>
///     Repository for Equipment entity - focused on reusable patterns only
///     Complex queries should use InventiveContext directly in services
/// </summary>
public interface IEquipmentRepository
{
    /// <summary>
    ///     Gets equipment by ID (ignores soft-deleted due to global query filter)
    /// </summary>
    public Task<Equipment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets all equipment with optional status filtering
    /// </summary>
    public Task<List<Equipment>> GetAllAsync(EquipmentStatus? status = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets paginated equipment with total count
    /// </summary>
    public Task<(List<Equipment> Items, int TotalCount)> GetPaginatedAsync(
        int skip,
        int pageSize,
        EquipmentStatus? status = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Checks if equipment with given name exists (case-insensitive)
    ///     Useful for searches
    /// </summary>
    public Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Checks if equipment with given id exists
    ///     Useful for preventing duplicates
    /// </summary>
    public Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default);


    /// <summary>
    ///     Adds new equipment to the database
    /// </summary>
    public Task AddAsync(Equipment equipment, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Saves all pending changes to the database
    /// </summary>
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
