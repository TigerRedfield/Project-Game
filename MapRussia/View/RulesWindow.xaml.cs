using System;
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
using System.Windows.Shapes;

namespace MapRussia.View
{
    /// <summary>
    /// Логика взаимодействия для RulesWindow.xaml
    /// </summary>
    public partial class RulesWindow : Window
    {
        public RulesWindow()
        {
            InitializeComponent();
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonQ_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Игра была разработана при участии следующих студентов 403 группы:\nВасильев Даниил\nЕмельянцев Никита\nЧерник Константин\n\nv.1.00");
        }

        private void ButtonExitMenu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonHidden_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
