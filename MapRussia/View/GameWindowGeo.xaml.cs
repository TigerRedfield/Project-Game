using MapRussia.Classes;
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
    /// Логика взаимодействия для GameWindowGeo.xaml
    /// </summary>
    public partial class GameWindowGeo : Window
    {
        public List<int> QuestionNumbersGeo = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26 };
        public int score = 0;
        int qnum = 0;

        int i;

        private void RestartGame()
        {
            qnum = -1;
            i = 0;
            StartGame();
        }

        public GameWindowGeo()
        {
            InitializeComponent();
            StartGame();
            NextQuestionGeo();
        }

        /// <summary>Игроки</summary>
        public Player[] m_players = new Player[4];


        /// <summary>ID игрока, который сейчас ходит</summary>
        public int m_total_moving_player_id;

        /// <summary>Создаёт игроков в этой форме</summary>
        /// <param name="is_player_1">Участвует ли игрок 1 в игре</param>
        /// <param name="name_player_1">Имя игрока 1</param>
        /// <param name="is_player_2">Участвует ли игрок 2 в игре</param>
        /// <param name="name_player_2">Имя игрока 2</param>
        /// <param name="is_player_3">Участвует ли игрок 3 в игре</param>
        /// <param name="name_player_3">Имя игрока 3</param>
        /// <param name="is_player_4">Участвует ли игрок 4 в игре</param>
        /// <param name="name_player_4">Имя игрока 4</param>
        public void CreatePlayers(bool is_player_1 = true, string name_player_1 = "", bool is_player_2 = true, string name_player_2 = "", bool is_player_3 = true, string name_player_3 = "", bool is_player_4 = true, string name_player_4 = "")
        {
            // если имя игрока пустое или содержит только пробелы, то его имя заменяется стандартным
            if (name_player_1.Split(' ')[0].Length == 0) name_player_1 = "Команда 1";
            if (name_player_2.Split(' ')[0].Length == 0) name_player_2 = "Команда 2";
            if (name_player_3.Split(' ')[0].Length == 0) name_player_3 = "Команда 3";
            if (name_player_4.Split(' ')[0].Length == 0) name_player_4 = "Команда 4";

            // заполнение массива игроков, если игрок не участвует, то его переменная будет равна null

            if (is_player_1) m_players[0] = new Player(name_player_1);
            else m_players[0] = null;

            if (is_player_2) m_players[1] = new Player(name_player_2);
            else m_players[1] = null;

            if (is_player_3) m_players[2] = new Player(name_player_3);
            else m_players[2] = null;

            if (is_player_4) m_players[3] = new Player(name_player_4);
            else m_players[3] = null;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // у каждого игрока свой groupBox. Для удобства выравнивания личных ячеек игроков используется цикл
            GroupBox[] group_boxes = new GroupBox[4];
            group_boxes[0] = gpPlayer1;
            group_boxes[1] = gpPlayer2;
            group_boxes[2] = gpPlayer3;
            group_boxes[3] = gpPlayer4;

            for (int i = 0; i < 4; i++) // для каждого игрока
            {
                if (m_players[i] != null) // если игрок участвует
                {
                    group_boxes[i].IsEnabled = true; // делаем доступными элементы его groupBox
                    group_boxes[i].Visibility = Visibility.Visible; // показываем элементы его groupBox

                    group_boxes[i].Header = m_players[i].Name; // имя игрока является заголовком его groupBox



                }
                else // если игрок не участвует
                {
                    group_boxes[i].IsEnabled = false; // делаем недоступными элементы его groupBox
                    group_boxes[i].Visibility = Visibility.Hidden; // скрываем элементы его groupBox
                }
            }

            m_total_moving_player_id = -1; // id игрока, который сейчас ходит
            StartGame();
            NextPlayerMove();

        }

        /// <summary>Передача хода следующему игроку</summary>
        private void NextPlayerMove()
        {

            // меняем id игрока, который сейчас ходит на следующего, если тот участвует в игре
            do
            {
                m_total_moving_player_id++;
                if (m_total_moving_player_id == 4) m_total_moving_player_id = 0; // переход по кругу
            }
            while (m_players[m_total_moving_player_id] == null);


            // у каждого игрока свой groupBox, для удобства обработки используется цикл
            GroupBox[] group_boxes = new GroupBox[4];
            group_boxes[0] = gpPlayer1;
            group_boxes[1] = gpPlayer2;
            group_boxes[2] = gpPlayer3;
            group_boxes[3] = gpPlayer4;

            Image[] images = new Image[4];
            images[0] = imgAct1;
            images[1] = imgAct2;
            images[2] = imgAct3;
            images[3] = imgAct4;

            TextBlock[] textblocks = new TextBlock[4];
            textblocks[0] = txtturnP1;
            textblocks[1] = txtturnP2;
            textblocks[2] = txtturnP3;
            textblocks[3] = txtturnP4;

            TextBlock[] txtb = new TextBlock[4];
            txtb[0] = TxtPointP1;
            txtb[1] = TxtPointP2;
            txtb[2] = TxtPointP3;
            txtb[3] = TxtPointP4;

            {
                // активация groupBox игрока, делающего ход сейчас, деактивация groupBox других игроков
                for (int i = 0; i < 4; i++)
                {
                    if (m_players[i] != null)
                    {

                        if (i == m_total_moving_player_id)
                        {
                            Uri resource1 = new Uri("/Resources/player_1_active.png", UriKind.Relative);
                            Uri resource2 = new Uri("/Resources/player_2_active.png", UriKind.Relative);
                            Uri resource3 = new Uri("/Resources/player_3_active.png", UriKind.Relative);
                            Uri resource4 = new Uri("/Resources/player_4_active.png", UriKind.Relative);

                            group_boxes[i].IsEnabled = true;
                            textblocks[i].IsEnabled = true;
                            if (i == 0) { imgAct1.Source = new BitmapImage(resource1); txtturnP1.Text = "Ваш ход"; }
                            else if (i == 1) { imgAct2.Source = new BitmapImage(resource2); txtturnP2.Text = "Ваш ход"; }
                            else if (i == 2) { imgAct3.Source = new BitmapImage(resource3); txtturnP3.Text = "Ваш ход"; }
                            else if (i == 3) { imgAct4.Source = new BitmapImage(resource4); txtturnP4.Text = "Ваш ход"; }
                        }

                        else
                        {
                            Uri resource1 = new Uri("/Resources/player_1_not_active.png", UriKind.Relative);
                            Uri resource2 = new Uri("/Resources/player_2_not_active.png", UriKind.Relative);
                            Uri resource3 = new Uri("/Resources/player_3_not_active.png", UriKind.Relative);
                            Uri resource4 = new Uri("/Resources/player_4_not_active.png", UriKind.Relative);

                            group_boxes[i].IsEnabled = false;
                            textblocks[i].IsEnabled = false;
                            if (i == 0) { imgAct1.Source = new BitmapImage(resource1); txtturnP1.Text = ""; }
                            else if (i == 1) { imgAct2.Source = new BitmapImage(resource2); txtturnP2.Text = ""; }
                            else if (i == 2) { imgAct3.Source = new BitmapImage(resource3); txtturnP3.Text = ""; }
                            else if (i == 3) { imgAct4.Source = new BitmapImage(resource4); txtturnP4.Text = ""; }
                        }
                    }
                }
                if (m_players[0] != null) TxtPointP1.Text = "Количество очков: " + m_players[0].Points.ToString();
                if (m_players[1] != null) TxtPointP2.Text = "Количество очков: " + m_players[1].Points.ToString();
                if (m_players[2] != null) TxtPointP3.Text = "Количество очков: " + m_players[2].Points.ToString();
                if (m_players[3] != null) TxtPointP4.Text = "Количество очков: " + m_players[3].Points.ToString();
            }
        }

        private void ButtonTbBlock()
        {
            But1.IsEnabled = false;
            But2.IsEnabled = false;
            But3.IsEnabled = false;
            But4.IsEnabled = false;
        }

        public void checkAnswerEvent(object sender, RoutedEventArgs e)
        {
            if (m_players[m_total_moving_player_id].Points >= 10)
            {
                MessageBox.Show("Первая закончила и победила " + $"«{m_players[m_total_moving_player_id].Name}»");
                ButtonTbBlock();
                return;
            }
            else
            {
                if (But1.Content.ToString() == TBA.Text)
                {
                    MessageBox.Show("Ответ правильный!\n" + $"\n({TBAQ.Text.ToString()})");
                    m_players[m_total_moving_player_id].Points++;
                    NextQuestionGeo();
                    NextPlayerMove();
                }

                else
                {
                    MessageBox.Show("Ответ неправильный!\n" + "Правильный будет: " + TBA.Text.ToString() + $"\n({TBAQ.Text.ToString()})");
                    NextQuestionGeo();
                    NextPlayerMove();
                }

                if (qnum < 0)
                {
                    qnum = 0;
                }

                else
                {
                    qnum++;
                }
            }

        }


        public void checkAnswerEvent2(object sender, RoutedEventArgs e)
        {
            if (m_players[m_total_moving_player_id].Points >= 10)
            {
                MessageBox.Show("Первая закончила и победила " + $"«{m_players[m_total_moving_player_id].Name}»");
                ButtonTbBlock();
                return;
            }
            else
            {
                if (But2.Content.ToString() == TBA.Text)
                {
                    MessageBox.Show("Ответ правильный!\n" + $"\n({TBAQ.Text.ToString()})");
                    m_players[m_total_moving_player_id].Points++;
                    NextQuestionGeo();
                    NextPlayerMove();


                }
                else
                {
                    MessageBox.Show("Ответ неправильный!\n" + "Правильный будет: " + TBA.Text.ToString() + $"\n\n({TBAQ.Text.ToString()})");
                    NextQuestionGeo();
                    NextPlayerMove();
                }


                if (qnum < 0)
                {
                    qnum = 0;
                }

                else
                {
                    qnum++;
                }
            }
        }


        public void checkAnswerEvent3(object sender, RoutedEventArgs e)
        {

            if (m_players[m_total_moving_player_id].Points >= 10)
            {
                MessageBox.Show("Первая закончила и победила " + $"«{m_players[m_total_moving_player_id].Name}»");
                ButtonTbBlock();
                return;
            }
            else
            {
                if (But3.Content.ToString() == TBA.Text)
                {
                    MessageBox.Show("Ответ правильный!\n" + $"\n({TBAQ.Text.ToString()})");
                    m_players[m_total_moving_player_id].Points++;
                    NextQuestionGeo();
                    NextPlayerMove();

                }
                else
                {
                    MessageBox.Show("Ответ неправильный!\n" + "Правильный будет: " + TBA.Text.ToString() + $"\n\n({TBAQ.Text.ToString()})");
                    NextQuestionGeo();
                    NextPlayerMove();
                }


                if (qnum < 0)
                {
                    qnum = 0;
                }

                else
                {
                    qnum++;
                }

            }
        }

        public void checkAnswerEvent4(object sender, RoutedEventArgs e)
        {
            if (m_players[m_total_moving_player_id].Points >= 10)
            {
                MessageBox.Show("Первая закончила и победила " + $"«{m_players[m_total_moving_player_id].Name}»");
                ButtonTbBlock();
                return;
            }
            else
            {

                if (TBAB.Text == TBA.Text)
                {
                    MessageBox.Show("Ответ правильный!\n" + $"\n({TBAQ.Text.ToString()})");
                    m_players[m_total_moving_player_id].Points = m_players[m_total_moving_player_id].Points + 2;
                    NextQuestionGeo();
                    NextPlayerMove();
                    TBAB.Text = "";
                }
                else
                {
                    MessageBox.Show("Ответ неправильный!\n" + "Правильный будет: " + TBA.Text.ToString() + $"\n\n({TBAQ.Text.ToString()})");
                    NextQuestionGeo();
                    NextPlayerMove();
                    TBAB.Text = "";
                }


                if (qnum < 0)
                {
                    qnum = 0;
                }

                else
                {
                    qnum++;
                }

            }
        }




        private void NextQuestionGeo()
        {
            if (qnum < QuestionNumbersGeo.Count)
            {
                i = QuestionNumbersGeo[qnum];
            }
            else
            {
                RestartGame();
            }

            switch (i)
            {
                case 1:
                    TBQ.Text = "Правда, что Чуйский тракт – единственная дорога,\r\nимеющая собственный музей?";
                    But1.Content = "Да";
                    But3.Content = "Нет";
                    TBA.Text = "Да";
                    TBAQ.Text = "Правда, протяженность исторической части – 630 км, относится к самым живописным дорогам не только России, но и всего мира, Алтайский край";
                    GeoType1();
                    break;

                case 2:
                    TBQ.Text = "Какой единственный регион в России\r\nрасположен в зоне пустынь и полупустынь?";
                    But4.Content = "Ответить";
                    TBA.Text = "Астраханская область";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    GeoType2();
                    break;

                case 3:
                    TBQ.Text = "Правда, что название городу Белгород подарила гора Белая?";
                    But1.Content = "Да";
                    But3.Content = "Нет";
                    TBA.Text = "Да";
                    TBAQ.Text = "Правда, гора состоит из меловых пород и там, где она не заросла растительностью, имеет белый цвет";
                    GeoType1();
                    break;
                case 4:
                    TBQ.Text = "В какой области центральной России сходятся границы\r\nтрёх государств – России, Беларуси и Украины?";
                    But4.Content = "Ответить";
                    TBA.Text = "Брянская область";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    GeoType2();
                    break;
                case 5:
                    TBQ.Text = "В каком городе находится одно\r\nиз семи чудес России–Мамаев курган?";
                    But4.Content = "Ответить";
                    TBA.Text = "Волгоград";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    GeoType2();
                    break;
                case 6:
                    TBQ.Text = "Самая северная пустыня в мире?";
                    But4.Content = "Ответить";
                    TBA.Text = "Чарские пески";
                    TBAQ.Text = "Чарские пески, Забайкальский край";
                    GeoType2();
                    break;
                case 7:
                    TBQ.Text = "Правда, что возраст озера Байкал\r\nсоставляет порядка 25 миллионов лет?";
                    But1.Content = "Да";
                    But3.Content = "Нет";
                    TBA.Text = "Да";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    GeoType1();
                    break;

                case 8:
                    TBQ.Text = "В каком регионе находится сказочное место\r\n«Кудыкина гора»?";
                    But4.Content = "Ответить";
                    TBA.Text = "Липецкая область";
                    TBAQ.Text = "Липецкая область, недалеко от города Задонск";
                    GeoType2();
                    break;
                case 9:
                    TBQ.Text = "Правда, что несколько сотен миллионов лет назад\r\nПодмосковье было частью морского дна?";
                    But1.Content = "Да";
                    But3.Content = "Нет";
                    TBA.Text = "Да";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    GeoType1();
                    break;
                case 10:
                    TBQ.Text = "Сулакский каньон – самое красивое и грандиозное ущелье\r\nна территории России. Занимает шестое место\r\nв списке самых глубоких каньонов мира.\r\nЕго глубина составляет 1920 метров, а протяженность 53 км.\r\nВ каком регионе он находится?";
                    But4.Content = "Ответить";
                    TBA.Text = "Республика Дагестан";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    GeoType2();
                    break;
                case 11:
                    TBQ.Text = "В каком регионе находится самый крупный по объёму воды\r\nпресноводный водоем Европы–Ладожское озеро?";
                    But4.Content = "Ответить";
                    TBA.Text = "Республика Карелия";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    GeoType2();
                    break;
                case 12:
                    TBQ.Text = "Этот минерал добывается в Республике Саха (Якутия),\r\nон является самым твёрдым минералом. Что это за минерал?";
                    But4.Content = "Ответить";
                    TBA.Text = "Алмаз";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    GeoType2();
                    break;
                case 13:
                    TBQ.Text = "Какие реки несут свои воды параллельно друг другу,\r\nно с противоположным течением?";
                    But4.Content = "Ответить";
                    TBA.Text = "Свияга и Волга";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    GeoType2();
                    break;
                case 14:
                    TBQ.Text = "Какой регион славится лучшим сортом каменного угля,\r\nотличающийся чёрным цветом, сильным блеском,\r\nбольшой теплотворной способностью – антрацитом?";
                    But4.Content = "Ответить";
                    TBA.Text = "Луганская Народная Республика";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    GeoType2();
                    break;
                case 15:
                    TBQ.Text = "Какой остров является самым крупным на реке Днепр?";
                    But4.Content = "Ответить";
                    TBA.Text = "Хортица";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    GeoType2();
                    break;
                case 16:
                    TBQ.Text = "С этой страной Камчатку разделяет лишь пролив,\r\nшириной 4 км. Что это за страна?";
                    But4.Content = "Ответить";
                    TBA.Text = "США";
                    TBAQ.Text = "США, Аляска";
                    GeoType2();
                    break;
                case 17:
                    TBQ.Text = "Регион, где 80% территории занято смешанными лесами\r\n(самыми необычными по своему составу:\r\nв сотне метров друг от друга могут находиться\r\nобычная лиственница и настоящая лиана, здесь сосны – корейские, клёны - манчжурские, ели – аянские, а дубы – nмонгольские), а леопарды уживаются с тиграми и медведями.";
                    But4.Content = "Ответить";
                    TBA.Text = "Приморский край";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    GeoType2();
                    break;
                case 18:
                    TBQ.Text = "Все реки, как реки, а наша красавица! Катунь или «Кадын» – так уважительно обращаются к женщине на Алтае.\r\nКатунь, как полагается женщине, любит переодеваться.\r\nКакого цвета самый красивый наряд реки?";
                    But4.Content = "Ответить";
                    TBA.Text = "Бирюзовый";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    GeoType2();
                    break;
                case 19:
                    TBQ.Text = "Как может быть, что на территории Республики Коми\r\nесть целый океан, если у региона нет выхода к морям?";
                    But4.Content = "Ответить";
                    TBA.Text = "Самое крупное болото";
                    TBAQ.Text = "В Республике Коми находится самое крупное болото в Европе, оно называется Океан";
                    GeoType2();
                    break;
                case 20:
                    TBQ.Text = "В какой стране находится космодром Байконур?";
                    But4.Content = "Ответить";
                    TBA.Text = "Казахстан";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    GeoType2();
                    break;
                case 21:
                    TBQ.Text = "С какой страной граничит Хабаровский край?";
                    But4.Content = "Ответить";
                    TBA.Text = "Китай";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    GeoType2();
                    break;

                case 22:
                    TBQ.Text = "В каком регионе находится пещера Шульган-Таш,\r\nв которой изображены уникальные рисунки древних людей?";
                    But4.Content = "Ответить";
                    TBA.Text = "Республика Башкортостан";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    GeoType2();
                    break;

                case 23:
                    TBQ.Text = "Столица Нальчик, Кабардино-Балкарская Республика,\r\nрасположена в полукруге гор и напоминает\r\nопределенный предмет. Что это за предмет?";
                    But1.Content = "Месяц";
                    But2.Content = "Подкова";
                    But3.Content = "Сыр";
                    TBA.Text = "Подкова";
                    TBAQ.Text = "Подкова, с балкарского, и с кабардинского слово «нал» переводится, как подкова";
                    GeoType3();
                    break;

                case 24:
                    TBQ.Text = "Правда ли, что в Кургане есть озеро\r\nпо свойствам воды схожее с мертвым морем?";
                    But1.Content = "Да";
                    But3.Content = "Нет";
                    TBA.Text = "Да";
                    TBAQ.Text = "Да, это озеро Медвежье";
                    GeoType1();
                    break;

                case 25:
                    TBQ.Text = "Чем так знаменито Лемурийское озеро Херсонщины?";
                    But1.Content = "Историей\nобразования\nводоёма";
                    But2.Content = "Очень большим\nсодержанием\nсоли в воде";
                    But3.Content = "Самыми необычными\nжителями этого водоема";
                    TBA.Text = "Очень большим\nсодержанием\nсоли в воде";
                    TBAQ.Text = "На 1 литр воды 300 грам соли.";
                    GeoType3();
                    break;

                case 26:
                    TBQ.Text = "В каком регионе находится Саяно-Шушенская ГЭС?";
                    But4.Content = "Ответить";
                    TBA.Text = "Республика Хакасия";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    GeoType2();
                    break;
            }
        }



        private void StartGame()
        {
            var randomlist = QuestionNumbersGeo.OrderBy(a => Guid.NewGuid()).ToList();

            QuestionNumbersGeo = randomlist;


            for (int i = 0; i < QuestionNumbersGeo.Count; i++)
            {
                QuestionNumbersGeo[i].ToString();
            }

        }

        private void GeoType1()
        {
            But1.Visibility = Visibility.Visible;
            But3.Visibility = Visibility.Visible;
            But2.Visibility = Visibility.Hidden;
            But4.Visibility = Visibility.Hidden;
          
            TBAB.Visibility = Visibility.Hidden;
        }

        private void GeoType2()
        {
            But4.Visibility = Visibility.Visible;
            TBAB.Visibility = Visibility.Visible;
            But1.Visibility = Visibility.Hidden;
            But2.Visibility = Visibility.Hidden;
            But3.Visibility = Visibility.Hidden;
            
        }

        private void GeoType3()
        {
            But1.Visibility = Visibility.Visible;
            But2.Visibility = Visibility.Visible;
            But3.Visibility = Visibility.Visible;
            TBAB.Visibility = Visibility.Hidden;
            But4.Visibility = Visibility.Hidden;
       
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("Вы точно хотите выйти из текущей игры?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Application.Current.MainWindow.Visibility = System.Windows.Visibility.Collapsed;
                this.Close();
                //do something
            }

        }

        private void ButtonHidden_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

    }
}

