namespace Leopold95.Parser
{
    internal class BoldPart : IBoldPart
    {
        public BoldPart(bool bold)
        {
            Bold = bold;
        }

        public bool Bold { get; }
    }
}