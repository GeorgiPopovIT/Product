using Grpc.Core;
using ProductShop.Data;

namespace ProductShop.Services;

public class ProductService(ProductDbContext dbContext) : Product.ProductBase
{
    private readonly ProductDbContext _dbContext = dbContext;

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
