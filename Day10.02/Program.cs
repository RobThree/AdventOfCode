using System;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        var s = "1113122113";
        for (var i = 0; i < 50; i++)
            s = LookAndSay(s);
        Console.WriteLine(s.Length);
    }

    private static string LookAndSay(string v)
    {
        var s = new StringBuilder();
        for (int i = 0, c = 0; i < v.Length;)
        {
            while (i + c < v.Length && v[i] == v[i + c])
                c++;
            s.Append(c).Append(v[i]);
            i += c;
            c = 0;
        }
        return s.ToString();
    }
}
