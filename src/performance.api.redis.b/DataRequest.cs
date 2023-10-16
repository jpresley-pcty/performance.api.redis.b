namespace performance.api.redis.b;

public class DataRequest
{
  public string Key { get; set; }
  public string RedisEndpoint { get; set; }
  public string? ApiEndpoint { get; set; }
}