using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        var s = "hepxcrrq";
        while (!IsValid(s))
            s = NextPass(s);
        do
        {
            s = NextPass(s);
        } while (!IsValid(s));
        Console.WriteLine($"Next next password: {s}");
    }

    static string NextPass(string v)
    {
        var c = v.ToCharArray();
        var i = c.Length - 1;
        while (i >= 0)
        {
            if (c[i] == 'z')
            {
                c[i--] = 'a';
            }
            else
            {
                c[i]++;
                break;
            }
        }
        return new string(c);
    }

    static bool IsValid(string v)
    {
        if ("iol".Any(c => v.IndexOf(c) > 0))
            return false;

        bool hasstraight = false;
        for (int i = 0; !hasstraight && i < v.Length - 2; i++)
            hasstraight |= (v[i] + 1 == v[i + 1] && v[i] + 2 == v[i + 2]);

        int sequencecount = 0, prevdupe = -2;
        for (int i = 0; sequencecount < 2 && i < v.Length - 1; i++)
        {
            if (v[i] == v[i + 1] && (prevdupe != i - 1))
            {
                sequencecount++;
                prevdupe = i;
            }
        }

        return hasstraight && sequencecount > 1;
    }
}