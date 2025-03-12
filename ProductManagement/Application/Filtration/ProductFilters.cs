namespace ProductManagement.Application.Filtration;

public class ProductFilters
{
    public string? ProductName { get; set; } = string.Empty;
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    public bool? Available { get; set; }
}