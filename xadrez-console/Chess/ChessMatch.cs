using BoardMain;

namespace Chess
{
    public class ChessMatch
    {
        private int _Turn;
        private Colors _ActualPlayer;
        public Board Board { get; private set; }
        public bool ended { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            _Turn = 1;
            _ActualPlayer = Colors.White;
            ended = false;
            PutPiece();
        }

        public void ExecuteMoviment(Position origin, Position destination)
        {
            Piece p = Board.RemovePiece(origin);
            p.IncreaseMovement();
            Piece capturedPiece = Board.RemovePiece(destination);
            Board.PutPiece(p, destination);
        }

        private void PutPiece()
        {
            Board.PutPiece(new Pawn(Colors.Black, Board), new ChessPosition('c', 1).ToPosition());
            Board.PutPiece(new King(Colors.White, Board), new ChessPosition('d', 4).ToPosition());
            Board.PutPiece(new Queen(Colors.Black, Board), new ChessPosition('a', 3).ToPosition());
        }
    }
}