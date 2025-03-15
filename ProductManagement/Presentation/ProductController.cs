using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ProductManagement.Presentation;

[ApiController]
[Route("products")]
public class ProductController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
    {
        var products = await sender.Send();
        return Ok(products);
    }
}