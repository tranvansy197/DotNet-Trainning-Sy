using App.Api.Domains;

namespace App.Api.Service;

public interface IBrandService
{
    Task<List<Brand>> GetAllAsync();
}