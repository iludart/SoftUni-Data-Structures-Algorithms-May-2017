using System;
using System.Collections.Generic;

public class Snakes
{
    static int count = 0;
    static HashSet<string> snakes = new HashSet<string>();
    static HashSet<string> visited = new HashSet<string>();

    static char[] snake = { 'S', 'R', 'U' };

    static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine());

        snake = new char[n];
        snake[0] = 'S';
        visited.Add(0 + " " + 0);

        Generate(1, 0, 1, 'R');
        Console.WriteLine("Snakes count = " + count);
    }

    private static void Generate(int index, int row, int col, char direction)
    {
        if (index >= snake.Length)
        {
            TryAddSnake();
        }
        else
        {
            var cell = row + " " + col;
            if (!visited.Contains(cell))
            {
                visited.Add(cell);
                snake[index] = direction;

                Generate(index + 1, row, col + 1, 'R');
                Generate(index + 1, row + 1, col, 'D');
                Generate(index + 1, row, col - 1, 'L');
                Generate(index + 1, row - 1, col, 'U');

                visited.Remove(cell);
            }
        }
    }

    private static void TryAddSnake()
    {
        var normal = new string(snake);
        var flipped = FlipY(normal);
        var reversed = Reversed();

        if (snakes.Contains(normal) || snakes.Contains(flipped) || snakes.Contains(reversed))
        {
            return;
        }
        
        snakes.Add(Rotate(reversed.ToCharArray()));
        snakes.Add(Rotate(flipped.ToCharArray()));

        Console.WriteLine(normal);
        count++;
    }

    private static string Reversed()
    {
        var reversed = new char[snake.Length];
        reversed[0] = 'S';
        for (int i = snake.Length - 1; i > 0; i--)
        {
            reversed[snake.Length - i] = snake[i];
        }

        return new string(reversed);
    }

    private static string FlipY(string toFlip)
    {
        var flipped = new char[toFlip.Length];
        for (int i = 0; i < toFlip.Length; i++)
        {
            if (toFlip[i] == 'D')
            {
                flipped[i] = 'U';
            }
            else if (toFlip[i] == 'U')
            {
                flipped[i] = 'D';
            }
            else
            {
                flipped[i] = toFlip[i];
            }
        }

        return new string(flipped);
    }

    private static string Rotate(char[] toRotate)
    {
        while (toRotate[1] != 'R')
        {
            for (int i = 1; i < toRotate.Length; i++)
            {
                switch (toRotate[i])
                {
                    case 'R': toRotate[i] = 'D'; break;
                    case 'D': toRotate[i] = 'L'; break;
                    case 'L': toRotate[i] = 'U'; break;
                    case 'U': toRotate[i] = 'R'; break;
                    default: break;
                }
            }
        }

        return new string(toRotate);
    }
}
