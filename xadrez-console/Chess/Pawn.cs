using BoardMain;

namespace Chess
{
    class Pawn : Piece
    {
        private ChessMatch _chessMatch;
        public Pawn(Colors color, Board board, ChessMatch chessMatch) : base(color, board)
        {
            _chessMatch = chessMatch;
        }

        public override string ToString()
        {
            return "Pn";
        }
        
        private bool ExistAdversary(Position pos) 
        {
            Piece p = Board.Piece(pos);

            return p != null && p.Color != Color;
        }

        private bool Clear(Position pos)
        {
            return Board.Piece(pos) == null;
        }
        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];
            Position pos = new Position(0, 0);

            if(Color == Colors.White) 
            {
                pos.DefineValues(Position.Line - 1, Position.Column);
                if(Board.ValidPosition(pos) && Clear(pos)) 
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.DefineValues(Position.Line - 2, Position.Column);
                if (Board.ValidPosition(pos) && Clear(pos) && AmountMovement == 0)
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.DefineValues(Position.Line - 1, Position.Column - 1);
                if (Board.ValidPosition(pos) && ExistAdversary(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.DefineValues(Position.Line - 1, Position.Column + 1);
                if (Board.ValidPosition(pos) && ExistAdversary(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                
                //#En Passant
                if (Position.Line == 3)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    if (Board.ValidPosition(left) && ExistAdversary(left) && Board.Piece(left) == _chessMatch.VulnerableEnPassant)
                    {
                        mat[left.Line - 1, left.Column] = true;
                    }
                    
                    Position right = new Position(Position.Line, Position.Column + 1);
                    if (Board.ValidPosition(right) && ExistAdversary(right) && Board.Piece(right) == _chessMatch.VulnerableEnPassant)
                    {
                        mat[right.Line - 1, right.Column] = true;
                    }
                }
            }
            else 
            {
                pos.DefineValues(Position.Line + 1, Position.Column);
                if (Board.ValidPosition(pos) && Clear(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.DefineValues(Position.Line + 2, Position.Column);
                if (Board.ValidPosition(pos) && Clear(pos) && AmountMovement == 0)
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.DefineValues(Position.Line + 1, Position.Column + 1);
                if (Board.ValidPosition(pos) && ExistAdversary(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.DefineValues(Position.Line + 1, Position.Column - 1);
                if (Board.ValidPosition(pos) && ExistAdversary(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                
                if (Position.Line == 4)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    if (Board.ValidPosition(left) && ExistAdversary(left) && Board.Piece(left) == _chessMatch.VulnerableEnPassant)
                    {
                        mat[left.Line + 1, left.Column] = true;
                    }
                    
                    Position right = new Position(Position.Line, Position.Column + 1);
                    if (Board.ValidPosition(right) && ExistAdversary(right) && Board.Piece(right) == _chessMatch.VulnerableEnPassant)
                    {
                        mat[right.Line + 1, right.Column] = true;
                    }
                }
            }

            return mat;
        }
    }
}