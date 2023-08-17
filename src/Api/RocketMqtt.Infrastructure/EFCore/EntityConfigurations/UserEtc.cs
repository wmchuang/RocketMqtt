using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RocketMqtt.Domain.Domain;

namespace RocketMqtt.Infrastructure.EFCore.EntityConfigurations;

public class UserEtc : EntityTypeConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.UserName).IsRequired().HasMaxLength(100).HasComment("用户名");
        builder.Property(x => x.Password).IsRequired().HasMaxLength(100).HasComment("用户名");
        builder.Property(x => x.Salt).IsRequired().HasMaxLength(10).HasComment("加密盐");
        builder.Property(x => x.Remark).IsRequired().HasMaxLength(100).HasComment("备注");
    }
}