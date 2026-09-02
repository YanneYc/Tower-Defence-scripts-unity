public class MinHeap<T> where T : IHeapItem<T>
{
    T[] heap;
    int count;

    public MinHeap(int size)
    {
        heap = new T[size];
    }
    public int Count() => count;
    public T Peek() => heap[0];
    public bool Contains(T item)
    {
        return Equals(item, heap[item.Id]);
    }

    public void Insert(T item)
    {
        item.Id = count;
        heap[count] = item;
        ShiftUp(item);
        count++;
    }
    public T RemoveFirst()
    {
        T first = heap[0];
        count--;
        heap[0] = heap[count];
        heap[0].Id = 0;
        ShiftDown(heap[0]);

        return first;
    }
    public void ShiftUp(T item)
    {
        int parentId = (item.Id - 1) / 2;
        while (item.Id > 0 && item.CompareTo(heap[parentId]) > 0)
        {
            Swap(heap[item.Id], heap[parentId]);
            parentId = (item.Id - 1) / 2;

        }
    }
    public void ShiftDown(T item)
    {
        int left = item.Id * 2 + 1;
        while (left < count)
        {
            int right = item.Id * 2 + 2;
            int swapPosition = 0;
            if (right < count && heap[left].CompareTo(heap[right]) < 0)
            {
                swapPosition = right;
            }
            else
            {
                swapPosition = left;
            }
            if (item.CompareTo(heap[swapPosition]) < 0)
            {
                Swap(item, heap[swapPosition]);
                left = item.Id * 2 + 1;
            }
            else
            {
                return;
            }
        }
    }
    void Swap(T item1, T item2)
    {
        int holder = item1.Id;
        heap[item1.Id] = item2;
        heap[item2.Id] = item1;
        item1.Id = item2.Id;
        item2.Id = holder;
    }

}
