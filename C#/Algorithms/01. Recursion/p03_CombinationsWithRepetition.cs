using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine());
        int m = int.Parse(Console.ReadLine());

        generate(n, m, new int[m], 0, 1);
    }

    private static void generate(int n, int m, int[] array, int idx, int startNum)
    {
        if (idx == array.Length)
        {
            Console.WriteLine(String.Join(" ", array));
            return;
        }


        for (int i = startNum; i <= n; i++)
        {
            array[idx] = i;
            generate(n, m, array, idx + 1, i);
        }
    }
}

