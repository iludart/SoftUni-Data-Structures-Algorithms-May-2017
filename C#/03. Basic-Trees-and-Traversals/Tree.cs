using System;
using System.Collections.Generic;

public class Tree<T>
{
    public T Value { get; set; }
    public Tree<T> Parent { get; set; }
    public List<Tree<T>> Children { get;set; }

    public Tree(T value, params Tree<T>[] children)
    {
        this.Value = value;
        this.Children = new List<Tree<T>>(children);
    }

    public void Print(int indentation = 0)
    {
        Console.WriteLine(new string(' ', indentation) + this.Value);

        foreach (var child in this.Children)
        {
            child.Print(indentation + 2);
        }
    }

    public override string ToString()
    {
        return this.Value.ToString();
    }
}
