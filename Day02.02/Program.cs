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
            .Select(d => d.OrderBy(i => i).Take(2).Sum() * 2 + d[0] * d[1] * d[2])
            .Sum()} feet");
    }
}
