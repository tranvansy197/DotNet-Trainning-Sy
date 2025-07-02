using App.Api.Domains;
using App.Data;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace App.Api.Service.impl;

public class BrandService : IBrandService
{
    private readonly IMongoCollection<Brand> _brandCollection;
    public BrandService(IOptions<MongoDbSettings> settings, IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
        _brandCollection = database.GetCollection<Brand>("brands");
    }
    
    public async Task<List<Brand>> GetAllAsync()
    {
        return await _brandCollection.Find(_ => true).ToListAsync();
    }
    
    public async Task AddBrand(Brand brand)
    {
        await _brandCollection.InsertOneAsync(brand);
    }

}