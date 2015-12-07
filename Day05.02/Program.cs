using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine($@"{File.ReadAllLines("input.txt").Where(
            l => l.ToPairs()
                .Select((p, i) => new { p, i })
                .GroupBy(p => p.p)
                .Where(g => g.OrderBy(p => p.i).Last().i - g.OrderBy(p => p.i).First().i > 1)
                .Any()
        ).Where(l=> l.Skip(2).Select((c, i) => new { c, i }).Any(v => v.c == l[v.i])).Count()} strings");
    }
}

static class Helpers
{
    public static IEnumerable<string> ToPairs(this string c)
    {
        var p = c[0];
        foreach (var i in c.Skip(1))
        {
            yield return p.ToString() + i.ToString();
            p = i;
        }
    }
}