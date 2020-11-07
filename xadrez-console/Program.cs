using System;
using BoardMain;
using Chess;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);

            board.PutPiece(new Bishop(Colors.Black, board), new Position(0,4));
            board.PutPiece(new Pawn(Colors.Black, board), new Position(0, 2));
            board.PutPiece(new King(Colors.Black, board), new Position(0, 3));

            board.PutPiece(new Bishop(Colors.White, board), new Position(1, 3));

            Screen.PrintBoard(board);
        }
    }
}
