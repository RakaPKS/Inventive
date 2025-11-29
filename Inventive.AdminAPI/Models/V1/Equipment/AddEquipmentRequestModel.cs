namespace Inventive.AdminAPI.Models.V1.Equipment;

internal sealed class AddEquipmentRequestModel
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public required decimal Length { get; set; }

    public required decimal Width { get; set; }

    public required decimal Height { get; set; }

    public required decimal Weight { get; set; }

}
