using RocketMqtt.Domain.Domain;

namespace RocketMqtt.Infrastructure.DbContext;

/// <summary>
/// 内存数据库
/// </summary>
public class MemoryDbContext
{
    private static readonly MemoryDbContext Instance = new();

    public static MemoryDbContext GetInstance() => Instance;

    private Dictionary<Type, object> EntityDics = new();

    private MemoryDbContext()
    {
        ConnInfos = new List<ConnInfo>();
        EntityDics.Add(typeof(ConnInfo), ConnInfos);
    }

    public List<ConnInfo> ConnInfos { get; set; }

    public object SetEntities<TEntity>() where TEntity : EntityBase
    {
        if (typeof(TEntity) == typeof(ConnInfo))
        {
            if (EntityDics.TryGetValue(typeof(TEntity), out var entitys))
            {
                return entitys;
            }
        }

        // 没有匹配的实体类型，返回默认值
        return null;
    }
}