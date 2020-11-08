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
            Board.PutPiece(new Rook(Colors.White, Board), new ChessPosition('c', 2).ToPosition());
            Board.PutPiece(new Rook(Colors.White, Board), new ChessPosition('d', 2).ToPosition());
            Board.PutPiece(new Rook(Colors.White, Board), new ChessPosition('e', 2).ToPosition());
            Board.PutPiece(new Bishop(Colors.White, Board), new ChessPosition('c', 1).ToPosition());
            Board.PutPiece(new King(Colors.White, Board), new ChessPosition('d', 1).ToPosition());
            Board.PutPiece(new Bishop(Colors.White, Board), new ChessPosition('e', 1).ToPosition());
            
            Board.PutPiece(new Rook(Colors.Black, Board), new ChessPosition('c', 7).ToPosition());
            Board.PutPiece(new Rook(Colors.Black, Board), new ChessPosition('d', 7).ToPosition());
            Board.PutPiece(new Rook(Colors.Black, Board), new ChessPosition('e', 7).ToPosition());
            Board.PutPiece(new Bishop(Colors.Black, Board), new ChessPosition('c', 8).ToPosition());
            Board.PutPiece(new King(Colors.Black, Board), new ChessPosition('d', 8).ToPosition());
            Board.PutPiece(new Bishop(Colors.Black, Board), new ChessPosition('e', 8).ToPosition());
        }
    }
}