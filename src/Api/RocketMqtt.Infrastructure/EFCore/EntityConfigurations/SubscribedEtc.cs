using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RocketMqtt.Domain.Domain;

namespace RocketMqtt.Infrastructure.EFCore.EntityConfigurations;

public class SubscribedEtc: EntityTypeConfiguration<Subscribed>
{
    public override void Configure(EntityTypeBuilder<Subscribed> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.ClientId).IsRequired().HasMaxLength(100).HasComment("客户端Id");
        builder.Property(x => x.TopicName).IsRequired().HasMaxLength(100).HasComment("主题名称");
        builder.Property(x => x.Qos).IsRequired().HasComment("Qos");
    }
}