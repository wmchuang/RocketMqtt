using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RocketMqtt.Domain.Domain;

namespace RocketMqtt.Infrastructure.EFCore.EntityConfigurations;

public class TopicEtc : EntityTypeConfiguration<Topic>
{
    public override void Configure(EntityTypeBuilder<Topic> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.TopicName).IsRequired().HasMaxLength(100).HasComment("主题名称");
        builder.Property(x => x.Node).IsRequired().HasMaxLength(100).HasComment("节点");
    }
}