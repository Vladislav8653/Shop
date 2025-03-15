using AutoMapper;
using MediatR;
using ProductManagement.Application.DTO;
using ProductManagement.Infrastructure.Repositories;

namespace ProductManagement.Application.UseCases.Queries.GetProductById;

public class GetProductByIdCommandHandler(
    ProductRepository productRepository,
    IMapper mapper) : 
    IRequestHandler<GetProductByIdCommand, ProductResponseDto>
{
    public async Task<ProductResponseDto> Handle(GetProductByIdCommand request, CancellationToken cancellationToken)
    {
        var products = await productRepository.FindByCondition(
            product => product.Id == request.ProductId, false, cancellationToken);
        var product = products.First();
        
        var productResponseDto = mapper.Map<ProductResponseDto>(product);
        
        return productResponseDto;
    }
}