using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

class Program
{
    private static Dictionary<string, ushort> values = new Dictionary<string, ushort>();
    private static Dictionary<string, Func<ushort>> expr = new Dictionary<string, Func<ushort>>();
    // Functionally same as in part 1 decoder; however this time we don't use a switch case but have more regexes
    private static Dictionary<Regex, Action<Match>> decoder = new Dictionary<Regex, Action<Match>>
    {
        [new Regex(@"^(\w+) -> (\w+)$", RegexOptions.Compiled)] = (m) => { expr[m.Groups[2].Value] = () => EvalValue(m.Groups[1].Value); },                                                         // Assignment x -> y
        [new Regex(@"^NOT (\w+) -> (\w+)$", RegexOptions.Compiled)] = (m) => { expr[m.Groups[2].Value] = () => (ushort)~EvalValue(m.Groups[1].Value); },                                            // NOT x -> y
        [new Regex(@"^(\w+) OR (\w+) -> (\w+)$", RegexOptions.Compiled)] = (m) => { expr[m.Groups[3].Value] = () => (ushort)(EvalValue(m.Groups[1].Value) | EvalValue(m.Groups[2].Value)); },       // x OR y -> z
        [new Regex(@"^(\w+) AND (\w+) -> (\w+)$", RegexOptions.Compiled)] = (m) => { expr[m.Groups[3].Value] = () => (ushort)(EvalValue(m.Groups[1].Value) & EvalValue(m.Groups[2].Value)); },      // x AND y -> z
        [new Regex(@"^(\w+) LSHIFT (\w+) -> (\w+)$", RegexOptions.Compiled)] = (m) => { expr[m.Groups[3].Value] = () => (ushort)(EvalValue(m.Groups[1].Value) << EvalValue(m.Groups[2].Value)); },  // x LSHIFT y -> z
        [new Regex(@"^(\w+) RSHIFT (\w+) -> (\w+)$", RegexOptions.Compiled)] = (m) => { expr[m.Groups[3].Value] = () => (ushort)(EvalValue(m.Groups[1].Value) >> EvalValue(m.Groups[2].Value)); },  // x RSHIFT y -> z
    };

    static void Main(string[] args)
    {
        Match m = null;
        foreach (var l in File.ReadAllLines("input.txt"))
            decoder.First(kv => { m = kv.Key.Match(l); return m.Success; }).Value(m);   // Find first function that matches syntax and execute it

        var result = expr["a"]();
        values.Clear();
        values["b"] = result;
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