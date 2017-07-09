using System;
using System.Linq;
using System.Text;

public class Program
{
    public static void Main(string[] args)
    {
        int[] arr = Console.ReadLine().Split().Select(int.Parse).ToArray();
        int x = int.Parse(Console.ReadLine());

        int index = BinarySearchIterative(arr, x, 0, arr.Length - 1);
        Console.WriteLine(index);
    }

    private static int BinarySearchIterative(int[] arr, int x, int low, int high)
    {
        int index = -1;
        while(low <= high)
        {
            int mid = (low + high) / 2;
            if (arr[low] == x)
            {
                return low;
            }
            if (arr[mid] == x)
            {
                return mid;
            }
            else
            {
                if (arr[mid] > x)
                {
                    high = mid - 1;
                } else
                {
                    low = mid + 1;
                }
            }
        }

        return index;
    }

    private static int BinarySearchRecursive(int[] arr, int x, int low, int high)
    {
        if (low > high)
        {
            return -1;
        }

        int mid = (high + low) / 2;
        if (arr[mid] == x)
        {
            return mid;
        }
        else
        {
            if (arr[mid] > x)
            {
                return BinarySearchRecursive(arr, x, low, mid);
            } else
            {
                return BinarySearchRecursive(arr, x, mid + 1, high);
            }
        }
    
    }
}
