using BoardMain;

namespace Chess
{
    class Pawn : Piece
    {
        public Pawn(Colors color, Board board) : base(color, board)
        {
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
            }

            return mat;
        }
    }
}