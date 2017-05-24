using System;
using System.Collections.Generic;
using System.Linq;

public class RemoveOddOccurrences
{
    static void Main(string[] args)
    {
        List<int> numbers = Console.ReadLine()
            .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(x => int.Parse(x))
            .ToList();

        Dictionary<int, int> occurrences = new Dictionary<int, int>();

        for (int i = 0; i < numbers.Count; i++)
        {
            if (! occurrences.ContainsKey(numbers[i]))
            {
                occurrences.Add(numbers[i], 0);
            }

            occurrences[numbers[i]] += 1;
        }

        for (int i = 0; i < numbers.Count; i++)
        {
            if (occurrences[numbers[i]] % 2 == 0)
            {
                Console.Write(numbers[i] + " ");
            }
        }
    }
}
