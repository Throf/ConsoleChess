using System;


namespace BoardMain
{
    class BoardException : ApplicationException
    {
        public BoardException(string msg) : base(msg) 
        {
        }
    }
}
