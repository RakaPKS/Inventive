using Inventive.Core.Enums;

namespace Inventive.Core.Models;

/// <summary>
/// Represents a piece of equipment available for reservation.
/// </summary>
public class Equipment : AuditableEntity
{
    /// <summary>
    /// Unique identifier for this equipment
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Equipment name (e.g., "Projector - Epson EB-2250U")
    /// </summary>
    public string Name { get; private set; } = string.Empty;

    /// <summary>
    /// Optional description with additional details
    /// </summary>
    public string? Description { get; private set; }

    /// <summary>
    /// Current status of the equipment
    /// </summary>
    public EquipmentStatus Status { get; private set; }

    /// <summary>
    /// Length in centimeters
    /// </summary>
    public decimal Length { get; private set; }

    /// <summary>
    /// Width in centimeters
    /// </summary>
    public decimal Width { get; private set; }

    /// <summary>
    /// Height in centimeters
    /// </summary>
    public decimal Height { get; private set; }

    /// <summary>
    /// Weight in kilograms
    /// </summary>
    public decimal Weight { get; private set; }

    /// <summary>
    /// Soft delete flag - true if equipment has been deleted
    /// </summary>
    public bool IsDeleted { get; private set; }

    /// <summary>
    /// When this equipment was soft-deleted (null if not deleted)
    /// </summary>
    public DateTimeOffset? DeletedAt { get; private set; }

    // EF Core constructor
    private Equipment()
    {
    }

    /// <summary>
    /// Creates a new equipment instance with default values
    /// </summary>
    public Equipment(
        string name,
        string? description,
        decimal length,
        decimal width,
        decimal height,
        decimal weight)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Status = EquipmentStatus.Available;
        Length = length;
        Width = width;
        Height = height;
        Weight = weight;
        IsDeleted = false;
        DeletedAt = null;
    }

    /// <summary>
    /// Updates equipment properties
    /// </summary>
    public void Update(
        string name,
        string? description,
        decimal length,
        decimal width,
        decimal height,
        decimal weight)
    {
        Name = name;
        Description = description;
        Length = length;
        Width = width;
        Height = height;
        Weight = weight;
    }

    /// <summary>
    /// Changes equipment status
    /// </summary>
    public void ChangeStatus(EquipmentStatus newStatus)
    {
        Status = newStatus;
    }

    /// <summary>
    /// Soft-deletes this equipment
    /// </summary>
    public void Delete()
    {
        IsDeleted = true;
        DeletedAt = DateTimeOffset.UtcNow;
    }

    /// <summary>
    /// Restores a soft-deleted equipment
    /// </summary>
    public void Restore()
    {
        IsDeleted = false;
        DeletedAt = null;
    }
}
