using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RocketMqtt.Domain.Domain;

namespace RocketMqtt.Infrastructure.EFCore.EntityConfigurations;

public class EntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : EntityBase
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        var genericType = typeof(TEntity);
        builder.ToTable(genericType.Name);
        builder.Property(x => x.Id).IsRequired().HasMaxLength(36).HasComment("主键");
    }
}