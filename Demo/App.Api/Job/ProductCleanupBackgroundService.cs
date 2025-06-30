using App.Data;
using Microsoft.EntityFrameworkCore;

namespace App.Api.Job;

public class ProductCleanupBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<ProductCleanupBackgroundService> _logger;
    
public ProductCleanupBackgroundService(IServiceProvider serviceProvider, ILogger<ProductCleanupBackgroundService> logger)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    var threeMonthsAgo = DateTime.UtcNow.AddMonths(-3);

                    var productsToDelete = await dbContext.Products
                        .Where(p => p.DeletedAt != null && p.DeletedAt < threeMonthsAgo)
                        .ToListAsync(stoppingToken);

                    if (productsToDelete.Any())
                    {
                        dbContext.Products.RemoveRange(productsToDelete);
                        await dbContext.SaveChangesAsync(stoppingToken);

                        _logger.LogInformation($"[ProductCleanup] Deleted {productsToDelete.Count} soft-deleted products older than 3 months.");
                    }
                    else
                    {
                        _logger.LogInformation("[ProductCleanup] No products to clean.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[ProductCleanup] Error occurred while cleaning up products.");
            }
            
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}