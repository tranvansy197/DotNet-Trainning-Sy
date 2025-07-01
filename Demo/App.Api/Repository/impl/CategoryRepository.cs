using App.Data;
using Microsoft.EntityFrameworkCore;

namespace App.Api.repository.impl;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;
    
    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> CheckIfCategoryExists(long id)
    {
        return await _context.Categories.AnyAsync(c => c.Id == id);
    }
}