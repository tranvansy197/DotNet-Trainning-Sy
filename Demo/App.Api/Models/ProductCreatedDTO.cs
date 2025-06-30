namespace App.Api.Models;

public class ProductCreatedDTO
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public long CategoryId { get; set; }
    public int Quantity { get; set; }
}