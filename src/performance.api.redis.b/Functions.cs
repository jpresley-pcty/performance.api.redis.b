using Amazon.Lambda.Core;
using Amazon.Lambda.Annotations;
using Amazon.Lambda.Annotations.APIGateway;
using performance.api.redis.b.RedisService;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace performance.api.redis.b;

/// <summary>
/// A collection of sample Lambda functions that provide a REST api for doing simple math calculations.
/// </summary>
public class Functions
{
    private readonly IRedisService _redisService;

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <remarks>
    /// The <see cref="IRedisService"/> implementation that we
    /// instantiated in <see cref="Startup"/> will be injected here.
    ///
    /// As an alternative, a dependency could be injected into each
    /// Lambda function handler via the [FromServices] attribute.
    /// </remarks>
    public Functions(IRedisService redisService)
    {
        _redisService = redisService;
    }

    /// <summary>
    /// Root route that provides information about the other requests that can be made.
    /// </summary>
    /// <returns>API descriptions.</returns>
    [LambdaFunction(@Policies = "AWSLambdaBasicExecutionRole,AmazonVPCFullAccess,AmazonElastiCacheFullAccess")]
    [HttpApi(LambdaHttpMethod.Get, "/")]
    public string Default()
    {
        const string docs = @"Performance API Redis A B testing.
/direct/{key} - retrieves data from redis directly, key is a redis key ref small|medium|large";
        return docs;
    }

    /// <summary>
    /// Get data from redis directly.
    /// </summary>
    /// <returns>200 status</returns>
    [LambdaFunction(@Policies = "AWSLambdaBasicExecutionRole,AmazonVPCFullAccess,AmazonElastiCacheFullAccess")]
    [HttpApi(LambdaHttpMethod.Get, "/direct/{key}")]
    public async Task<IHttpResult> DirectData(string key, ILambdaContext context)
    {
        return HttpResults.Ok(await _redisService.DirectData(key, context));
    }
}