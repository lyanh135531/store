namespace Application.Core.DTOs;

public class PaginatedList<TListDto>
{
    public PaginatedList(List<TListDto> items, int total, int offset, int limit)
    {
        Items = items;
        Total = total;
        Offset = offset;
        Limit = limit;
    }

    public List<TListDto> Items { get; }
    public int Total { get; } = 0;
    public int Offset { get; }
    public int Limit { get; }
}