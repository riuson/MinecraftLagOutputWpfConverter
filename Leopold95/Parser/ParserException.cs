using System;

namespace Leopold95.Parser
{
    public class ParserException : Exception
    {
        public ParserException(string message) : base(message)
        {
        }
    }
}