namespace RocketMqtt.Domain.Domain;

/// <summary>
/// 主题
/// </summary>
public class Topic : EntityBase
{
    /// <summary>
    /// 主题名称
    /// </summary>
    public string TopicName { get; set; }

    /// <summary>
    /// 节点
    /// </summary>
    public string Node { get; set; }

    public Topic()
    {
    }

    public Topic(string topicName, string node) : this()
    {
        TopicName = topicName;
        Node = node;
    }
}