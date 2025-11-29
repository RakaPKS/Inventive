using Inventive.Core.Enums;

namespace Inventive.Core.DTO.Equipment;

public record EquipmentResponseDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string? Description { get; init; }
    public EquipmentStatus Status { get; init; }
    public decimal Length { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
    public decimal Weight { get; init; }
    public DateTime CreatedAt { get; init; }
    public string? CreatedBy { get; init; }
    public DateTime? ModifiedAt { get; init; }
    public string? ModifiedBy { get; init; }
}
