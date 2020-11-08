namespace BoardMain
{
    public class Board
        {
            private Piece[,] _Pieces; 
            public int Columns { get; set; }
            public int Lines { get; set; }

            public Board(int lines, int columns) 
            {
                Columns = columns;
                Lines = lines;
                _Pieces = new Piece[lines, columns];
            }

            public Piece Piece(int lines, int columns) 
            {
                return _Pieces[lines, columns];
            }

            public Piece Piece(Position pos) 
            {
                return _Pieces[pos.Line, pos.Column];
            }

            public void PutPiece(Piece p, Position pos) 
            {
                if (ExistPiece(pos)) 
                {
                    throw new BoardException("There is already a piece in this position");
                }
                _Pieces[pos.Line, pos.Column] = p;
                p.Position = pos;
            }

            public Piece RemovePiece(Position pos)
            {
                if (Piece(pos) == null)
                {
                    return null;
                }
                
                Piece aux = Piece(pos);
                _Pieces[pos.Line, pos.Column] = null;
                return aux;
            }
            public bool ExistPiece(Position pos) 
            {
                ValidatePosition(pos);
                return Piece(pos) != null;
            }

            public bool ValidPosition(Position pos) 
            {
                if(pos.Line < 0 || pos.Line >= Lines || pos.Column < 0 || pos.Column >= Columns) 
                {
                    return false;
                }

                return true;
            }

            public void ValidatePosition(Position pos) 
            {
                if (!ValidPosition(pos))
                {
                    throw new BoardException("Invalid position!");
                }

            }
        }
}
