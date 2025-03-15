using AutoMapper;
using FluentValidation;
using MediatR;
using ProductManagement.Domain.Models;
using ProductManagement.Infrastructure.Repositories;

namespace ProductManagement.Application.UseCases.Commands.CreateProduct;

public class CreateProductCommandHandler(
    IMapper mapper, 
    ProductRepository productRepository,
    IValidator<Product> validator) 
    : IRequestHandler<CreateProductCommand>
{
    public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        //проверить на уникальность имени
        var product = mapper.Map<Product>(request);
        await productRepository.Create(product, cancellationToken);
    }
}