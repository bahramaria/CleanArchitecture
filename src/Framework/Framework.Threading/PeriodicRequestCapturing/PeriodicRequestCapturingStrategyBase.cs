namespace Framework.Threading.PeriodicRequestCapturing;

public abstract class PeriodicRequestCapturingStrategyBase(
    TimeSpan minIntervalTime,
    TimeSpan? maxIntervalTime = null) : IDisposable
{
    protected abstract void Evaluate();
    protected abstract void Disposing();

    private bool disposed = false;
    private volatile bool signaled = false;
    private DateTime lastEvaluationTime = Time;

    public TimeSpan MinIntervalTime { get; } = minIntervalTime;
    public TimeSpan MaxIntervalTime { get; } = maxIntervalTime ?? TimeSpan.MaxValue;

    public void Signal()
    {
        signaled = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposed)
        {
            return;
        }

        if (disposing)
        {
            Disposing();
        }

        disposed = true;
    }

    private static DateTime Time => DateTime.Now;

    protected void OnTimer()
    {
        var elapsed = Time - lastEvaluationTime;

        if (elapsed >= MaxIntervalTime || elapsed >= MinIntervalTime && signaled)
        {
            Evaluating();
        }
    }

    private void Evaluating()
    {
        signaled = false;
        lastEvaluationTime = Time;
        Evaluate();
    }
}