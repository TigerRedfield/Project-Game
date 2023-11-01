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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MapRussia
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            //Предупреждение о том, что пользователь выйдет из приложения
            if (MessageBox.Show("Вы точно хотите закрыть приложение?", "Внимание!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown(); //Полное закрытие приложения
            }
        }

        private void ButtonRules_Click(object sender, RoutedEventArgs e)
        {
            View.RulesWindow rulesWindow = new View.RulesWindow();
            this.Hide();
            rulesWindow.ShowDialog();
            this.Show();
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            View.SettingsGameWindow settingsGame = new View.SettingsGameWindow();
            this.Hide();
            settingsGame.ShowDialog();
            this.Show();

        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ButtonUnwrap_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;

            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }

        private void ButtonHidden_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
