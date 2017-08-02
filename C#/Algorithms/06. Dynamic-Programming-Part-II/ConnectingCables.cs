using System;

public class ConnectingCables
{
    static int[] p1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    static int[] p2 = { 2, 5, 3, 8, 7, 4, 6, 9, 1 };

    static int[,] maxConnected;

    static void Main(string[] args)
    {
        maxConnected = new int[p1.Length + 1, p2.Length + 1];
        for (int i = 1; i < p1.Length + 1; i++)
        {
            for (int j = 1; j < p2.Length + 1; j++)
            {
                maxConnected[i, j] = -1;
            }
        }

        Console.WriteLine(GetMaxConnected(p1.Length, p2.Length));
    }

    private static int GetMaxConnected(int x, int y)
    {
        if (x < 0 || y < 0)
        {
            return 0;
        }

        if (maxConnected[x, y] != -1)
        {
            return maxConnected[x, y];
        }

        if (p1[x - 1] == p2[y - 1])
        {
            maxConnected[x, y] = 1 + GetMaxConnected(x - 1, y - 1);
        }
        else
        {
            int reduceP1 = GetMaxConnected(x - 1, y);
            int reduceP2 = GetMaxConnected(x, y - 1);

            maxConnected[x, y] = Math.Max(reduceP1, reduceP2);
        }

        return maxConnected[x, y];
    }
}
