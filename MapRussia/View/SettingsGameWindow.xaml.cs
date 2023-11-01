using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Логика взаимодействия для SettingsGameWindow.xaml
    /// </summary>
    public partial class SettingsGameWindow : Window
    {
        public SettingsGameWindow()
        {
            InitializeComponent();
            ButtonStart.IsEnabled = false;
            ButtonStartGeo.IsEnabled = false;
            ButtonStartHistory.IsEnabled = false;
            ButtonStartFacts.IsEnabled = false;
            ButtonStartFactsGeo.IsEnabled = false;
            ButtonStartGeoHistory.IsEnabled = false;
            ButtonStartHistoryFacts.IsEnabled = false;
            TextBoxPlayer1.IsEnabled = false;
            TextBoxPlayer2.IsEnabled = false;
            TextBoxPlayer3.IsEnabled = false;
            TextBoxPlayer4.IsEnabled = false;

            ButtonStartFactsGeo.Foreground = Brushes.Black;
            ButtonStart.Foreground = Brushes.Black;
            ButtonStartGeo.Foreground = Brushes.Black;
            ButtonStartHistory.Foreground = Brushes.Black;
            ButtonStartFacts.Foreground = Brushes.Black;
            ButtonStartGeoHistory.Foreground = Brushes.Black;
            ButtonStartHistoryFacts.Foreground = Brushes.Black;
        }


        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            View.GameWindow gameWindow = new View.GameWindow();
            gameWindow.CreatePlayers(CheckPlayer1.IsChecked == true, TextBoxPlayer1.Text, CheckPlayer2.IsChecked == true, TextBoxPlayer2.Text, CheckPlayer3.IsChecked == true, TextBoxPlayer3.Text, CheckPlayer4.IsChecked == true, TextBoxPlayer4.Text);
            this.Hide();
            gameWindow.ShowDialog();
            this.Show();
        }


        private void CheckedPlayers()
        {
            int checked_players = 0; // количество отмеченных игроков
            if (CheckPlayer1.IsChecked == true) checked_players++;
            if (CheckPlayer2.IsChecked == true) checked_players++;
            if (CheckPlayer3.IsChecked == true) checked_players++;
            if (CheckPlayer4.IsChecked == true) checked_players++;

            if (checked_players >= 2) // если отмечено достаточное количество
            {
                ButtonStart.IsEnabled = true; // кнопка доступна
                ButtonStartGeo.IsEnabled = true;
                ButtonStartHistory.IsEnabled = true;
                ButtonStartFacts.IsEnabled = true;
                ButtonStartFactsGeo.IsEnabled = true;
                ButtonStartGeoHistory.IsEnabled = true;
                ButtonStartHistoryFacts.IsEnabled = true;

                ButtonStart.Foreground = Brushes.Gold;
                ButtonStartGeo.Foreground = Brushes.Gold;
                ButtonStartHistory.Foreground = Brushes.Gold;
                ButtonStartFacts.Foreground = Brushes.Gold;
                ButtonStartGeoHistory.Foreground = Brushes.Gold;
                ButtonStartHistoryFacts.Foreground = Brushes.Gold;
                ButtonStartFactsGeo.Foreground = Brushes.Gold;


            }
            else
            {
                ButtonStart.IsEnabled = false; // кнопка недоступна
                ButtonStartGeo.IsEnabled = false;
                ButtonStartHistory.IsEnabled = false;
                ButtonStartFacts.IsEnabled = false;
                ButtonStartFactsGeo.IsEnabled = false;
                ButtonStartGeoHistory.IsEnabled = false;
                ButtonStartHistoryFacts.IsEnabled = false;

                ButtonStart.Foreground = Brushes.Black;
                ButtonStartGeo.Foreground = Brushes.Black;
                ButtonStartHistory.Foreground = Brushes.Black;
                ButtonStartFacts.Foreground = Brushes.Black;
                ButtonStartGeoHistory.Foreground = Brushes.Black;
                ButtonStartHistoryFacts.Foreground = Brushes.Black;
                ButtonStartFactsGeo.Foreground = Brushes.Black;


            }
        }

        private void CheckPlayer1_Checked(object sender, RoutedEventArgs e)
        {
                TextBoxPlayer1.IsEnabled = true;
                Uri resource = new Uri("/Resources/player_1_active.png", UriKind.Relative);
                img1.Source = new BitmapImage(resource); // меняем иконку игрока на более яркую
                CheckedPlayers();

        }


        private void CheckPlayer2_Checked(object sender, RoutedEventArgs e)
        {

            TextBoxPlayer2.IsEnabled = true;
            Uri resource = new Uri("/Resources/player_2_active.png", UriKind.Relative);
            img2.Source = new BitmapImage(resource); // меняем иконку игрока на более яркую
            CheckedPlayers();
        }

        private void CheckPlayer3_Checked(object sender, RoutedEventArgs e)
        {
            TextBoxPlayer3.IsEnabled = true;
            Uri resource = new Uri("/Resources/player_3_active.png", UriKind.Relative);
            img3.Source = new BitmapImage(resource); // меняем иконку игрока на более яркую
            CheckedPlayers();
        }

        private void CheckPlayer4_Checked(object sender, RoutedEventArgs e)
        {

            TextBoxPlayer4.IsEnabled = true;
            Uri resource = new Uri("/Resources/player_4_active.png", UriKind.Relative);
            img4.Source = new BitmapImage(resource); // меняем иконку игрока на более яркую
            CheckedPlayers();
        }

        private void CheckPlayer1_Unchecked(object sender, RoutedEventArgs e)
        {
            TextBoxPlayer1.IsEnabled = false;
            Uri resource = new Uri("/Resources/player_1_not_active.png", UriKind.Relative);
            img1.Source = new BitmapImage(resource); // меняем иконку игрока на менее яркую
            CheckedPlayers(); // проверяем отмеченных игроков и в зависимости от этого блокируем/разблокируем кнопку старта игры

        }

        private void CheckPlayer2_Unchecked(object sender, RoutedEventArgs e)
        {
            TextBoxPlayer2.IsEnabled = false;
            Uri resource = new Uri("/Resources/player_2_not_active.png", UriKind.Relative);
            img2.Source = new BitmapImage(resource); // меняем иконку игрока на менее яркую
            CheckedPlayers(); // проверяем отмеченных игроков и в зависимости от этого блокируем/разблокируем кнопку старта игры

        }

        private void CheckPlayer3_Unchecked(object sender, RoutedEventArgs e)
        {

            TextBoxPlayer3.IsEnabled = false;
            Uri resource = new Uri("/Resources/player_3_not_active.png", UriKind.Relative);
            img3.Source = new BitmapImage(resource); // меняем иконку игрока на менее яркую
            CheckedPlayers(); // проверяем отмеченных игроков и в зависимости от этого блокируем/разблокируем кнопку старта игры

        }

        private void CheckPlayer4_Unchecked(object sender, RoutedEventArgs e)
        {

            TextBoxPlayer4.IsEnabled = false;
            Uri resource = new Uri("/Resources/player_4_not_active.png", UriKind.Relative);
            img4.Source = new BitmapImage(resource); // меняем иконку игрока на менее яркую
            CheckedPlayers(); // проверяем отмеченных игроков и в зависимости от этого блокируем/разблокируем кнопку старта игры

        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            
            this.Close();
        }

        private void ButtonStartGeo_Click(object sender, RoutedEventArgs e)
        {
            View.GameWindowGeo gameWindowGeo = new View.GameWindowGeo();
            gameWindowGeo.CreatePlayers(CheckPlayer1.IsChecked == true, TextBoxPlayer1.Text, CheckPlayer2.IsChecked == true, TextBoxPlayer2.Text, CheckPlayer3.IsChecked == true, TextBoxPlayer3.Text, CheckPlayer4.IsChecked == true, TextBoxPlayer4.Text);
            this.Hide();
            gameWindowGeo.ShowDialog();
            this.Show();
        }

        private void ButtonStartFacts_Click(object sender, RoutedEventArgs e)
        {
            View.GameWindowFacts gameWindowFacts = new View.GameWindowFacts();
            gameWindowFacts.CreatePlayers(CheckPlayer1.IsChecked == true, TextBoxPlayer1.Text, CheckPlayer2.IsChecked == true, TextBoxPlayer2.Text, CheckPlayer3.IsChecked == true, TextBoxPlayer3.Text, CheckPlayer4.IsChecked == true, TextBoxPlayer4.Text);
            this.Hide();
            gameWindowFacts.ShowDialog();
            this.Show();
        }

        private void ButtonStartHistory_Click(object sender, RoutedEventArgs e)
        {
            View.GameWindowHistory gameWindowHistory = new View.GameWindowHistory();
            gameWindowHistory.CreatePlayers(CheckPlayer1.IsChecked == true, TextBoxPlayer1.Text, CheckPlayer2.IsChecked == true, TextBoxPlayer2.Text, CheckPlayer3.IsChecked == true, TextBoxPlayer3.Text, CheckPlayer4.IsChecked == true, TextBoxPlayer4.Text);
            this.Hide();
            gameWindowHistory.ShowDialog();
            this.Show();
        }

        private void ButtonStartGeoHistory_Click(object sender, RoutedEventArgs e)
        {
            View.GameWindowGeoHistory gameWindowGeoHistory = new View.GameWindowGeoHistory();
            gameWindowGeoHistory.CreatePlayers(CheckPlayer1.IsChecked == true, TextBoxPlayer1.Text, CheckPlayer2.IsChecked == true, TextBoxPlayer2.Text, CheckPlayer3.IsChecked == true, TextBoxPlayer3.Text, CheckPlayer4.IsChecked == true, TextBoxPlayer4.Text);
            this.Hide();
            gameWindowGeoHistory.ShowDialog();
            this.Show();
        }

        private void ButtonStartHistoryFacts_Click(object sender, RoutedEventArgs e)
        {
            View.GameWindowHistoryFacts gameWindowHistoryFacts = new View.GameWindowHistoryFacts();
            gameWindowHistoryFacts.CreatePlayers(CheckPlayer1.IsChecked == true, TextBoxPlayer1.Text, CheckPlayer2.IsChecked == true, TextBoxPlayer2.Text, CheckPlayer3.IsChecked == true, TextBoxPlayer3.Text, CheckPlayer4.IsChecked == true, TextBoxPlayer4.Text);
            this.Hide();
            gameWindowHistoryFacts.ShowDialog();
            this.Show();

        }

        private void ButtonStartFactsGeo_Click(object sender, RoutedEventArgs e)
        {
            View.GameWindowGeoFacts gameWindowGeoFacts = new View.GameWindowGeoFacts();
            gameWindowGeoFacts.CreatePlayers(CheckPlayer1.IsChecked == true, TextBoxPlayer1.Text, CheckPlayer2.IsChecked == true, TextBoxPlayer2.Text, CheckPlayer3.IsChecked == true, TextBoxPlayer3.Text, CheckPlayer4.IsChecked == true, TextBoxPlayer4.Text);
            this.Hide();
            gameWindowGeoFacts.ShowDialog();
            this.Show();

        }
    }
}
