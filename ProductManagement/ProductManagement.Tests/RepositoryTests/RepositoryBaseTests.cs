using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain.Models;
using ProductManagement.Infrastructure;
using ProductManagement.Infrastructure.Repositories;

namespace ProductManagement.Tests.RepositoryTests;

public class RepositoryBaseTests
{
    private ApplicationContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
            .Options;

        return new ApplicationContext(options);
    }

    private class TestRepository : RepositoryBase<Product>
    {
        public TestRepository(ApplicationContext context) : base(context) { }
    }

    [Fact]
    public async Task Create_ShouldAddProduct()
    {
        // Arrange
        var context = CreateContext();
        var repository = new TestRepository(context);
        var product = new Product
        {
            Id = Guid.NewGuid(),
            ProductName = "TestProduct",
            Description = "Test Description",
            Price = 100,
            Available = true,
            UserId = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };
        var cancellationToken = new CancellationToken();

        // Act
        await repository.Create(product, cancellationToken);

        // Assert
        var result = await repository.FindByCondition(p => p.ProductName == "TestProduct", false, cancellationToken);
        Assert.Single(result);
        Assert.Equal("TestProduct", result.First().ProductName);
    }

    [Fact]
    public async Task Delete_ShouldRemoveProduct()
    {
        // Arrange
        var context = CreateContext();
        var repository = new TestRepository(context);
        var product = new Product
        {
            Id = Guid.NewGuid(),
            ProductName = "TestProduct",
            Description = "Test Description",
            Price = 100,
            Available = true,
            UserId = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };
        var cancellationToken = new CancellationToken();

        await repository.Create(product, cancellationToken);
        await context.SaveChangesAsync();

        // Act
        await repository.Delete(product, cancellationToken);
        await context.SaveChangesAsync();

        // Assert
        var result = await repository.FindByCondition(p => p.ProductName == "TestProduct", false, cancellationToken);
        Assert.Empty(result);
    }

    [Fact]
    public async Task Update_ShouldModifyProduct()
    {
        // Arrange
        var context = CreateContext();
        var repository = new TestRepository(context);
        var product = new Product
        {
            Id = Guid.NewGuid(),
            ProductName = "TestProduct",
            Description = "Test Description",
            Price = 100,
            Available = true,
            UserId = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };
        var cancellationToken = new CancellationToken();

        await repository.Create(product, cancellationToken);
        await context.SaveChangesAsync();

        product.ProductName = "UpdatedProduct";

        // Act
        await repository.Update(product, cancellationToken);

        // Assert
        var result = await repository.FindByCondition(p => p.ProductName == "UpdatedProduct", false, cancellationToken);
        Assert.Single(result);
        Assert.Equal("UpdatedProduct", result.First().ProductName);
    }

    [Fact]
    public async Task FindByCondition_ShouldReturnMatchingProducts()
    {
        // Arrange
        var context = CreateContext();
        var repository = new TestRepository(context);
        var product1 = new Product
        {
            Id = Guid.NewGuid(),
            ProductName = "TestProduct1",
            Description = "Test Description 1",
            Price = 100,
            Available = true,
            UserId = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };
        var product2 = new Product
        {
            Id = Guid.NewGuid(),
            ProductName = "TestProduct2",
            Description = "Test Description 2",
            Price = 200,
            Available = true,
            UserId = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };
        var cancellationToken = new CancellationToken();

        await repository.Create(product1, cancellationToken);
        await repository.Create(product2, cancellationToken);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.FindByCondition(p => p.ProductName.EndsWith("1"), false, cancellationToken);

        // Assert
        Assert.Single(result);
        Assert.Equal("TestProduct1", result.First().ProductName);
    }

    [Fact]
    public async Task FindAll_ShouldReturnAllProducts()
    {
        // Arrange
        var context = CreateContext();
        var repository = new TestRepository(context);
        var product1 = new Product
        {
            Id = Guid.NewGuid(),
            ProductName = "TestProduct1",
            Description = "Test Description 1",
            Price = 100,
            Available = true,
            UserId = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };
        var product2 = new Product
        {
            Id = Guid.NewGuid(),
            ProductName = "TestProduct2",
            Description = "Test Description 2",
            Price = 200,
            Available = true,
            UserId = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };
        var cancellationToken = new CancellationToken();

        await repository.Create(product1, cancellationToken);
        await repository.Create(product2, cancellationToken);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.FindAll(false, cancellationToken);

        // Assert
        Assert.Equal(2, result.Count());
    }
}