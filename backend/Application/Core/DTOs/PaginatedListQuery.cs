namespace Application.Core.DTOs;

public class PaginatedListQuery
{
    public int Offset { get; set; }
    public int Limit { get; set; } = 10;

    public List<Filter> Filters { get; set; }
}

public class Filter
{
    public string PropertyName { get; set; }
    public Operator Operator { get; set; }
    public string Value { get; set; }
}

public enum Operator
{
    Contains,
    Equals
}