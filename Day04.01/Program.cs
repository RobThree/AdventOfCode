using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        //TODO: Use all cores for speedup...
        var h = MD5.Create();
        var input = Encoding.ASCII.GetBytes("yzbqklnj");
        var i = 0;
        while (true)
        {
            var r = h.ComputeHash(input.Concat(Encoding.ASCII.GetBytes(i++.ToString())).ToArray());
            if (r[0] == 0 && r[1] == 0 && (r[2] & 0xF0) == 0)
                break;
        }
        Console.WriteLine(--i);
    }
}
