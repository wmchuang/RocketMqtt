using System.Collections.ObjectModel;
using Coldairarrow.Util;
using RocketMqtt.Domain.Event;
using SqlSugar;

namespace RocketMqtt.Domain.Domain;

public abstract class EntityBase
{
    /// <summary>
    /// 主键
    /// </summary>
    [SugarColumn(IsPrimaryKey = true)] 
    public virtual string Id { get; protected set; } = IdHelper.GetId();
    
    
    private List<IDomainEvent> _domainEvents;
    
    [SugarColumn(IsIgnore = true)]
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly() ?? new ReadOnlyCollection<IDomainEvent>(new List<IDomainEvent>(0));

    public void AddDomainEvent(IDomainEvent eventItem)
    {
        _domainEvents = _domainEvents ?? new List<IDomainEvent>();
        _domainEvents.Add(eventItem);
    }

    public void RemoveDomainEvent(IDomainEvent eventItem)
    {
        _domainEvents?.Remove(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }
}