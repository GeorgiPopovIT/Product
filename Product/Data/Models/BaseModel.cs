namespace ProductShop.Data.Models;

public abstract class BaseModel
{
    public int Id { get; init; }
    public DateTime CreatedOn { get; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; }
}
