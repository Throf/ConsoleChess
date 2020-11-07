using BoardMain;

namespace Chess
{
    class Rook : Piece
    {
        public Rook(Colors color, Board board) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "Rk";
        }
    }
}
