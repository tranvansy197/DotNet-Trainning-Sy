using App.Api.common;
using App.Api.Domains;
using App.Api.Models;

namespace App.Api.Service;

public interface IProductService
{
    Task<ProductDTO> GetById(long id);
    Task<PageResponse<ProductDTO>> GetAllProducts(ProductFilter request);
    Task<ProductDTO> CreateProduct(ProductCreatedDTO productCreatedDto);
    Task DeleteProduct(long id);
}