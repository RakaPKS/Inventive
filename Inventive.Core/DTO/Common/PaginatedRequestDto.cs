namespace Inventive.Core.DTO.Common;

public record PaginatedRequestDto
{
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 20;

    public int Skip => (Page - 1) * PageSize;
}
