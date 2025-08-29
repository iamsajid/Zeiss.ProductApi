namespace ProductApi.Infrastructure.Repositories;

using ProductApi.Domain;
using ProductApi.Common.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

public class RedisCachedProductRepository : IProductRepository
{
    private readonly IProductRepository _inner;
    private readonly IDatabase _redisDb;
    private readonly IConfiguration _configuration;
    private readonly ILogger<RedisCachedProductRepository> _logger;

    private const string ProductAllKey = "products:all";
    private int CacheExpirySeconds => _configuration.GetValue<int>("CacheExpirySeconds", 300);

    public RedisCachedProductRepository(
        IProductRepository inner,
        IConnectionMultiplexer redis,
        IConfiguration configuration,
        ILogger<RedisCachedProductRepository> logger)
    {
        _inner = inner;
        _redisDb = redis.GetDatabase();
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        string key = $"product:{id}";
        var cached = await _redisDb.StringGetAsync(key);
        if (cached.HasValue)
        {
            _logger.LogInformation($"Redis cache hit for key: {key}");
            return JsonSerializer.Deserialize<Product>(cached!);
        }

        var product = await _inner.GetByIdAsync(id);
        if (product != null)
            await _redisDb.StringSetAsync(key, JsonSerializer.Serialize(product), TimeSpan.FromSeconds(CacheExpirySeconds));
        return product;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        var cached = await _redisDb.StringGetAsync(ProductAllKey);
        if (cached.HasValue)
        {
            _logger.LogInformation($"Redis cache hit for key: {ProductAllKey}");
            return JsonSerializer.Deserialize<List<Product>>(cached!);
        }

        var products = await _inner.GetAllAsync();
        var productList = products?.ToList() ?? new List<Product>();
        await _redisDb.StringSetAsync(ProductAllKey, JsonSerializer.Serialize(productList), TimeSpan.FromSeconds(CacheExpirySeconds));
        return productList;
    }

    public async Task<int> AddAsync(Product product)
    {
        var id = await _inner.AddAsync(product);
        await InvalidateCache(product.ProductId);
        return id;
    }

    public async Task<bool> UpdateAsync(Product product)
    {
        var result = await _inner.UpdateAsync(product);
        await InvalidateCache(product.ProductId);
        return result;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var result = await _inner.DeleteAsync(id);
        await InvalidateCache(id);
        return result;
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _inner.ExistsByNameAsync(name);
    }

    private async Task InvalidateCache(int productId)
    {
        await _redisDb.KeyDeleteAsync($"product:{productId}");
        await _redisDb.KeyDeleteAsync(ProductAllKey);
    }
}
