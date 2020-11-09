using BoardMain;

namespace Chess
{
    class Queen : Piece
    {
        public Queen(Colors color, Board board) : base(color, board) 
        { 
        }

        public override string ToString()
        {
            return "Qn";
        }

        private bool CanMove(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p == null || p.Color != Color;

        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];
            Position pos = new Position(0, 0);

            //N
            pos.DefineValues(Position.Line - 1, Position.Column);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }

                pos.Line = pos.Line - 1;
            }
            //S
            pos.DefineValues(Position.Line + 1, Position.Column);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }

                pos.Line = pos.Line + 1;
            }
            //W
            pos.DefineValues(Position.Line, Position.Column - 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }

                pos.Column = pos.Column - 1;
            }
            //E
            pos.DefineValues(Position.Line, Position.Column + 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }

                pos.Column = pos.Column + 1;
            }

            //Ne
            pos.DefineValues(Position.Line - 1, Position.Column - 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }

                pos.Line = pos.Line - 1;
                pos.Column = pos.Column - 1;
            }
            //Se
            pos.DefineValues(Position.Line + 1, Position.Column + 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }

                pos.Line = pos.Line + 1;
                pos.Column = pos.Column + 1;
            }
            //Nw
            pos.DefineValues(Position.Line - 1, Position.Column + 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }

                pos.Line = pos.Line - 1;
                pos.Column = pos.Column + 1;
            }
            //Ne
            pos.DefineValues(Position.Line + 1, Position.Column - 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }

                pos.Line = pos.Line + 1;
                pos.Column = pos.Column - 1;
            }

            return mat;
        }
    }
}
