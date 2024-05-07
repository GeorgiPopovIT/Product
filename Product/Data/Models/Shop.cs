using System.ComponentModel.DataAnnotations;

namespace ProductShop.Data.Models;

public class Shop : BaseModel
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    [Required]
    [MaxLength(100)]
    public string? Address { get; set; }

    [Required]
    public int QuantityProducts => Products.Count;

    public ICollection<Product> Products { get; set; } = new HashSet<Product>();
}
