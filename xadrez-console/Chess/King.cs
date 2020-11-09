using System;
using BoardMain;

namespace Chess
{
    class King : Piece
    {
        private ChessMatch ChessMatch;
        public King(Colors color, Board board, ChessMatch chessMatch) : base(color, board) 
        {
            ChessMatch = chessMatch;
        }

        public override string ToString()
        {
            return "Kg";
        }

        private bool CanMove(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p == null || p.Color != Color;
        }

        private bool CanDoRoque(Position pos) 
        {
            Piece p = Board.Piece(pos);

            return p.AmountMovement == 0 && p is Rook && p != null && p.Color == Color;
        }
        
        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Lines,Board.Columns];
            Position pos = new Position(0, 0);
            
            //N
            pos.DefineValues(Position.Line - 1, Position.Column);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            //Ne
            pos.DefineValues(Position.Line - 1, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            //E
            pos.DefineValues(Position.Line, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            //Se
            pos.DefineValues(Position.Line + 1, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            //S
            pos.DefineValues(Position.Line + 1, Position.Column);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            //Sw
            pos.DefineValues(Position.Line + 1, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            //W
            pos.DefineValues(Position.Line, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            //Nw
            pos.DefineValues(Position.Line - 1, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            //ROQUE
            if(AmountMovement == 0 && !ChessMatch.Check) 
            {
                //#smallRoque
                Position posT1 = new Position(Position.Line, Position.Column + 3);
                if (CanDoRoque(posT1)) 
                {
                    Position p1 = new Position(Position.Line, Position.Column + 1);
                    Position p2 = new Position(Position.Line, Position.Column + 2);
                    if (Board.Piece(p1) == null && Board.Piece(p2) == null) 
                    {
                        mat[Position.Line, Position.Column + 2] = true;
                    }
                }
                //#bigRoque
                Position posT2 = new Position(Position.Line, Position.Column - 4);
                if (CanDoRoque(posT2))
                {
                    Position p1 = new Position(Position.Line, Position.Column - 1);
                    Position p2 = new Position(Position.Line, Position.Column - 2);
                    Position p3 = new Position(Position.Line, Position.Column - 3);
                    if (Board.Piece(p1) == null && Board.Piece(p2) == null && Board.Piece(p3) == null)
                    {
                        mat[Position.Line, Position.Column - 2] = true;
                    }
                }

            }

            return mat;
        }
    }
}
