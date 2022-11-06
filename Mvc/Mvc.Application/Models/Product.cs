using System.ComponentModel.DataAnnotations;

namespace Mvc.Application.Models;

public class Product
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = default!;
    [Required]
    public decimal Price { get; set; }
    [Required]
    public int Stock { get; set; }
    [Required]
    public string Color { get; set; } = default!;
}
