using System;
using System.Collections.Generic;
using System.Linq;

public class QuadTree<T> where T : IBoundable
{
    public const int DefaultMaxDepth = 5;

    public readonly int MaxDepth;

    private Node<T> root;

    public QuadTree(int width, int height, int maxDepth = DefaultMaxDepth)
    {
        this.root = new Node<T>(0, 0, width, height);
        this.Bounds = this.root.Bounds;
        this.MaxDepth = maxDepth;
    }

    public int Count { get; private set; }

    public Rectangle Bounds { get; private set; }

    public void ForEachDfs(Action<List<T>, int, int> action)
    {
        this.ForEachDfs(this.root, action);
    }

    public bool Insert(T item)
    {
        var current = this.root;
        var depth = 1;
        while (current.Children != null)
        {
            var q = GetQ(current, item.Bounds);
            if (q == -1) break;
            current = current.Children[q];
            depth++;
        }

        current.Items.Add(item);
        TrySplit(current, depth);
        this.Count++;
        return true;
    }

    private void TrySplit(Node<T> node, int depth)
    {
        if (!node.ShouldSplit || depth >= MaxDepth)
        {
            return;
        }

        node.Children = new Node<T>[4];

        var leftWidth = node.Bounds.Width / 2;
        var rightWidth = node.Bounds.Width - leftWidth;
        var topHeight = node.Bounds.Height / 2;
        var botHeight = node.Bounds.Height - topHeight;

        node.Children[0] = new Node<T>(node.Bounds.MidX, node.Bounds.Y1, rightWidth, topHeight);
        node.Children[1] = new Node<T>(node.Bounds.X1, node.Bounds.Y1, leftWidth, topHeight);
        node.Children[2] = new Node<T>(node.Bounds.X1, node.Bounds.MidX, leftWidth, botHeight);
        node.Children[3] = new Node<T>(node.Bounds.MidX, node.Bounds.MidY, rightWidth, botHeight);

        var toRemove = new HashSet<T>();
        foreach (var item in node.Items)
        {
            var q = GetQ(node, item.Bounds);
            if (q != -1)
            {
                node.Children[q].Items.Add(item);
                toRemove.Add(item);
            }
        }

        node.Items.RemoveAll(x => toRemove.Contains(x));

        foreach (var child in node.Children)
        {
            TrySplit(child, depth + 1);
        }
    }

    private int GetQ(Node<T> current, Rectangle bounds)
    {
        if (current.Children != null)
        {
            if (bounds.IsInside(current.Children[0].Bounds)) return 0;
            if (bounds.IsInside(current.Children[1].Bounds)) return 1;
            if (bounds.IsInside(current.Children[2].Bounds)) return 2;
            if (bounds.IsInside(current.Children[3].Bounds)) return 3;
        }

        return -1;
    }

    public List<T> Report(Rectangle bounds)
    {
        List<T> results = new List<T>();
        GetPotentialCollisions(this.root, bounds, results);
        return results;
    }

    private void GetPotentialCollisions(Node<T> node, Rectangle bounds, List<T> results)
    {
        var q = GetQ(node, bounds);
        if (q != -1)
        {
            results.AddRange(node.Items);
            GetPotentialCollisions(node.Children[q], bounds, results);
        }
        else
        {
            GetNodeItems(node, results);
        }
    }

    private void GetNodeItems(Node<T> node, List<T> results)
    {
        if (node == null)
        {
            return;
        }

        results.AddRange(node.Items);

        if (node.Children != null)
        {
            foreach (var child in node.Children)
            {
                GetNodeItems(child, results);
            }
        }
    }

    private void ForEachDfs(Node<T> node, Action<List<T>, int, int> action, int depth = 1, int quadrant = 0)
    {
        if (node == null)
        {
            return;
        }

        if (node.Items.Any())
        {
            action(node.Items, depth, quadrant);
        }

        if (node.Children != null)
        {
            for (int i = 0; i < node.Children.Length; i++)
            {
                ForEachDfs(node.Children[i], action, depth + 1, i);
            }
        }
    }
}
