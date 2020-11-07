using System;
using BoardMain;
using Chess;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Board board = new Board(8, 8);

                board.PutPiece(new King(Colors.Black, board), new Position(0, 3));
                board.PutPiece(new Pawn(Colors.Black, board), new Position(3, 4));
                board.PutPiece(new Knight(Colors.Black, board), new Position(0, 5));

                Screen.PrintBoard(board);
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
