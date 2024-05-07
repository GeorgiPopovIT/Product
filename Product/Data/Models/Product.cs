using System.ComponentModel.DataAnnotations;

namespace ProductShop.Data.Models;

public class Product : BaseModel
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    public ICollection<Shop> Shop { get; set; } = new HashSet<Shop>();
}
