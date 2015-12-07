using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        var positions = new ConcurrentDictionary<Point, int>();
        var current = new Point(0, 0);
        positions.TryAdd(current, 1);
        foreach (var c in File.ReadAllText("input.txt"))
        {
            switch (c)
            {
                case '>': current.Offset(1, 0); break;
                case '<': current.Offset(-1, 0); break;
                case '^': current.Offset(0, 1); break;
                case 'v': current.Offset(0, -1); break;
            }
            positions.AddOrUpdate(current, 1, (p, v) => ++v);
        }
        Console.WriteLine($"{positions.Count} houses");
    }
}
