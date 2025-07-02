using App.Api.Domains;
using App.Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BrandController
{
    private readonly IBrandService _service;
    public BrandController(IBrandService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllBrands()
    {
        var brands = await _service.GetAllAsync();
        return new OkObjectResult(brands);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddBrand([FromBody] Brand brand)
    {
        await _service.AddBrand(brand);
        return new CreatedAtActionResult(nameof(GetAllBrands), "Brand", new { id = brand.Id }, brand);
    }
}