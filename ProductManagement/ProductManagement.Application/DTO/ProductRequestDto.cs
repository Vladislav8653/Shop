using System.Text.Json.Serialization;

namespace ProductManagement.Application.DTO;

public record ProductRequestDto
{
    public string ProductName { get; init; } = null!;
    public string Description { get; init; } = null!;
    public int Price { get; init; }
    public bool Available { get; init; }
}