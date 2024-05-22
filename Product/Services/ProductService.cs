using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
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

}
