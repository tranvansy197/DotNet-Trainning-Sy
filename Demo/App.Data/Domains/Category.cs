namespace App.Api.Domains;

public class Category
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public List<Product> Products { get; set; } = new List<Product>();
}