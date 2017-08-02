using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        int tasksCount = int.Parse(Console.ReadLine().Split(' ')[1]);

        List<Task> tasks = new List<Task>();
        for (int i = 0; i < tasksCount; i++)
        {
            int[] taskTokens = Console.ReadLine()
                .Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(e => int.Parse(e))
                .ToArray();

            int value = taskTokens[0];
            int deadline = taskTokens[1];

            Task task = new Task(i + 1, value, deadline);
            tasks.Add(task);
        }


        tasks.Sort((t1, t2) => t2.value.CompareTo(t1.value));

        List<Task> executedTasks = new List<Task>();
        int maxDeadLine = tasks.Max(t => t.deadline);
        foreach (var task in tasks)
        {
                executedTasks.Add(task);
        }

        executedTasks = executedTasks.Take(maxDeadLine).ToList();
        executedTasks.Sort();
        int totalValue = 0;
        foreach (var task in executedTasks)
        {
            totalValue += task.value;
        }
        Console.WriteLine($"Optimal schedule: {string.Join(" -> ", executedTasks)}");
        Console.WriteLine($"Total value: {totalValue}");
    }

    class Task : IComparable<Task>
    {
        int number;
        public int value;
        public int deadline;

        public Task(int number, int value, int deadline)
        {
            this.number = number;
            this.value = value;
            this.deadline = deadline;
        }

        public int CompareTo(Task other)
        {
            int cmp = this.deadline.CompareTo(other.deadline);

            if (cmp == 0)
            {
                cmp = other.value.CompareTo(this.value);
            }

            return cmp;
        }

        public override string ToString()
        {
            return this.number.ToString();
        }
    }
}

