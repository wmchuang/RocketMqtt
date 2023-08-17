using System.ComponentModel.DataAnnotations;

namespace RocketMqtt.Util.Model;

public class BasePageRequest
{
    /// <summary>
    /// 页码
    /// </summary>
    [Required, Range(1, int.MaxValue)]
    public int PageIndex { get; set; }

    /// <summary>
    /// 页容量
    /// </summary>
    [Required, Range(1, int.MaxValue)]
    public int PageSize { get; set; }

}