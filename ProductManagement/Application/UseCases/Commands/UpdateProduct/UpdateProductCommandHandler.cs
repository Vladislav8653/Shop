using AutoMapper;
using FluentValidation;
using MediatR;
using ProductManagement.Application.DTO;
using ProductManagement.Domain.Models;
using ProductManagement.Infrastructure.Repositories;

namespace ProductManagement.Application.UseCases.Commands.UpdateProduct;

public class UpdateProductCommandHandler(
    ProductRepository productRepository, 
    IMapper mapper,
    IValidator<Product> validator) :
    IRequestHandler<UpdateProductCommand, Unit>
{
    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var products = await productRepository.FindByCondition(product => 
                product.Id == request.ProductId,false, cancellationToken);
        var product = products.First();
        
        mapper.Map(request.NewProduct, product);
        var validationResult = await validator.ValidateAsync(product, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        await productRepository.Update(product, cancellationToken);
        
        return Unit.Value;
    }
}