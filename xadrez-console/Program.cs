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
                
                while (!chessMatch.Ended)
                {
                    try
                    {
                        Console.Clear();
                        Screen.PrintMatch(chessMatch);

                        Console.Write("Origin: ");
                        Position origin = Screen.ReadChessPosition().ToPosition();

                        chessMatch.ValidateOrigin(origin);
                        bool[,] possiblePositions = chessMatch.Board.Piece(origin).PossibleMoves();

                        Console.Clear();
                        Screen.PrintBoard(chessMatch.Board, possiblePositions);
                        Console.WriteLine();

                        Console.Write("Destination: ");
                        Position destination = Screen.ReadChessPosition().ToPosition();

                        chessMatch.ValidateDestination(origin, destination);

                        chessMatch.MakePlay(origin, destination);
                    }
                    catch (BoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Unexpected error: {0}!", e.Message);
                        Console.ReadLine();
                    }
                }
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
    }
}
