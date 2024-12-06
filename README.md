# Задание 1
## Постановка задачи
Связать третью лабораторную с WPF.
## Алгоритм решения
1. Создаём WPF окно.
2. Добавляем в него нужные нам элементы интерфейса.
3. Создаём нужные методы для взаимодействия пользователя с программой.
4. Пишем класс для хранения и обработки данных о счёте денег.
5. Инициализируем его в главном классе.
## Тестирование.
![image](https://github.com/user-attachments/assets/29098098-bb1f-4add-a504-946baf6b1365)

## Код.
Главный класс
```c#
public partial class MainWindow : Window
{
    basa a;
    uint cur=100;
    public MainWindow()
    {
        a = new basa(123456);
        InitializeComponent();
        updateLabel();
    }
    private void updateLabel()
    {
        mony.Content = a/100+" руб. "+a%100+" коп.";
    }
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        a--;
        updateLabel();
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        a++;
        updateLabel();
    }
    private void Button_Click_2(object sender, RoutedEventArgs e)
    {
        a += cur;
        updateLabel();
    }
    private void Button_Click_3(object sender, RoutedEventArgs e)
    {
        a -= cur;
        updateLabel();
    }
    private void some_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            cur=uint.Parse(some.Text);
        }
        catch
        {
            MessageBox.Show("В поле денежных оборотов можно записывать только целые положительные числа обозначающие копейки","Error#pishikopeyki",MessageBoxButton.OK,MessageBoxImage.Error);
            some.Text = "100";
        }
    }
}
```
Класс денежного счёта.
```C#
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
```
