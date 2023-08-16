using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RocketMqtt.Domain.Domain;

namespace RocketMqtt.Infrastructure.EFCore.EntityConfigurations;

public class ConnInfoEtc : EntityTypeConfiguration<ConnInfo>
{
    public override void Configure(EntityTypeBuilder<ConnInfo> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.ClientId).IsRequired().HasMaxLength(100).HasComment("客户端Id");
        builder.Property(x => x.UserName).IsRequired().HasMaxLength(100).HasComment("客户端用户名");
        builder.Property(x => x.Endpoint).IsRequired().HasMaxLength(100).HasComment("地址");
        builder.Property(x => x.KeepAlive).HasComment("心跳（秒）");
        builder.Property(x => x.CreateTime).IsRequired().HasComment("创建时间");
    }
}