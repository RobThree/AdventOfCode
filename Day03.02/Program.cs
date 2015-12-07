using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        var positions = new ConcurrentDictionary<Point, int>();
        var current = new[] { new Point(0, 0), new Point(0, 0) };
        var offset = new Point(0, 0);
        var i = 0;
        positions.TryAdd(offset, 1);
        foreach (var c in File.ReadAllText("input.txt"))
        {
            switch (c)
            {
                case '>': current[i % 2].Offset(1, 0); break;
                case '<': current[i % 2].Offset(-1, 0); break;
                case '^': current[i % 2].Offset(0, 1); break;
                case 'v': current[i % 2].Offset(0, -1); break;
            }
            positions.AddOrUpdate(current[i++ % 2], 1, (p, v) => ++v);
        }
        Console.WriteLine($"{positions.Count} houses");
    }
}
