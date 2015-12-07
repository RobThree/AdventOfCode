using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine($@"Result: {File.ReadAllLines("input.txt")
            .Select(l => l.Split('x')
                .Select(i => int.Parse(i)).ToArray()
            )
            .Select(d => new { l = d[0], w = d[1], h = d[2] })
            .Select(d => new { p1 = d.l * d.w, p2 = d.w * d.h, p3 = d.h * d.l })
            .Select(d => d.p1 * 2 + d.p2 * 2 + d.p3 * 2 + Math.Min(Math.Min(d.p1, d.p2), d.p3))
            .Sum()} square feet");
    }
}
