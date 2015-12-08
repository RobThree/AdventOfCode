using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        var r = new Regex(@"\\x[0-9a-f]{2}|\\""|\\\\");
        Console.WriteLine($"Length: {File.ReadAllLines("input.txt").Sum(l => l.Length - (r.Replace(l, "☃").Length - 2))}");
    }
}
