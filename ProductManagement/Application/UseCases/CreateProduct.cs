using AutoMapper;
using ProductManagement.Application.Contracts.UseCasesContracts;
using ProductManagement.Application.DTO;
using ProductManagement.Domain.Models;
using ProductManagement.Infrastructure.Repositories;

namespace ProductManagement.Application.UseCases;

public class CreateProduct(IMapper mapper, ProductRepository productRepository) : ICreateProduct
{
    public async Task Handle(ProductRequestDto productRequestDto, CancellationToken cancellationToken)
    {   
        //проверить на уникальность имени
        var product = mapper.Map<Product>(productRequestDto);
        await productRepository.Create(product, cancellationToken);
    }
}