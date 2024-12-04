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
    struct accaunt
    {
        string name;
        string date;
        public accaunt(string n, string t)
        {
            name = n;
            date = t;
        }
        override public string ToString()
        {
            return name + " " + date;
        }

    }
    struct curs
    {
        string letter;
        float ko;
        string name;
        public curs(string l, float k, string n)
        {
            letter = l;
            ko = k;
            name = n;
        }
        override public string ToString()
        {
            return letter + " " + ko + " " + name;
        }
    }
    struct income
    {
        int aid;
        int cid;
        string date;
        float sum;
        public income(int aid, int cid, string date, float sum)
        {
            this.aid = aid;
            this.cid = cid;
            this.date = date;
            this.sum = sum;
        }
        override public string ToString()
        {
            return aid + " " + cid + " " + date + " " + sum;
        }
    }
    internal class basa
    {
        public Dictionary<int, accaunt> acc;
        public Dictionary<int, curs> cur;
        public Dictionary<int, income> inc;
        StreamWriter log;
        public basa(bool logNew)
        {
            Console.WriteLine(DateTime.Now.ToString());
            if (logNew) log = new StreamWriter("log" + DateTime.Now.ToString().Replace(':','-').Replace('.','-') + ".txt");
            else log = new StreamWriter("log.txt",true);
            LinqToExcel.ExcelQueryFactory excelFile=null;
            try { 
                excelFile = new LinqToExcel.ExcelQueryFactory(@"LR6-var13.xls");
                toLog("Открыта БД");
            }
            catch{
                toLog("Не найдена база данных");
                return;
            }
            var a = from shet in excelFile.Worksheet(0) select shet;
            cur = a.ToDictionary(row => int.Parse(row[0].ToString()), row => new curs(row[1], float.Parse(row[2]), row[3]));
            a = from shet in excelFile.Worksheet(1) select shet;
            inc = a.ToDictionary(row => int.Parse(row[0].ToString()), row => new income(int.Parse(row[1]), int.Parse(row[2]), row[3], float.Parse(row[4])));
            a = from shet in excelFile.Worksheet(2) select shet;
            acc = a.ToDictionary(row => int.Parse(row[0].ToString()), row => new accaunt(row[1], row[2]));
        }
        private void toLog(string l)
        {
            log.Write(DateTime.Now+" "+l+"\n");
        }
        public void save()
        {
            toLog("Закрытие программы");
            log.Close();
        }
        public string printDict<T>(Dictionary<int, T> d) where T : struct
        {
            string o = "";
            if (d.Count < 50)
                foreach (var i in d) o += i.Key + " " + i.Value + "\n";
            else
            {
                int c = 0;
                foreach (var i in d)
                {
                    if (c++ < 50) o += i.Key + " " + i.Value + "\n";
                    else return o;
                }
            }
            return o;
        }
        public void Dosmth(int c,int d,int t1,string t2,string t3,string t4,string t5)
        {
            switch (d)
            {
                case 0:
                    switch (c)
                    {
                        case 0:
                            cur[cur.Count] = new curs(t2, float.Parse(t3), t4);
                            toLog("В таблицу 'Курсы валют' добавлена запись:"+cur[cur.Count-1].ToString());
                            break;
                        case 1:
                            inc[inc.Count] = new income(int.Parse(t2),int.Parse(t3),t4,float.Parse(t5));
                            toLog("В таблицу 'Поступления' добавлена запись:" + inc[inc.Count-1].ToString());
                            break;
                        case 2:
                            acc[acc.Count] = new accaunt(t2, t3);
                            toLog("В таблицу 'Счета' добавлена запись:" + acc[acc.Count-1].ToString());
                            break;
                    }
                    break;
                case 1:
                    switch (c)
                    {
                        case 0:
                            toLog("Из таблицы 'Курсы валют' удалена запись:" + cur[t1].ToString());
                            cur.Remove(t1);
                            break;
                        case 1:
                            toLog("Из таблицы 'Поступления' удалена запись:" + inc[t1].ToString());
                            inc.Remove(t1);
                            break;
                        case 2:
                            toLog("Из таблицы 'Счета' удалена запись:" + acc[t1].ToString());
                            acc.Remove(t1);
                            break;
                    }
                    break;
                case 2:
                    switch (c)
                    {
                        case 0:
                            toLog("В таблице 'Курсы валют' изменена запись:" + cur[t1].ToString());
                            cur[t1] = new curs(t2, float.Parse(t3), t4);
                            break;
                        case 1:
                            toLog("В таблице 'Поступления' изменена запись:" + inc[t1].ToString());
                            inc[t1] = new income(int.Parse(t2), int.Parse(t3), t4, float.Parse(t5));
                            break;
                        case 2:
                            toLog("В таблице 'Счета' изменена запись:" + acc[t1].ToString());
                            acc[t1] = new accaunt(t2, t3);
                            break;
                    }
                    break;
            }

        }
    }
}
