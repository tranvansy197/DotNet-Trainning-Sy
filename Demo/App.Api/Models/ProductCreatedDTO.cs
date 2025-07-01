using System.ComponentModel.DataAnnotations;

namespace App.Api.Models;

public class ProductCreatedDTO
{
    [Required(ErrorMessage = "Product name is required.")]
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public long CategoryId { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
    public int Quantity { get; set; }
}