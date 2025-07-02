using App.Api.common;

namespace App.Api.Models;

public class ProductFilter : PageRequest
{
    public string? Name { get; set; }
    
}