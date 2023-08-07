namespace RocketMqtt.Web.Core.Results;

/// <summary>
/// Api结果
/// </summary>
public class ApiResult
{
    public ApiResult(int? statusCode, bool succeeded, object? errors)
    {
        StatusCode = statusCode;
        Succeeded = succeeded;
        Errors = errors;
        Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }

    /// <summary>
    /// 状态码
    /// </summary>
    public int? StatusCode { get; set; }

    /// <summary>
    /// 执行成功
    /// </summary>
    public bool Succeeded { get; set; }

    /// <summary>
    /// 错误信息
    /// </summary>
    public object? Errors { get; set; }

    /// <summary>
    /// 时间戳
    /// </summary>
    public long Timestamp { get; set; }
}

/// <summary>
/// Api结果
/// </summary>
/// <typeparam name="T"></typeparam>
public class ApiResult<T> : ApiResult
{
    /// <summary>
    /// 数据
    /// </summary>
    public T? Data { get; set; }

    public ApiResult(int? statusCode, bool succeeded, T? data = default, object? errors = default) : base(statusCode, succeeded, errors)
    {
        Data = data;
    }
}