using System.Xml.Serialization;
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
        static private void toggle(List<int> r,int a)
        {
            for(int i = 0; i < r.Count; i++)
                if (r[i] == a)
                {
                    r.RemoveAt(i);
                    return;
                }
            r.Add(a);
        }
        static public List<int> xor(List<int> a,List<int> b)
        {
            List<int> result = new List<int>();
            for (int i=0;i<a.Count; i++)
                toggle(result, a[i]);
            for (int i = 0; i < b.Count; i++)
                toggle(result, b[i]);
            return result;
        }
        static public int sos(LinkedList<int> a)
        {
            if (a.Count < 3) return 0;
            int c = 0;
            LinkedListNode<int> t = a.First.Next;
            while (t != a.Last)
            {
                if (t.Previous.Value == t.Next.Value) c++;
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
            for(int i = 0; i < toys.Count; i++)
                if (toys[i].minAge <= 2 && toys[i].maxAge>=3)
                    if (toys[i].price > maxPrice) maxPrice = toys[i].price;
            for (int i = 0; i < toys.Count; i++)
                if (toys[i].minAge <= 2 && toys[i].maxAge >= 3)
                    if (toys[i].price == maxPrice) Console.WriteLine(toys[i].name);
            Console.WriteLine(maxPrice);
            f.Close();
        }
    }
}
