# Задание 1
## Постановка задачи
Разработать консольное приложение с дружественным интерфейсом с возможностью выбора
заданий. Приложение должно выполнять следующие функции:
1. Чтение базы данных из excel файла.
2. Просмотр базы данных.
3. Удаление элементов (по ключу).
4. Корректировка элементов (по ключу).
5. Добавление элементов.
6. Реализация 4 запросов (формулировки запросов придумать самостоятельно и отразить в
отчёте, можно использовать запрос, данный в примере):
1. 1 запрос с обращением к одной таблице
2. 1 запрос с обращением к двум таблицам
3. 2 запроса с обращением к трем таблицам
2 запроса должны возвращать перечень, 2 запроса одно значение.
7. Во время всего сеанса работы ведется полное протоколирование действий в текстовом
файле (в начале сеанса запросить, будет ли это новый файл или дописывать в уже
существующий). 
___
Элементами базы данных являются объекты классов согласно вашему варианту. Содержание классов
определить самостоятельно и отразить в отчете (в классах должны присутствовать свойства,
конструкторы, перегруженный метод ToString). Весь функционал приложения реализовать в виде
методов вспомогательного класса с помощью LINQ-запросов.
Предусмотреть обработку возможных ошибок при работе программы.

## Алгоритм решения
1. Создаём WPF окно.
2. Добавляем в него нужные нам элементы интерфейса.
3. Создаём нужные методы для взаимодействия пользователя с программой.
4. Инициализируем "ядро" для работы с БД в виде excel.
5. Создаём сам класс для работы с БД.
6. Создаём структуры для хранения данных из БД.
7. Подгружаем БД в словари.
8. Создаём функцию вывода первых 50 строчек из БД.
9. Пишем функцию для удаления/добавления/корректирования таблицы.
10. Создаём функцию для записи логов, вызываем при любом действии.
11. Не забываем сохранить логи после закрытия программы.
## Тестирование.
![image](https://github.com/user-attachments/assets/8c0d5d61-8972-4ab1-b64a-ef3f0a2c1a85)
![image](https://github.com/user-attachments/assets/f39fc8d8-654b-4c57-92da-52c5a8bbb0e4)
![image](https://github.com/user-attachments/assets/f38d4992-b3c6-4525-b37a-002a856bdc84)
![image](https://github.com/user-attachments/assets/4313cb8b-5c51-4e29-9bd3-c4e3a1e4f105)

## Код.
Ниже фрагменты кода, весь код в файлах.
___
Куда записывать логи?
```c#
basa b;
public MainWindow()
{
    MessageBoxResult r= MessageBox.Show("Писать логи в новый файл?","Логи", MessageBoxButton.YesNo, MessageBoxImage.Question);
    b = new basa(r==MessageBoxResult.Yes);
    InitializeComponent();
}
```
Операции над таблицами.
```C#
private void Button_Click(object sender, RoutedEventArgs e)
{
    try
    {
        b.Dosmth(choise.SelectedIndex, delat.SelectedIndex, int.Parse(t1.Text), t2.Text, t3.Text, t4.Text, t5.Text);
    }
    catch
    {
        MessageBox.Show("Чтото ты напутал с вводом, проверь на всякий", "Ошиблися", MessageBoxButton.OK, MessageBoxImage.Warning);
    }
    ComboBox_SelectionChanged(null, null);//обновляю выведенную таблицу

}
```
Сохранение логов и индекс обязанный быть числом.
```C#
private void Window_Closing(object sender, CancelEventArgs e)
{
    b.save();
}
private void t1_TextChanged(object sender, TextChangedEventArgs e)
{
    if (!int.TryParse(t1.Text,out int i)) t1.Text = "0";
}
```
Структуры строк таблиц.
```c#
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
```
Чтение БД.
```c#
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
```
Вывод 50 элементов выбранной таблицы.
```c#
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
```
