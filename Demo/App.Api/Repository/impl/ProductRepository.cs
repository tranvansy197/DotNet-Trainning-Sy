using App.Api.common;
using App.Api.Domains;
using App.Api.Models;
using App.Data;
using Microsoft.EntityFrameworkCore;

namespace App.Api.repository.impl;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;
    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Product> GetById(long id)
    {
        Product? product = await _context.Products
            .Include(p => p.Category)
            .Where(p => p.Id == id && !p.IsDeleted).SingleOrDefaultAsync();
        return product;
    }

    public async Task<PageResponse<Product>> GetAllProducts(PageRequest request)
    {
        var query = _context.Products.AsQueryable();
        query = query.OrderByDescending(p => p.CreatedAt)
            .Where(delete => !delete.IsDeleted);
        
        var totalItems = await query.CountAsync();
        
        var products = await query.Include(p => p.Category)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return new PageResponse<Product>
        {
            Items = products,
            TotalItems = totalItems,
            PageSize = request.PageSize,
            PageNumber = request.PageNumber
        };
    }
    
    public async Task<Product> CreateProduct(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        await _context.Entry(product).Reference(p => p.Category).LoadAsync();
        return product!;
    }
    
    public async Task DeleteProduct(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }
}