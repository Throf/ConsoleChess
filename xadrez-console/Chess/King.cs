using System;
using BoardMain;

namespace Chess
{
    class King : Piece
    {
        public King(Colors color, Board board) : base(color, board) 
        {
        }

        public override string ToString()
        {
            return "Kg";
        }
    }
}
