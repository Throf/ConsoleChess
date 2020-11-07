namespace BoardMain
{
    class Position
    {
        public int Column { get; set; }
        public int Line { get; set; }

        public Position(int column, int line) 
        {
            Column = column;
            Line = line;
        }

        public override string ToString()
        {
            return Column + "," + Line;
        }
    }
}
