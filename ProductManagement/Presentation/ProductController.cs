﻿using MediatR;
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

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductRequestDto request,
        CancellationToken cancellationToken)
    {
        var query = new CreateProductCommand
        {
            NewProduct = request
        };
        await sender.Send(query, cancellationToken);
        return NoContent();
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateProduct([FromBody] ProductRequestDto request,
        [FromQuery] Guid productId,
        CancellationToken cancellationToken)
    {
        var query = new UpdateProductCommand()
        {
            NewProduct = request,
            ProductId = productId
        };
        await sender.Send(query, cancellationToken);
        return NoContent();
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteProduct([FromQuery] Guid productId,
        CancellationToken cancellationToken)
    {
        var query = new DeleteProductCommand()
        {
            ProductId = productId
        };
        await sender.Send(query, cancellationToken);
        return NoContent();
    }
}