using System.Collections.Generic;
using BoardMain;

namespace Chess
{
    public class ChessMatch
    {
        private HashSet<Piece> _pieces;
        private HashSet<Piece> _capturedPiece;
        

        public int Turn { get; private set; }
        public  Colors ActualPlayer { get; private set; }
        public Board Board { get; private set; }
        public bool Ended { get; private set; }
        public bool Check { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            ActualPlayer = Colors.White;
            Ended = false;
            _pieces = new HashSet<Piece>();
            _capturedPiece = new HashSet<Piece>();
            Check = false;
            PutPiece();
        }

        public Piece ExecuteMoviment(Position origin, Position destination)
        {
            Piece p = Board.RemovePiece(origin);
            p.IncreaseMovement();
            Piece capturedPiece = Board.RemovePiece(destination);
            Board.PutPiece(p, destination);
            if (capturedPiece != null)
            {
                _capturedPiece.Add(capturedPiece);
            }

            return capturedPiece;
        }

        public void RemakePlay(Position origin, Position destination, Piece capturedPiece)
        {
            Piece p = Board.RemovePiece(destination);
            p.DecreaseMovement();
            if(capturedPiece != null) 
            {
                Board.PutPiece(capturedPiece, destination);
                _capturedPiece.Remove(capturedPiece);
            }

            Board.PutPiece(p, origin);
        }

        public void MakePlay(Position origin, Position destination)
        {
            Piece capturedPiece = ExecuteMoviment(origin, destination);

            if (IsInCheck(ActualPlayer)) 
            {
                RemakePlay(origin, destination, capturedPiece);
                throw new BoardException("You can't put yourself in check");
            }
            if (IsInCheck(Adversary(ActualPlayer))) 
            {
                Check = true;
            }
            else
            {
                Check = false;
            }
            if (CheckMate(Adversary(ActualPlayer))) 
            {
                Ended = true;
                SwitchPlayer();
            }

            Turn++;
            SwitchPlayer();
        }

        

        private Colors Adversary(Colors color) 
        {
            if(color == Colors.White) 
            {
                return Colors.Black;
            }
            else 
            {
                return Colors.White;
            }
        }

        private Piece King(Colors color) 
        {
            foreach (Piece x in InGamePiece(color)) 
            {
                if(x is King) 
                {
                    return x;
                }
            }

            return null;
        }

        public bool IsInCheck(Colors color) 
        {
            Piece K = King(color);
            if(K == null) 
            {
                throw new BoardException("There's no King in the game");
            }

            foreach(Piece x in InGamePiece(Adversary(color))) 
            {
                bool[,] mat = x.PossibleMoves();
                if (mat[K.Position.Line, K.Position.Column]) 
                {
                    return true;
                }
            }

            return false;
        }

        public bool CheckMate(Colors color) 
        {
            if (!IsInCheck(color)) 
            {
                return false;
            }

            foreach (Piece x in InGamePiece(color)) 
            {
                bool[,] mat = x.PossibleMoves();
                for(int i = 0; i < Board.Lines; i++) 
                {
                    for(int j = 0; j < Board.Columns; j++) 
                    {
                        if (mat[i, j]) 
                        {
                            Position origin = x.Position;
                            Position destination = new Position(i, j);
                            Piece capturedPiece = ExecuteMoviment(origin, destination);
                            bool testCheck = IsInCheck(color);
                            RemakePlay(origin, destination, capturedPiece);
                            if (!testCheck) 
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
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
            InsertNewPiece('d', 1, new King(Colors.White, Board));
            InsertNewPiece('c', 1, new Rook(Colors.White, Board));
            InsertNewPiece('h', 7, new Rook(Colors.White, Board));

            InsertNewPiece('a', 8, new King(Colors.Black, Board));
            InsertNewPiece('b', 8, new Rook(Colors.Black, Board));
        }
    }
}