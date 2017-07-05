using System;
using System.Collections.Generic;

public class BinaryHeap<T> where T : IComparable<T>
{
    private List<T> heap;

    public BinaryHeap()
    {
        this.heap = new List<T>();
    }

    public int Count
    {
        get
        {
            return this.heap.Count;
        }
    }

    public void Insert(T item)
    {
        this.heap.Add(item);
        this.HeapifyUp(this.heap.Count - 1);
    }

    private void HeapifyUp(int current)
    {
        int parent = this.Parent(current);
        while (current > 0 && IsGreater(current, parent))
        {
            Swap(current, parent);
            current = parent;
            parent = this.Parent(current);
        }
    }

    private int Parent(int index)
    {
        return ((index - 1) / 2);
    }

    public T Peek()
    {
        return this.heap[0];
    }

    public T Pull()
    {
        if (this.heap.Count <= 0)
        {
            throw new InvalidOperationException();
        }

        T item = this.heap[0];
        this.heap[0] = this.heap[this.heap.Count - 1];
        this.heap.RemoveAt(this.heap.Count - 1);
        this.HeapifyDown(0);

        return item;
    }

    private void HeapifyDown(int parent)
    {
        while (parent < this.heap.Count / 2)
        {
            int child = Left(parent);
            if (HasRight(parent) && IsGreater(Right(parent), child))
            {
                child++;
            }

            if (! IsGreater(child, parent))
            {
                break;
            }

            Swap(child, parent);
            parent = child;
        }
    }

    private int Right(int parent)
    {
        return 2 * parent + 2;
    }

    private bool HasRight(int parent)
    {
        return 2 * parent + 2 < this.heap.Count; 
    }

    private int Left(int index)
    {
        return 2 * index + 1;
    }

    private void Swap(int a, int b)
    {
        T temp = this.heap[a];
        this.heap[a] = this.heap[b];
        this.heap[b] = temp;
    }

    private bool IsGreater(int index, int parent)
    {
        return this.heap[index].CompareTo(this.heap[parent]) > 0;
    }
}
