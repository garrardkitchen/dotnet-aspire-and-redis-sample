using Microsoft.Extensions.Caching.Distributed;

namespace sample1.Web;

public class RedisClient(IDistributedCache cache)  
{
    public async Task<int> GetCounterAsync(string key)
    {
        var value = await cache.GetStringAsync(key);
        return value == null ? 0 : int.Parse(value);
    }

    public async Task SetCounterAsync(string key, int value)
    {
        await cache.SetStringAsync(key, value.ToString());
    }   
}


