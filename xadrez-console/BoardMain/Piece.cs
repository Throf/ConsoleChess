namespace BoardMain
{
    public abstract class Piece
    {
        public Position Position { get; set; }
        public Colors Color{get; protected set;}
        public Board Board { get; protected set; }
        public int AmountMovement { get; protected set; }

        public Piece(Colors color, Board board) 
        {
            Position = null;
            Color = color;
            Board = board;
            AmountMovement = 0;
        }

        public void IncreaseMovement()
        {
            AmountMovement++;
        }

        public abstract bool[,] PossibleMoves();
    }
}
