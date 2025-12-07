namespace Inventive.Core.DTO.Common;

public record PaginatedResultDto<T>
{
    public required IReadOnlyList<T> Items { get; init; }
    public required int TotalCount { get; init; }
}
