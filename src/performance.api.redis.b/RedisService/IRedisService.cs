using Amazon.Lambda.Core;
using performance.api.redis.b;

namespace performance.api.redis.b.RedisService;

/// <summary>
/// An interface for a service that implements the business logic of our Lambda functions
/// </summary>
public interface IRedisService
{
    Task<ResultTime> SetupData(SetupRequest request, ILambdaContext context);
    Task<ResultTime> DirectData(DataRequest request, ILambdaContext context);
}
