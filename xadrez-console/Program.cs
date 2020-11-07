using System;
using BoardMain;
using Chess;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8,8);

            board.PutPiece(new King(Colors.Black, board), new Position(0,3));
            board.PutPiece(new Pawn(Colors.Black, board), new Position(3,4));

            Screen.PrintBoard(board);

        }
    }
}
