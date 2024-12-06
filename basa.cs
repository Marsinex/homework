using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WPFapp1
{
    internal class basa
    {
        private uint money;
        public basa(uint kopeek)
        {
           money= kopeek;
        }
        public static basa operator +(basa b,uint a)
        {
            return new basa(a + b);
        }
        public static basa operator -(basa b, uint a)
        {
            if (b.money <a) return b;
            return new basa(b.money-a);
        }
        public static basa operator ++(basa b)
        {
            b.money++;
            return b;
        }
        public static basa operator --(basa b)
        {
            if(!(bool)b)return b;
            b.money--;
            return b;
        }
        public static explicit operator bool(basa b)
        {
            return b.money > 0;
        }
        public static implicit operator uint(basa b)
        {
            return b.money;
        }
    }
}
