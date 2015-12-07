using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine($@"{File.ReadAllLines("input.txt").Count(l =>
            l.Count(c => "aeiou".IndexOf(c) >= 0) > 2
                && l.Skip(1).Where((c, i) => c == l[i]).Any()
                && !(new string[] { "ab", "cd", "pq", "xy" }).Any(n => l.IndexOf(n) >= 0)
        )} strings");
    }
}