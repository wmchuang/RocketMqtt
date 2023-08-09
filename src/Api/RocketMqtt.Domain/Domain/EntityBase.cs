using Coldairarrow.Util;
using SqlSugar;

namespace RocketMqtt.Domain.Domain;

public abstract class EntityBase
{
    [SugarColumn(IsPrimaryKey = true)]
    public virtual string Id { get; protected set; } =  IdHelper.GetId();
}