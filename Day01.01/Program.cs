using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine($"Floor: {File.ReadAllText("input.txt").Sum(c => c == '(' ? 1 : -1)}");
    }
}