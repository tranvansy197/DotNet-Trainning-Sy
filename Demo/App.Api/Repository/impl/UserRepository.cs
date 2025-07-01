using App.Api.Domains;
using App.Data;
using Microsoft.EntityFrameworkCore;

namespace App.Api.repository.impl;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    public UserRepository(AppDbContext context)
    {
        _context = context;
    }


    public async Task<User?> FindByUsername(string username)
    {
        return await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(x => x.Username == username);
    }

    public async Task SaveUser(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }
}