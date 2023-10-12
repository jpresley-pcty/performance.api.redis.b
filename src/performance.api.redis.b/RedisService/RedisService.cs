using System.Diagnostics;
using System.Text.Json;
using Amazon.Lambda.Core;
using performance.api.redis.b;
using performance.api.redis.b.RedisService;
using StackExchange.Redis;

namespace performance.api.redis.b.RedisService;

/// <summary>
/// The implementation of <see cref="IRedisService"/>
/// that will be used by our Lambda functions.
/// </summary>
public class RedisService : IRedisService
{
    public async Task<ResultTime> DirectData(string key, ILambdaContext context)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        var redis = await ConnectionMultiplexer.ConnectAsync(Environment.GetEnvironmentVariable("redisEndpoint")!);
        var db = redis.GetDatabase();
        var data = await db.HashGetAsync(key,"test");

        stopwatch.Stop();
        var result = new ResultTime()
        {
            Message = $"Retrieve cluster data. Total time in ms: {stopwatch.ElapsedMilliseconds}",
            ElapsedTime = stopwatch.ElapsedMilliseconds,
            Data = JsonSerializer.Deserialize<List<int>>(data)
        };

        context.Logger.LogInformation(result.Message);
        return result;
    }
}
