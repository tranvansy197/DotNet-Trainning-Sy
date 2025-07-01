using App.Api.Domains;

namespace App.Api.repository;

public interface IUserRepository
{
    Task<User?> FindByUsername(string username);
    Task SaveUser(User user);
}