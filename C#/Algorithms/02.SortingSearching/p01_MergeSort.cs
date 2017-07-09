using System;
using System.Linq;
using System.Text;

public class Mergesort<T> where T : IComparable
{
    private static T[] aux;

    public static void Sort(T[] arr)
    {
        aux = new T[arr.Length];
        Sort(arr, 0, arr.Length - 1);
    }

    private static void Sort(T[] arr, int lo, int hi)
    {
        if (lo >= hi)
        {
            return;
        }

        int mid = lo + (hi - lo) / 2;
        Sort(arr, lo, mid);
        Sort(arr, mid + 1, hi);
        Merge(arr, lo, mid, hi);
    }

    private static void Merge(T[] arr, int left, int mid, int right)
    {
        if (IsLess(arr[mid], arr[mid + 1]))
        {
            return;
        }

        for (int index = left; index <= right; index++)
        {
            aux[index] = arr[index];
        }


        int i = left;
        int j = mid + 1;
        for (int index = left; index <= right; index++)
        { 
            if (i > mid)
            {
                arr[index] = aux[j++];
            }
            else if (j > right)
            {
                arr[index] = aux[i++];
            }
            else if (IsLess(aux[i], aux[j]))
            {
                arr[index] = aux[i++];
            }
            else
            {
                arr[index] = aux[j++];
            }
        }
    }

    private static bool IsLess(T i, T j)
    {
        return i.CompareTo(j) < 0;
    }

    private static bool IsSorted(T[] a, int lo, int hi)
    {
        for (int i = lo + 1; i < hi; i++)
        {
            if (IsLess(a[i], a[i - 1]))
            {
                return false;
            }
        }

        return true;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        int[] arr = Console.ReadLine().Split().Select(int.Parse).ToArray();

        Mergesort<int>.Sort(arr);

        StringBuilder builder = new StringBuilder();
        foreach (var num in arr)
        {
            builder.Append(num + " ");
        }

        Console.WriteLine(builder);
    }
}
