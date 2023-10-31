using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapRussia.Classes
{
    public class Player
    {
        /// <summary>Имя игрока</summary>
        private string m_name;

        /// <summary>Количество текущих очков</summary>
        public int Points = 0;

        /// <summary>Имя игрока</summary>
        public string Name
        {
            get
            {
                return m_name;
            }
        }


        public Player(string name)
        {
            m_name = name;

            Points = 0;
        }
        
    }
}
