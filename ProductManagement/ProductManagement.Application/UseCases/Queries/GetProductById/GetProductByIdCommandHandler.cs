using AutoMapper;
using MediatR;
using ProductManagement.Application.Contracts.RepositoryContracts;
using ProductManagement.Application.DTO;

namespace ProductManagement.Application.UseCases.Queries.GetProductById;

public class GetProductByIdCommandHandler(
    IProductRepository productRepository,
    IMapper mapper) : 
    IRequestHandler<GetProductByIdCommand, ProductResponseDto>
{
    public async Task<ProductResponseDto> Handle(GetProductByIdCommand request, CancellationToken cancellationToken)
    {
        var products = await productRepository.FindByCondition(
            product => product.Id == request.ProductId &&
            product.IsActive == true, false, cancellationToken);
        var product = products.FirstOrDefault();
        if (product is null)
            throw new InvalidOperationException($"Product with id {request.ProductId} not found");
        
        var productResponseDto = mapper.Map<ProductResponseDto>(product);
        
        return productResponseDto;
    }
}