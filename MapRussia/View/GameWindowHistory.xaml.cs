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
    /// Логика взаимодействия для GameWindowHistory.xaml
    /// </summary>
    public partial class GameWindowHistory : Window
    {
        public List<int> QuestionNumbersHistory = new List<int> { 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, };
        public int score = 0;
        int qnum = 0;

        int i;

        private void RestartGame()
        {
            qnum = -1;
            i = 0;
            StartGame();
        }

        public GameWindowHistory()
        {
            InitializeComponent();
            StartGame();
            NextQuestionHistory();
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
                    NextQuestionHistory();
                    NextPlayerMove();
                }

                else
                {
                    MessageBox.Show("Ответ неправильный!\n" + "Правильный будет: " + TBA.Text.ToString() + $"\n\n({TBAQ.Text.ToString()})");
                    NextQuestionHistory();
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
                    NextQuestionHistory();
                    NextPlayerMove();


                }
                else
                {
                    MessageBox.Show("Ответ неправильный!\n" + "Правильный будет: " + TBA.Text.ToString() + $"\n\n({TBAQ.Text.ToString()})");
                    NextQuestionHistory();
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
                    NextQuestionHistory();
                    NextPlayerMove();

                }
                else
                {
                    MessageBox.Show("Ответ неправильный!\n" + "Правильный будет: " + TBA.Text.ToString() + $"\n\n({TBAQ.Text.ToString()})");
                    NextQuestionHistory();
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
                    NextQuestionHistory();
                    NextPlayerMove();
                    TBAB.Text = "";
                }
                else
                {
                    MessageBox.Show("Ответ неправильный!\n" + "Правильный будет: " + TBA.Text.ToString() + $"\n\n({TBAQ.Text.ToString()})");
                    NextQuestionHistory();
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




        private void NextQuestionHistory()
        {
            if (qnum < QuestionNumbersHistory.Count)
            {
                i = QuestionNumbersHistory[qnum];
            }
            else
            {
                RestartGame();
            }

            switch (i)
            {
                case 27:
                    TBQ.Text = "Какой город является колыбелью российского кораблестроения?";
                    But4.Content = "Ответить";
                    TBA.Text = "Воронеж";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    HistoryType2();
                    
                    break;

                case 28:
                    TBQ.Text = "Правда, что Обнинская АЭС - первая в мире атомная электростанция?";
                    But1.Content = "Да";
                    But3.Content = "Нет";
                    TBA.Text = "Да";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    HistoryType1();
                    
                    break;

                case 29:
                    TBQ.Text = "Правда, что на территории Краснодарского края\r\nесть необычные каменные сооружения – дольмены,\r\nкоторые датируются от IV до II тысячелетия до н. э.?";
                    But1.Content = "Да";
                    But3.Content = "Нет";
                    TBA.Text = "Да";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    HistoryType1();
                    
                    break;
                case 30:
                    TBQ.Text = "На территории какого региона находится единственный в России средневековый замок?";
                    But4.Content = "Ответить";
                    TBA.Text = "Ленинградская область";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    HistoryType2();
                    
                    break;
                case 31:
                    TBQ.Text = "Правда, что Омск был столицей Российской Империи?";
                    But1.Content = "Да";
                    But3.Content = "Нет";
                    TBA.Text = "Да";
                    TBAQ.Text = "Правда, но недолго, всего пару лет";
                    HistoryType1();
                    
                    break;
                case 32:
                    TBQ.Text = "Правда, что Оренбургскую область и её столицу населяют представители более 120 этносов?";
                    But1.Content = "Да";
                    But3.Content = "Нет";
                    TBA.Text = "Да";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    HistoryType1();
                    
                    break;
                case 33:
                    TBQ.Text = "Какой регион является центром буддизма в России?";
                    But4.Content = "Ответить";
                    TBA.Text = "Республика Бурятия";
                    TBAQ.Text = "Республика Бурятия, Иволгинский дацан";
                    HistoryType2();
                    
                    break;

                case 34:
                    TBQ.Text = "Символом какого региона является «Красная лиса»?";
                    But4.Content = "Ответить";
                    TBA.Text = "Республика Мордовия";
                    TBAQ.Text = "Республика Мордовия, образ используется в качестве символа и является исторической составляющей социокультурного имиджа столицы региона";
                    HistoryType2();
                    
                    break;
                case 35:
                    TBQ.Text = "Этот город занимает 10 место из 15 городов-миллионников России по количеству жителей.\r\nА еще его традиционно называют вратами на юг России и Северный Кавказ, \r\nкогда в современности это уже не так.\r\nКакой это город и центром какого региона он является?";
                    But4.Content = "Ответить";
                    TBA.Text = "Ростовская область, Ростов-на-Дону";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    HistoryType2();
                    
                    break;
                case 36:
                    TBQ.Text = "В каком регионе родился первый президент России Борис Ельцин?";
                    But4.Content = "Ответить";
                    TBA.Text = "Свердловская область";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    HistoryType2();
                    
                    break;
                case 37:
                    TBQ.Text = "Какой регион является родиной южных слонов?";
                    But4.Content = "Ответить";
                    TBA.Text = "Ставропольский край";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    HistoryType2();
                    
                    break;
                case 38:
                    TBQ.Text = "Какое животное изображено на гербе Челябинской области и почему?";
                    But4.Content = "Ответить";
                    TBA.Text = "Верблюд, торговые взаимоотношения с соседними казахами и киргизами";
                    TBAQ.Text = "Верблюд, навьюченный товаром - символ торговли, связано с торговыми взаимоотношениями с соседними казахами и киргизами";
                    HistoryType2();
                    
                    break;
                case 39:
                    TBQ.Text = "Кундурское месторождение динозавров в Амурской области исследовано в середине 20 века. \r\nБыл обнаружен практически полный скелет травоядного динозавра.\r\nЧем ещё примечательна эта местность?";
                    But4.Content = "Ответить";
                    TBA.Text = "Миграция динозавров из Европы в Азию";
                    TBAQ.Text = "В этой местности 65 миллионов лет тому назад пролегал путь миграции динозавров из Европы в Азию";
                    HistoryType2();
                    
                    break;
                case 40:
                    TBQ.Text = "В каком месте города Пскова Псковской области Николай II отрекся от престола?";
                    But1.Content = "В Псковском кремле";
                    But2.Content = "На железнодорожном вокзале г.Пскова";
                    But3.Content = "В Свято-Троицком кафедральном соборе г.Пскова";
                    TBA.Text = "В Псковском кремле";
                    TBAQ.Text = "2 марта 1917 года (по новому стилю – 15 марта) последний император Российской империи Николай II отрёкся от престола.\r\nИ произошло это в Пскове – в царском вагоне,  стоявшем на железнодорожном вокзале";
                    HistoryType3();
                    
                    break;
                case 41:
                    TBQ.Text = "В каком городе родился адмирал Михаил Петрович Лазарев, первооткрыватель Антарктиды?";
                    But4.Content = "Ответить";
                    TBA.Text = "Владимир";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    HistoryType2();
                    
                    break;
                case 42:
                    TBQ.Text = "Правда ли, что в Архангельской области никогда не было крепостного права?";
                    But1.Content = "Да";
                    But3.Content = "Нет";
                    TBA.Text = "Да";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    HistoryType1();
                    
                    break;
                case 43:
                    TBQ.Text = "Как петербуржцы узнают, что наступил полдень?";
                    But4.Content = "Ответить";
                    TBA.Text = "Выстрел пушки Петропавловской крепости";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    HistoryType2();
                    
                    break;
                case 44:
                    TBQ.Text = "Губернатором какой губернии был прапрадед А.С.Пушкина Юрий Алексеевич Ржевский?";
                    But4.Content = "Ответить";
                    TBA.Text = "Нижегородская губерния";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    HistoryType2();
                    
                    break;
                case 45:
                    TBQ.Text = "Правда, что родина самого известного народного промысла – Дымковской игрушки – Кировская область?";
                    But1.Content = "Да";
                    But3.Content = "Нет";
                    TBA.Text = "Да";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    HistoryType1();
                    
                    break;
                case 46:
                    TBQ.Text = "Бывший дом и могила какого известного писателя находится в усадьбе «Ясная Поляна» в Туле?";
                    But4.Content = "Ответить";
                    TBA.Text = "Лев Николаевич Толстой";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    HistoryType2();
                    
                    break;
                case 47:
                    TBQ.Text = "Закончите легенду: «Как гласит легенда,\r\nкогда Орловский кремль города был завершён и готов к сдаче, \r\nВеликий князь Ивана IV лично приехал принимать военный объект.\r\nИ вот ходит по крепости Иван Грозный с воеводой,осма тривает всё: \r\nна совесть ли сделано, аль схалтурил ли кто.\r\nПроходит мимо раскатистого многовекового дуба, и вдруг на ветвь могучего дерева \r\n. . . и грозно смотрит на кремль! \r\n- А вот и хозяин,— сказал Иван Васильевич.";
                    But4.Content = "Ответить";
                    TBA.Text = "Садится орёл";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    HistoryType2();
                    
                    break;

                case 48:
                    TBQ.Text = "Из какого города был Садко?";
                    But1.Content = "Новгород";
                    But2.Content = "Москва";
                    But3.Content = "Ростов";
                    TBA.Text = "Новгород";
                    TBAQ.Text = "Ай же ты, Садко Новгородский!\r\nНе знаю, чем буде тебя пожаловать\r\nЗа твои утехи за великия,\r\nАль бессчётной золотой казной?";
                    HistoryType1();
                    
                    break;

                case 49:
                    TBQ.Text = "У тувинцев один из самых крупных праздников – Шагаа.\r\nЧто это за праздник?";
                    But1.Content = "Новый год";
                    But2.Content = "Национальный праздник тувинского народа";
                    But3.Content = "Национальный праздник солнца";
                    TBA.Text = "Новый год";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    HistoryType3();
                    
                    break;

                case 50:
                    TBQ.Text = "В России Бурятия, Тува, Алтайский край и этот субъект являются регионами, \r\nгде наиболее широко распространён буддизм. \r\nНо географически этот субъект относится не к Азии, а к Европе.\r\nЧто это за регион?";
                    But4.Content = "Ответить";
                    TBA.Text = "Республика Калмыкия";
                    TBAQ.Text = "Дополнительной информации по этому вопросу нет";
                    HistoryType2();
                    
                    break;
            }
        }

        private void StartGame()
        {
            var randomlist = QuestionNumbersHistory.OrderBy(a => Guid.NewGuid()).ToList();

            QuestionNumbersHistory = randomlist;


            for (int i = 0; i < QuestionNumbersHistory.Count; i++)
            {
                QuestionNumbersHistory[i].ToString();
            }

        }

        private void HistoryType1()
        {
            But1.Visibility = Visibility.Visible;
            But3.Visibility = Visibility.Visible;
            But2.Visibility = Visibility.Hidden;
            But4.Visibility = Visibility.Hidden;

            TBAB.Visibility = Visibility.Hidden;
        }

        private void HistoryType2()
        {
            But4.Visibility = Visibility.Visible;
            TBAB.Visibility = Visibility.Visible;
            But1.Visibility = Visibility.Hidden;
            But2.Visibility = Visibility.Hidden;
            But3.Visibility = Visibility.Hidden;

        }

        private void HistoryType3()
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
