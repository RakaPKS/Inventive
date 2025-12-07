using Inventive.Core.Enums;

namespace Inventive.AdminAPI.Models.V1.Equipment;

public sealed record AddEquipmentResponseModel
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string? Description { get; init; }
    public EquipmentStatus Status { get; init; }
    public decimal Length { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
    public decimal Weight { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
}
