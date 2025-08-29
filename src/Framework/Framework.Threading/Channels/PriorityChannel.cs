using Framework.Collections;

namespace Framework.Threading.Channels;

public class PriorityChannel<T>(IComparer<T> comparer) : IChannel<T>
{
    private readonly object sync = new object();
    private readonly MaxHeap<Wrapper> heap = new MaxHeap<Wrapper>(new Comparer(comparer));
    private int orderNumber = 0;

    public int Count => heap.Count;

    public T Receive()
    {
        lock (sync)
        {
            while (heap.Count == 0)
                Monitor.Wait(sync);
            return heap.Pop().Value;
        }
    }

    public void ClearAndPost(T value)
    {
        lock (sync)
        {
            heap.Clear();
            heap.Push(new Wrapper(value, ++orderNumber));
            Monitor.PulseAll(sync);
        }
    }

    public void Post(T value)
    {
        lock (sync)
        {
            heap.Push(new Wrapper(value, ++orderNumber));
            Monitor.PulseAll(sync);
        }
    }

    public bool PostIfEmpty(T value)
    {
        lock (sync)
        {
            if (heap.Count > 0)
            {
                return false;
            }

            heap.Push(new Wrapper(value, ++orderNumber));
            Monitor.PulseAll(sync);

            return true;
        }
    }

    public void Clear()
    {
        lock (sync)
        {
            heap.Clear();
        }
    }


    private sealed class Wrapper(T value, int orderNumber)
    {
        public T Value { get; } = value;
        public int OrderNumber { get; } = orderNumber;
    }

    private sealed class Comparer(IComparer<T> comparer) : IComparer<Wrapper>
    {
        public int Compare(Wrapper? x, Wrapper? y)
        {
            var result = comparer.Compare(x!.Value, y!.Value);

            if (result == 0)
            {
                result = y.OrderNumber.CompareTo(x.OrderNumber);
            }

            return result;
        }
    }
}
