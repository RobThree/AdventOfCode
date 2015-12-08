using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

class Program
{
    private static Dictionary<string, ushort> values = new Dictionary<string, ushort>();
    private static Dictionary<string, Func<ushort>> expr = new Dictionary<string, Func<ushort>>();
    // Little more readable decoder than in part 2
    private static Dictionary<Regex, Action<Match>> decoder = new Dictionary<Regex, Action<Match>>
    {

        [new Regex(@"^(\w+) -> (\w+)$", RegexOptions.Compiled)] = (m) => { expr[m.Groups[2].Value] = () => EvalValue(m.Groups[1].Value); },                 // Assignment x -> y
        [new Regex(@"^NOT (\w+) -> (\w+)$", RegexOptions.Compiled)] = (m) => { expr[m.Groups[2].Value] = () => (ushort)~EvalValue(m.Groups[1].Value); },    // NOT x -> y
        [new Regex(@"^(\w+) (AND|OR|LSHIFT|RSHIFT) (\w+) -> (\w+)$", RegexOptions.Compiled)] = (m) =>                                                       // Expression x OPERATOR y -> z
        {
            switch (m.Groups[2].Value)
            {
                case "AND": expr[m.Groups[4].Value] = () => (ushort)(EvalValue(m.Groups[1].Value) & EvalValue(m.Groups[3].Value)); break;
                case "OR": expr[m.Groups[4].Value] = () => (ushort)(EvalValue(m.Groups[1].Value) | EvalValue(m.Groups[3].Value)); break;
                case "LSHIFT": expr[m.Groups[4].Value] = () => (ushort)(EvalValue(m.Groups[1].Value) << EvalValue(m.Groups[3].Value)); break;
                case "RSHIFT": expr[m.Groups[4].Value] = () => (ushort)(EvalValue(m.Groups[1].Value) >> EvalValue(m.Groups[3].Value)); break;
            }
        }
    };

    static void Main(string[] args)
    {
        Match m = null;
        foreach (var l in File.ReadAllLines("input.txt"))
            decoder.First(kv => { m = kv.Key.Match(l); return m.Success; }).Value(m);   // Find first function that matches syntax and execute it
        Console.WriteLine($"Value: {expr["a"]()}");
    }

    public static ushort EvalValue(string value)
    {
        ushort tmp;
        if (ushort.TryParse(value, out tmp))
            return tmp;

        if (!values.ContainsKey(value))
            values[value] = expr[value]();
        return values[value];
    }
}
