namespace BoardMain
{
    class Board
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
    }
}
