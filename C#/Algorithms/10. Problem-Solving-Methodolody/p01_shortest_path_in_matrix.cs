using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static int INFINITY = int.MaxValue / 2 + 2017;

    static int[,] matrix;
    static Dictionary<Cell, List<Cell>> graph;
    static Dictionary<Cell, int> bestPathToCell;
    static Dictionary<Cell, Cell> prevCell;
    static Cell onCell;

    static void Main(string[] args)
    {
        // read matrix
        matrix = readMatrix();

        // build graph
        buildGraph(matrix);

        // dikstra
        dijkstra();

        // print result
        printResult();
    }

    private static void printResult()
    {
        Console.WriteLine($"Length: {onCell.Value}");
        LinkedList<Cell> path = getPath();
        Console.WriteLine($"Path: {string.Join(" ", path)}");
    }

    private static LinkedList<Cell> getPath()
    {
        LinkedList<Cell> recoveredPath = new LinkedList<Cell>();
        recoveredPath.AddFirst(onCell);
        while (true)
        {
            onCell = prevCell[onCell];
            recoveredPath.AddFirst(onCell);

            if (onCell.Row == 0 && onCell.Col == 0)
            {
                break;
            }
        }

        return recoveredPath;
    }

    private static void dijkstra()
    {
        PriorityQueue<Cell> priorityQueue = new PriorityQueue<Cell>();
        priorityQueue.Enqueue(onCell);

        prevCell = new Dictionary<Cell, Cell>();

        while (true)
        {
            Cell cell = priorityQueue.ExtractMin();
            if (cell.Row == matrix.GetLength(0) - 1 && cell.Col == matrix.GetLength(1) - 1)
            {
                onCell = cell;
                return;
            }

            List<Cell> connectedCells = graph[cell];
            foreach (var neighbour in connectedCells)
            {
                if (cell.Value + neighbour.Value < bestPathToCell[neighbour])
                {
                    prevCell.Add(neighbour, cell);
                    int currentPathWeight = cell.Value + neighbour.Value;
                    neighbour.Value = currentPathWeight;
                    bestPathToCell[neighbour] = currentPathWeight;

                    if (priorityQueue.Contains(neighbour))
                    {
                        priorityQueue.DecreaseKey(neighbour);
                    }
                    else
                    {
                        priorityQueue.Enqueue(neighbour);
                    }
                }
            }
        }
    }

    private static void buildGraph(int[,] matrix)
    {
        graph = new Dictionary<Cell, List<Cell>>();
        bestPathToCell = new Dictionary<Cell, int>();

        for (int row = 0; row < matrix.GetLength(0); row++)
        {
            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                Cell cell = new Cell(row, col, matrix[row, col]);
                if (row == 0 && col == 0)
                {
                    onCell = cell;
                }

                List<Cell> neighbours = getConnectedCells(cell);
                graph.Add(cell, neighbours);

                bestPathToCell.Add(cell, INFINITY);
            }
        }
    }

    private static List<Cell> getConnectedCells(Cell cell)
    {
        List<Cell> cells = new List<Cell>();
        // up
        if (isInMatrix(cell.Row - 1, cell.Col))
        {
            cells.Add(new Cell(cell.Row - 1, cell.Col, matrix[cell.Row - 1, cell.Col]));
        }

        // right
        if (isInMatrix(cell.Row, cell.Col + 1))
        {
            cells.Add(new Cell(cell.Row, cell.Col + 1, matrix[cell.Row, cell.Col + 1]));
        }

        // down
        if (isInMatrix(cell.Row + 1, cell.Col))
        {
            cells.Add(new Cell(cell.Row + 1, cell.Col, matrix[cell.Row + 1, cell.Col]));
        }

        // left
        if (isInMatrix(cell.Row, cell.Col - 1))
        {
            cells.Add(new Cell(cell.Row, cell.Col - 1, matrix[cell.Row, cell.Col - 1]));
        }

        return cells;
    }

    private static bool isInMatrix(int row, int col)
    {
        return row >= 0 && row < matrix.GetLength(0) &&
            col >= 0 && col < matrix.GetLength(1);
    }

    private static int[,] readMatrix()
    {
        int rows = int.Parse(Console.ReadLine());
        int cols = int.Parse(Console.ReadLine());
        matrix = new int[rows, cols];
        for (int row = 0; row < rows; row++)
        {
            int[] currentRow = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x))
                .ToArray();

            for (int col = 0; col < cols; col++)
            {
                matrix[row, col] = currentRow[col];
            }
        }

        return matrix;
    }

    private static void printMatrix(int[,] matrix)
    {
        for (int row = 0; row < matrix.GetLength(0); row++)
        {
            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                Console.Write(matrix[row, col] + " ");
            }

            Console.WriteLine();
        }
    }

    private class Cell : IComparable<Cell>
    {
        private int row;

        private int col;

        private int value;

        public Cell(int row, int col, int value)
        {
            this.Row = row;
            this.Col = col;
            this.Value = value;
        }

        public int Row { get; set; }

        public int Col { get; set; }

        public int Value { get; set; }

        public override bool Equals(object obj)
        {
            Cell other = (Cell)obj;
            return this.Row == other.Row && this.Col == other.Col;
        }

        public override int GetHashCode()
        {
            return (this.Row + " " + this.Col).GetHashCode();
        }

        public int CompareTo(Cell other)
        {
            int cmp = this.Value.CompareTo(other.Value);
            if (cmp == 0)
            {
                cmp = this.Row.CompareTo(other.Row);
                if (cmp == 0)
                {
                    return this.Col.CompareTo(other.Col);
                }

                return cmp;
            }

            return cmp;
        }

        public override string ToString()
        {
            return matrix[this.Row, this.Col].ToString();
        }
    }


    public class PriorityQueue<T> where T : IComparable<T>
    {
        private Dictionary<T, int> searchCollection;
        private List<T> heap;

        public PriorityQueue()
        {
            this.heap = new List<T>();
            this.searchCollection = new Dictionary<T, int>();
        }

        public int Count
        {
            get
            {
                return this.heap.Count;
            }
        }

        public bool Contains(T node)
        {
            return this.searchCollection.ContainsKey(node);
        }

        public T ExtractMin()
        {
            var min = this.heap[0];
            var last = this.heap[this.heap.Count - 1];
            this.searchCollection[last] = 0;
            this.heap[0] = last;
            this.heap.RemoveAt(this.heap.Count - 1);
            if (this.heap.Count > 0)
            {
                this.HeapifyDown(0);
            }

            this.searchCollection.Remove(min);

            return min;
        }

        public T PeekMin()
        {
            return this.heap[0];
        }

        public void Enqueue(T element)
        {
            this.searchCollection.Add(element, this.heap.Count);
            this.heap.Add(element);
            this.HeapifyUp(this.heap.Count - 1);
        }

        private void HeapifyDown(int i)
        {
            var left = (2 * i) + 1;
            var right = (2 * i) + 2;
            var smallest = i;

            if (left < this.heap.Count && this.heap[left].CompareTo(this.heap[smallest]) < 0)
            {
                smallest = left;
            }

            if (right < this.heap.Count && this.heap[right].CompareTo(this.heap[smallest]) < 0)
            {
                smallest = right;
            }

            if (smallest != i)
            {
                T old = this.heap[i];
                this.searchCollection[old] = smallest;
                this.searchCollection[this.heap[smallest]] = i;
                this.heap[i] = this.heap[smallest];
                this.heap[smallest] = old;
                this.HeapifyDown(smallest);
            }
        }

        private void HeapifyUp(int i)
        {
            var parent = (i - 1) / 2;
            while (i > 0 && this.heap[i].CompareTo(this.heap[parent]) < 0)
            {
                T old = this.heap[i];
                this.searchCollection[old] = parent;
                this.searchCollection[this.heap[parent]] = i;
                this.heap[i] = this.heap[parent];
                this.heap[parent] = old;

                i = parent;
                parent = (i - 1) / 2;
            }
        }

        public void DecreaseKey(T element)
        {
            int index = this.searchCollection[element];
            this.HeapifyUp(index);
        }
    }
}

