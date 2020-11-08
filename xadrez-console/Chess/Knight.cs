using BoardMain;

namespace Chess
{
    class Knight : Piece
    {
        public Knight(Colors color, Board board) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "Kt";
        }
        
        public override bool[,] PossibleMoves()
        {
            throw new System.NotImplementedException();
        }
    }
}
