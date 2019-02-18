using System.Collections.Generic;

namespace BattleSnake2019
{
    public class Board
    {
        public long Height { get; set; }
        public long Width { get; set; }
        public List<Position> Food { get; set; }
        public List<Snake> Snakes { get; set; }

        public Board()
        {
            this.Food = new List<Position>();
            this.Snakes = new List<Snake>(); 
        }
        
    }
}