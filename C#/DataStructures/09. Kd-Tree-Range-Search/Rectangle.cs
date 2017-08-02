using System;

public class Rectangle
{
    public Rectangle(int x1, int y1, int width, int height)
    {
        this.X1 = x1;
        this.Y1 = y1;
        this.X2 = x1 + width;
        this.Y2 = y1 + height;
    }

    public int Y1 { get; set; }

    public int X1 { get; set; }

    public int Y2 { get; set; }

    public int X2 { get; set; }

    public int Width { get { return this.X2 - this.X1; } }

    public int Height { get { return this.Y2 - this.Y1; } }

    public Rectangle GetLeft(Point2D point)
    {
        if (!point.IsInside(this))
        {
            throw new ArgumentException("Point should be in rectangle");
        }

        return new Rectangle(this.X1, this.Y1, point.X - this.X1, this.Height);
    }

    public Rectangle GetRight(Point2D point)
    {
        if (!point.IsInside(this))
        {
            throw new ArgumentException("Point should be in rectangle");
        }

        return new Rectangle(point.X, this.Y1, this.X2 - point.X, this.Height);
    }

    public Rectangle GetTop(Point2D point)
    {
        if (!point.IsInside(this))
        {
            throw new ArgumentException("Point should be in rectangle");
        }

        return new Rectangle(this.X1, this.Y1, this.Width, point.Y - this.Y1);
    }

    public Rectangle GetBot(Point2D point)
    {
        if (!point.IsInside(this))
        {
            throw new ArgumentException("Point should be in rectangle");
        }

        return new Rectangle(this.X1, point.Y, this.Width, this.Y2 - point.Y);
    }

    public bool Intersects(Rectangle other)
    {
        return 
            this.X1 <= other.X2 &&
            other.X1 <= this.X2 &&
            this.Y1 <= other.Y2 &&
            other.Y1 <= this.Y2;
    }

    public bool IsInside(Rectangle other)
    {
        return 
            this.X2 <= other.X2 &&
            this.X1 >= other.X1 &&
            this.Y1 >= other.Y1 &&
            this.Y2 <= other.Y2;
    }

    public override string ToString()
    {
        return string.Format("({0}, {1}) .. ({2}, {3})",
            this.X1, this.Y1, this.X2, this.Y2);
    }
}
