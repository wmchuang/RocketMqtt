using MediatR;

namespace RocketMqtt.Application.ConnInfos.Command;

public class DeleteConnInfoCommand : IRequest<bool>
{
    /// <summary>
    /// 客户端Id
    /// </summary>
    public string ClientId { get; set; } = string.Empty;
}