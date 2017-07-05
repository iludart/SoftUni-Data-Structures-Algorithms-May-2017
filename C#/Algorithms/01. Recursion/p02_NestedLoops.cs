using System;

class Program
{
    static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine());

        int[] array = new int[n];
        int idx = 0;
        GenerarateValues(array, idx);

        // Console.WriteLine(string.Join(" ", array));
    }

    private static void GenerarateValues(int[] array, int idx)
    {
        if (idx == array.Length)
        {
            Console.WriteLine(string.Join(" ", array));
            return;
        }

        for (int i = 1; i <= array.Length; i++)
        {
            array[idx] = i;
            GenerarateValues(array, idx + 1);
        }
    }
}

