using System.ComponentModel.DataAnnotations;

namespace ProductShop.Data.Models;

public class Category : BaseModel
{
    [Required]
    [MaxLength(60)]
    public string Name { get; set; }

    public ICollection<Product> Products { get; set; } = new HashSet<Product>();
}
