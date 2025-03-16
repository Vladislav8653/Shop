using AutoMapper;
using FluentValidation;
using MediatR;
using ProductManagement.Application.Contracts.RepositoryContracts;
using ProductManagement.Domain.Models;

namespace ProductManagement.Application.UseCases.Commands.UpdateProduct;

public class UpdateProductCommandHandler(
    IProductRepository productRepository, 
    IMapper mapper,
    IValidator<Product> validator) :
    IRequestHandler<UpdateProductCommand, Unit>
{
    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var products = await productRepository.FindByCondition(product => 
                product.Id == request.ProductId,false, cancellationToken);
        var product = products.FirstOrDefault();
        if (product is null)
            throw new InvalidOperationException($"Product with id {request.ProductId} not found");
        
        mapper.Map(request.NewProduct, product);
        var validationResult = await validator.ValidateAsync(product, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        await productRepository.Update(product, cancellationToken);
        
        return Unit.Value;
    }
}