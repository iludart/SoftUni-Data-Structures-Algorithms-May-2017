using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static char WALL = '*';
    static char VISITED = 'v';

    static List<Area> areas = new List<Area>();
  
    static void Main(string[] args)
    {
        int row = int.Parse(Console.ReadLine());
        int col = int.Parse(Console.ReadLine());
        char[,] matrix = readMatrix(row, col);


        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                FindConnectedArea(matrix, i, j);
            }
        }

        areas.Sort();
        Console.WriteLine($"Total areas found: {areas.Count}");
        int positionCount = 1;
        foreach (var area in areas)
        {
            Console.WriteLine
                ($"Area #{positionCount++} at ({area.row}, {area.col}), size: {area.size}");
        
        }
    }

    private static void FindConnectedArea(char[,]matrix, int i, int j)
    {
        if (matrix[i,j] == WALL || matrix[i,j] == VISITED)
        {
            return;
        }

        Area area = new Area(i, j);
        FillArea(matrix, i, j, area);

        areas.Add(area);
    }

    private static void FillArea
        (char[,] matrix, int row, int col, Area area)
    {
        if (!IsInBoundaries(matrix, row, col) ||
            matrix[row, col] == VISITED ||
            matrix[row, col] == WALL)
        {
            return;
        }

        matrix[row, col] = VISITED;
        area.size++;

        FillArea(matrix, row + 1, col, area);
        FillArea(matrix, row, col + 1, area);
        FillArea(matrix, row - 1, col, area);
        FillArea(matrix, row, col - 1, area);
    }

    private static bool IsInBoundaries(char[,] matrix, int row, int col)
    {
        return row >= 0 && row < matrix.GetLength(0) &&
            col >= 0 && col < matrix.GetLength(1);
    }
    private static char[,] readMatrix(int row, int col)
    {
        var matrix = new char[row, col];
        for (int i = 0; i < row; i++)
        {
            char[] currentRow = Console.ReadLine().ToCharArray();
            for (int j = 0; j < currentRow.Length; j++)
            {
                matrix[i, j] = currentRow[j];
            }
        }
        return matrix;
    }

    class Area : IComparable<Area>
    {
        public int row;
        public int col;

        public int size;

        public Area(int row, int col)
        {
            this.row = row;
            this.col = col;

            this.size = 0;
        }

        public int CompareTo(Area other)
        {
            int cmp = other.size.CompareTo(this.size);
            if (cmp == 0)
            {
                cmp = this.row.CompareTo(other.row);
            }

            if (cmp == 0)
            {
                cmp = this.col.CompareTo(other.col);
            }

            return cmp;
        }
    }
}
