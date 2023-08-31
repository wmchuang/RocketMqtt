using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RocketMqtt.Domain.Domain;
using RocketMqtt.Domain.Repository;

namespace RocketMqtt.Infrastructure.EFCore;

public class DataContext : DbContext, IUnitOfWork
{
    private readonly IMediator _mediator;
    public DbSet<ConnInfo> ConnInfos { get; set; }
    public DbSet<Subscribed> Subscribeds { get; set; }
    public DbSet<Topic> Topics { get; set; }

    public DataContext(DbContextOptions<DataContext> options, IMediator mediator) : base(options)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        System.Diagnostics.Debug.WriteLine("OrderingContext::ctor ->" + this.GetHashCode());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assembly = Assembly.GetAssembly(GetType());

        if (assembly != null)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(assembly, t => t.GetInterfaces().Any(i => t.Name.EndsWith("Etc")));
        }

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        await _mediator.DispatchDomainEventsAsync(this);

        var result = await base.SaveChangesAsync(cancellationToken);

        return result;
    }
}