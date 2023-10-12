using Microsoft.Extensions.DependencyInjection;
using performance.api.redis.b.RedisService;

namespace performance.api.redis.b;

[Amazon.Lambda.Annotations.LambdaStartup]
public class Startup
{
  public void ConfigureServices(IServiceCollection services)
  {
    // Here we'll add an instance of our calculator service that will be used by each function
    services.AddSingleton<IRedisService>(new RedisService.RedisService());
  }
}