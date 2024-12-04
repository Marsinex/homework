using System.IO;
using System;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Principal;
using LinqToExcel.Extensions;
using System.Security.Cryptography;
using System.ComponentModel;

namespace WPFapp1
{
    public partial class MainWindow : Window
    {
        basa b;
        public MainWindow()
        {
            MessageBoxResult r= MessageBox.Show("Писать логи в новый файл?","Логи", MessageBoxButton.YesNo, MessageBoxImage.Question);
            b = new basa(r==MessageBoxResult.Yes);
            InitializeComponent();
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            display.Text = "";
            switch (choise.SelectedIndex)
            {
                case 0:
                    display.Text=b.printDict(b.cur);
                    l1.Content = "ID";
                    l2.Content = "Обозначение";
                    l3.Content = "Курс";
                    l4.Content = "Название";
                    l5.Content = "";
                    break;
                case 1:
                    display.Text = b.printDict(b.inc);
                    l1.Content = "ID";
                    l2.Content = "ID счёта";
                    l3.Content = "ID валюты";
                    l4.Content = "Дата";
                    l5.Content = "Сумма";
                    break;
                case 2:
                    display.Text = b.printDict(b.acc);
                    l1.Content = "ID";
                    l2.Content = "Имя";
                    l3.Content = "Дата";
                    l4.Content = "";
                    l5.Content = "";
                    break;
            }
        }
        private void wannaDo(object sender, SelectionChangedEventArgs e)
        {
            switch (delat.SelectedIndex)
            {
                case 0://добавить
                   
                    l1.Visibility = Visibility.Hidden;
                    l2.Visibility = Visibility.Visible;
                    l3.Visibility = Visibility.Visible;
                    l4.Visibility = Visibility.Visible;
                    l5.Visibility = Visibility.Visible;
                    break;
                case 1://удалить
                   
                    l1.Visibility = Visibility.Visible;
                    l2.Visibility = Visibility.Hidden;
                    l3.Visibility = Visibility.Hidden;
                    l4.Visibility = Visibility.Hidden;
                    l5.Visibility = Visibility.Hidden;
                    break;
                case 2://редактировать
                   
                    l1.Visibility = Visibility.Visible;
                    l2.Visibility = Visibility.Visible;
                    l3.Visibility = Visibility.Visible;
                    l4.Visibility = Visibility.Visible;
                    l5.Visibility = Visibility.Visible;
                    break;
            }
        }
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
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            b.save();
        }
        private void t1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(t1.Text,out int i)) t1.Text = "0";
        }
    }
}
