using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        var f = 0;
        Console.WriteLine($@"Floor: {File.ReadAllText("input.txt")
            .Select((c, i) => { f += c == '(' ? 1 : -1; return new { f, i }; })
            .First(o => o.f == -1).i + 1}");
    }
}

