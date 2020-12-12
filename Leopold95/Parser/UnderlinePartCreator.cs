namespace Leopold95.Parser
{
    internal class UnderlinePartCreator : IPartCreator
    {
        public bool CanHandle(string part)
        {
            if (part.Length != 2) return false;

            var ch = part[1];

            return ch == 'n';
        }

        public IPart Create(string _)
        {
            return new UnderlinePart();
        }

        public bool Fallback => false;
    }
}