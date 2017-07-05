using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        int[] array = Console.ReadLine()
            .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(e => int.Parse(e))
            .ToArray();

        ReverseArray(array, 0, array.Length - 1);
        Console.WriteLine(String.Join(" ", array));
    }

    private static void ReverseArray
        (int[] array, int startIndex, int endIndex)
    {
        if (startIndex < endIndex)
        {
            int front = array[startIndex];
            int end = array[endIndex];
            array[startIndex] = end;
            array[endIndex] = front;

            ReverseArray(array, startIndex + 1, endIndex - 1);
        }
    }
}

