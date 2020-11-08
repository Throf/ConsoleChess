using BoardMain;

namespace Chess
{
    class Pawn : Piece
    {
        public Pawn(Colors color, Board board) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "Pn";
        }
        
        public override bool[,] PossibleMoves()
        {
            throw new System.NotImplementedException();
        }
    }
}