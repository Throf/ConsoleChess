namespace BoardMain
{
    class Piece
    {
        public Position Position { get; set; }
        public Colors Color{get; protected set;}
        public Board Board { get; protected set; }
        public int AmountMovement { get; protected set; }

        public Piece(Position position, Colors color, Board board) 
        {
            Position = position;
            Color = color;
            Board = board;
            AmountMovement = 0;
        }
    }
}
