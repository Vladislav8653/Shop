using MediatR;

namespace ProductManagement.Application.UseCases.Commands.CreateProduct;

public record CreateProductCommand : IRequest<Unit>
{
    public string ProductName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Price { get; set; }
    public bool Available { get; set; }
    public Guid UserId { get; set; }
}