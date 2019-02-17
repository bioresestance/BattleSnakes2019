namespace BattleSnake2019
{
    public class BattleSnakeRequest
    {
        public Game Game { get; set; }
        public long Turn { get; set; }
        public Board Board { get; set; }
        public Snake You { get; set; }
    }
}