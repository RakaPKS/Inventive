using Inventive.Core.Enums;
using Inventive.Core.Interfaces.Repositories;
using Inventive.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventive.Data.Repositories;

/// <summary>
///     Repository implementation for Equipment entity
/// </summary>
public class EquipmentRepository(InventiveContext context) : IEquipmentRepository
{
    public async Task<Equipment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Equipment
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<List<Equipment>> GetAllAsync(EquipmentStatus? status = null,
        CancellationToken cancellationToken = default)
    {
        var query = context.Equipment.AsQueryable();

        if (status.HasValue)
        {
            query = query.Where(e => e.Status == status.Value);
        }

        return await query
            .OrderBy(e => e.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await context.Equipment
            .AnyAsync(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase), cancellationToken);
    }

    public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await context.Equipment.AnyAsync(e => e.Id.Equals(id), cancellationToken);

    public async Task AddAsync(Equipment equipment, CancellationToken cancellationToken = default) =>
        await context.Equipment.AddAsync(equipment, cancellationToken);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await context.SaveChangesAsync(cancellationToken);
}
