using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        var r = new Regex(@"^(.*?) to (.*?) = (\d+)$", RegexOptions.Compiled);
        var distances = File.ReadAllLines("input.txt").Select(l => { var m = r.Match(l); return new { F = m.Groups[1].Value, T = m.Groups[2].Value, D = int.Parse(m.Groups[3].Value) }; });
        distances = distances.Union(distances.Select(d => new { F = d.T, T = d.F, D = d.D })).ToArray();
        var matrix = distances.ToDictionary(k => k.F + "☃" + k.T, (x) => x.D);
        var perms = Permutations(distances.Select(p => p.F).Distinct().ToArray()).ToArray();

        Console.WriteLine($@"Longest: {perms.Select(p =>
        {
            var d = 0;
            for (var i = 1; i < p.Length; i++)
            {
                var k = p[i - 1] + "☃" + p[i];
                if (!matrix.ContainsKey(k))
                    return new { p, d = int.MaxValue };
                d += matrix[k];
            }
            return new { p, d };
        }).OrderByDescending(x => x.d).First().d}");
    }

    public static IEnumerable<string[]> Permutations(string[] input)
    {
        if (input.Length == 0) yield break;
        if (input.Length == 1) yield return input;

        foreach (var item in input)
            foreach (var perm in Permutations(input.Where(l => !l.Equals(item)).ToArray()))
                yield return perm.Concat(new[] { item }).ToArray();
    }
}
