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
    IValidator<ProductRequestDto> validator) 
    : IRequestHandler<CreateProductCommand, Unit>
{
    public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        //проверить на уникальность имени
        var product = mapper.Map<Product>(request);
        
        await productRepository.Create(product, cancellationToken);
        
        return Unit.Value;
    }
}