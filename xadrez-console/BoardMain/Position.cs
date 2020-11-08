﻿namespace BoardMain
{
    public class Position
    {
        public int Column { get; set; }
        public int Line { get; set; }

        public Position(int line, int column) 
        {
            Line = line;
            Column = column;
        }

        public void DefineValues(int line, int column)
        {
            Line = line;
            Column = column;
        }
        public override string ToString()
        {
            return Column + "," + Line;
        }
    }
}
