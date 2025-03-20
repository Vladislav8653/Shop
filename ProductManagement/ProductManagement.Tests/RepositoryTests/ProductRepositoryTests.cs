using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.Filtration;
using ProductManagement.Application.Pagination;
using ProductManagement.Domain.Models;
using ProductManagement.Infrastructure;
using ProductManagement.Infrastructure.Repositories;

namespace ProductManagement.Tests.RepositoryTests;

public class ProductRepositoryTests
{
    private ApplicationContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
            .Options;

        return new ApplicationContext(options);
    }

    private async Task SeedData(ApplicationContext context)
    {
        context.Products.AddRange(new[]
        {
            new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "TestProduct1",
                Description = "Description1",
                Price = 100,
                Available = true,
                UserId = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            },
            new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "TestProduct2",
                Description = "Description2",
                Price = 200,
                Available = false,
                UserId = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            },
            new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "TestProduct3",
                Description = "Description3",
                Price = 150,
                Available = true,
                UserId = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            }
        });
        await context.SaveChangesAsync();
    }

    [Fact]
    public async Task GetByParamsAsync_ShouldReturnFilteredProducts()
    {
        // Arrange
        var context = CreateContext();
        await SeedData(context);
        var repository = new ProductRepository(context);
        var pageParams = new PageParams { Page = 1, PageSize = 10 };
        var filters = new ProductFilters { ProductName = "TestProduct", Available = true };
        var cancellationToken = new CancellationToken();

        // Act
        var result = await repository.GetByParamsAsync(pageParams, filters, p => true, cancellationToken);

        // Assert
        Assert.Equal(2, result.Total); // Должно вернуть 2 продукта
        Assert.Equal(2, result.Items.Count()); // Поскольку запрашиваем 1 страницу с размером 10
    }

    [Fact]
    public async Task GetByParamsAsync_ShouldReturnPagedResults()
    {
        // Arrange
        var context = CreateContext();
        await SeedData(context);
        var repository = new ProductRepository(context);
        var pageParams = new PageParams { Page = 1, PageSize = 2 };
        var filters = new ProductFilters();
        var cancellationToken = new CancellationToken();

        // Act
        var result = await repository.GetByParamsAsync(pageParams, filters, p => true, cancellationToken);

        // Assert
        Assert.Equal(3, result.Total); // Всего 3 продукта
        Assert.Equal(2, result.Items.Count()); // Должно вернуть 2 продукта на первой странице
    }

    [Fact]
    public async Task GetByParamsAsync_ShouldFilterByAvailability()
    {
        // Arrange
        var context = CreateContext();
        await SeedData(context);
        var repository = new ProductRepository(context);
        var pageParams = new PageParams { Page = 1, PageSize = 10 };
        var filters = new ProductFilters { Available = false };
        var cancellationToken = new CancellationToken();

        // Act
        var result = await repository.GetByParamsAsync(pageParams, filters, p => true, cancellationToken);

        // Assert
        Assert.Single(result.Items); // Должно вернуть 1 продукт, который недоступен
        Assert.Equal("TestProduct2", result.Items.First().ProductName); // Продукт должен быть TestProduct2
    }
}