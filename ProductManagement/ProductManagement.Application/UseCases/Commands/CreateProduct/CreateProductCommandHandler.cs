using AutoMapper;
using FluentValidation;
using MediatR;
using ProductManagement.Application.Contracts.RepositoryContracts;
using ProductManagement.Domain.Models;

namespace ProductManagement.Application.UseCases.Commands.CreateProduct;

public class CreateProductCommandHandler(
    IMapper mapper, 
    IProductRepository productRepository,
    IValidator<Product> validator) 
    : IRequestHandler<CreateProductCommand, Unit>
{
    public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.UserId, out var userIdGuid))
        {
            throw new ValidationException("UserId is invalid");
        }
        var product = mapper.Map<Product>(request.NewProduct);
        product.UserId = userIdGuid;
        var validationResult = await validator.ValidateAsync(product, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        await productRepository.Create(product, cancellationToken);
        
        return Unit.Value;
    }
}