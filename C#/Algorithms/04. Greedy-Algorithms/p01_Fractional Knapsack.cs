using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        double capacity = double.Parse(Console.ReadLine().Substring(10));
        int itemsCount = int.Parse(Console.ReadLine().Substring(7));

        List<Item> items = new List<Item>();

        for (int i = 0; i < itemsCount; i++)
        {
            double[] itemTokens = Console.ReadLine()
                .Split(new string[] { " -> " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(e => double.Parse(e))
                .ToArray();
            double price = itemTokens[0];
            double weight = itemTokens[1];

            Item item = new Item(price, weight);
            items.Add(item);
        }

        items.Sort();
        List<Item> takenItems = new List<Item>();
        foreach (var item in items)
        {
            if (item.weight < capacity)
            {
                item.percentageTaken = 100.0;
                takenItems.Add(item);
                capacity -= item.weight;
            }
            else
            {
                // capacity / item.weight * 100;
                item.percentageTaken = (capacity / item.weight) * 100;
                takenItems.Add(item);
                break;
            }
        }

        double totalPrice = 0;
        foreach (var item in takenItems)
        {
            Console.WriteLine(item);
            item.price = (item.price * item.percentageTaken) / 100;
            totalPrice += item.price;
        }

        Console.WriteLine($"Total price: {totalPrice:f2}");
    }

    class Item : IComparable<Item>
    {
        public double price;
        public double weight;

        double value;
        public double percentageTaken;

        public Item(double price, double weight)
        {
            this.price = price;
            this.weight = weight;

            this.value = this.price / this.weight;
        }

        public int CompareTo(Item other)
        {
            return other.value.CompareTo(this.value);
        }

        public override string ToString()
        {
            string percentageToPrint = this.percentageTaken == 100 ? "100" : String.Format($"{this.percentageTaken:f2}");
            return ($"Take {percentageToPrint}% of item with price {this.price:f2} and weight {this.weight:f2}");
            // return this.value.ToString();
            // return base.ToString();
        }
    }
}

