namespace App.Api.repository;

public interface ICategoryRepository
{
    Task<bool> CheckIfCategoryExists(long id);
}