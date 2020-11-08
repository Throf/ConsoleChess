using System;
using BoardMain;
using Chess;

namespace xadrez_console
{
    class dProgram
    {
        static void Main(string[] args)
        {
            try
            {
                ChessMatch chessMatch = new ChessMatch();

                while (!chessMatch.ended)
                {
                    Console.Clear();
                    Screen.PrintBoard(chessMatch.Board);

                    Console.WriteLine();
                    
                    Console.Write("Origin: ");
                    Position origin = Screen.ReadChessPosition().ToPosition();
                    Console.Write("Destination: ");
                    Position destination = Screen.ReadChessPosition().ToPosition();
                    
                    chessMatch.ExecuteMoviment(origin, destination);
                }
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
    }
}
