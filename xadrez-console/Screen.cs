using System;
using System.Collections.Generic;
using BoardMain;
using Chess;

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
                    PrintPiece(board.Piece(i, j));
                }

                Console.WriteLine();
            }

            Console.WriteLine("  A  B  C  D  E  F  G  H");
        }
        
        public static void PrintBoard(Board board, bool[,] possiblePosisitons) 
        {
            for (int i = 0; i < board.Lines; i++)
            {
                ConsoleColor backgroundOriginal = Console.BackgroundColor;
                ConsoleColor backgroundAlternative = ConsoleColor.DarkGray;
                
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++) 
                {
                    if (possiblePosisitons[i, j])
                    {
                        Console.BackgroundColor = backgroundAlternative;
                    }
                    else
                    {
                        Console.BackgroundColor = backgroundOriginal;
                    }
                    PrintPiece(board.Piece(i, j));
                    Console.BackgroundColor = backgroundOriginal;
                }

                Console.WriteLine();
            }

            Console.WriteLine("  A  B  C  D  E  F  G  H");
        }
        public static void PrintPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.Write("-  ");
            }
            else
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
                Console.Write(" ");
            }
        }

        public static void PrintMatch(ChessMatch chessMatch)
        {
            PrintBoard(chessMatch.Board);
            Console.WriteLine();

            PrintCapturedPieces(chessMatch);
            Console.WriteLine();
            
            Console.WriteLine("Turn: " + chessMatch.Turn);
            Console.WriteLine("Waiting for: " + chessMatch.ActualPlayer);
            if (chessMatch.Check)
            {
                Console.WriteLine("CHECK!");
            }
        }

        public static void PrintCapturedPieces(ChessMatch chessMatch)
        {
            
            Console.WriteLine("Captured pieces: ");
            Console.Write("White: ");
            PrintGroup(chessMatch.CapturedPiece(Colors.White));
            Console.WriteLine();
            Console.Write("Black: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintGroup(chessMatch.CapturedPiece(Colors.Black));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void PrintGroup(HashSet<Piece> capturedPieces)
        {
            Console.Write("[ ");
            foreach (Piece x in capturedPieces)
            {
                Console.Write(x + " ");
            }
            Console.Write(" ]");
        }
        public static ChessPosition ReadChessPosition()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int line = int.Parse(s[1] + "");
            
            return new ChessPosition(column, line);
        }
    }
}
