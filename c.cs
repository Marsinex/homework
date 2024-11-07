using System.Xml.Serialization;
using System.Collections.Generic;
namespace csharp
{
    [Serializable]
    public struct toy {
        public string name;
        public uint price;
        public uint minAge;
        public uint maxAge;
        public toy(string n,uint p,uint l,uint h)
        {
            name = n;
            price = p;
            minAge = l;
            maxAge = h;
        }
    
    };
    static internal class c
    {
        static private void toggle<T>(List<T> r,T a)
        {
            for(int i = 0; i < r.Count; i++)
                if (r[i].Equals(a))
                {
                    r.RemoveAt(i);
                    return;
                }
            r.Add(a);
        }
        static public List<T> xor<T>(List<T> a,List<T> b)
        {
            List<T> result = new List<T>();
            for (int i=0;i<a.Count; i++)
                toggle<T>(result, a[i]);
            for (int i = 0; i < b.Count; i++)
                toggle<T>(result, b[i]);
            return result;
        }
        static public int sos<T>(LinkedList<T> a) where T : IComparable
        {
            if (a.Count < 3) return 0;
            int c = 0;
            LinkedListNode<T> t = a.First.Next;
            while (t != a.Last)
            {
                if (t.Previous.Value.Equals(t.Next.Value)) c++;
                t = t.Next;
            }
            return c;
        }
        static public HashSet<int> cif(StreamReader f)
        {
            int t;
            HashSet<int> r = new HashSet<int>(); 
            while (f.Peek() != -1)
            {
                t=f.Read()-'0';
                if(t<10&&t>-1)r.Add(t);
            }
            return r;
        }
        static public void createToys()
        {
            List<toy> toys = new List<toy>();
            toys.Add(new toy("dall",15,3,9));
            toys.Add(new toy("ball", 12, 1, 27));
            toys.Add(new toy("soldier", 13, 6, 12));
            toys.Add(new toy("ula", 18, 1, 7));
            XmlSerializer xml = new XmlSerializer(toys.GetType());
            FileStream f = new FileStream("toys.xml", FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
            xml.Serialize(f, toys);
            f.Close();
        }
        static public void t6()
        {
            List<toy> toys=new List<toy>();
            XmlSerializer xml = new XmlSerializer(toys.GetType());
            FileStream f = new FileStream("toys.xml", FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
            toys = (List<toy>)xml.Deserialize(f);
            uint maxPrice = 0;
            int index = 0;
            for (int i = 0; i < toys.Count; i++)
                if (toys[i].minAge <= 2 && toys[i].maxAge >= 3)
                    if (toys[i].price > maxPrice)
                    {
                        maxPrice = toys[i].price;
                        index=i;
                    }
            Console.WriteLine(toys[index].name);
            Console.WriteLine(maxPrice);
            f.Close();
        }
    }
}
