using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Project.SDK.Redis
{
    public class RedisHelper
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase _db;
        private readonly string _instanceName;

        public RedisHelper(IConnectionMultiplexer redis, string instanceName = null)
        {
            _redis = redis;
            _db = redis.GetDatabase();
            _instanceName = instanceName;
        }

        private string GetFullKey(string key)
        {
            return _instanceName == null ? key : $"{_instanceName}:{key}";
        }

        // 字符串操作
        public async Task<bool> SetStringAsync(string key, string value, TimeSpan? expiry = null)
        {
            return await _db.StringSetAsync(GetFullKey(key), value, expiry);
        }

        public async Task<string> GetStringAsync(string key)
        {
            return await _db.StringGetAsync(GetFullKey(key));
        }

        // 键操作
        public async Task<bool> KeyExistsAsync(string key)
        {
            return await _db.KeyExistsAsync(GetFullKey(key));
        }

        public async Task<bool> DeleteKeyAsync(string key)
        {
            return await _db.KeyDeleteAsync(GetFullKey(key));
        }

        // 哈希操作
        public async Task<bool> HashSetAsync<T>(string key, string field, T value)
        {
            return await _db.HashSetAsync(GetFullKey(key), field, Serialize(value));
        }

        public async Task<T> HashGetAsync<T>(string key, string field)
        {
            var value = await _db.HashGetAsync(GetFullKey(key), field);
            return Deserialize<T>(value);
        }

        // 列表操作
        public async Task<long> ListAddAsync<T>(string key, T value)
        {
            return await _db.ListRightPushAsync(GetFullKey(key), Serialize(value));
        }

        public async Task<T> ListGetAsync<T>(string key, long index)
        {
            var value = await _db.ListGetByIndexAsync(GetFullKey(key), index);
            return Deserialize<T>(value);
        }

        // 序列化/反序列化
        private static byte[] Serialize<T>(T obj)
        {
            if (obj == null) return null;

            return JsonSerializer.SerializeToUtf8Bytes(obj);
        }

        private static T Deserialize<T>(byte[] bytes)
        {
            if (bytes == null) return default;

            return JsonSerializer.Deserialize<T>(bytes);
        }
    }
}
