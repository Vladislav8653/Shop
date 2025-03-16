namespace ProductManagement.Application.Filtration;

public record ProductFilters
{
    public string? ProductName { get; init; } = string.Empty;
    public int? MinPrice { get; init; }
    public int? MaxPrice { get; init; }
    public bool? Available { get; init; }
}