namespace Framework.Threading.PeriodicRequestCapturing;

public abstract class PeriodicRequestCapturingStrategyBase<T>(
    int capacity,
    TimeSpan minIntervalTime,
    TimeSpan? maxIntervalTime = null,
    T? defaultValueOnMaxIntervalEvaluation = default) : IDisposable
{
    protected abstract void Evaluate(T? value);
    protected abstract void Disposing();

    private readonly Lock sync = new Lock();
    private readonly Queue<T?> queue = new Queue<T?>();
    private bool disposed = false;
    private DateTime lastEvaluationTime = Time;

    public TimeSpan MinIntervalTime { get; } = minIntervalTime;
    public TimeSpan MaxIntervalTime { get; } = maxIntervalTime ?? TimeSpan.MaxValue;
    public int Capacity { get; } = capacity;
    public T? DefaultValueOnMaxIntervalEvaluation { get; } = defaultValueOnMaxIntervalEvaluation;

    public void Signal(T? value)
    {
        lock (sync)
        {
            while (queue.Count >= Capacity)
            {
                queue.Dequeue();
            }

            queue.Enqueue(value);
        }
    }

    public void Clear()
    {
        lock (sync)
        {
            queue.Clear();
        }
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

        if (elapsed > MaxIntervalTime)
        {
            if (TryPop(out var value))
            {
                Evaluating(value);
            }
            else
            {
                Evaluating(DefaultValueOnMaxIntervalEvaluation);
            }
        }
        else if (elapsed >= MinIntervalTime && TryPop(out var value))
        {
            Evaluating(value);
        }
    }

    private bool TryPop(out T? value)
    {
        lock (sync)
        {
            if (queue.Count > 0)
            {
                value = queue.Dequeue();
                return true;
            }
            else
            {
                value = default;
                return false;
            }
        }
    }

    private void Evaluating(T? value)
    {
        lastEvaluationTime = Time;
        Evaluate(value);
    }
}