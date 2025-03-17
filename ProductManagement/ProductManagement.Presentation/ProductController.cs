using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.DTO;
using ProductManagement.Application.Filtration;
using ProductManagement.Application.Pagination;
using ProductManagement.Application.UseCases.Commands.CreateProduct;
using ProductManagement.Application.UseCases.Commands.DeleteProduct;
using ProductManagement.Application.UseCases.Commands.UpdateProduct;
using ProductManagement.Application.UseCases.Queries.GetProductById;
using ProductManagement.Application.UseCases.Queries.GetProducts;

namespace ProductManagement.Presentation;

[ApiController]
[Route("products")]
public class ProductController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] PageParams pageParams,
        [FromQuery] ProductFilters filters, CancellationToken cancellationToken)
    {
        var query = new GetProductsCommand
        {
            PageParams = pageParams,
            Filters = filters
        };
        var products = await sender.Send(query, cancellationToken);
        return Ok(products);
    }

    
    [HttpGet("{productId}")]
    public async Task<IActionResult> GetProductById(Guid productId, 
        CancellationToken cancellationToken)
    {
        var query = new GetProductByIdCommand
        {
            ProductId = productId
        };
        var product = await sender.Send(query, cancellationToken);
        return Ok(product);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductRequestDto request,
        CancellationToken cancellationToken)
    {
        var query = new CreateProductCommand
        {
            UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
            NewProduct = request
        };
        await sender.Send(query, cancellationToken);
        return NoContent();
    }
    
    [Authorize]
    [HttpPut("{productId}")]
    public async Task<IActionResult> UpdateProduct([FromBody] ProductRequestDto request,
        Guid productId,
        CancellationToken cancellationToken)
    {
        var query = new UpdateProductCommand
        {
            UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
            NewProduct = request,
            ProductId = productId
        };
        await sender.Send(query, cancellationToken);
        return NoContent();
    }
    
    [Authorize]
    [HttpDelete("{productId}")]
    public async Task<IActionResult> DeleteProduct(Guid productId,
        CancellationToken cancellationToken)
    {
        var query = new DeleteProductCommand
        {
            UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
            ProductId = productId
        };
        await sender.Send(query, cancellationToken);
        return NoContent();
    }
}