using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Framework.MassTransit;

public sealed class MassTransitDbContext(DbContextOptions<MassTransitDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(Settings.Persistence.SchemaNames.MassTransit);
        modelBuilder.AddTransactionalOutboxEntities();
    }
}
