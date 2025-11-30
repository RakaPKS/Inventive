namespace Inventive.Core.Models;

/// <summary>
/// Base class for entities that require audit trail tracking.
/// Automatically populated by InventiveContext.SaveChangesAsync().
/// Uses hybrid approach: Guid for operational queries, string for immutable compliance.
/// </summary>
public abstract class AuditableEntity
{
    /// <summary>
    /// When this entity was created (UTC)
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// User ID who created this entity (references AdminUser.Id).
    /// Defaults to System user (00000000-0000-0000-0000-000000000001) for system actions.
    /// Used for joining to User table to get current user info.
    /// </summary>
    public Guid CreatedById { get; set; }

    /// <summary>
    /// Username/email at time of creation (immutable audit record).
    /// Preserved even if user account is deleted or email changes.
    /// Examples: "admin@company.com", "System", "Migration", "BackgroundJob:Cleanup"
    /// </summary>
    public string CreatedBy { get; set; } = string.Empty;

    /// <summary>
    /// When this entity was last modified (UTC).
    /// Null if never modified.
    /// </summary>
    public DateTimeOffset? ModifiedAt { get; set; }

    /// <summary>
    /// User ID who last modified this entity (references AdminUser.Id).
    /// Null if never modified, otherwise defaults to System user for system actions.
    /// </summary>
    public Guid? ModifiedById { get; set; }

    /// <summary>
    /// Username/email at time of last modification (immutable audit record).
    /// Null if never modified.
    /// </summary>
    public string? ModifiedBy { get; set; }

    // Navigation properties - will be configured with FK constraints in Feature 02 (Admin User Management)
    // Commented out until AdminUser entity exists
    // public AdminUser? CreatedByUser { get; set; }
    // public AdminUser? ModifiedByUser { get; set; }
}
