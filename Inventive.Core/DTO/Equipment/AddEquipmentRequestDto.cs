namespace Inventive.Core.DTO.Equipment;

public record AddEquipmentRequestDto
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public required decimal Length { get; set; }

    public required decimal Width { get; set; }

    public required decimal Height { get; set; }

    public required decimal Weight { get; set; }
}
