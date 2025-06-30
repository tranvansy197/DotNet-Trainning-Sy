namespace DemoProject.Models;

public class Product
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public long CategoryId { get; set; }
    public Category Category { get; set; }
    public int Quantity { get; set; }
}