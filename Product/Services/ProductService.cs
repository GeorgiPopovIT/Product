using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using ProductShop.Data;

namespace ProductShop.Services;

public class ProductService(ProductDbContext dbContext) : Product.ProductBase
{
    private readonly ProductDbContext _dbContext = dbContext;

    public override Task<ListProducts> GetProducts(Empty request, ServerCallContext context)
    {
        var products = this._dbContext.Products.Select(p => new ProductRequest
        {
            Name = p.Name,
            Price = (double)p.Price,
            CategoryId = p.CategoryId,
        }).ToList();

        ListProducts list = new();
        list.Products.AddRange(products);

        return Task.FromResult(list);
    }

    public override Task<ProductResponse> SaveProduct(ProductRequest request, ServerCallContext context)
    {
        Data.Models.Product product = new()
        {
            Name = request.Name,
            Price = (decimal)request.Price,
            CategoryId = request.CategoryId
        };

        this._dbContext.Products.Add(product);
        this._dbContext.SaveChanges();

        return Task.FromResult(new ProductResponse
        {
            IsSuccess = true
        });
    }

    public override Task<ProductResponse> UpdateProduct(ProductToUpdate request, ServerCallContext context)
    {
        var productToUpdate = this._dbContext.Products.FirstOrDefault(p => p.Id == request.Id);

        ArgumentNullException.ThrowIfNull(productToUpdate);

        Data.Models.Product productToAdd = new()
        {
            Name = request.Name,
            Price = (decimal)request.Price,
            CategoryId = request.CategoryId
        };

        this._dbContext.SaveChanges();

        return Task.FromResult(new ProductResponse
        {
            IsSuccess = true
        });

    }

    public override Task<ProductResponse> DeleteProduct(ProductId request, ServerCallContext context)
    {
        var currentProduct = this._dbContext.Products
                .FirstOrDefault(P => P.Id == request.ProductId_);

        ArgumentNullException.ThrowIfNull(currentProduct);

        this._dbContext.Products.Remove(currentProduct);

        this._dbContext.SaveChanges();
        return Task.FromResult(new ProductResponse { IsSuccess = true });
    }
}