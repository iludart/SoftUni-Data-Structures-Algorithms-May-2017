using System;
using System.Collections.Generic;
using System.Linq;

public class Cubes
{
    static int count = 0;
    static HashSet<string> marked = new HashSet<string>();

    static void Main(string[] args)
    {
        var cube = Console.ReadLine()
            .Split()
            .Select(x => x[0])
            .ToArray();

        Permute(cube, 0);
        
        Console.WriteLine(count);
    }

    private static void Permute(char[] cube, int index)
    {
        if (index >= cube.Length)
        {
            if (!marked.Contains(new string(cube)))
            {
                MarkCube(cube);
                count++;
            }
        }
        else
        {
            Permute(cube, index + 1);
            HashSet<char> used = new HashSet<char>();
            used.Add(cube[index]);
            for (int i = index + 1; i < cube.Length; i++)
            {
                if (!used.Contains(cube[i]))
                {
                    Swap(cube, index, i);
                    Permute(cube, index + 1);
                    Swap(cube, index, i);

                    used.Add(cube[i]);
                }
            }
        }
    }

    private static void MarkCube(char[] cube)
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                for (int z = 0; z < 4; z++)
                {
                    marked.Add(new string(cube));
                    cube = RotateZ(cube);
                }
                cube = RotateY(cube);
            }
            cube = RotateX(cube);
        }
    }

    private static char[] RotateZ(char[] cube) // ok
    {
        var rotated = new char[cube.Length];
        rotated[0] = cube[1];
        rotated[1] = cube[2];
        rotated[2] = cube[3];
        rotated[3] = cube[0];

        rotated[4] = cube[5];
        rotated[5] = cube[6];
        rotated[6] = cube[7];
        rotated[7] = cube[4];

        rotated[8] = cube[9];
        rotated[9] = cube[10];
        rotated[10] = cube[11];
        rotated[11] = cube[8];

        return rotated;
    }

    private static char[] RotateX(char[] cube)
    {
        var rotated = new char[cube.Length];
        rotated[0] = cube[9];
        rotated[1] = cube[5];
        rotated[2] = cube[10];
        rotated[3] = cube[1];

        rotated[4] = cube[8];
        rotated[5] = cube[7];
        rotated[6] = cube[11];
        rotated[7] = cube[3];

        rotated[8] = cube[0];
        rotated[9] = cube[4];
        rotated[10] = cube[6];
        rotated[11] = cube[2];

        return rotated;
    }

    private static char[] RotateY(char[] cube)
    {
        var rotated = new char[cube.Length];
        rotated[0] = cube[2];
        rotated[1] = cube[10];
        rotated[2] = cube[6];
        rotated[3] = cube[11];

        rotated[4] = cube[0];
        rotated[5] = cube[9];
        rotated[6] = cube[4];
        rotated[7] = cube[8];

        rotated[8] = cube[3];
        rotated[9] = cube[1];
        rotated[10] = cube[5];
        rotated[11] = cube[7];

        return rotated;
    }

    private static void Swap(char[] cube, int i, int j)
    {
        var temp = cube[i];
        cube[i] = cube[j];
        cube[j] = temp;
    }
}