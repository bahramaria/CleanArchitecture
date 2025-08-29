using Infrastructure.RequestAudit.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.RequestAudit.Persistence;

public class AuditDbContext(DbContextOptions<AuditDbContext> options) : Framework.Persistence.DbContextBase(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new AuditTrailConfiguration());
    }
}
