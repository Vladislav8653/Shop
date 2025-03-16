using FluentValidation;
using ProductManagement.Domain.Models;

namespace ProductManagement.Application.Validation;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(p => p.ProductName)
            .NotEmpty().WithMessage("Product name cannot be empty")
            .MaximumLength(100).WithMessage("Product name cannot be more than 100 characters");
        
        RuleFor(p => p.Description)
            .MaximumLength(1000).WithMessage("Description cannot be more than 1000 characters");
        
        RuleFor(p => p.Price)
            .GreaterThan(0).WithMessage("Price cannot be less than 0");
    }
}