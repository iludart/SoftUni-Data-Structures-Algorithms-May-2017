using System;
using System.Collections.Generic;
using System.Linq;

public class TreeSolver
{
    static Dictionary<int, Tree<int>> nodes =
        new Dictionary<int, Tree<int>>();

    static void Main()
    {
        ReadTree();

        Tree<int> root = GetRoot();

        foreach (var node in GetSubtreeWithSum(root))
        {
            PrintPreOrder(node);
            Console.WriteLine();
        }
    }

    private static void PrintPreOrder(Tree<int> node)
    {
        Console.Write(node.Value + " ");
        foreach (var child in node.Children)
        {
            PrintPreOrder(child);
        }
    }

    private static void PrintPath(Tree<int> leaf)
    {
        var stack = new Stack<int>();
        var current = leaf;
        while (current != null)
        {
            stack.Push(current.Value);
            current = current.Parent;
        }

        Console.WriteLine(string.Join(" ", stack.ToArray()));
    }

    private static void ReadTree()
    {
        var n = int.Parse(Console.ReadLine());

        for (int i = 0; i < n - 1; i++)
        {
            var edge = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();

            Tree<int> parent = GetNode(edge[0]);
            Tree<int> child = GetNode(edge[1]);

            parent.Children.Add(child);
            child.Parent = parent;
        }
    }

    private static Tree<int> GetRoot()
    {
        return nodes.Values.Where(x => x.Parent == null).FirstOrDefault();
    }

    private static Tree<int> GetNode(int value)
    {
        if (!nodes.ContainsKey(value))
        {
            nodes[value] = new Tree<int>(value);
        }

        return nodes[value];
    }

    private static List<Tree<int>> GetSubtreeWithSum(Tree<int> root)
    {
        var target = int.Parse(Console.ReadLine());
        Console.WriteLine("Subtrees of sum {0}:", target);

        var roots = new List<Tree<int>>();

        var sum = FindSubtreeDFS(root, target, 0, roots);

        return roots;
    }

    private static int FindSubtreeDFS(Tree<int> node, int target, int current, List<Tree<int>> roots)
    {
        if (node == null)
        {
            return 0;
        }

        current = node.Value;

        foreach (var child in node.Children)
        {
            current += FindSubtreeDFS(child, target, current, roots);
        }

        if (current == target)
        {
            roots.Add(node);
        }

        return current;
    }

    private static Tree<int> GetDeepestNode(Tree<int> root)
    {
        int maxLevel = 0;
        Tree<int> deepest = null;
        GetDeepestNode(root, 0, ref maxLevel, ref deepest);
        return deepest;
    }

    private static void GetDeepestNode(Tree<int> node, int level, ref int max, ref Tree<int> deepest)
    {
        if (node == null)
        {
            return;
        }

        if (level > max)
        {
            max = level;
            deepest = node;
        }

        foreach (var child in node.Children)
        {
            GetDeepestNode(child, level + 1, ref max, ref deepest);
        }
    }

    private static void GetLeafNodesWithTargetSum(Tree<int> node, int current, int target, List<Tree<int>> nodes)
    {
        if (node == null)
        {
            return;
        }

        current += node.Value;

        if (current == target && node.Children.Count == 0)
        {
            nodes.Add(node);
        }

        foreach (var child in node.Children)
        {
            GetLeafNodesWithTargetSum(child, current, target, nodes);
        }
    }
}
