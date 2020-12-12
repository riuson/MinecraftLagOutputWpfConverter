namespace Leopold95.Parser
{
    internal class BoldPartCreator : IPartCreator
    {
        public bool CanHandle(string part)
        {
            if (part.Length != 2) return false;

            var ch = part[1];

            return ch == 'l';
        }

        public IPart Create(string _)
        {
            return new BoldPart();
        }

        public bool Fallback => false;
    }
}