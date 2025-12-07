using Inventive.Core.Enums;

namespace Inventive.AdminAPI.Models.V1.Equipment;

public sealed record GetAllEquipmentResponseModel
{
    public required IReadOnlyList<EquipmentItemModel> Items { get; init; }
    public required int TotalCount { get; init; }
    public required int Page { get; init; }
    public required int PageSize { get; init; }
    public required int TotalPages { get; init; }
    public required bool HasNextPage { get; init; }
    public required bool HasPreviousPage { get; init; }
}

public sealed record EquipmentItemModel
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public required EquipmentStatus Status { get; init; }
    public required decimal Length { get; init; }
    public required decimal Width { get; init; }
    public required decimal Height { get; init; }
    public required decimal Weight { get; init; }
    public required DateTimeOffset CreatedAt { get; init; }
    public required string? CreatedBy { get; init; }
    public DateTimeOffset? ModifiedAt { get; init; }
    public string? ModifiedBy { get; init; }
}
