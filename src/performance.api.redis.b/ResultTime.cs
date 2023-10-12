namespace performance.api.redis.b;

public class ResultTime
{
  public string? Message { get; set; }
  public long ElapsedTime { get; set; }
  public List<int>? Data { get; set; }
}