using Framework.Threading.BackgroundServices;
using Medallion.Threading.SqlServer;

namespace Framework.Persistence;

internal class SqlDistributedLockWrapper(string dbConnectionString, string distributedLockName) : IDistributedLock
{
    public IDisposable? TryAcquire(CancellationToken cancellationToken)
    {
        var @lock = new SqlDistributedLock(distributedLockName, dbConnectionString);
        return @lock.TryAcquire(cancellationToken: cancellationToken);
    }
}
