using CleanArchitecture.Ordering.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CleanArchitecture.Ordering.Persistence;

public class OrderingDbContext(DbContextOptions<OrderingDbContext> options) : Framework.Persistence.DbContextBase(options), IOrderingQueryDb
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
