using csharp;
using System.Collections.Generic;
using System.Security.Cryptography;

class Program
{
    public static void Main(string[] args)
    {
        List<int> a = new List<int> { 1, 2, 3, 4, 5};
        List<int> b = new List<int> { 1, 4, 5 };
        List<int> t = c.xor(a, b);
        for (int i = 0; i < t.Count; i++)
            Console.Write(t[i]+" ");
        Console.WriteLine();
        LinkedList<int> l = new LinkedList<int>();
        for(int i=0;i<10;i++)l.AddLast(i);
        l.AddLast(8);
        Console.WriteLine(c.sos(l));
        StreamReader f;
        HashSet<int> ci = new HashSet<int>();
        try
        {
            f = new StreamReader("t4.txt");
            ci=c.cif(f);
        }
        catch
        {
            StreamWriter fi = new StreamWriter("t4.txt");
            fi.WriteLine("1542655");
            fi.Close();
            Console.WriteLine("oops t4, new file...");
            return;
        }
        Console.WriteLine(string.Join(",", ci));
        c.createToys();
        c.t6();
    }
}