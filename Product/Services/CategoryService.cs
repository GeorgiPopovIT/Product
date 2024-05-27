using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using ProductShop.Data;

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

    public override Task<ListCategories> GetCategories(Empty request, ServerCallContext context)
    {

        var categories = this._dbContext.Categories
        .Select(p => new CategoryRequest
        {
            Name = p.Name
           
        }).ToList();

        ListCategories list = new();
        list.Categories.AddRange(categories);

        return Task.FromResult(list);
    }

    public override Task<CategoryResponse> UpdateCategory(CategoryToUpdate request, ServerCallContext context)
    {
        var categoryToUpdate = this._dbContext.Categories
                .FirstOrDefault(p => p.Id == request.Id);

        ArgumentNullException.ThrowIfNull(categoryToUpdate);

        Data.Models.Category categoryToAdd = new()
        {
            Name = request.Name
        };

        this._dbContext.SaveChanges();

        return Task.FromResult(new CategoryResponse
        {
            IsSuccess = true
        });
    }

    public override Task<CategoryResponse> DeleteCategory(CategoryId request, ServerCallContext context)
    {
        var categoryProduct = this._dbContext.Categories
               .FirstOrDefault(c => c.Id == request.CategoryId_);

        ArgumentNullException.ThrowIfNull(categoryProduct);

        this._dbContext.Categories.Remove(categoryProduct);

        this._dbContext.SaveChanges();
        return Task.FromResult(new CategoryResponse { IsSuccess = true });
    }
}
