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
    /// Логика взаимодействия для GameWindowFacts.xaml
    /// </summary>
    public partial class GameWindowFacts : Window
    {
        public List<int> QuestionNumbersFacts = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16,
            17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37  };
        public int score = 0;

        int qnum = 0;

        int i;

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

        private void RestartGame()
        {
            qnum = -1;
            i = 0;
            StartGame();
        }

        public GameWindowFacts()
        {
            InitializeComponent();
            StartGame();
            NextQuestionFacts();
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
            TBAB.IsEnabled = false;
        }

        public void checkAnswerEvent(object sender, RoutedEventArgs e)
        {
            if (m_players[m_total_moving_player_id].Points >= 19)
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
                    NextQuestionFacts();
                    NextPlayerMove();
                }

                else
                {
                    MessageBox.Show("Ответ неправильный!\n" + "Правильный будет: " + TBA.Text.ToString() + $"\n\n({TBAQ.Text.ToString()})");
                    NextQuestionFacts();
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
            if (m_players[m_total_moving_player_id].Points >= 19)
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
                    NextQuestionFacts();
                    NextPlayerMove();


                }
                else
                {
                    MessageBox.Show("Ответ неправильный!\n" + "Правильный будет: " + TBA.Text.ToString() + $"\n\n({TBAQ.Text.ToString()})");
                    NextQuestionFacts();
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

            if (m_players[m_total_moving_player_id].Points >= 19)
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
                    NextQuestionFacts();
                    NextPlayerMove();

                }
                else
                {
                    MessageBox.Show("Ответ неправильный!\n" + "Правильный будет: " + TBA.Text.ToString() + $"\n\n({TBAQ.Text.ToString()})");
                    NextQuestionFacts();
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
            if (m_players[m_total_moving_player_id].Points >= 19)
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
                    NextQuestionFacts();
                    NextPlayerMove();
                    TBAB.Text = "";
                }
                else
                {
                    MessageBox.Show("Ответ неправильный!\n" + "Правильный будет: " + TBA.Text.ToString() + $"\n\n({TBAQ.Text.ToString()})");
                    NextQuestionFacts();
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

        private void NextQuestionFacts()
        {
            if (qnum < QuestionNumbersFacts.Count)
            {
                i = QuestionNumbersFacts[qnum];
            }
            else
            {
                RestartGame();
            }

            switch (i)
            {
                case 1:
                    TBQ.Text = "Родина Дедушки Мороза";
                    But4.Content = "Ответить";
                    TBA.Text = "Великий устюг";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    FactsType2();
                    break;

                case 2:
                    TBQ.Text = "В каком городе находится самая большая коллекция янтаря в России?";
                    But4.Content = "Ответить";
                    TBA.Text = "Кострома";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    FactsType2();
                    break;

                case 3:
                    TBQ.Text = "Какой город считается родиной Снегурочки?";
                    But4.Content = "Ответить";
                    TBA.Text = "Калининград";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    FactsType2();
                    break;

                case 4:
                    TBQ.Text = "Правда, что водяная мельница в селе Красниково, Курская область, была построена в 1861 году, без единого гвоздя.";
                    But1.Content = "Да";
                    But3.Content = "Нет";
                    TBA.Text = "Да";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    FactsType1();
                    break;
                case 5:
                    TBQ.Text = "Правда, что Морской порт Мурманска,\r\nрасположенный за Полярным кругом, никогда не замерзает?";
                    But1.Content = "Да";
                    But3.Content = "Нет";
                    TBA.Text = "Да";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    FactsType1();
                    break;

                case 6:
                    TBQ.Text = "Что такое посикунчики?";
                    But4.Content = "Ответить";
                    TBA.Text = "Пермские жареные пирожки";
                    TBAQ.Text = "Пермские жареные пирожки на один-два укуса";
                    FactsType2();
                    break;
                case 7:
                    TBQ.Text = "На флаге и гербе этого региона и его административного центра расположились три стерляди.\r\nГеографически регион расположен в Нижнем Поволжье,\r\nа сам город стоит на берегу Волги. Про какой регион и город идет речь?";
                    But4.Content = "Ответить";
                    TBA.Text = "Саратовская область, Саратов";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    FactsType2();
                    break;

                case 8:
                    TBQ.Text = "В каком городе из-за особенностей рельефной структуры\r\nмного лестниц, а не улиц?";
                    But4.Content = "Ответить";
                    TBA.Text = "Севастополь";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    FactsType2();
                    break;

                case 9:
                    TBQ.Text = "Правда, что в Тамбове находится вторая\r\nпо высоте колокольня в России?.";
                    But1.Content = "Да";
                    But3.Content = "Нет";
                    TBA.Text = "Да";
                    TBAQ.Text = "Правда, Казанский Богородичный монастырь";
                    FactsType1();
                    break;
                case 10:
                    TBQ.Text = "Родина Дмитрия Ивановича Менделеева";
                    But4.Content = "Ответить";
                    TBA.Text = "Тюменьская область";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    FactsType2();
                    break;

                case 11:
                    TBQ.Text = "Как по-чувашски будет «да»?";
                    But4.Content = "Ответить";
                    TBA.Text = "Нет такого слова";
                    TBAQ.Text = "В чувашском языке нет слова «да», положительным ответом на вопрос будет повтор глагола";
                    FactsType2();
                    break;
                case 12:
                    TBQ.Text = "Эта ягода созревает как бы «наоборот» — \r\nсначала краснеет, а потом желтеет.Что это за ягода?";
                    But4.Content = "Ответить";
                    TBA.Text = "Морошка";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    FactsType2();
                    break;

                case 13:
                    TBQ.Text = "В каком регионе находится «город миллиона роз»?";
                    But4.Content = "Ответить";
                    TBA.Text = "Донецкая народная республика";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    FactsType2();
                    break;
                case 14:
                    TBQ.Text = "Ценный поделочный камень, который использовали в облицовке станций метро «Сокол», «Аэропорт», «Белорусская» г.Москва.";
                    But4.Content = "Ответить";
                    TBA.Text = "Бираканский розовый мрамор";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    FactsType2();
                    break;

                case 15:
                    TBQ.Text = "Кто первым установил рекорд на производстве:\r\nТкачихи Виноградовы или шахтер Стаханов,\r\nв честь которого и названо движение?";
                    But4.Content = "Ответить";
                    TBA.Text = "Виноградовыми";
                    TBAQ.Text = "В мае 1935 года установлен рекорд по количеству обслуживания станков ткачихами Виноградовыми.\r\nА рекорд Стаханова датирован только августом 1935 года.";
                    FactsType2();
                    break;
                case 16:
                    TBQ.Text = "Какое уникальное явление на территории Красноярского края произошло 30 июня 1908 года?\r\nПо числу легенд, связанных с этим явлением, может сравниться лишь мифологическая Атлантида и чудовище из озера Лох-Несс.";
                    But4.Content = "Ответить";
                    TBA.Text = "Падение Тунгусского метеорита";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    FactsType2();
                    break;
                case 17:
                    TBQ.Text = "Правда ли, что в Ненецкого автономного округа на каждого жителя приходится по 4 оленя?";
                    But1.Content = "Да";
                    But3.Content = "Нет";
                    TBA.Text = "Да";
                    TBAQ.Text = "Да, в тундре Ненецкого автономного округа проживает 168 тысяч оленей, что в 4 раза больше, чем людей";
                    FactsType1();
                    break;

                case 18:
                    TBQ.Text = "142 кг этого продукта было изготовлено на фестивале,\r\nпосвященному гастрономическому символу Адыгеи.\r\nВы встречаете его на прилавках магазинов в малосольном,\r\nкопчёном, сыром виде, со специями и без.\r\nЧто это за продукт?";
                    But4.Content = "Ответить";
                    TBA.Text = "Адыгейский сыр";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    FactsType2();
                    break;
                case 19:
                    TBQ.Text = "Какой город изображён на купюре номиналом 1000 рублей?";
                    But4.Content = "Ответить";
                    TBA.Text = "Ярославль";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    FactsType2();
                    break;

                case 20:
                    TBQ.Text = "Правда ли, что жители Чукотки могут одновременно находиться в сегодняшнем дне и завтрашнем?";
                    But1.Content = "Да";
                    But3.Content = "Нет";
                    TBA.Text = "Да";
                    TBAQ.Text = "Правда, через восточную часть Чукотки проходит 180 меридиан,\r\nТлиния перемены дат.Именно здесь начинаются каждые новые сутки";
                    FactsType1();
                    break;
                case 21:
                    TBQ.Text = "Мечта любого школьника— отдых в самом знаменитом детском лагере в Крыму.\r\nТКак он называется?";
                    But4.Content = "Ответить";
                    TBA.Text = "Артек";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    FactsType2();
                    break;

                case 22:
                    TBQ.Text = "Какой памятник является покровителем и символом\r\nстолицы Республики Марий Эл\r\n(столица – город Йошкар-Ола)?\r\nТПодсказка: так еще называют\r\nкота Бабы Яги, кота Баюна.";
                    But4.Content = "Ответить";
                    TBA.Text = "Йошкин кот";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    FactsType2();
                    break;
                case 23:
                    TBQ.Text = "Что символизируют три пирога\r\nв этно-религиозной обрядности осетин?";
                    But4.Content = "Ответить";
                    TBA.Text = "Древнейшая троичная модель мира";
                    TBAQ.Text = "Древнейшая троичная модель мира – солнце, вода, земля";
                    FactsType2();
                    break;

                case 24:
                    TBQ.Text = "Этот город по праву считается «Бриллиантовой\r\nстолицей России».";
                    But4.Content = "Ответить";
                    TBA.Text = "Смоленск";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    FactsType2();
                    break;
                case 25:
                    TBQ.Text = "Происхождение какого блюда связано с владельцем\r\nтрактира в Торжке в начале XIX век?";
                    But4.Content = "Ответить";
                    TBA.Text = "Пожарские котлеты";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    FactsType2();
                    break;

                case 26:
                    TBQ.Text = "Возле какого известного театра в Москве\r\nвстречаются магаданцы каждый год 31 августа?";
                    But4.Content = "Ответить";
                    TBA.Text = "Большой Театр";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    FactsType2();
                    break;
                case 27:
                    TBQ.Text = "Правда ли, что в Рязани есть Переулок счастья?";
                    But1.Content = "Да";
                    But3.Content = "Нет";
                    TBA.Text = "Да";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    FactsType1();
                    break;

                case 28:
                    TBQ.Text = "Какой динозавр обитал только в Кузбассе?";
                    But4.Content = "Ответить";
                    TBA.Text = "Пситтакозавр";
                    TBAQ.Text = "Пситтакозавр";
                    FactsType2();
                    break;
                case 29:
                    TBQ.Text = "Как называется удмуртский Дед Мороз?";
                    But1.Content = "Тол Бабай";
                    But2.Content = "Сагаан Убургун";
                    But3.Content = "Ямал Ири";
                    TBA.Text = "Тол Бабай";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    FactsType3();
                    break;

                case 30:
                    TBQ.Text = "Супруга какого известного художника родилась в Казани?";
                    But4.Content = "Ответить";
                    TBA.Text = "Сальвадора Дали";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    FactsType2();
                    break;
                case 31:
                    TBQ.Text = "Какой город был прототипом города «N»\r\nв пьесе «Ревизор» Н. В. Гоголя?";
                    But4.Content = "Ответить";
                    TBA.Text = "Пенза";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    FactsType2();
                    break;

                case 32:
                    TBQ.Text = "На территории какой области находится самое большое\r\nколичество памятников деревянного зодчества?";
                    But4.Content = "Ответить";
                    TBA.Text = "Томская область";
                    TBAQ.Text = "Томская область, около 2000 домов";
                    FactsType2();
                    break;

                case 33:
                    TBQ.Text = "Какой Всероссийский фестиваль проводится\r\nв Самарской области с 1968 года.";
                    But4.Content = "Ответить";
                    TBA.Text = "Грушинский фестиваль бардовской песни";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    FactsType2();
                    break;
                case 34:
                    TBQ.Text = "Всем известный напиток кефир был получен горцами,\r\nблагодаря попаданию в молоко кефирного грибка\r\nв горной местности вблизи Эльбруса.\r\nКефирные грибки настолько ценились местными народами,\r\nчто их использовали в качестве валюты\r\nпри обмене на другие товары.\r\nКакой регион является родиной кефира?";
                    But4.Content = "Ответить";
                    TBA.Text = "Карачаево-Черкесская Республика";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    FactsType2();
                    break;
                case 35:
                    TBQ.Text = "Как за его большие размеры называют в народе здание\r\nНовосибирского театра оперы и балета?";
                    But1.Content = "Сибирский Колизей";
                    But2.Content = "Сибирский Большой театр";
                    But3.Content = "Сибирское чудо света";
                    TBA.Text = "Сибирский Колизей";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    FactsType3();
                    break;
                case 36:
                    TBQ.Text = "Какой праздник ежегодно отмечается\r\n1 марта в Республике Ингушетия?";
                    But1.Content = "День уважительного отношения к старшим";
                    But2.Content = "День джигита";
                    But3.Content = "День айра (День пастилы)";
                    TBA.Text = "День джигита";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    FactsType3();
                    break;
                case 37:
                    TBQ.Text = "В городе Грозном несколько лет назад\r\nстроили башню Олимп, но при строительстве\r\nтам произошел пожар.\r\nПосле восстановления ее переименовали в ...";
                    But1.Content = "Феникс";
                    But2.Content = "Возрождение";
                    But3.Content = "Сердце Чечни";
                    TBA.Text = "Феникс";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    FactsType3();
                    break;
            }
        }

        private void StartGame()
        {
            var randomlist2 = QuestionNumbersFacts.OrderBy(a => Guid.NewGuid()).ToList();

            QuestionNumbersFacts = randomlist2;
            for (int i = 0; i < QuestionNumbersFacts.Count; i++)
            {
                QuestionNumbersFacts[i].ToString();
            }
        }

        private void FactsType1()
        {
            But1.Visibility = Visibility.Visible;
            But3.Visibility = Visibility.Visible;
            But2.Visibility = Visibility.Hidden;
            But4.Visibility = Visibility.Hidden;

            TBAB.Visibility = Visibility.Hidden;
        }

        private void FactsType2()
        {
            But4.Visibility = Visibility.Visible;
            TBAB.Visibility = Visibility.Visible;
            But1.Visibility = Visibility.Hidden;
            But2.Visibility = Visibility.Hidden;
            But3.Visibility = Visibility.Hidden;
        }

        private void FactsType3()
        {
            But1.Visibility = Visibility.Visible;
            But2.Visibility = Visibility.Visible;
            But3.Visibility = Visibility.Visible;
            TBAB.Visibility = Visibility.Hidden;
            But4.Visibility = Visibility.Hidden;
        }
    }
}
