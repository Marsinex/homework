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
}
