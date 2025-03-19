namespace ProductManagement.Domain.Models;

public class Product
{
    public Guid Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Price { get; set; }
    public bool Available { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; } 
}