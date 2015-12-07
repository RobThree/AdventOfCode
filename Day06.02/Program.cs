using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        var g = new int[1000 * 1000];
        var r = new Regex(@"(toggle|turn on|turn off) (\d+),(\d+) through (\d+),(\d+)", RegexOptions.Compiled);
        foreach (var l in File.ReadAllLines("input.txt"))
        {
            var m = r.Match(l);
            var opcode = m.Groups[1].Value;
            var tl = new Point(int.Parse(m.Groups[2].Value), int.Parse(m.Groups[3].Value));
            var br = new Point(int.Parse(m.Groups[4].Value), int.Parse(m.Groups[5].Value));

            for (int y = tl.Y; y <= br.Y; y++)
            {
                for (int x = tl.X; x <= br.X; x++)
                {
                    var p = x + y * 1000;
                    switch (opcode)
                    {
                        case "toggle":
                            g[p] += 2;
                            break;
                        case "turn on":
                            g[p]++;
                            break;
                        case "turn off":
                            g[p] = g[p] > 0 ? g[p] - 1 : 0;
                            break;
                    }
                }
            }
        }
        Console.WriteLine($"Total brightness is {g.Sum()}");
    }
}
