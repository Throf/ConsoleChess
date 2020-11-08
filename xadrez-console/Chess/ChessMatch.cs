using System.Collections.Generic;
using BoardMain;

namespace Chess
{
    public class ChessMatch
    {
        public  int Turn { get; private set; }
        public  Colors ActualPlayer { get; private set; }
        public Board Board { get; private set; }
        public bool Ended { get; private set; }

        private HashSet<Piece> _pieces;
        private HashSet<Piece> _capturedPiece;

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            ActualPlayer = Colors.White;
            Ended = false;
            _pieces = new HashSet<Piece>();
            _capturedPiece = new HashSet<Piece>();
            PutPiece();
        }

        public void ExecuteMoviment(Position origin, Position destination)
        {
            Piece p = Board.RemovePiece(origin);
            p.IncreaseMovement();
            Piece capturedPiece = Board.RemovePiece(destination);
            Board.PutPiece(p, destination);
            if (capturedPiece != null)
            {
                _capturedPiece.Add(capturedPiece);
            }
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

        public HashSet<Piece> CapturedPiece(Colors color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in _capturedPiece)
            {
                if (x.Color == color)
                {
                    aux.Add(x);
                }
            }

            return aux;
        }

        public HashSet<Piece> InGamePiece(Colors color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in _pieces)
            {
                if (x.Color == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(CapturedPiece(color));

            return aux;
        }
        public void InsertNewPiece(char column, int line, Piece piece)
        {
            Board.PutPiece(piece, new ChessPosition(column, line).ToPosition());
            _pieces.Add(piece);
        }
        private void PutPiece()
        {
            InsertNewPiece('c', 1, new Bishop(Colors.White,Board));
            InsertNewPiece('d', 1, new King(Colors.White,Board));
            InsertNewPiece('e', 1, new Bishop(Colors.White,Board));
            InsertNewPiece('c', 2, new Rook(Colors.White,Board));
            InsertNewPiece('d', 2, new Rook(Colors.White,Board));
            InsertNewPiece('e', 2, new Rook(Colors.White,Board));
            
            InsertNewPiece('c', 8, new Bishop(Colors.Black,Board));
            InsertNewPiece('d', 8, new King(Colors.Black,Board));
            InsertNewPiece('e', 8, new Bishop(Colors.Black,Board));
            InsertNewPiece('c', 7, new Rook(Colors.Black,Board));
            InsertNewPiece('d', 7, new Rook(Colors.Black,Board));
            InsertNewPiece('e', 7, new Rook(Colors.Black,Board));
            
        }
    }
}