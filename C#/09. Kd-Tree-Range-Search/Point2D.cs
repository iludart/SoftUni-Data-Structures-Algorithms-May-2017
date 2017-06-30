public class Point2D
{
    public Point2D(string id, int x, int y)
    {
        this.ID = id;
        this.X = x;
        this.Y = y;
    }

    public string ID { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public override string ToString()
    {
        return string.Format("{0} {1} {2}", this.ID, this.X, this.Y);
    }

    public bool IsInside(Rectangle rectangle)
    {
        return
            this.X >= rectangle.X1 &&
            this.X <= rectangle.X2 &&
            this.Y >= rectangle.Y1 &&
            this.Y <= rectangle.Y2;
    }

    public override bool Equals(object obj)
    {
        if (obj == this) return true;
        if (obj == null) return false;
        if (obj.GetType() != this.GetType()) return false;
        Point2D that = (Point2D)obj;
        return this.X == that.X && this.Y == that.Y;
    }

    public override int GetHashCode()
    {
        int hashX = this.X.GetHashCode();
        int hashY = this.Y.GetHashCode();
        return 31 * hashX + hashY;
    }

    public int CompareTo(Point2D that)
    {
        if (this.Y < that.Y) return -1;
        if (this.Y > that.Y) return +1;
        if (this.X < that.X) return -1;
        if (this.X > that.X) return +1;
        return 0;
    }
}
