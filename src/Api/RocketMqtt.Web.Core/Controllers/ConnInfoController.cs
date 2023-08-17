using Microsoft.AspNetCore.Mvc;
using MQTTnet.Server;
using RocketMqtt.Application.Common;
using RocketMqtt.Application.ConnInfos;
using RocketMqtt.Application.ConnInfos.Request;
using RocketMqtt.Application.ConnInfos.Result;
using RocketMqtt.Domain.Domain;
using RocketMqtt.Util.Model;

namespace RocketMqtt.Web.Core.Controllers;

/// <summary>
/// 连接信息
/// </summary>
public class ConnInfoController : BaseController
{
    private readonly IConnInfoQuery _connInfoQuery;
    private readonly MqttServer _mqttServer;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="connInfoQuery"></param>
    /// <param name="mqttServer"></param>
    public ConnInfoController(IConnInfoQuery connInfoQuery,MqttServer mqttServer)
    {
        _connInfoQuery = connInfoQuery;
        _mqttServer = mqttServer;
    }

    /// <summary>
    /// ServerStatus
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public bool ServerStatus()
    {
        return _mqttServer.IsStarted;
    }

    /// <summary>
    /// 列表
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public Task<List<ConnInfo>> ListAsync()
    {
        return _connInfoQuery.GetListAsync();
    }

    /// <summary>
    /// 分页列表
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public Task<PageListResult<ConnInfoResult>> PageListAsync(ConnInfoPageRequest request)
    {
        return _connInfoQuery.GetPageListAsync(request);
    }
    
    /// <summary>
    /// 断开连接
    /// </summary>
    /// <param name="clientId"></param>
    /// <returns></returns>
    [HttpGet("{clientId}")]
    public async Task<bool> DisconnectAsync(string clientId)
    {
        var clients = await _mqttServer.GetClientsAsync();
        var status =  clients.FirstOrDefault(x => x.Id == clientId);
        if (status != null)
        {
            await status.DisconnectAsync();
        }
        
        return true;
    }
}