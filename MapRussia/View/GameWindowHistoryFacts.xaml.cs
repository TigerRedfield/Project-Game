using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
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
using MapRussia.Classes;
using MapRussia.Properties;

namespace MapRussia.View
{
    /// <summary>
    /// Логика взаимодействия для GameWindow.xaml
    /// </summary>
    public partial class GameWindowHistoryFacts : Window
    {
        public List<int> QuestionNumbersHistoryFacts = new List<int> {27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41,
            42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 
                64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87 };
        public int score = 0;
        int qnum = 0;

        int i;



        private void RestartGame()
        {
            qnum = -1;
            i = 0;
            StartGame();
        }

        public GameWindowHistoryFacts()
        {
            InitializeComponent();
            StartGame();
            NextQuestion();

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
            TBAB.IsEnabled = false;

        }

        public void checkAnswerEvent(object sender, RoutedEventArgs e)
        {
             if (m_players[m_total_moving_player_id].Points >= 26)
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
                    NextQuestion();
                    NextPlayerMove();
                }

                else
                {
                    MessageBox.Show("Ответ неправильный!\n" + "Правильный будет: " + TBA.Text.ToString() + $"\n\n({TBAQ.Text.ToString()})");
                    NextQuestion();
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
            if (m_players[m_total_moving_player_id].Points >= 26)
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
                    NextQuestion();
                    NextPlayerMove();
                }

                else
                {
                    MessageBox.Show("Ответ неправильный!\n" + "Правильный будет: " + TBA.Text.ToString() + $"\n\n({TBAQ.Text.ToString()})");
                    NextQuestion();
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

            if (m_players[m_total_moving_player_id].Points >= 26)
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
                    NextQuestion();
                    NextPlayerMove();
                }

                else
                {
                    MessageBox.Show("Ответ неправильный!\n" + "Правильный будет: " + TBA.Text.ToString() + $"\n\n({TBAQ.Text.ToString()})");
                    NextQuestion();
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
            if (m_players[m_total_moving_player_id].Points >= 26)
            {
                MessageBox.Show("Первая закончила и победила " + $"«{m_players[m_total_moving_player_id].Name}»");
                ButtonTbBlock();
                return;
            }
            else
            {
                if (TBAB.Text== TBA.Text)
                {
                    MessageBox.Show("Ответ правильный!\n" + $"\n({TBAQ.Text.ToString()})");
                    m_players[m_total_moving_player_id].Points = m_players[m_total_moving_player_id].Points + 2;
                    NextQuestion();
                    NextPlayerMove();
                    TBAB.Text = "";
                }

                else
                {
                    MessageBox.Show("Ответ неправильный!\n" + "Правильный будет: " + TBA.Text.ToString() + $"\n\n({TBAQ.Text.ToString()})");
                    NextQuestion();
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



        private void NextQuestion()
        {
            if (qnum < QuestionNumbersHistoryFacts.Count)
            {
                i = QuestionNumbersHistoryFacts[qnum];
            }
            else
            {
                RestartGame();
            }

            switch (i)
            {
                case 27:
                    TBQ.Text = "Какой город является колыбелью\r\nроссийского кораблестроения?";
                    But4.Content = "Ответить";
                    TBA.Text = "Воронеж";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType2();
                    txt2();
                    break;

                case 28:
                    TBQ.Text = "Правда, что Обнинская АЭС — первая в мире атомная электростанция?";
                    But1.Content = "Да";
                    But3.Content = "Нет";
                    TBA.Text = "Да";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType1();
                    txt2();
                    break;

                case 29:
                    TBQ.Text = "Правда, что на территории Краснодарского края\r\nесть необычные каменные сооружения – дольмены,\r\nкоторые датируются от IV до II тысячелетия до н. э.?";
                    But1.Content = "Да";
                    But3.Content = "Нет";
                    TBA.Text = "Да";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType1();
                    txt2();
                    break;
                case 30:
                    TBQ.Text = "На территории какого региона находится\r\nединственный в России средневековый замок?";
                    But4.Content = "Ответить";
                    TBA.Text = "Ленинградская область";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType2();
                    txt2();
                    break;
                case 31:
                    TBQ.Text = "Правда, что Омск был столицей Российской Империи?";
                    But1.Content = "Да";
                    But3.Content = "Нет";
                    TBA.Text = "Да";
                    TBAQ.Text = "Правда, но недолго, всего пару лет";
                    AllQType1();
                    txt2();
                    break;
                case 32:
                    TBQ.Text = "Правда, что Оренбургскую область и её столицу\r\nнаселяют представители более 120 этносов?";
                    But1.Content = "Да";
                    But3.Content = "Нет";
                    TBA.Text = "Да";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType1();
                    txt2();
                    break;
                case 33:
                    TBQ.Text = "Какой регион является центром буддизма в России?";
                    But4.Content = "Ответить";
                    TBA.Text = "Республика Бурятия";
                    TBAQ.Text = "Республика Бурятия, Иволгинский дацан";
                    AllQType2();
                    txt2();
                    break;

                case 34:
                    TBQ.Text = "Символом какого региона является «Красная лиса»?";
                    But4.Content = "Ответить";
                    TBA.Text = "Республика Мордовия";
                    TBAQ.Text = "Республика Мордовия, образ используется в качестве символа\r\nи является исторической составляющей\r\nсоциокультурного имиджа столицы региона";
                    AllQType2();
                    txt2();
                    break;
                case 35:
                    TBQ.Text = "Этот город занимает 10 место из 15\r\nгородов-миллионников России по количеству жителей.\r\nА еще его традиционно называют вратами на юг России\r\nи Северный Кавказ, когда в современности это уже не так.\r\nКакой это город и центром какого региона он является?";
                    But4.Content = "Ответить";
                    TBA.Text = "Ростовская область, Ростов-на-Дону";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType2();
                    txt2();
                    break;
                case 36:
                    TBQ.Text = "В каком регионе родился\r\nпервый президент России Борис Ельцин?";
                    But4.Content = "Ответить";
                    TBA.Text = "Свердловская область";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType2();
                    txt2();
                    break;
                case 37:
                    TBQ.Text = "Какой регион является родиной южных слонов?";
                    But4.Content = "Ответить";
                    TBA.Text = "Ставропольский край";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType2();
                    txt2();
                    break;
                case 38:
                    TBQ.Text = "Какое животное изображено на гербе\r\nЧелябинской области и почему?";
                    But4.Content = "Ответить";
                    TBA.Text = "Верблюд, торговые взаимоотношения с соседними казахами и киргизами";
                    TBAQ.Text = "Верблюд, навьюченный товаром - символ торговли,\r\nсвязано с торговыми взаимоотношениями\r\nс соседними казахами и киргизами";
                    AllQType2();
                    txt2();
                    break;
                case 39:
                    TBQ.Text = "Кундурское месторождение динозавров в Амурской области\r\nисследовано в середине 20 века. Был обнаружен\r\nпрактически полный скелет травоядного динозавра.\r\nЧем ещё примечательна эта местность?";
                    But4.Content = "Ответить";
                    TBA.Text = "Миграцией динозавров из Европы в Азию";
                    TBAQ.Text = "В этой местности 65 миллионов лет тому назад\r\nпролегал путь миграции динозавров из Европы в Азию";
                    AllQType2();
                    txt2();
                    break;
                case 40:
                    TBQ.Text = "В каком месте города Пскова Псковской области\r\nНиколай II отрекся от престола?";
                    But1.Content = "В Псковском кремле";
                    But2.Content = "На железнодорожном вокзале г.Пскова";
                    But3.Content = "В Свято-Троицком кафедральном соборе г.Пскова";
                    TBA.Text = "В Псковском кремле";
                    TBAQ.Text = "2 марта 1917 года (по новому стилю – 15 марта)\r\nпоследний император Российской империи Николай II\r\nотрёкся от престола.И произошло это в Пскове – в царском вагоне, стоявшем на железнодорожном вокзале";
                    AllQType3();
                    txt2();
                    break;
                case 41:
                    TBQ.Text = "В каком городе родился адмирал Михаил Петрович Лазарев,\r\nпервооткрыватель Антарктиды?";
                    But4.Content = "Ответить";
                    TBA.Text = "Владимир";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType2();
                    txt2();
                    break;
                case 42:
                    TBQ.Text = "Правда ли, что в Архангельской области\r\nникогда не было крепостного права?";
                    But1.Content = "Да";
                    But3.Content = "Нет";
                    TBA.Text = "Да";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType1();
                    txt2();
                    break;
                case 43:
                    TBQ.Text = "Как петербуржцы узнают, что наступил полдень?";
                    But4.Content = "Ответить";
                    TBA.Text = "Выстрел пушки Петропавловской крепости";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType2();
                    txt2();
                    break;
                case 44:
                    TBQ.Text = "Губернатором какой губернии был прапрадед А.С.Пушкина\r\nЮрий Алексеевич Ржевский?";
                    But4.Content = "Ответить";
                    TBA.Text = "Нижегородская губерния";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType2();
                    txt2();
                    break;
                case 45:
                    TBQ.Text = "Правда, что родина самого известного народного промысла – Дымковской игрушки – Кировская область?";
                    But1.Content = "Да";
                    But3.Content = "Нет";
                    TBA.Text = "Да";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType1();
                    txt2();
                    break;
                case 46:
                    TBQ.Text = "Бывший дом и могила какого известного писателя\r\nнаходится в усадьбе «Ясная Поляна» в Туле?";
                    But4.Content = "Ответить";
                    TBA.Text = "Лев Николаевич Толстой";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType2();
                    txt2();
                    break;
                case 47:
                    TBQ.Text = "Закончите легенду: «Как гласит легенда,\r\nкогда Орловский кремль города был завершён\r\nи готов к сдаче, Великий князь Ивана IV\r\nлично приехал принимать военный объект.\r\nИ вот ходит по крепости Иван Грозный с воеводой,\r\nосматривает всё: на совесть ли сделано, аль схалтурил ли кто.\r\nПроходит мимо раскатистого многовекового дуба,\r\nи вдруг на ветвь могучего дерева . . . и грозно\r\nсмотрит на кремль! - А вот и хозяин,— сказал Иван Васильевич.";
                    But4.Content = "Ответить";
                    TBA.Text = "Садится орёл";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType2();
                    txt2();
                    break;

                case 48:
                    TBQ.Text = "Из какого города был Садко?";
                    But1.Content = "Новгород";
                    But2.Content = "Москва";
                    But3.Content = "Ростов";
                    TBA.Text = "Новгород";
                    TBAQ.Text = "Ай же ты, Садко Новгородский!\r\nНе знаю, чем буде тебя пожаловать\r\nЗа твои утехи за великия,\r\nАль бессчётной золотой казной?";
                    AllQType1();
                    txt2();
                    break;

                case 49:
                    TBQ.Text = "У тувинцев один из самых крупных праздников – Шагаа.\r\nЧто это за праздник?";
                    But1.Content = "Новый год";
                    But2.Content = "Национальный праздник тувинского народа";
                    But3.Content = "Национальный праздник солнца";
                    TBA.Text = "Новый год";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType3();
                    txt2();
                    break;

                case 50:
                    TBQ.Text = "В России Бурятия, Тува, Алтайский край и этот субъект\r\nявляются регионами, где наиболее широко\r\nраспространён буддизм.Но географически\r\nэтот субъект относится не к Азии, а к Европе.\r\nЧто это за регион?";
                    But4.Content = "Ответить";
                    TBA.Text = "Республика Калмыкия";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType2();
                    txt2();
                    break;
                case 51:
                    TBQ.Text = "Родина Дедушки Мороза";
                    But4.Content = "Ответить";
                    TBA.Text = "Великий устюг";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType2();
                    txt3();
                    break;

                case 52:
                    TBQ.Text = "В каком городе находится самая большая коллекция янтаря в России?";
                    But4.Content = "Ответить";
                    TBA.Text = "Кострома";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType2();
                    txt3();
                    break;

                case 53:
                    TBQ.Text = "Какой город считается родиной Снегурочки?";
                    But4.Content = "Ответить";
                    TBA.Text = "Калининград";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType2();
                    txt3();
                    break;

                case 54:
                    TBQ.Text = "Правда, что водяная мельница в селе Красниково, Курская область, была построена в 1861 году, без единого гвоздя.";
                    But1.Content = "Да";
                    But3.Content = "Нет";
                    TBA.Text = "Да";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType1();
                    txt3();
                    break;
                case 55:
                    TBQ.Text = "Правда, что Морской порт Мурманска,\r\nрасположенный за Полярным кругом, никогда не замерзает?";
                    But1.Content = "Да";
                    But3.Content = "Нет";
                    TBA.Text = "Да";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType1();
                    txt3();
                    break;

                case 56:
                    TBQ.Text = "Что такое посикунчики?";
                    But4.Content = "Ответить";
                    TBA.Text = "Пермские жареные пирожки";
                    TBAQ.Text = "Пермские жареные пирожки на один-два укуса";
                    AllQType2();
                    txt3();
                    break;
                case 57:
                    TBQ.Text = "На флаге и гербе этого региона и его административного центра расположились три стерляди.\r\nГеографически регион расположен в Нижнем Поволжье,\r\nа сам город стоит на берегу Волги. Про какой регион и город идет речь?";
                    But4.Content = "Ответить";
                    TBA.Text = "Саратовская область, Саратов";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType2();
                    txt3();
                    break;

                case 58:
                    TBQ.Text = "В каком городе из-за особенностей рельефной структуры\r\nмного лестниц, а не улиц?";
                    But4.Content = "Ответить";
                    TBA.Text = "Севастополь";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType2();
                    txt3();
                    break;

                case 59:
                    TBQ.Text = "Правда, что в Тамбове находится вторая\r\nпо высоте колокольня в России?.";
                    But1.Content = "Да";
                    But3.Content = "Нет";
                    TBA.Text = "Да";
                    TBAQ.Text = "Правда, Казанский Богородичный монастырь";
                    AllQType1();
                    txt3();
                    break;
                case 60:
                    TBQ.Text = "Родина Дмитрия Ивановича Менделеева";
                    But4.Content = "Ответить";
                    TBA.Text = "Тюменьская область";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType2();
                    txt3();
                    break;

                case 61:
                    TBQ.Text = "Как по-чувашски будет «да»?";
                    But4.Content = "Ответить";
                    TBA.Text = "Нет такого слова";
                    TBAQ.Text = "В чувашском языке нет слова «да», положительным ответом на вопрос будет повтор глагола";
                    AllQType2();
                    txt3();
                    break;
                case 62:
                    TBQ.Text = "Эта ягода созревает как бы «наоборот» — сначала краснеет, а потом желтеет.Что это за ягода?";
                    But4.Content = "Ответить";
                    TBA.Text = "Морошка";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType2();
                    txt3();
                    break;

                case 63:
                    TBQ.Text = "В каком регионе находится «город миллиона роз»?";
                    But4.Content = "Ответить";
                    TBA.Text = "Донецкая народная республика";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType2();
                    txt3();
                    break;
                case 64:
                    TBQ.Text = "Ценный поделочный камень, который использовали в облицовке станций метро «Сокол», «Аэропорт», «Белорусская» г.Москва.";
                    But4.Content = "Ответить";
                    TBA.Text = "Бираканский розовый мрамор";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType2();
                    txt3();
                    break;

                case 65:
                    TBQ.Text = "Кто первым установил рекорд на производстве:\r\nТкачихи Виноградовы или шахтер Стаханов,\r\nв честь которого и названо движение?";
                    But4.Content = "Ответить";
                    TBA.Text = "Виноградовыми";
                    TBAQ.Text = "В мае 1935 года установлен рекорд по количеству обслуживания станков ткачихами Виноградовыми.\r\nА рекорд Стаханова датирован только августом 1935 года.";
                    AllQType2();
                    txt3();
                    break;
                case 66:
                    TBQ.Text = "Какое уникальное явление на территории Красноярского края произошло 30 июня 1908 года?\r\nПо числу легенд, связанных с этим явлением, может сравниться лишь мифологическая Атлантида и чудовище из озера Лох-Несс.";
                    But4.Content = "Ответить";
                    TBA.Text = "Падение Тунгусского метеорита";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType2();
                    txt3();
                    break;
                case 67:
                    TBQ.Text = "Правда ли, что в Ненецкого автономного округа на каждого жителя приходится по 4 оленя?";
                    But1.Content = "Да";
                    But3.Content = "Нет";
                    TBA.Text = "Да";
                    TBAQ.Text = "Да, в тундре Ненецкого автономного округа проживает 168 тысяч оленей, что в 4 раза больше, чем людей";
                    AllQType1();
                    txt3();
                    break;

                case 68:
                    TBQ.Text = "142 кг этого продукта было изготовлено на фестивале,\r\nпосвященному гастрономическому символу Адыгеи.\r\nВы встречаете его на прилавках магазинов в малосольном,\r\nкопчёном, сыром виде, со специями и без.\r\nЧто это за продукт?";
                    But4.Content = "Ответить";
                    TBA.Text = "Адыгейский сыр";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType2();
                    txt3();
                    break;
                case 69:
                    TBQ.Text = "Какой город изображён на купюре номиналом 1000 рублей?";
                    But4.Content = "Ответить";
                    TBA.Text = "Ярославль";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType2();
                    txt3();
                    break;

                case 70:
                    TBQ.Text = "Правда ли, что жители Чукотки могут одновременно находиться в сегодняшнем дне и завтрашнем?";
                    But1.Content = "Да";
                    But3.Content = "Нет";
                    TBA.Text = "Да";
                    TBAQ.Text = "Правда, через восточную часть Чукотки проходит 180 меридиан,\r\nТлиния перемены дат.Именно здесь начинаются каждые новые сутки";
                    AllQType1();
                    txt3();
                    break;
                case 71:
                    TBQ.Text = "Мечта любого школьника— отдых в самом знаменитом детском лагере в Крыму.\r\nТКак он называется?";
                    But4.Content = "Ответить";
                    TBA.Text = "Артек";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType2();
                    txt3();
                    break;

                case 72:
                    TBQ.Text = "Какой памятник является покровителем и символом\r\nстолицы Республики Марий Эл\r\n(столица – город Йошкар-Ола)?\r\nПодсказка: так еще называют\r\nкота Бабы Яги, кота Баюна.";
                    But4.Content = "Ответить";
                    TBA.Text = "Йошкин кот";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType2();
                    txt3();
                    break;
                case 73:
                    TBQ.Text = "Что символизируют три пирога\r\nв этно-религиозной обрядности осетин?";
                    But4.Content = "Ответить";
                    TBA.Text = "Древнейшая троичная модель мира";
                    TBAQ.Text = "Древнейшая троичная модель мира – солнце, вода, земля";
                    AllQType2();
                    txt3();
                    break;

                case 74:
                    TBQ.Text = "Этот город по праву считается «Бриллиантовой\r\nстолицей России».";
                    But4.Content = "Ответить";
                    TBA.Text = "Смоленск";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType2();
                    txt3();
                    break;
                case 75:
                    TBQ.Text = "Происхождение какого блюда связано с владельцем\r\nтрактира в Торжке в начале XIX век?";
                    But4.Content = "Ответить";
                    TBA.Text = "Пожарские котлеты";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType2();
                    txt3();
                    break;

                case 76:
                    TBQ.Text = "Возле какого известного театра в Москве\r\nвстречаются магаданцы каждый год 31 августа?";
                    But4.Content = "Ответить";
                    TBA.Text = "Большой Театр";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType2();
                    txt3();
                    break;
                case 77:
                    TBQ.Text = "Правда ли, что в Рязани есть Переулок счастья?";
                    But1.Content = "Да";
                    But3.Content = "Нет";
                    TBA.Text = "Да";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType1();
                    txt3();
                    break;

                case 78:
                    TBQ.Text = "Какой динозавр обитал только в Кузбассе?";
                    But4.Content = "Ответить";
                    TBA.Text = "Пситтакозавр";
                    TBAQ.Text = "Пситтакозавр";
                    AllQType2();
                    txt3();
                    break;
                case 79:
                    TBQ.Text = "Как называется удмуртский Дед Мороз?";
                    But1.Content = "Тол Бабай";
                    But2.Content = "Сагаан Убургун";
                    But3.Content = "Ямал Ири";
                    TBA.Text = "Тол Бабай";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType3();
                    txt3();
                    break;

                case 80:
                    TBQ.Text = "Супруга какого известного художника родилась в Казани?";
                    But4.Content = "Ответить";
                    TBA.Text = "Сальвадор Дали";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType2();
                    txt3();
                    break;
                case 81:
                    TBQ.Text = "Какой город был прототипом города «N»\r\nв пьесе «Ревизор» Н. В. Гоголя?";
                    But4.Content = "Ответить";
                    TBA.Text = "Пенза";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType2();
                    txt3();
                    break;

                case 82:
                    TBQ.Text = "На территории какой области находится самое большое\r\nколичество памятников деревянного зодчества?";
                    But4.Content = "Ответить";
                    TBA.Text = "Томская область";
                    TBAQ.Text = "Томская область, около 2000 домов";
                    AllQType2();
                    txt3();
                    break;

                case 83:
                    TBQ.Text = "Какой Всероссийский фестиваль проводится\r\nв Самарской области с 1968 года.";
                    But4.Content = "Ответить";
                    TBA.Text = "Грушинский фестиваль бардовской песни";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType2();
                    txt3();
                    break;
                case 84:
                    TBQ.Text = "Всем известный напиток кефир был получен горцами,\r\nблагодаря попаданию в молоко кефирного грибка\r\nв горной местности вблизи Эльбруса.\r\nКефирные грибки настолько ценились местными народами,\r\nчто их использовали в качестве валюты\r\nпри обмене на другие товары.\r\nКакой регион является родиной кефира?";
                    But4.Content = "Ответить";
                    TBA.Text = "Карачаево-Черкесская Республика";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType2();
                    txt3();
                    break;
                case 85:
                    TBQ.Text = "Как за его большие размеры называют в народе здание\r\nНовосибирского театра оперы и балета?";
                    But1.Content = "Сибирский Колизей";
                    But2.Content = "Сибирский Большой театр";
                    But3.Content = "Сибирское чудо света";
                    TBA.Text = "Сибирский Колизей";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType3();
                    txt3();
                    break;
                case 86:
                    TBQ.Text = "Какой праздник ежегодно отмечается\r\n1 марта в Республике Ингушетия?";
                    But1.Content = "День уважительного отношения к старшим";
                    But2.Content = "День джигита";
                    But3.Content = "День айра (День пастилы)";
                    TBA.Text = "День джигита";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType3();
                    txt3();
                    break;
                case 87:
                    TBQ.Text = "В городе Грозном несколько лет назад\r\nстроили башню Олимп, но при строительстве\r\nтам произошел пожар.\r\nПосле восстановления ее переименовали в ...";
                    But1.Content = "Феникс";
                    But2.Content = "Возрождение";
                    But3.Content = "Сердце Чечни";
                    TBA.Text = "Феникс";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    AllQType3();
                    txt3();
                    break;

            }
        }
        

        public void txt2()
        {
            TBInfo.Text = "Вопрос по истории";
        }

        public void txt3()
        {
            TBInfo.Text = "Вопрос по интересным фактам";
        }


        private void StartGame()
        {
            var randomlist = QuestionNumbersHistoryFacts.OrderBy(a => Guid.NewGuid()).ToList();

            QuestionNumbersHistoryFacts = randomlist;


            for (int i = 0; i < QuestionNumbersHistoryFacts.Count; i++)
            {
                QuestionNumbersHistoryFacts[i].ToString();
            }

        }

        private void AllQType1()
        {
            But1.Visibility = Visibility.Visible;
            But3.Visibility = Visibility.Visible;
            But2.Visibility = Visibility.Hidden;
            But4.Visibility = Visibility.Hidden;

            TBAB.Visibility = Visibility.Hidden;
        }

        private void AllQType2()
        {
            But4.Visibility = Visibility.Visible;
            TBAB.Visibility = Visibility.Visible;
            But1.Visibility = Visibility.Hidden;
            But2.Visibility = Visibility.Hidden;
            But3.Visibility = Visibility.Hidden;

        }

        private void AllQType3()
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
