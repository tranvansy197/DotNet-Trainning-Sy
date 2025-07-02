using App.Api.common;
using App.Api.Domains;
using App.Api.Models;

namespace App.Api.repository;

public interface IProductRepository
{
    Task<bool> CheckIfExists(string name, long categoryId);
    Task<Product> GetById(long id);
    Task<PageResponse<Product>> GetAllProducts(ProductFilter request);
    Task<Product> CreateProduct(Product product);
    Task DeleteProduct(Product product);
}