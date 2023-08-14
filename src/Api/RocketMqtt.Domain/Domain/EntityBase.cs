using System.Collections.ObjectModel;
using Coldairarrow.Util;
using MediatR;
using SqlSugar;

namespace RocketMqtt.Domain.Domain;

public abstract class EntityBase
{
    /// <summary>
    /// 主键
    /// </summary>
    [SugarColumn(IsPrimaryKey = true)] 
    public virtual string Id { get; protected set; } = IdHelper.GetId();
    
    
    private List<INotification> _domainEvents;
    
    [SugarColumn(IsIgnore = true)]
    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly() ?? new ReadOnlyCollection<INotification>(new List<INotification>(0));

    public void AddDomainEvent(INotification eventItem)
    {
        _domainEvents = _domainEvents ?? new List<INotification>();
        _domainEvents.Add(eventItem);
    }

    public void RemoveDomainEvent(INotification eventItem)
    {
        _domainEvents?.Remove(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }
}