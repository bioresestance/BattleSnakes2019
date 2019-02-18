using System.Collections.Generic;

namespace BattleSnake2019
{
    public class Snake
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public long Health { get; set; }
        public List<Position> Body { get; set; }

        public Snake()
        {
            this.Body = new List<Position>();
        }
    }
}