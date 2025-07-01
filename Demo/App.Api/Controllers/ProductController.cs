
using App.Api.common;
using App.Api.Models;
using App.Api.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;
    
    public ProductController(IProductService service)
    {
        _service = service;
    }
    
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var product = await _service.GetById(id);
        return Ok(product);
    }
    
    [HttpGet]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> GetAllProducts([FromQuery] PageRequest request)
    {
        var products = await _service.GetAllProducts(request);
        return Ok(products);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductCreatedDTO productCreatedDto)
    {
        var createdProduct = await _service.CreateProduct(productCreatedDto);
        return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(long id)
    {
        await _service.DeleteProduct(id);
        return NoContent();
    }
}