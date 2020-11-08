using BoardMain;

namespace Chess
{
    public class ChessMatch
    {
        public  int Turn { get; private set; }
        public  Colors ActualPlayer { get; private set; }
        public Board Board { get; private set; }
        public bool ended { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            ActualPlayer = Colors.White;
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

        public void MakePlay(Position origin, Position destination)
        {
            ExecuteMoviment(origin, destination);
            Turn++;
            SwitchPlayer();
        }

        public void ValidateOrigin(Position pos)
        {
            if (Board.Piece(pos) == null)
            {
                throw new BoardException("There is no piece in this position!");
            }

            if (Board.Piece(pos).Color != ActualPlayer)
            {
                throw new BoardException("The piece chosen is not yours!");
            }

            if (!Board.Piece(pos).ExistPossibleMoves())
            {
                throw new BoardException("The chosen piece has no possible movements!");
            }
        }

        public void ValidateDestination(Position origin, Position destination)
        {
            if (!Board.Piece(origin).CanMoveFor(destination))
            {
                throw new BoardException("You cannot move to this position!");
            }
        }
        private void SwitchPlayer()
        {
            ActualPlayer = ActualPlayer == Colors.White ? ActualPlayer = Colors.Black : ActualPlayer = Colors.White;
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