using Grpc.Core;
using ProductShop.Data;
using ProductShop.Protos;

namespace ProductShop.Services;

public class CategoryService(ProductDbContext dbContext) : Category.CategoryBase
{
    private readonly ProductDbContext _dbContext = dbContext;

    public override Task<CategoryResponse> SaveCategory(CategoryRequest request, ServerCallContext context)
    {
        Data.Models.Category category = new()
        {
            Name = request.Name
        };

        this._dbContext.Categories.Add(category);
        this._dbContext.SaveChanges();

        return Task.FromResult(new CategoryResponse
        {
            IsSuccess = true
        });
    }

}
