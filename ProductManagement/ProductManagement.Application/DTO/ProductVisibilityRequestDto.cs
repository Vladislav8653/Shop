namespace ProductManagement.Application.DTO;

public record ProductVisibilityRequestDto
{
    public string UserId { get; init; }
    public bool Hide { get; init; }
}