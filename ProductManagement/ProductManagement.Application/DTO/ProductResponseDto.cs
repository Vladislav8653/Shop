namespace ProductManagement.Application.DTO;

public record ProductResponseDto
{
    public Guid Id { get; init; }
    public string ProductName { get; init; } = null!;
    public string Description { get; init; } = null!;
    public int Price { get; init; }
    public bool Available { get; init; }
    public Guid UserId { get; init; }
    public DateTime CreatedAt { get; init; }
}