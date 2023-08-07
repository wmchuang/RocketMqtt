using Coldairarrow.Util;

namespace RocketMqtt.Domain.Domain;

public abstract class EntityBase
{
    public virtual string Id { get; protected set; } =  IdHelper.GetId();
}