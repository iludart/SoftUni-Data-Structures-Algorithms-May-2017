using System;
using System.Collections.Generic;

public class AStar
{
    private char[,] map;

    public AStar(char[,] map)
    {
        this.map = map;
    }

    public static int GetH(Node current, Node goal)
    {
        var deltaY = Math.Abs(current.Row - goal.Row);
        var deltaX = Math.Abs(current.Col - goal.Col);

        return deltaY + deltaX;
    }

    public IEnumerable<Node> GetPath(Node start, Node goal)
    {
        var cost = new Dictionary<Node, int>();
        var parent = new Dictionary<Node, Node>();
        var queue = new PriorityQueue<Node>();

        queue.Enqueue(start);
        parent[start] = null;
        cost[start] = 0;

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current.Equals(goal))
            {
                break;
            }

            var neighbours = GetNeighbours(current);
            foreach (var neighbour in neighbours)
            {
                var newCost = cost[current] + 1;
                if (!cost.ContainsKey(neighbour) || newCost < cost[neighbour])
                {
                    cost[neighbour] = newCost;
                    neighbour.F = newCost + GetH(neighbour, goal);
                    queue.Enqueue(neighbour);
                    parent[neighbour] = current;
                }
            }
        }

        return GetPath(parent, start, goal);
    }

    private IEnumerable<Node> GetNeighbours(Node current)
    {
        var result = new List<Node>();

        AddNeighbour(current.Row - 1, current.Col, result);
        AddNeighbour(current.Row, current.Col + 1, result);
        AddNeighbour(current.Row + 1, current.Col, result);
        AddNeighbour(current.Row, current.Col - 1, result);

        return result;
    }

    private void AddNeighbour(int row, int col, List<Node> result)
    {
        if (InBounds(row, col) && IsPassable(row, col))
        {
            var neighbour = new Node(row, col);
            result.Add(neighbour);
        }
    }

    private bool IsPassable(int row, int col)
    {
        return this.map[row, col] != 'W';
    }

    private bool InBounds(int row, int col)
    {
        return row >= 0 && row < this.map.GetLength(0) && col >= 0 && col < this.map.GetLength(1);
    }

    private IEnumerable<Node> GetPath(Dictionary<Node, Node> parent, Node start, Node goal)
    {
        var path = new Stack<Node>();

        if (!parent.ContainsKey(goal))
        {
            path.Push(start);
            return path;
        }

        var current = goal;
        while (current != null)
        {
            path.Push(current);
            current = parent[current];
        }

        return path;
    }
}

