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

        public void DecreaseMovement() 
        {
            AmountMovement--;
        }
        public bool ExistPossibleMoves()
        {
            bool[,] mat = PossibleMoves();

            for (int i = 0; i < Board.Lines; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool CanMoveFor(Position pos)
        {
            return PossibleMoves()[pos.Line, pos.Column];
        }
        public abstract bool[,] PossibleMoves();
    }
}
