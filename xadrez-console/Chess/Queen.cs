using BoardMain;

namespace Chess
{
    class Queen : Piece
    {
        public Queen(Colors color, Board board) : base(color, board) 
        { 
        }

        public override string ToString()
        {
            return "Qn";
        }
    }
}
