using AutoMapper;
using FluentValidation;
using MediatR;
using ProductManagement.Application.DTO;
using ProductManagement.Domain.Models;
using ProductManagement.Infrastructure.Repositories;

namespace ProductManagement.Application.UseCases.Commands.CreateProduct;

public class CreateProductCommandHandler(
    IMapper mapper, 
    ProductRepository productRepository,
    IValidator<Product> validator) 
    : IRequestHandler<CreateProductCommand, Unit>
{
    public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        //проверить на уникальность имени
        var product = mapper.Map<Product>(request.NewProduct);
        var validationResult = await validator.ValidateAsync(product, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        await productRepository.Create(product, cancellationToken);
        
        return Unit.Value;
    }
}