using System;
using BoardMain;

namespace xadrez_console
{
    class Screen
    {
        public static void PrintBoard(Board board) 
        {
            for (int i = 0; i < board.Lines; i++) 
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++) 
                {
                    if (board.Piece(i,j) == null) 
                    {
                        Console.Write("-  ");
                    }
                    else 
                    {
                        PrintPiece(board.Piece(i, j));
                        Console.Write(" ");
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine("  A  B  C  D  E  F  G  H");
        }

        public static void PrintPiece(Piece piece)
        {
            if (piece.Color == Colors.White)
            {
                Console.Write(piece);
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(piece);
                Console.ForegroundColor = aux;
            }
        }
    }
}
