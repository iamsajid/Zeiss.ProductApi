using ProductApi.Common.Interfaces;
using StackExchange.Redis;

public class RedisProductIdGenerator : IProductIdGenerator
{
    private readonly IDatabase _db;
    private const string Key = "product:id";

    public RedisProductIdGenerator(IConnectionMultiplexer redis)
    {
        _db = redis.GetDatabase();
    }

    public async Task<int> GenerateUniqueProductIdAsync()
    {
        long id = await _db.StringIncrementAsync(Key);
        if (id < 100000)
        {
            await _db.StringSetAsync(Key, 100000);
            id = 100000;
        }
        if (id > 999999)
            throw new Exception("ID range exceeded");
        return (int)id;
    }
}
