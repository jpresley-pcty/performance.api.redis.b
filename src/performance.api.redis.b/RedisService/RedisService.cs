using System.Diagnostics;
using System.Text.Json;
using Amazon.Lambda.Core;
using StackExchange.Redis;

namespace performance.api.redis.b.RedisService;

/// <summary>
/// The implementation of <see cref="IRedisService"/>
/// that will be used by our Lambda functions.
/// </summary>
public class RedisService : IRedisService
{
    public async Task<ResultTime> DirectData(DataRequest request, ILambdaContext context)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        context.Logger.LogInformation(JsonSerializer.Serialize(request));
        var redis = await ConnectionMultiplexer.ConnectAsync(request.RedisEndpoint);
        var db = redis.GetDatabase();
        var data = await db.HashGetAsync(request.Key,"test");

        stopwatch.Stop();
        var result = new ResultTime()
        {
            Message = $"Retrieve cluster data. Total time in ms: {stopwatch.ElapsedMilliseconds}",
            ElapsedTime = stopwatch.ElapsedMilliseconds,
            Data = JsonSerializer.Deserialize<List<int>>(data!)
        };

        context.Logger.LogInformation(result.Message);
        return result;
    }
}
