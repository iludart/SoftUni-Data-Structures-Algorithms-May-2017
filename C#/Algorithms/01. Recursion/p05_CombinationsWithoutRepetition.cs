using System;
using System.Collections;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        var end = int.Parse(Console.ReadLine());
        var k = int.Parse(Console.ReadLine());

        int start = 1;
        int[] arr = new int[k];
        int idx = 0;
        GenerateCombinationNoRepetitions(arr, idx, start, end);
    }

    private static void GenerateCombinationNoRepetitions
        (int[] arr, int idx, int start, int end)
    {
        if (idx == arr.Length)
        {
            Console.WriteLine(string.Join(" ", arr));
            return;
        }

        for (int i = start; i <= end; i++)
        {
            arr[idx] = i;
            GenerateCombinationNoRepetitions(arr, idx + 1, i + 1, end);
        }
    }
}
