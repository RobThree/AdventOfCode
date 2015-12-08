using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        var r = new Regex(@"(\\|"")");
        Console.WriteLine($"Length: {File.ReadAllLines("input.txt").Sum(l => 2 + (r.Replace(l, "\\$1").Length - l.Length))}");
    }
}