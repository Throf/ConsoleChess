using System;
using System.Collections.Generic;
using BoardMain;

namespace Chess
{
    public class ChessMatch
    {
        private HashSet<Piece> _pieces;
        private HashSet<Piece> _capturedPiece;
        
        public Piece VulnerableEnPassant { get; private set; }
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
            VulnerableEnPassant = null;
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

            //#smallRoque

            if(p is King && destination.Column == origin.Column + 2) 
            {
                Position originR = new Position(origin.Line, origin.Column + 3);
                Position destinationR = new Position(origin.Line, origin.Column + 1);
                Piece R = Board.RemovePiece(originR);
                R.IncreaseMovement();
                Board.PutPiece(R, destinationR);
            }

            //#BigRoque

            if (p is King && destination.Column == origin.Column - 2)
            {
                Position originR = new Position(origin.Line, origin.Column - 4);
                Position destinationR = new Position(origin.Line, origin.Column - 1);
                Piece R = Board.RemovePiece(originR);
                R.IncreaseMovement();
                Board.PutPiece(R, destinationR);
            }
            
            //#EnPassant

            if (p is Pawn)
            {
                if (origin.Column != destination.Column && capturedPiece == null)
                {
                    Position posP;
                    if (p.Color == Colors.White)
                    {
                        posP = new Position(destination.Line + 1, destination.Column);
                    }
                    else
                    {
                        posP = new Position(destination.Line - 1, destination.Column);
                    }

                    capturedPiece = Board.RemovePiece(posP);
                    _capturedPiece.Add(capturedPiece);
                }
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
            
            //#smallRoque

            if (p is King && destination.Column == origin.Column + 2)
            {
                Position originR = new Position(origin.Line, origin.Column + 3);
                Position destinationR = new Position(origin.Line, origin.Column + 1);
                Piece R = Board.RemovePiece(destinationR);
                R.DecreaseMovement();
                Board.PutPiece(R, originR);
            }

            //#bigRoque

            if (p is King && destination.Column == origin.Column - 2)
            {
                Position originR = new Position(origin.Line, origin.Column - 4);
                Position destinationR = new Position(origin.Line, origin.Column - 1);
                Piece R = Board.RemovePiece(destinationR);
                R.DecreaseMovement();
                Board.PutPiece(R, originR);
            }
            
            //#EnPassant

            if (p is Pawn)
            {
                if (destination.Column != origin.Column && capturedPiece == VulnerableEnPassant)
                {
                    Piece pawn = Board.RemovePiece(destination);
                    Position posP;
                    if (p.Color == Colors.White)
                    {
                        posP = new Position(3, destination.Column);
                    }
                    else
                    {
                        posP = new Position(4, destination.Column);
                    }
                    Board.PutPiece(pawn, posP);
                }
            }
        }

        public void MakePlay(Position origin, Position destination)
        {
            Piece capturedPiece = ExecuteMoviment(origin, destination);

            if (IsInCheck(ActualPlayer)) 
            {
                RemakePlay(origin, destination, capturedPiece);
                throw new BoardException("You can't put yourself in check");
            }
            
            Piece p = Board.Piece(destination);
            
            //#Promotion

            if (p is Pawn)
            {
                if (p.Color == Colors.White && destination.Line == 0 || p.Color == Colors.Black && destination.Line == 7)
                {
                    p = Board.RemovePiece(destination);
                    _pieces.Remove(p);
                    
                    Console.Write("Promotion: ('Q'|'K'|'B'|'R')");
                    char promotion = char.Parse(Console.ReadLine());

                    switch (promotion)
                    {
                        case 'q':
                            p = new Queen(p.Color, Board);
                            break;
                        case 'k':
                            p = new Knight(p.Color, Board);
                            break;
                        case 'b':
                            p = new Bishop(ActualPlayer, Board);
                            break;
                        case 'r':
                            p = new Rook(ActualPlayer, Board);
                            break;
                        default:
                            p = new Queen(ActualPlayer, Board);
                            break;
                    }
                    
                    Board.PutPiece(p, destination);
                    _pieces.Add(p);
                }
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
            }
            else
            {
                Turn++;
                SwitchPlayer();
            }

            //#EnPassant
            if (p is Pawn && (destination.Line == origin.Line + 2) || (destination.Line == origin.Line - 2))
            {
                VulnerableEnPassant = p;
            }
            else
            {
                VulnerableEnPassant = null;
            }
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
            InsertNewPiece('a', 2, new Pawn(Colors.White, Board, this));
            InsertNewPiece('b', 2, new Pawn(Colors.White, Board, this));
            InsertNewPiece('c', 2, new Pawn(Colors.White, Board, this));
            InsertNewPiece('d', 2, new Pawn(Colors.White, Board, this));
            InsertNewPiece('e', 2, new Pawn(Colors.White, Board, this));
            InsertNewPiece('f', 2, new Pawn(Colors.White, Board, this));
            InsertNewPiece('g', 2, new Pawn(Colors.White, Board, this));
            InsertNewPiece('h', 2, new Pawn(Colors.White, Board, this));

            InsertNewPiece('a', 1, new Rook(Colors.White, Board));
            InsertNewPiece('b', 1, new Knight(Colors.White, Board));
            InsertNewPiece('c', 1, new Bishop(Colors.White, Board));
            InsertNewPiece('d', 1, new Queen(Colors.White, Board));
            InsertNewPiece('e', 1, new King(Colors.White, Board, this));
            InsertNewPiece('f', 1, new Bishop(Colors.White, Board));
            InsertNewPiece('g', 1, new Knight(Colors.White, Board));
            InsertNewPiece('h', 1, new Rook(Colors.White, Board));

            InsertNewPiece('a', 7, new Pawn(Colors.Black, Board, this));
            InsertNewPiece('b', 7, new Pawn(Colors.Black, Board, this));
            InsertNewPiece('c', 7, new Pawn(Colors.Black, Board, this));
            InsertNewPiece('d', 7, new Pawn(Colors.Black, Board, this));
            InsertNewPiece('e', 7, new Pawn(Colors.Black, Board, this));
            InsertNewPiece('f', 7, new Pawn(Colors.Black, Board, this));
            InsertNewPiece('g', 7, new Pawn(Colors.Black, Board, this));
            InsertNewPiece('h', 7, new Pawn(Colors.Black, Board, this));

            InsertNewPiece('a', 8, new Rook(Colors.Black, Board));
            InsertNewPiece('b', 8, new Knight(Colors.Black, Board));
            InsertNewPiece('c', 8, new Bishop(Colors.Black, Board));
            InsertNewPiece('d', 8, new Queen(Colors.Black, Board));
            InsertNewPiece('e', 8, new King(Colors.Black, Board, this));
            InsertNewPiece('f', 8, new Bishop(Colors.Black, Board));
            InsertNewPiece('g', 8, new Knight(Colors.Black, Board));
            InsertNewPiece('h', 8, new Rook(Colors.Black, Board));



        }
    }
}