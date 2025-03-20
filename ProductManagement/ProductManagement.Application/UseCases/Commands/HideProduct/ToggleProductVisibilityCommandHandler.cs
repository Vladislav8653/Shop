using FluentValidation;
using MediatR;
using ProductManagement.Application.Contracts.RepositoryContracts;

namespace ProductManagement.Application.UseCases.Commands.HideProduct;

public class ToggleProductVisibilityCommandHandler(IProductRepository productRepository)
    : IRequestHandler<ToggleProductVisibilityCommand, Unit>
{
    public async Task<Unit> Handle(ToggleProductVisibilityCommand request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.ProductVisibilityRequestDto.UserId, out var userIdGuid))
        {
            throw new ValidationException("UserId is invalid");
        }
        
        var products = await productRepository.FindByCondition
            (p => p.UserId == userIdGuid, false, cancellationToken);

        foreach (var product in products)
        {
            product.IsActive = request.ProductVisibilityRequestDto.Hide;
            await productRepository.Update(product, cancellationToken);
        }
        
        return Unit.Value;
    }
}