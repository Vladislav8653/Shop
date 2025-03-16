using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.Filtration;
using ProductManagement.Application.Pagination;
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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetProductByIdCommand
        {
            ProductId = id
        };
        var product = await sender.Send(query, cancellationToken);
        return Ok(product);
    }
}