using System;
using System.Collections.Generic;

public class KdTree : IBoundable
{
    private Node root;

    public class Node : IBoundable
    {
        public Node(Point2D point, int x1, int y1, int width, int height)
        {
            this.Point = point;
            this.Bounds = new Rectangle(x1, y1, width, height);
        }

        public Node(Point2D point, Rectangle bounds)
        {
            this.Point = point;
            this.Bounds = bounds;
        }

        public Point2D Point { get; set; }
        public Rectangle Bounds { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }

    public KdTree(int width, int height)
    {
        this.Bounds = new Rectangle(0, 0, width, height);
    }

    public Node Root
    {
        get
        {
            return this.root;
        }
    }

    public Rectangle Bounds { get; set; }

    public bool Contains(Point2D point)
    {
        var node = this.Search(this.root, point, 0);
        return node != null;
    }

    public void Insert(Point2D point)
    {
        if (point == null)
        {
            throw new ArgumentException("Argument is null");
        }

        if (this.root == null)
        {
            this.root = new Node(point, this.Bounds);
        }
        else
        {
            this.root = this.Insert(this.root, null, point, 0);
        }
    }

    public void EachInOrder(Action<Point2D> action)
    {
        this.EachInOrder(this.root, action);
    }

    public IEnumerable<Point2D> RangeSearch(Rectangle bounds)
    {
        var results = new List<Point2D>();
        this.RangeSearch(this.root, bounds, results);
        return results;
    }

    private void RangeSearch(Node node, Rectangle bounds, List<Point2D> results)
    {
        if (node == null)
        {
            return;
        }

        if (node.Point.IsInside(bounds))
        {
            results.Add(node.Point);
        }

        var goLeft = node.Left != null && node.Left.Bounds.Intersects(bounds);
        var goRight = node.Right != null && node.Right.Bounds.Intersects(bounds);

        if (goLeft)
        {
            RangeSearch(node.Left, bounds, results);
        }

        if (goRight)
        {
            RangeSearch(node.Right, bounds, results);
        }
    }

    private Node Insert(Node node, Rectangle bounds, Point2D point, int depth)
    {
        if (node == null)
        {
            return new Node(point, bounds);
        }

        int cmp = Compare(point, node.Point, depth);
        var childBounds = GetBounds(node, cmp, depth);
        if (cmp < 0)
        {
            node.Left = this.Insert(node.Left, childBounds, point, depth + 1);
        }
        else if (cmp > 0)
        {
            node.Right = this.Insert(node.Right, childBounds, point, depth + 1);
        }

        return node;
    }

    private Rectangle GetBounds(Node node, int cmp, int depth)
    {
        if (cmp < 0 && depth % 2 == 0) return node.Bounds.GetLeft(node.Point);
        if (cmp > 0 && depth % 2 == 0) return node.Bounds.GetRight(node.Point);
        if (cmp < 0 && depth % 2 == 1) return node.Bounds.GetTop(node.Point);
        else return node.Bounds.GetBot(node.Point);
    }

    private Node Search(Node node, Point2D point, int depth)
    {
        if (node == null)
        {
            return null;
        }

        int cmp = Compare(point, node.Point, depth);
        if (cmp < 0)
        {
            return Search(node.Left, point, depth + 1);
        }
        else if (cmp > 0)
        {
            return Search(node.Right, point, depth + 1);
        }

        return node;
    }

    private void EachInOrder(Node node, Action<Point2D> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Point);
        this.EachInOrder(node.Right, action);
    }

    private int Compare(Point2D a, Point2D b, int depth)
    {
        int cmp = 0;
        if (depth % 2 == 0)
        {
            cmp = a.X.CompareTo(b.X);
            if (cmp == 0)
            {
                cmp = a.Y.CompareTo(b.Y);
            }

            return cmp;
        }
        else
        {
            cmp = a.Y.CompareTo(b.Y);
            if (cmp == 0)
            {
                cmp = a.X.CompareTo(b.X);
            }
        }

        return cmp;
    }
}
