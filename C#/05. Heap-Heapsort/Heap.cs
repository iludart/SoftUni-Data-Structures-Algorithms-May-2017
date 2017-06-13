using System;

public static class Heap<T> where T : IComparable<T>
{
    public static void Sort(T[] arr)
    {
        for (int i = arr.Length / 2; i >= 0; i--)
        {
            Sink(arr, i, arr.Length);
        }

        for (int i = arr.Length - 1; i > 0; i--)
        {
            Swap(arr, 0, i);
            Sink(arr, 0, i);
        }
    }

    private static void Sink(T[] arr, int index, int length)
    {
        while (index < length / 2)
        {
            int child = 2 * index + 1;
            if (child + 1 < length && arr[child + 1].CompareTo(arr[child]) > 0)
            {
                child++;
            }

            if (arr[index].CompareTo(arr[child]) > 0)
            {
                break;
            }

            Swap(arr, index, child);

            index = child;
        }
    }

    private static void Swap(T[] arr, int a, int b)
    {
        T temp = arr[a];
        arr[a] = arr[b];
        arr[b] = temp;
    }
}
