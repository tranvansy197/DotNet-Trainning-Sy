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
        this._service = service;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllBrands()
    {
        var brands = await _service.GetAllAsync();
        return new OkObjectResult(brands);
    }
}