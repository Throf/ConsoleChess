using BoardMain;

namespace Chess
{
    class Bishop : Piece
    {
        public Bishop(Colors color, Board board) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "Bp";
        }
    }
}