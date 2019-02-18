namespace BattleSnake2019
{
    public class BattleSnakeRequest
    {
        public Game Game { get; set; }
        public long Turn { get; set; }
        public Board Board { get; set; }
        public Snake You { get; set; }

        public BattleSnakeRequest()
        {
            this.Game = new Game();
            this.Turn = 0;
            this.Board = new Board();
            this.You = new Snake();
        }
        
        
        
    }
}