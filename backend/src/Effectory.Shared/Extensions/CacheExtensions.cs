using Effectory.Shared.JsonConfiguration;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Effectory.Shared.Extensions
{
    public static class CacheExtensions
    {
        public static bool TryGetValue<T>(
            this IDistributedCache distributedCache,
            string key,
            JsonSerializerSettings jsonSerializer,
            out T value)
        {
            var cacheEntry = distributedCache.GetString(key);

            if(string.IsNullOrEmpty(cacheEntry))
            {
                value = default;
                return false;
            }

            value = JsonConvert.DeserializeObject<T>(cacheEntry, jsonSerializer);
            return true;
        }

        public static async Task AddOrUpdateEntry<T>(this IDistributedCache distributedCache, DistributedCacheEntryOptions options, string key, T entry)
        {
            await distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(entry), options);
        }
    }
}
