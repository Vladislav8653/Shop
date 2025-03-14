﻿using FluentValidation;
using MediatR;
using ProductManagement.Application.DTO;
using ProductManagement.Infrastructure.Repositories;

namespace ProductManagement.Application.UseCases.Commands.DeleteProduct;

public class DeleteProductCommandHandler(
    ProductRepository productRepository) :
    IRequestHandler<DeleteProductCommand, Unit>
{
    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var products = await productRepository
            .FindByCondition(product => product.Id == request.ProductId,
            false, cancellationToken);
        var product = products.First();
        
        await productRepository.Delete(product, cancellationToken);
        
        return Unit.Value;
    }
}